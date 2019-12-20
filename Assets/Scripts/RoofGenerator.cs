using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofGenerator : MonoBehaviour {

    public Material mat;
    
    [Range(0.1f, 2f)]
    public float semiHeight;
    [Range(0.1f, 2f)]
    public float height;
    
    
    [Range(0f, 1.6f)]
    public float topSize;

    [Range(0.1f, 1.6f)]
    public float xSizeA;
    [Range(0.1f, 1.6f)]
    public float zSizeA;
    
    [Range(0.1f, 1.6f)]
    public float xSizeB;
    [Range(0.1f, 1.6f)]
    public float zSizeB;

    public bool autoUpdate = false;
    
    void Start() {
        GenerateRoof();
    }

    public void GenerateRoof() {
        if (gameObject.GetComponent<MeshFilter>() == null) {
            gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
        }

        
        Vector3[] vertices = new Vector3[10];

        float xs = xSizeA / 2;
        float zs = zSizeA / 2;
        
        vertices[0] = new Vector3(-xs, 0, -zs);
        vertices[1] = new Vector3(-xs, 0, +zs);
        vertices[2] = new Vector3(+xs, 0, +zs);
        vertices[3] = new Vector3(+xs, 0, -zs);
        
        xs = xSizeB / 2;
        zs = zSizeB / 2;
        
        vertices[4] = new Vector3(-xs, semiHeight, -zs);
        vertices[5] = new Vector3(-xs, semiHeight, +zs);
        vertices[6] = new Vector3(+xs, semiHeight, +zs);
        vertices[7] = new Vector3(+xs, semiHeight, -zs);
        
        vertices[8] = new Vector3(0, height, -topSize/2);
        vertices[9] = new Vector3(0, height, +topSize/2);


        int[] triangles = {
            0, 1, 4,
            1, 5, 4,
            1, 2, 5,
            2, 6, 5,
            2, 3, 6,
            3, 7, 6,
            0, 7, 3,
            0, 4, 7,

            4, 5, 9,
            4, 9, 8,
            8, 9, 6,
            8, 6, 7,
            5, 6, 9,
            4, 8, 7
        };

        Mesh mesh = new Mesh {vertices = vertices, triangles = triangles};

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = mat;


    }
}
