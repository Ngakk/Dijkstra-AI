using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    public List<Node> nodes = new List<Node>();
    public int StartingNode = 0;
    public int EndingNode = 1;

    private List<int> Visited = new List<int>();
    private LineRenderer lineRenderer;

    int stoper = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            nodes[i].Distance = Mathf.Infinity;
            nodes[i].Parent = null;
            nodes[i].Id = i;
        }  
        
        if(nodes.Count > StartingNode)
        {
            nodes[StartingNode].Distance = 0;
        }
        else
        {
            Debug.LogError("Starting node " + StartingNode + " not found");
        }

        if(EndingNode >= nodes.Count)
        {
            Debug.LogError("Ending node " + EndingNode + " not found");
        }

        DoDijkstra(StartingNode);

        PrintSequence(EndingNode);
    }

    public void DoDijkstra(int _id)
    {
        List<Node> _con = nodes[_id].Connections;
        List<int> _wei = nodes[_id].ConnectionWeight;

        int _min = -1;

        Visited.Add(_id);

        Debug.Log("Doing dijkstra on node " + nodes[_id].Name);

        for(int i = 0; i < _con.Count; i++)
        {
            if (_min != -1) //Finding the shortest route that leads to a node that i haven't visited
            {
                if(_wei[_min] > _wei[i] && !Visited.Contains(_con[i].Id))
                {
                    _min = i;
                }
            }
            else if(!Visited.Contains(_con[i].Id))
            {
                _min = i;
            }

            if(nodes[_id].Distance + _wei[i] < _con[i].Distance)
            {
                _con[i].Distance = nodes[_id].Distance + _wei[i];
                _con[i].Parent = nodes[_id];
            }
        }

        stoper++;

        if (stoper > 100)
            return;

        if (_min != -1)
            DoDijkstra(_con[_min].Id);
        else if(nodes[_id].Parent != null)
            DoDijkstra(nodes[_id].Parent.Id);
    }

    private void PrintSequence(int _id)
    {
        Node current = nodes[_id];
        string path = current.Name;
        while(current.Parent != null)
        {
            current = current.Parent;
            path = current.Name + " > " + path;
        }
        Debug.Log(path);
    }

    // Update is called once per frame
    [ExecuteInEditMode]
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
                Debug.DrawLine(nodes[i].transform.position, nodes[i].Connections[j].transform.position);
            }
        }
    }
}
