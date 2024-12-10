using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MST_Script
{
    private List<NodeConnect> Nodes = new List<NodeConnect>();
    private List<NodeConnect> mstEdges = new List<NodeConnect>();
    private Dictionary<RoomScript, RoomScript> parent = new Dictionary<RoomScript, RoomScript>();

    public List<NodeConnect> orderEdge(List<RoomScript> roomList)
    {
        Nodes.Clear();
        
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomScript room = roomList[i];
            foreach (var connection in room.connectedRooms)
            {
                for (int k = i; k < roomList.Count; k++)
                {
                    if (connection.roomConnected.roomName == roomList[k].roomName)
                    {
                        Nodes.Add(new 
                            (room ,connection.roomConnected, connection.distance));
                    }
                }
            }
        }
        
        Nodes.Sort((node1, node2) => node1.distance.CompareTo(node2.distance));
        
        MakeSet(roomList);

        foreach (var edge in Nodes)
        {
            RoomScript rootA = Find(edge.NodeA);
            RoomScript rootB = Find(edge.NodeB);

            if (rootA != rootB)
            {
                mstEdges.Add(edge);
                Union(rootA, rootB);
            }
        }

        return mstEdges;

    }
    
    public float GraphTotalValue(List<RoomScript> roomList)
    {
        Nodes.Clear();

        float total = 0;
        
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomScript room = roomList[i];
            foreach (var connection in room.connectedRooms)
            {
                for (int k = i; k < roomList.Count; k++)
                {
                    if (connection.roomConnected.roomName == roomList[k].roomName)
                    {
                        total += connection.distance;
                    }
                }
            }
        }
        return total;
    }
    
    private void MakeSet(List<RoomScript> rooms)
    {
        foreach (var room in rooms)
        {
            parent[room] = room;
        }
    }

    private RoomScript Find(RoomScript room)
    {
        if (parent[room] != room)
        {
            parent[room] = Find(parent[room]);
        }
        return parent[room];
    }

    private void Union(RoomScript roomA, RoomScript roomB)
    {
        RoomScript rootA = Find(roomA);
        RoomScript rootB = Find(roomB);
        if (rootA != rootB)
        {
            parent[rootA] = rootB;
        }
    }
}

public class NodeConnect : IComparable<NodeConnect>
{
    public RoomScript NodeA;
    public RoomScript NodeB;
    public float distance;

    public NodeConnect(RoomScript roomA, RoomScript roomB, float dis)
    {
        NodeA = roomA;
        NodeB = roomB;
        distance = dis;
    }
    
    public int CompareTo(NodeConnect other)
    {
        if (other == null) return 1;
        return distance.CompareTo(other.distance);
    }
    
}