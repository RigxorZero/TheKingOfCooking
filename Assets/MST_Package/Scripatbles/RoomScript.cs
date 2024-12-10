using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRoom", menuName = "Room")]
public class RoomScript : ScriptableObject {
    
    public string roomName;
    public List<Connections> connectedRooms = new List<Connections>();
    public List<EnemyScript> enemies = new List<EnemyScript>();
}

[Serializable]
public class Connections
{
    public RoomScript roomConnected;
    public float distance;
}
