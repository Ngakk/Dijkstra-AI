using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Graph))]
public class GraphEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Graph graph = (Graph)target;
        if (GUILayout.Button("Run Algorithm"))
        {
            graph.Initialize();
        }
    }
}
