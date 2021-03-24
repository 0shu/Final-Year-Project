using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(vertRandomiser))]
public class vertRandomiserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        vertRandomiser myVert = (vertRandomiser)target;

        if(GUILayout.Button("Add new vert"))
        {
            myVert.AddNewVert();
        }
    }
}
