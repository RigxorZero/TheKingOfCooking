using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class ExporterJSON : EditorWindow {
    private List<RoomScript> allRooms = new List<RoomScript>();
    private List<RoomScript> importedRooms = new List<RoomScript>();

    private const string RoomPathKey = "RoomPathKey";
    private string RoomPath = "Assets/Rooms";
    
    private const string EnemyPathKey = "EnemyPathKey";
    private string EnemyPath = "Assets/Enemies";
    
    private const string RoomPathJsonKey = "RoomPathJsonKey";
    private string RoomPathJson = "Assets/JSONRooms";

    private const string ConnectPathKey = "ConnectPathKey";
    private string ConnectPath = "Assets/JSONConnect";
    
    private string jsonPath;

    private Vector2 scrollPositionGlobal;
    

    [MenuItem("Tools/JSON")]
    public static void ShowWindow()
    {
        GetWindow<ExporterJSON>("Exportar");
    }

    private void OnEnable()
    {
        RoomPath = EditorPrefs.GetString(RoomPathKey, RoomPath);
        EnemyPath = EditorPrefs.GetString(EnemyPathKey, EnemyPath);
        RoomPathJson = EditorPrefs.GetString(RoomPathJsonKey, RoomPathJson);
        ConnectPath = EditorPrefs.GetString(ConnectPathKey, ConnectPath);
    }

    private void OnDisable()
    {
        EditorPrefs.SetString(RoomPathJsonKey, RoomPathJson);
        EditorPrefs.SetString(ConnectPathKey, ConnectPath);
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        scrollPositionGlobal = EditorGUILayout.BeginScrollView(scrollPositionGlobal);
        
        RoomPathJson = EditorGUILayout.TextField("Carpeta de Habitaciones", RoomPathJson);
        EditorGUILayout.LabelField("Exportar Todas las Habitaciones como JSON", EditorStyles.boldLabel);
        if (GUILayout.Button("Exportar Json"))
        {
            ExportAllRoomsToJson();
        }
        EditorGUILayout.LabelField("Exportar Habitaciones como JSON (Individuales)", EditorStyles.boldLabel);
        if (GUILayout.Button("Exportar JSONs"))
        {
            ExportRoomsToJson();
        }
        GUILayout.Space(30);
        ConnectPath = EditorGUILayout.TextField("Carpeta de Grafo", ConnectPath);
        EditorGUILayout.LabelField("Exportar Conexiones como TXT", EditorStyles.boldLabel);
        if (GUILayout.Button("Exportar Conexiones"))
        {
            ExportConnectionGraphToJson();
        }
        
        GUILayout.Space(30);
        
        Rect rect = EditorGUILayout.GetControlRect(false, 1 );
        EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );
        GUILayout.Space(30);

        EditorGUILayout.LabelField("Importar Habitaciones desde JSON", EditorStyles.boldLabel);
        if (GUILayout.Button("Seleccionar archivo JSON"))
        {
            jsonPath = EditorUtility.OpenFilePanel("Seleccionar archivo JSON", "", "json");
            if (!string.IsNullOrEmpty(jsonPath))
            {
                ImportAllRoomsFromJson(jsonPath);
            }
        }

        EditorGUILayout.EndScrollView();
    }
    
    private void ExportRoomsToJson()
    {
        string[] roomguids = AssetDatabase.FindAssets("t:RoomScript", new[] { $"{RoomPath}" });
        foreach (string guid in roomguids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            RoomScript room = AssetDatabase.LoadAssetAtPath<RoomScript>(assetPath);
            if (room != null)
            {
                RoomWrapper roomWrapper = new RoomWrapper(room);
                string json = JsonUtility.ToJson(roomWrapper, true);
                File.WriteAllText(RoomPathJson + $"/{room.roomName}.json", json);
                AssetDatabase.Refresh();
            }
        }
    }

    private void ExportAllRoomsToJson()
    {
        allRooms.Clear();
        string[] roomguids = AssetDatabase.FindAssets("t:RoomScript", new[] { $"{RoomPath}" });
        foreach (string guid in roomguids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            RoomScript room = AssetDatabase.LoadAssetAtPath<RoomScript>(assetPath);
            if (room != null)
            {
                allRooms.Add(room);
            }
        }
        RoomListWrapper roomListWrapper = new RoomListWrapper(allRooms);
        string json = JsonUtility.ToJson(roomListWrapper, true);
        File.WriteAllText(RoomPathJson + "/AllRooms.json", json);
        AssetDatabase.Refresh();
    }
    
    private void ExportConnectionGraphToJson()
    {
        allRooms.Clear();
        string[] guids = AssetDatabase.FindAssets("t:RoomScript", new[] { $"{RoomPath}" });
        foreach (string guid in guids)
        {
            string roomPath = AssetDatabase.GUIDToAssetPath(guid);
            RoomScript room = AssetDatabase.LoadAssetAtPath<RoomScript>(roomPath);
            if (room != null)
            {
                allRooms.Add(room);
            }
        }

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

        if (!Directory.Exists(ConnectPath))
        {
            Directory.CreateDirectory(ConnectPath);
        }
        File.WriteAllLines(ConnectPath + "/Graph.txt", lines);
        AssetDatabase.Refresh();
    }
    
    private void ImportAllRoomsFromJson(string jsonPath)
    {
        if (!File.Exists(jsonPath))
        {
            Debug.LogWarning("No se encontró el archivo JSON para importar.");
            return;
        }

        string json = File.ReadAllText(jsonPath);
        RoomListWrapper roomListWrapper = JsonUtility.FromJson<RoomListWrapper>(json);
    
        importedRooms.Clear();

        foreach (RoomData roomWrap in roomListWrapper.rooms)
        {
            RoomScript room = CreateInstance<RoomScript>();
            room.roomName = roomWrap.roomName;
            
            for (var i = 0; i < roomWrap.connectedRooms.Count; i++)
            {
                RoomScript otherRoom = CreateInstance<RoomScript>();
                Connections roomConnectedRoom = new Connections();
                
                ConnectionData connection = roomWrap.connectedRooms[i];
                otherRoom.roomName = connection.roomConnected;

                roomConnectedRoom.roomConnected = otherRoom;
                roomConnectedRoom.distance = connection.distance;
                
                room.connectedRooms.Add(roomConnectedRoom);
                
            }

            foreach (EnemyData enemy in roomWrap.enemies)
            {
                EnemyScript enemyInfo = CreateInstance<EnemyScript>();
                enemyInfo.enemyName = enemy.enemyName;
                enemyInfo.level = enemy.level;
                enemyInfo.health = enemy.health;
                enemyInfo.damage = enemy.damage;
                
                room.enemies.Add(enemyInfo);
            }

            string[] guids = AssetDatabase.FindAssets(room.roomName, new[] { RoomPath });
            if (guids.Length == 0)
            {
                AssetDatabase.CreateAsset(room, $"{RoomPath}/{room.roomName}.asset");
                importedRooms.Add(room);
            }
            AssetDatabase.SaveAssets();
            
        }
        
        foreach (RoomScript room in importedRooms)
        {
            List<Connections> connectionsList = room.connectedRooms;
            for (var i = 0; i < connectionsList.Count; i++)
            {
                Connections connection = connectionsList[i];
                foreach (RoomScript anotherRoom in importedRooms)
                {
                    if (anotherRoom.roomName == connection.roomConnected.roomName)
                    {
                        room.connectedRooms[i].roomConnected = anotherRoom;
                    }
                }
            }
        }
        foreach (RoomScript room in importedRooms)
        {
            foreach (Connections otherRoom in room.connectedRooms)
            {
                CreateConnectionRoom(room, otherRoom.roomConnected, otherRoom.distance);
            }
        }
        
        
        List<EnemyScript> allEnemies = new List<EnemyScript>();
        string[] enemyGuids = AssetDatabase.FindAssets("t:EnemyScript", new[] { EnemyPath });
        foreach (var guid in enemyGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            allEnemies.Add(AssetDatabase.LoadAssetAtPath<EnemyScript>(assetPath));
        }
        
        foreach (RoomScript room in importedRooms)
        {
            List<EnemyScript> enemyList = room.enemies;
            for (var i = 0; i < enemyList.Count; i++)
            {
                foreach (EnemyScript enemy in allEnemies)
                {
                    if (room.enemies[i].enemyName == enemy.enemyName)
                    {
                        room.enemies[i] = enemy;
                    }
                }
            }
        }
        

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Importación de habitaciones completada.");
    }
    
    private void CreateConnectionRoom(RoomScript roomA, RoomScript roomB, float distance) {
        if (!roomA.connectedRooms.Exists(c => c.roomConnected == roomB)) 
        {
            roomA.connectedRooms.Add(new Connections { roomConnected = roomB, distance = distance });
            roomB.connectedRooms.Add(new Connections { roomConnected = roomA, distance = distance });
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
            } 
        }
    }
    
    
    [System.Serializable]
    public class RoomWrapper
    {
        public RoomData room;
        public RoomWrapper(RoomScript otherRoom)
        {
                room = new RoomData(otherRoom);
        }
    }
    
    [System.Serializable]
    public class RoomListWrapper
    {
        public List<RoomData> rooms;
        public RoomListWrapper(List<RoomScript> allRooms)
        {
            rooms = new List<RoomData>();
            foreach (RoomScript room in allRooms)
            {
                rooms.Add(new RoomData(room));
            }
        }
    }
    
    [System.Serializable]
    public class RoomData
    {
        public string roomName;
        public List<ConnectionData> connectedRooms;
        public List<EnemyData> enemies;

        public RoomData(RoomScript room)
        {
            roomName = room.roomName;
            connectedRooms = new List<ConnectionData>();
            enemies = new List<EnemyData>();

            foreach (var connection in room.connectedRooms)
            {
                if (connection.roomConnected != null)
                {
                    connectedRooms.Add(new ConnectionData(connection.roomConnected.roomName, connection.distance));
                }
            }

            foreach (EnemyScript enemy in room.enemies)
            {
                enemies.Add(new EnemyData(enemy));
            }
        }
    }
    
    [System.Serializable]
    public class EnemyData
    {
        public string enemyName;
        public int level;
        public int health;
        public int damage;

        public EnemyData(EnemyScript enemy)
        {
            enemyName = enemy.enemyName;
            level = enemy.level;
            health = enemy.health;
            damage = enemy.damage;
        }
    }
    
    [System.Serializable]
    public class ConnectionData
    {
        public string roomConnected;
        public float distance;

        public ConnectionData(string roomConnectedName, float distance)
        {
            roomConnected = roomConnectedName;
            this.distance = distance;
        }
    }
}
