using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.Reflection;

[CustomEditor (typeof (HouseGen))]
public class HouseGenEditor : Editor
{


     
    public static void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public override void OnInspectorGUI()
    {
        HouseGen houseGen = (HouseGen)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Destroy"))
        {
            houseGen.DestroyHouse();
        }

        if (GUILayout.Button("Build"))
        {
            ClearConsole();
            houseGen.Build();
        }
    }

}
