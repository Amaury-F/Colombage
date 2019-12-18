using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    public Vector2 baseSize;
    public float Floor;
    public List<Material> materials;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;
    private int indiceVertices = 0, indiceTriangles = 0, indiceUV = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        baseSize = new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
        Floor = Random.Range(1.0f, Floor);

        MakeHouse();
    }

    void MakeHouse()
    {
        int nbFloor = (int)Floor;

        vertices = new Vector3[24 * nbFloor + 12];
        triangles = new int[36 * nbFloor + 12];
        uv = new Vector2[24 * nbFloor + 12];

        float height = 0;
        if (height < nbFloor)
            height++;

        Vector3 lb = new Vector3(0, 0, 0);
        Vector3 rt = new Vector3(baseSize.x + 0.1f, 0, baseSize.y + 0.1f);

        Cube Cube;

        for (int i = 0; i < Floor; i++)
        {
            lb = new Vector3(lb.x + 0.1f, rt.y, lb.z + 0.1f);
            rt = new Vector3(rt.x - 0.1f, rt.y + height, rt.z - 0.1f);
            Cube = new Cube(lb, rt);
            addShape(Cube.getTriangles(), Cube.getVertices(), Cube.getUV());
        }

        lb = new Vector3(lb.x + 0.1f, rt.y, lb.z + 0.1f);
        rt = new Vector3(rt.x - 0.1f, rt.y + height, rt.z - 0.1f);

        CreateMesh(vertices, triangles, uv);
    }

    void addShape(int[] triangles, Vector3[] vertices, Vector2[] uv)
    {
        foreach (int i in triangles)
        {
            this.triangles[indiceTriangles++] = i + indiceVertices;
            Debug.Log(i);
        }
        foreach (Vector3 v in vertices)
        {
            this.vertices[indiceVertices++] = v;
        }
        foreach (Vector2 u in uv)
        {
            this.uv[indiceUV++] = u;
        }
    }

    void CreateMesh(Vector3[] vertices, int[] triangles, Vector2[] uv)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = materials[(int)Random.Range(0, materials.Count)];

        gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }
}
