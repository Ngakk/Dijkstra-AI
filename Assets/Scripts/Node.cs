using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
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
        SetText();

        if (Connections.Count != ConnectionWeight.Count)
        {
            Debug.LogError("Connections and ConnectionWeight for node " + Name + " are not the same sizse");
        }
    }

    public void SetText()
    {
        Name = name; //Esto lo hice al final para no matarme con el power grid xd
        nameDisplay.text = Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
