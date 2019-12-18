using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (RoofGenerator))]
public class RoofmGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        RoofGenerator roofGen = (RoofGenerator)target;

        if (DrawDefaultInspector()) {
            if (roofGen.autoUpdate) {
                roofGen.GenerateRoof();
            }
        }

        if (GUILayout.Button ("Generate")) {
            roofGen.GenerateRoof();
        }
        
    }
}