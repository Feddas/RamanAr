using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generate3dGraph))]
public class EditorGenerate3dGraph : Editor
{
    public override void OnInspectorGUI()
    {
        Generate3dGraph graphGen = (Generate3dGraph)target;

        // Auto update when value in inspector changed
        if (DrawDefaultInspector()) // returns true if any value in the inspector has changed
        {
            graphGen.GenerateMap();
        }

        // Manual update
        if (GUILayout.Button("Generate"))
        {
            graphGen.GenerateMap();
        }
    }
}
