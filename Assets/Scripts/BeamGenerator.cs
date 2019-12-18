using System;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeamGenerator : MonoBehaviour {

    public GameObject beamPrefab;

    [Range(0, 0.2f)]
    public float width = 0.06f;
    
    [Range(0, 100)]
    public int shift = 17;

    public int seed = 12345;
    
    public bool autoUpdateBeams;
    private void Start() {
        Generate();
    }

    public void Generate() {
        if (seed < 0) {
            seed = Random.Range(-100000, -1);
        }
        Random.InitState(seed);

        DestroyBeams();
        GenerateBeams();
    }

    private void DestroyBeams() {
        for (var did = 0; did < 10; ++did) {
            foreach (Transform obj in transform.parent) {
                if (obj.gameObject.tag == "beam") {
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
        
    }

    private void GenerateBeams() {
        Vector2 a = new Vector2(0, 0);
        Vector2 b = new Vector2(1, 1);

        Vector3[] faces = {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};

        foreach (var face in faces) {
            float eps = width * ((float) shift / 100);
            float w2 = width / 2;
            
            PlaceBeamOnFace(face, new Vector2(0-eps, 0+w2-eps), new Vector2(1+eps, 0+w2-eps), width, eps, "beam");
            PlaceBeamOnFace(face, new Vector2(0-eps, 1-w2+eps), new Vector2(1+eps, 1-w2+eps), width, eps, "beam");

            PlaceBeamOnFace(face, new Vector2(0+w2-eps, 0+w2), new Vector2(0+w2-eps, 1-w2), width, eps, "beam");
            PlaceBeamOnFace(face, new Vector2(1-w2+eps, 0+w2), new Vector2(1-w2+eps, 1-w2), width, eps, "beam");


            eps = 2 * eps / 3;
            Vector3 scale = transform.localScale;
            float ratio = mult(Vector3.Cross(face, Vector3.up), scale).magnitude / scale.y;
            float scalar = ratio + 1.5f;

            float rand = Random.value;
            float highProb = Mathf.Sqrt(rand) * scalar;
            float lowProb = rand * scalar;
        
            int nbrBeam = Mathf.RoundToInt(0.15f*highProb + 0.75f*lowProb);
        
            float xFract = 1f / (nbrBeam+1);
            float x = 0f;
            for (int i = 0; i < nbrBeam; ++i) {
                x += xFract;
                PlaceBeamOnFace(face, new Vector2(x, 0+w2), new Vector2(x, 1-w2), width, eps, "beam");
            }

            if (nbrBeam == 0) {
                if (Random.value < 0.3) {
                    PlaceBeamOnFace(face, new Vector2(Random.value, 0+w2), new Vector2(Random.value, 1-w2), width, eps, "beam");
                }
            }
        
            eps = 2 * eps / 3;
            x = 0f;
            for (int i = 0; i < nbrBeam+1; ++i) {
                ratio = xFract / scale.y;
                scalar = ratio + 1.5f;

                rand = Random.value;
                highProb = Mathf.Sqrt(rand) * scalar;
                lowProb = rand * scalar;
        
                float yNbrBeam = Mathf.RoundToInt(0.15f*highProb + 0.85f*lowProb);

                float yFract = 1f / (yNbrBeam+1);
                float y = 0f;
                for (int j = 0; j < yNbrBeam; ++j) {
                    y += yFract;
                    PlaceBeamOnFace(face, new Vector2(x, y), new Vector2(x+xFract, y), width, eps, "beam");
                    
                    if (Random.value < 0.1) {
                        PlaceBeamOnFace(face, new Vector2(x, y), new Vector2(x+xFract, y-yFract), width, eps, "beam");
                    }
                }

                x += xFract;
            }
            
        }

    }

    /*
     * put a beam of width on the face (V3.forward, V3.left ...),
     * the face becomes a 2D plan with 0,0 at topleft and 1,1 at bottomright
     */
    private void PlaceBeamOnFace(Vector3 face, Vector2 a, Vector2 b, float width, float eps, string objName) {
        Vector3 c0;
        Vector3 c1;
        float sh = - width / 2 + eps;
        Vector3 scale = transform.localScale;

        if (face == Vector3.forward) {
            c0 = new Vector3(0.5f, 0.5f, 0.5f + sh/scale.z);
            c1 = new Vector3(-1, -1, 0);
            Vector3 aa = c0 + new Vector3(c1.x * a.x, c1.y * a.y, 0);
            Vector3 bb = c0 + new Vector3(c1.x * b.x, c1.y * b.y, 0);
            PlaceBeam(aa, bb, width, objName);
            
        } else if (face == Vector3.right) {
            c0 = new Vector3(0.5f + sh/scale.x, 0.5f, -0.5f);
            c1 = new Vector3(0, -1, 1);
            Vector3 aa = c0 + new Vector3(0, c1.y * a.y, c1.z * a.x);
            Vector3 bb = c0 + new Vector3(0, c1.y * b.y, c1.z * b.x);
            PlaceBeam(aa, bb, width, objName);
            
        } else if (face == Vector3.back) {
            c0 = new Vector3(-0.5f, 0.5f, -0.5f - sh/scale.z);
            c1 = new Vector3(1, -1, 0);
            Vector3 aa = c0 + new Vector3(c1.x * a.x, c1.y * a.y, 0);
            Vector3 bb = c0 + new Vector3(c1.x * b.x, c1.y * b.y, 0);
            PlaceBeam(aa, bb, width, objName);
            
        } else if (face == Vector3.left) {
            c0 = new Vector3(-0.5f - sh/scale.z, 0.5f, 0.5f);
            c1 = new Vector3(0, -1, -1);
            Vector3 aa = c0 + new Vector3(0, c1.y * a.y, c1.z * a.x);
            Vector3 bb = c0 + new Vector3(0, c1.y * b.y, c1.z * b.x);
            PlaceBeam(aa, bb, width, objName);
        }
    }

    private void PlaceBeam(Vector3 a, Vector3 b, float width, string objName) {
        Vector3 localScale = transform.localScale;
        a = mult(a, localScale);
        b = mult(b, localScale);
        Vector3 center = (a + b) / 2;
        Vector3 forward = a - center;

        Quaternion rotation = Quaternion.LookRotation(forward.normalized);
        
        GameObject beam = Instantiate(beamPrefab, transform.position, rotation);
        beam.tag = "beam";
        beam.name = objName;

        Vector3 scale = mult(new Vector3(1, 1, 1), new Vector3(width, width, forward.magnitude * 2));
        beam.transform.position += center;
        beam.transform.localScale = scale;

        beam.transform.SetParent(transform.parent);
    }


    private static Vector3 mult(Vector3 a, Vector3 b) {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    
}
