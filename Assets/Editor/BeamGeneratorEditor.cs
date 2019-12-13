using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (BeamGenerator))]
public class BeamGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        BeamGenerator mapGen = (BeamGenerator)target;

        if (DrawDefaultInspector()) {
            if (mapGen.autoUpdateBeams) {
                mapGen.Generate();
            }
        }

        if (GUILayout.Button ("Generate Beams")) {
            mapGen.Generate();
        }
        
    }
}