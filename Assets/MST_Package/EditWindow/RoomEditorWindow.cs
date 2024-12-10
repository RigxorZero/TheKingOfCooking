using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RoomEditorWindow : EditorWindow {
    private List<RoomScript> allRooms = new List<RoomScript>();
    private List<EnemyScript> allEnemies = new List<EnemyScript>();

    private bool showCreationOptions = true;

    private bool showRoomOptions;
    private bool showNewRoomOptions;
    private bool deleteRoomOptions;
    
    private bool showEnemyOptions;
    private bool showNewEnemyOptions;
    private bool showEditEnemyOptions;
    private bool deleteEnemyOptions;

    private const string RoomPathKey = "RoomPathKey";
    private string RoomPath = "Assets/Rooms";
    private string RoomName = "";
    
    private const string EnemyPathKey = "EnemyPathKey";
    private string EnemyPath = "Assets/Enemies";
    private string EnemyName = "";
    private int newEnemyLevel;
    private int newEnemyHealth;
    private int newEnemyDamage;
    
    private EnemyScript selectedEnemyEdit;
    private string EditEnemyName = "";
    private int editEnemyLevel;
    private int editEnemyHealth;
    private int editEnemyDamage;

    private RoomScript selectedRoom;
    private EnemyScript selectedEnemy;

    private RoomScript selectedRoomA;
    private RoomScript selectedRoomB;
    private RoomScript deleteSelectedRoomA;
    private RoomScript deleteSelectedRoomB;
    
    private Dictionary<RoomScript, EnemyScript> roomEnemy = new Dictionary<RoomScript, EnemyScript>();
    private Dictionary<RoomScript, RoomScript> roomConnect = new Dictionary<RoomScript, RoomScript>();
    private Dictionary<RoomScript, float> roomDistance = new Dictionary<RoomScript, float>();
    private Dictionary<RoomScript, Dictionary<RoomScript, float>> roomConnectionDistances = new Dictionary<RoomScript, Dictionary<RoomScript, float>>();
    private Dictionary<RoomScript, bool> roomFoldouts = new Dictionary<RoomScript, bool>();
    private Dictionary<RoomScript, bool> roomFoldoutsConnections = new Dictionary<RoomScript, bool>();
    private Dictionary<RoomScript, bool> roomFoldoutsEnemies = new Dictionary<RoomScript, bool>();
    
    Vector2 scrollPositionGlobal, scrollPositionInfo, scrollPositionConnections;
    

    private MST_Script mst = new MST_Script();

    [MenuItem("Tools/Room Editor")]
    public static void ShowWindow() {
        GetWindow<RoomEditorWindow>("Room Editor");
    }

    private void OnEnable()
    {
        RoomPath = EditorPrefs.GetString(RoomPathKey, RoomPath);
        EnemyPath = EditorPrefs.GetString(EnemyPathKey, EnemyPath);
        UpdateRooms();
    }

    private void OnDisable()
    {
        EditorPrefs.SetString(RoomPathKey, RoomPath);
        EditorPrefs.SetString(EnemyPathKey, EnemyPath);
    }

    private void OnGUI() {
        
        scrollPositionGlobal = EditorGUILayout.BeginScrollView(scrollPositionGlobal);
        
        EditorGUILayout.HelpBox("Para Agregar o Eliminar Assets debes tener una carpeta seleccionada", MessageType.Info);
        
        GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
        foldoutStyle.fontStyle = FontStyle.Bold;
        
        GUILayout.Space(10);
        showCreationOptions = EditorGUILayout.Foldout(showCreationOptions, "Agregar/Eliminar Objetos", foldoutStyle);
        if (showCreationOptions)
        {
            EditorGUILayout.BeginVertical();
            #region Habitaciones

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            
            showRoomOptions = EditorGUILayout.Foldout(showRoomOptions, "Habitaciones", foldoutStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            if (showRoomOptions)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                RoomPath = EditorGUILayout.TextField("Carpeta de habitaciones", RoomPath);
                EditorGUILayout.EndHorizontal();
                
                // Agregar Habitación
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                showNewRoomOptions = EditorGUILayout.Foldout(showNewRoomOptions, "Añadir Nueva Habitación", foldoutStyle);
                EditorGUILayout.EndHorizontal();
                if (showNewRoomOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    RoomName = EditorGUILayout.TextField("Nombre de Habitación", RoomName);
                    if (GUILayout.Button("Añadir Habitación")) {
                        if (!string.IsNullOrEmpty(RoomName)) {
                            RoomScript newRoom = CreateRoom(RoomName);
                            RoomName = "";
                            UpdateRooms();
                            
                            EditorPrefs.SetString(RoomPathKey, RoomPath);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                
                // Eliminar Habitación
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                deleteRoomOptions = EditorGUILayout.Foldout(deleteRoomOptions, "Eliminar Habitación", foldoutStyle);
                EditorGUILayout.EndHorizontal();
                if (deleteRoomOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    selectedRoom = (RoomScript)EditorGUILayout.ObjectField("Habitación", selectedRoom, typeof(RoomScript), false);
                    if (GUILayout.Button("Eliminar Habitación")) {
                        if (selectedRoom!=null) {
                            RemoveRoom(selectedRoom);
                            selectedRoom = null;
                            UpdateRooms();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            #endregion


            #region Enemigos
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(20);
            
            showEnemyOptions = EditorGUILayout.Foldout(showEnemyOptions,"Enemigos", foldoutStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            if (showEnemyOptions)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EnemyPath = EditorGUILayout.TextField("Carpeta de enemigos", EnemyPath);
                EditorGUILayout.EndHorizontal();
                
                // Agregar Enemigo
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                showNewEnemyOptions = EditorGUILayout.Foldout(showNewEnemyOptions,"Añadir Nuevo Enemigo", foldoutStyle);
                EditorGUILayout.EndHorizontal();
                if (showNewEnemyOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    EditorGUILayout.BeginVertical();
                    EnemyName = EditorGUILayout.TextField("Nombre de Enemigo", EnemyName);
                    newEnemyLevel = EditorGUILayout.IntField("Nivel de Enemigo", newEnemyLevel);
                    newEnemyHealth = EditorGUILayout.IntField("Vida de Enemigo", newEnemyHealth);
                    newEnemyDamage = EditorGUILayout.IntField("Daño de Enemigo", newEnemyDamage);
                    EditorGUILayout.EndVertical();
                    if (GUILayout.Button("Añadir Enemigo")) {
                        if (!string.IsNullOrEmpty(EnemyName)) {
                            EnemyScript newEnemy = CreateEnemy(EnemyName, newEnemyLevel, newEnemyHealth, newEnemyDamage);
                            EnemyName = "";
                            newEnemyLevel = 0;
                            newEnemyHealth = 0;
                            newEnemyDamage = 0;
                            
                            UpdateRooms();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                
                // Editar Enemigo
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                showEditEnemyOptions = EditorGUILayout.Foldout(showEditEnemyOptions,"Editar Enemigo", foldoutStyle);
                EditorGUILayout.EndHorizontal();
                if (showEditEnemyOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    EditorGUILayout.BeginVertical();

                    EnemyScript previousSelectedEnemy = selectedEnemyEdit;
                    selectedEnemyEdit = (EnemyScript)EditorGUILayout.ObjectField("Enemigo", selectedEnemyEdit, typeof(EnemyScript), false);
                    
                    if (selectedEnemyEdit != previousSelectedEnemy)
                    {
                        if (selectedEnemyEdit != null)
                        {
                            EditEnemyName = selectedEnemyEdit.enemyName;
                            editEnemyLevel = selectedEnemyEdit.level;
                            editEnemyHealth = selectedEnemyEdit.health;
                            editEnemyDamage = selectedEnemyEdit.damage;
                        }
                        else
                        {
                            EditEnemyName = "";
                            editEnemyLevel = 0;
                            editEnemyHealth = 0;
                            editEnemyDamage = 0;
                        }
                    }

                    EditEnemyName = EditorGUILayout.TextField("Nombre de Enemigo", EditEnemyName);
                    editEnemyLevel = EditorGUILayout.IntField("Nivel de Enemigo", editEnemyLevel);
                    editEnemyHealth = EditorGUILayout.IntField("Vida de Enemigo", editEnemyHealth);
                    editEnemyDamage = EditorGUILayout.IntField("Daño de Enemigo", editEnemyDamage);
                    
                    EditorGUILayout.EndVertical();
                    
                    if (GUILayout.Button("Actualizar Enemigo"))
                    {
                        if (!string.IsNullOrEmpty(EditEnemyName) && selectedEnemyEdit != null)
                        {
                            EditEnemy(selectedEnemyEdit, EditEnemyName, editEnemyLevel, editEnemyHealth, editEnemyDamage);
                            selectedEnemyEdit = null;
                            EditEnemyName = "";
                            editEnemyLevel = 0;
                            editEnemyHealth = 0;
                            editEnemyDamage = 0;
                            UpdateRooms();
                        }
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
                
                // Eliminar Enemigo
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                deleteEnemyOptions = EditorGUILayout.Foldout(deleteEnemyOptions, "Eliminar Enemigo", foldoutStyle);
                EditorGUILayout.EndHorizontal();
                if (deleteEnemyOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    selectedEnemy = (EnemyScript)EditorGUILayout.ObjectField("Enemigo", selectedEnemy, typeof(EnemyScript), false);
                    if (GUILayout.Button("Eliminar Enemigo")) {
                        if (selectedEnemy != null) {
                            RemoveEnemy(selectedEnemy);
                            selectedEnemy = null;
                            UpdateRooms();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            #endregion

            EditorGUILayout.EndVertical();
        }
        
        GUILayout.Space(10);
        if (GUILayout.Button("Actualizar"))
        {
            UpdateRooms();
        }
        GUILayout.Space(10);
        
        EditorGUILayout.HelpBox("Para Agregar o conexiones, selecciona la habitacion y define la distancia. Dentro de las conexiones podrás editar o eliminar la conexion.", MessageType.Info);
        
        #region Informacion

        EditorGUILayout.LabelField("Informacion de las Habitaciones", EditorStyles.boldLabel);
        
        scrollPositionInfo = EditorGUILayout.BeginScrollView(scrollPositionInfo, "box",GUILayout.Height(350));

        if (allRooms.Count == 0)
        {
            GUILayout.Space(20);
            EditorGUILayout.LabelField("No hay Habitaciones, crea una nueva", EditorStyles.boldLabel);
        }
        
        foreach (RoomScript room in allRooms)
        {
            EditorGUILayout.BeginVertical("box");
            roomFoldouts[room] = EditorGUILayout.Foldout(roomFoldouts[room], room.roomName, foldoutStyle);

            if (roomFoldouts[room])
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                roomFoldoutsConnections[room] = EditorGUILayout.Foldout(roomFoldoutsConnections[room], $"Conexiones ({room.connectedRooms.Count})", foldoutStyle);
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.EndHorizontal(); 
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace(); 
                EditorGUILayout.LabelField("Habitación", GUILayout.Width(65)); 
                roomConnect[room] = (RoomScript)EditorGUILayout.ObjectField(roomConnect[room], typeof(RoomScript), false, GUILayout.Width(80));
                EditorGUILayout.LabelField("Distancia:", GUILayout.Width(55)); 
                roomDistance[room] = EditorGUILayout.FloatField(roomDistance[room], GUILayout.Width(30)); 
                if (GUILayout.Button("+", GUILayout.Width(40)) )
                {
                    if (roomConnect[room] != null && roomConnect[room] != room)
                    {
                        CreateConnection(room, roomConnect[room], roomDistance[room]);
                        roomConnect[room] = null;
                        roomDistance[room] = 0f;
                    }
                }
                EditorGUILayout.EndHorizontal(); 
                
                EditorGUILayout.EndHorizontal(); 
                
                if (roomFoldoutsConnections[room])
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(40);
                    EditorGUILayout.BeginVertical();
                    for (int i = 0; i < room.connectedRooms.Count; i++)
                    {
                        Connections connection = room.connectedRooms[i];
                        RoomScript connectedRoom = connection.roomConnected;

                        if (!roomConnectionDistances.ContainsKey(room))
                        {
                            roomConnectionDistances[room] = new Dictionary<RoomScript, float>();
                        }

                        if (!roomConnectionDistances[room].ContainsKey(connectedRoom))
                        {
                            roomConnectionDistances[room][connectedRoom] = connection.distance;
                        }
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(
                            $"- {connection.roomConnected.roomName} (Distancia: {connection.distance})",
                            GUILayout.Width(200));
                        GUILayout.Width(80);
                        EditorGUILayout.LabelField("Nueva Distancia:", GUILayout.Width(100));
                        roomConnectionDistances[room][connectedRoom] = EditorGUILayout.FloatField(roomConnectionDistances[room][connectedRoom], GUILayout.Width(30)); 
                        if (GUILayout.Button("Actualizar", GUILayout.Width(80)) )
                        {
                            CreateConnection(room, connectedRoom, roomConnectionDistances[room][connectedRoom]);
                        }
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Quitar Conexión", GUILayout.Width(100)))
                        {
                            RemoveConnection(room, connection.roomConnected);
                            i--;
                        }

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();   
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                roomFoldoutsEnemies[room] = EditorGUILayout.Foldout(roomFoldoutsEnemies[room], $"Enemigos ({room.enemies.Count})", foldoutStyle);
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.EndHorizontal(); 
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace(); 
                EditorGUILayout.LabelField("Enemigo", GUILayout.Width(50));
                roomEnemy[room] = (EnemyScript)EditorGUILayout.ObjectField(roomEnemy[room], typeof(EnemyScript), false, GUILayout.Width(185));
                if (GUILayout.Button("+", GUILayout.Width(40)) )
                {
                    if (roomEnemy[room] != null)
                    {
                        room.enemies.Add(roomEnemy[room]);
                        roomEnemy[room] = null;
                    }
                }
                EditorGUILayout.EndHorizontal(); 
                
                EditorGUILayout.EndHorizontal(); 
                
                if (roomFoldoutsEnemies[room])
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(40);
                    EditorGUILayout.BeginVertical();
                    foreach (var enemyGroup in room.enemies.GroupBy(e=>e.enemyName))
                    {
                        EnemyScript enemy = enemyGroup.First();  
                        int count = enemyGroup.Count();  

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"- {enemy.enemyName} (Cantidad: {count})", GUILayout.Width(200));

                        if (GUILayout.Button("-", GUILayout.Width(50)))
                        {
                            if (count > 0) 
                            {
                                room.enemies.Remove(enemy);
                            }
                        }

                        if (GUILayout.Button("+", GUILayout.Width(50)))
                        {
                            room.enemies.Add(enemy);  
                        }

                        EditorGUILayout.EndHorizontal();  
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();   
                }
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
        #endregion
        
        GUILayout.Space(10);
        if (GUILayout.Button("Aplicar MST"))
        {
            mstUpdate(mst.orderEdge(allRooms));
        }
        
        EditorGUILayout.LabelField("Informacion de las Conexiones", EditorStyles.boldLabel);
        
        scrollPositionConnections = EditorGUILayout.BeginScrollView(scrollPositionConnections,"box",GUILayout.Height(200));
        bool Connections = false;

        foreach (RoomScript room in allRooms)
        {
            if (room.connectedRooms.Count != 0)
            {
                Connections = true;
            }
        }
        if (!Connections)
        {
            GUILayout.Space(20);
            EditorGUILayout.LabelField("No hay Conexiones, crea una nueva", EditorStyles.boldLabel);
        }
        else
        {
            foreach (string connection in CreateGraph())
            {
                EditorGUILayout.LabelField(connection, EditorStyles.boldLabel);    
            }
            EditorGUILayout.LabelField($"Valor Total = {mst.GraphTotalValue(allRooms)}", EditorStyles.boldLabel);
        }
        
        EditorGUILayout.EndScrollView();
        
        EditorGUILayout.EndScrollView();
    }
    
    private RoomScript CreateRoom(string name) {
        if (AssetDatabase.IsValidFolder(RoomPath))
        {
            string[] guids = AssetDatabase.FindAssets(name, new[] { RoomPath });
            if (guids.Length > 0)
            {
                Debug.Log(guids);
                Debug.LogWarning("Ya existe un asset con el nombre en la carpeta.");
                return null;
            }
            
            RoomScript room = CreateInstance<RoomScript>();
            room.roomName = name;
            
            allRooms.Add(room);
            
            AssetDatabase.CreateAsset(room, $"{RoomPath}/{name}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = room;
            
            Debug.Log($"Habitacion {name} creada");

            return room;
        }
        Debug.LogWarning("No existe esa Carpeta");
        return null;
    }
    
    private void RemoveRoom(RoomScript roomToRemove) 
    {
        if (roomToRemove == null)
        {
            return;
        }
        
        foreach (RoomScript room in allRooms) 
        {
            if (room != roomToRemove) 
            {
                if (room.connectedRooms.Exists(c => c.roomConnected == roomToRemove))
                {
                    Connections existConnection = room.connectedRooms.Find(c => c.roomConnected == roomToRemove);
                    room.connectedRooms.Remove(existConnection);
                }
            }
        }
        allRooms.Remove(roomToRemove);
        
        string assetPath = AssetDatabase.GetAssetPath(roomToRemove);

        if (!string.IsNullOrEmpty(assetPath))
        {
            Debug.Log($"Habitacion {roomToRemove.roomName} eliminada");

            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.SaveAssets();
        }
    }

    private EnemyScript CreateEnemy(string name, int lvl, int hlt, int dmg) {

        if (AssetDatabase.IsValidFolder(EnemyPath))
        {
            string[] guids = AssetDatabase.FindAssets(name, new[] { EnemyPath });
            if (guids.Length > 0)
            {
                Debug.LogWarning("Ya existe un asset con el nombre en la carpeta.");
                return null;
            }
            
            EnemyScript enemy = CreateInstance<EnemyScript>();
            enemy.enemyName = name;
            enemy.level = lvl;
            enemy.health = hlt;
            enemy.damage = dmg;
            
            allEnemies.Add(enemy);
            
            AssetDatabase.CreateAsset(enemy, $"{EnemyPath}/{name}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = enemy;
            
            Debug.Log($"Enemigo {name} creado");
            
            return enemy;
        }
        Debug.LogWarning("No existe esa Carpeta");
        return null;
    }
    
    private void EditEnemy(EnemyScript edit, string name, int lvl, int hlt, int dmg) {
        
        string assetPath = AssetDatabase.GetAssetPath(edit);
        AssetDatabase.RenameAsset(assetPath, EditEnemyName);

        edit.enemyName = name;
        edit.level = lvl;
        edit.health = hlt;
        edit.damage = dmg;

        AssetDatabase.SaveAssets();
        
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = edit;
        
        Debug.Log($"Enemigo {name} editado");
        
    }

    private void RemoveEnemy(EnemyScript enemyToRemove) 
    {
        
        if (enemyToRemove == null)
        {
            return;
        }

        foreach (RoomScript room in allRooms)
        {
            while (room.enemies.Contains(enemyToRemove))
            {
                room.enemies.Remove(enemyToRemove);
            }
        }

        if (allEnemies.Contains(enemyToRemove))
        {
            allEnemies.Remove(enemyToRemove);
        }
        
        string assetPath = AssetDatabase.GetAssetPath(enemyToRemove);

        if (!string.IsNullOrEmpty(assetPath))
        {
            Debug.Log($"Enemigo {enemyToRemove.enemyName} eliminado");
            
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.SaveAssets();
        }
    }

    private void CreateConnection(RoomScript roomA, RoomScript roomB, float distance) {
        if (!roomA.connectedRooms.Exists(c => c.roomConnected == roomB)) 
        {
            roomA.connectedRooms.Add(new Connections { roomConnected = roomB, distance = distance });
            roomB.connectedRooms.Add(new Connections { roomConnected = roomA, distance = distance });
            
            Debug.Log($"Conexión {roomA.roomName} -> {roomB.roomName} = {distance} creada");
        } 
        else 
        {
            Connections existConnectionA = roomA.connectedRooms.Find(c => c.roomConnected == roomB);
            Connections existConnectionB = roomB.connectedRooms.Find(c => c.roomConnected == roomA);

            if (existConnectionA.distance != distance) 
            {
                int indexA = roomA.connectedRooms.IndexOf(existConnectionA);
                int indexB = roomB.connectedRooms.IndexOf(existConnectionB);
                
                roomA.connectedRooms.Remove(existConnectionA);
                roomB.connectedRooms.Remove(existConnectionB);
                
                roomA.connectedRooms.Insert(indexA, new Connections { roomConnected = roomB, distance = distance });
                roomB.connectedRooms.Insert(indexB, new Connections { roomConnected = roomA, distance = distance });
                
                Debug.Log($"Conexión {roomA.roomName} -> {roomB.roomName} = {distance} modificada");
            } 
        }
    }
    
    private void RemoveConnection(RoomScript roomA, RoomScript roomB) 
    {
        if (roomA.connectedRooms.Exists(c => c.roomConnected == roomB)) 
        {
            Connections existConnectionA = roomA.connectedRooms.Find(c => c.roomConnected == roomB);
            Connections existConnectionB = roomB.connectedRooms.Find(c => c.roomConnected == roomA);
            
            roomA.connectedRooms.Remove(existConnectionA);
            roomB.connectedRooms.Remove(existConnectionB);
            
            Debug.Log($"Conexión {roomA.roomName} -> {roomB.roomName} eliminada");
        } 
    }
    
    private List<string> CreateGraph()
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < allRooms.Count; i++)
        {
            RoomScript room = allRooms[i];
            foreach (var connection in room.connectedRooms)
            {
                for (int k = i; k < allRooms.Count; k++)
                {
                    if (connection.roomConnected.roomName == allRooms[k].roomName)
                    {
                        string line = $"({room.roomName} -> {connection.roomConnected.roomName} = {connection.distance})";
                        lines.Add(line);
                    }
                }
            }
        }
        return lines;
    }

    private void mstUpdate(List<NodeConnect> nodes)
    {
        foreach (RoomScript room in allRooms)
        {
            for (int i = 0; i < room.connectedRooms.Count; i++)
            {
                bool connectionExist = false;
                RoomScript connection = room.connectedRooms[i].roomConnected;
                foreach (var node in nodes)
                {
                    if ((node.NodeA == room && node.NodeB == connection) || (node.NodeB == room && node.NodeA == connection))
                    {
                        connectionExist = true;
                    }
                }
                if (!connectionExist)
                {
                    RemoveConnection(room, connection);
                }
            }
        }
        Debug.Log("Estructura Grafo Modificada");
    }
    
    private void UpdateRooms()
    {
        allRooms.Clear();
        List<RoomScript> rooms = new List<RoomScript>();
        string[] roomguids = AssetDatabase.FindAssets("t:RoomScript", new[] { $"{RoomPath}" });
        foreach (string guid in roomguids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            RoomScript room = AssetDatabase.LoadAssetAtPath<RoomScript>(assetPath);

            if (room != null)
            {
                rooms.Add(room);
                roomEnemy[room] = null;
                roomConnect[room] = null;
                roomFoldouts[room] = false;
                roomFoldoutsConnections[room] = false;
                roomFoldoutsEnemies[room] = false; 
                roomDistance[room] = 0;
            }
        }
        foreach (RoomScript room in rooms)
        {
            if (!allRooms.Contains(room))
            {
                allRooms.Add(room);
            }
        }

        foreach (RoomScript room in allRooms)
        {
            for (int i = 0; i < room.enemies.Count; i++)
            {
                string[] guids = AssetDatabase.FindAssets(room.enemies[i].enemyName, new[] { EnemyPath });
                if (guids.Length == 0)
                {
                    room.enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        allEnemies.Clear();
        List<EnemyScript> enemies = new List<EnemyScript>();
        string[] enemyguids = AssetDatabase.FindAssets("t:EnemyScript", new[] { $"{EnemyPath}" });
        foreach (string guid in enemyguids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            EnemyScript enemy = AssetDatabase.LoadAssetAtPath<EnemyScript>(assetPath);

            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
        foreach (EnemyScript enemy in enemies)
        {
            if (!allEnemies.Contains(enemy))
            {
                allEnemies.Add(enemy);
            }
        }
    }
}