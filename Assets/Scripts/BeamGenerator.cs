using System;
using System.Collections.Generic;
using UnityEngine;

public class BeamGenerator : MonoBehaviour {

    public GameObject beamPrefab;

    public List<GameObject> beams = new List<GameObject>();

    public bool autoUpdateBeams;
    private void Start() {
        Generate();
    }

    public void Generate() {
        DestroyBeams();
        GenerateBeams();
    }

    private void DestroyBeams() {
        foreach (var beam in beams) {
            DestroyImmediate(beam, false);
        }
        beams.Clear();
    }

    private void GenerateBeams() {
        float width = 0.1f;
        //float eps = 0.01f;

        Vector2 a = new Vector2(0, 0);
        Vector2 b = new Vector2(1, 1);

        PlaceBeamOnFace(a, b, Vector3.forward, width);
        PlaceBeamOnFace(a, b, Vector3.back, width);
        PlaceBeamOnFace(a, b, Vector3.left, width);
        PlaceBeamOnFace(a, b, Vector3.right, width);

    }

    private void PlaceBeamOnFace(Vector2 a, Vector2 b, Vector3 face, float width) {
        Vector3 c0;
        Vector3 c1;
        if (face == Vector3.forward) {
            c0 = new Vector3(0.5f, 0.5f, 0.5f);
            c1 = new Vector3(-1, -1, 0);
            Vector3 aa = c0 + new Vector3(c1.x * a.x, c1.y * a.y, 0);
            Vector3 bb = c0 + new Vector3(c1.x * b.x, c1.y * b.y, 0);
            PlaceBeam(aa, bb, width);
            
        } else if (face == Vector3.right) {
            c0 = new Vector3(0.5f, 0.5f, -0.5f);
            c1 = new Vector3(0, -1, 1);
            Vector3 aa = c0 + new Vector3(0, c1.y * a.x, c1.z * a.y);
            Vector3 bb = c0 + new Vector3(0, c1.y * b.x, c1.z * b.y);
            PlaceBeam(aa, bb, width);
            
        } else if (face == Vector3.back) {
            c0 = new Vector3(-0.5f, 0.5f, -0.5f);
            c1 = new Vector3(1, -1, 0);
            Vector3 aa = c0 + new Vector3(c1.x * a.x, c1.y * a.y, 0);
            Vector3 bb = c0 + new Vector3(c1.x * b.x, c1.y * b.y, 0);
            PlaceBeam(aa, bb, width);
            
        } else if (face == Vector3.left) {
            c0 = new Vector3(-0.5f, 0.5f, 0.5f);
            c1 = new Vector3(0, -1, -1);
            Vector3 aa = c0 + new Vector3(0, c1.y * a.x, c1.z * a.y);
            Vector3 bb = c0 + new Vector3(0, c1.y * b.x, c1.z * b.y);
            PlaceBeam(aa, bb, width);
        }
    }

    private void PlaceBeam(Vector3 a, Vector3 b, float width) {
        Vector3 localScale = transform.localScale;
        a = mult(a, localScale);
        b = mult(b, localScale);
        Vector3 center = (a + b) / 2;
        Vector3 forward = a - center;

        Quaternion rotation = Quaternion.LookRotation(forward.normalized);
        
        GameObject beam = Instantiate(beamPrefab, transform.position, rotation);
        
        beams.Add(beam);

        Vector3 scale = new Vector3(width*localScale.x, width*localScale.y, forward.magnitude * 2);
        beam.transform.position += center;
        beam.transform.localScale = scale;
        
        //beam.transform.SetParent(transform, true);
    }


    private static Vector3 mult(Vector3 a, Vector3 b) {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    
}
