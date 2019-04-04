using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    //public Graph Grafo;
    public TextMeshPro nameDisplay;

    [Header("Node info")]
    public string Name;
    public List<Node> Connections = new List<Node>();
    public List<int> ConnectionWeight = new List<int>();
    public float Distance;
    [HideInInspector]
    public Node Parent;
    [HideInInspector]
    public int Id;

    // Start is called before the first frame update
    void Start()
    {
        nameDisplay.text = Name;
        
        if(Connections.Count != ConnectionWeight.Count)
        {
            Debug.LogError("Connections and ConnectionWeight for node " + Name + " are not the same sizse");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
