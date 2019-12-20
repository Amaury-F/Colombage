using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (HouseSpawner))]
public class HouseSpawnerEditor : Editor {

    public override void OnInspectorGUI() {
        HouseSpawner mapGen = (HouseSpawner)target;

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