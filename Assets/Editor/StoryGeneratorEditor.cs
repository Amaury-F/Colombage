using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (StorySpawner))]
public class StorySpawnerEditor : Editor {

    public override void OnInspectorGUI() {
        StorySpawner mapGen = (StorySpawner)target;

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