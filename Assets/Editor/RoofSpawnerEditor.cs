using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (RoofSpawner))]
public class RoofSpawnerEditor : Editor {

    public override void OnInspectorGUI() {
        RoofSpawner mapGen = (RoofSpawner)target;

        if (DrawDefaultInspector()) {

        }
        
        if (GUILayout.Button ("Reset")) {
            mapGen.Kill();
        }

        if (GUILayout.Button ("Generate")) {
            mapGen.Generate();
        }
        
    }
}