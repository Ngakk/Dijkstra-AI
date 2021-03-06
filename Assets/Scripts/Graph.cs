﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GraphMethod : int
{
    DIJKSTRA,
    BREADTH_FIRST,
    DEPTH_FIRST
}

[ExecuteInEditMode]
public class Graph : MonoBehaviour
{

    public List<Node> nodes = new List<Node>();
    public int StartingNode = 0;
    public int EndingNode = 1;
    public GraphMethod Method;

    private List<int> Visited = new List<int>();
    private LineRenderer lineRenderer;
    private List<Action<int>> actions;
    private Stack<Vector2Int> stack = new Stack<Vector2Int>();
    private Queue<Vector2Int> queue = new Queue<Vector2Int>();
    private List<Vector2Int> path = new List<Vector2Int>();

    int stoper = 0;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        actions = new List<Action<int>>(new Action<int>[] { DoDijkstra, DoBreadthFirst, DoDepthFirst });

        Visited = new List<int>();
        stack = new Stack<Vector2Int>();        //Clearing all previous data
        queue = new Queue<Vector2Int>();

        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].SetText();
            nodes[i].Distance = Mathf.Infinity; //setting the nodes's initial values
            nodes[i].Parent = null;
            nodes[i].Id = i;
        }

        if (nodes.Count > StartingNode)
        {
            nodes[StartingNode].Distance = 0; //Initializing the value of the starting node
        }
        else
        {
            Debug.LogError("Starting node " + StartingNode + " not found");
        }

        if (EndingNode >= nodes.Count) //Checking if end node exists
        {
            Debug.LogError("Ending node " + EndingNode + " not found");
        }

        Debug.Log("Starting method " + (int)Method);
        actions[(int)Method](StartingNode);
        PrintSequence(EndingNode);
    }

    private void DoDijkstra(int _id)
    {
        List<Node> _con = nodes[_id].Connections;
        List<int> _wei = nodes[_id].ConnectionWeight;

        Visited.Add(_id); //Adding this node to the visited nodes list

        for(int i = 0; i < _con.Count; i++)
        {
            if(nodes[_id].Distance + _wei[i] < _con[i].Distance)
            {
                _con[i].Distance = nodes[_id].Distance + _wei[i];
                _con[i].Parent = nodes[_id];
                queue.Enqueue(new Vector2Int(_id, _con[i].Id));
            }
        }

        if(queue.Count > 0)
            DoDijkstra(queue.Dequeue().y);
    }

    private void PrintSequence(int _id)
    {
        Node current = nodes[_id];
        string _path = current.Name;
        while(current.Parent != null)
        {
            current = current.Parent;
            _path = current.Name + " > " + _path;

        }
        if(Method == GraphMethod.DIJKSTRA)
            Debug.Log(_path + " with distance " + nodes[_id].Distance);
        else
            Debug.Log(_path);
    }

    private void DoDepthFirst(int _id)
    {
        if(_id == EndingNode)
        {
            return;
        }

        Visited.Add(_id);
        for (int i = nodes[_id].Connections.Count - 1; i >= 0; i--)
        {
            if (!Visited.Contains(nodes[_id].Connections[i].Id))
            {
                Vector2Int _connection = new Vector2Int(_id, nodes[_id].Connections[i].Id);
                if (!stack.Contains(_connection))
                    stack.Push(_connection);
            }
        }

        if(stack.Count != 0)
        {
            Vector2Int _nextid = stack.Peek();
            stack.Pop();
            nodes[_nextid.y].Parent = nodes[_nextid.x];
            DoDepthFirst(_nextid.y);
        }

    }

    private void DoBreadthFirst(int _id)
    {
        if (_id == EndingNode)
        {
            return;
        }

        Visited.Add(_id);
        for (int i = nodes[_id].Connections.Count - 1; i >= 0; i--)
        {
            if (!Visited.Contains(nodes[_id].Connections[i].Id))
            {
                Vector2Int _connection = new Vector2Int(_id, nodes[_id].Connections[i].Id);
                if (!queue.Contains(_connection))
                    queue.Enqueue(_connection);
            }
        }

        if (queue.Count != 0)
        {
            Vector2Int _nextid = queue.Peek();
            queue.Dequeue();
            nodes[_nextid.y].Parent = nodes[_nextid.x];
            DoBreadthFirst(_nextid.y);
        }
    }
    // Update is called once per frame
    
    void Update()
    {
        DrawGraph();
    }

    
    void DrawGraph()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            for(int j = 0; j < nodes[i].Connections.Count; j++)
            {
                Debug.DrawLine(nodes[i].transform.position, nodes[i].Connections[j].transform.position, Color.black);
            }
        }
    }
}


//TODO: ver que este bien el grafo de power grid y arreglar dijkstra si es que esta mal