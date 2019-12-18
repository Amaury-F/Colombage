using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGen : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Objects;
    public Material material;

    [Header("Prefabs")]
    public GameObject[] Doors;
    public GameObject[] Windows;

    [Header ("Building Settings")]
    public int minPieces = 2;
    public int maxPieces = 5;
    public float shift = .05f;

    [Header("Scale Settings")]
    public float scaleStep = .05f;
    public float scaleDecrease = .5f;
    //public Vector3 minScale = new Vector3 (.8f, .4f, .8f);
    //public Vector3 maxScale = new Vector3(1.2f, .6f, 1.2f);

    private List<GameObject> storeys = new List<GameObject>();

    

    // Start is called before the first frame update
    void Start()
    {
        Build();
    }

    public void DestroyHouse()
    {
        foreach (var storey in storeys)
        {
            DestroyImmediate(storey);
        }
    }

    public void Build()
    {
        DestroyHouse();

        int Storeys = Random.Range(minPieces, maxPieces);
        float height = .0f;
        GameObject clone;

        Objects.transform.localScale = new Vector3(Objects.transform.localScale.x, 0.5f, Objects.transform.localScale.z);

        clone = SpawnObject(Objects);
        height += clone.transform.localScale.y;
        clone.transform.parent.position += new Vector3(0, height / 2, 0);
        clone.name = "Ground";
        spawnDoor(clone);
        //spawnWindow(clone);
        storeys.Add(clone);

        for (int i = 1; i < Storeys; i++)
        {
            clone = SpawnObject(storeys[storeys.Count - 1]);
            height += clone.transform.localScale.y / 2;
            PositionShift(clone, height);
            ComparePosition(clone, storeys[storeys.Count - 1]);
            height += clone.transform.localScale.y / 2;
            clone.name = "Storey " + i;
            storeys.Add(clone);
        }

    }

    GameObject SpawnObject(GameObject prev)
    {
        GameObject myChildObject, clone;
        if (prev.transform.childCount > 0)
        {
            myChildObject = prev.transform.GetChild(0).gameObject;
            prev.transform.DetachChildren();
            clone = Instantiate(prev, transform.position, transform.rotation);
            myChildObject.transform.parent = prev.transform;
        }
        else
        {
            clone = Instantiate(prev, transform.position, transform.rotation);
        }

        GameObject storey = new GameObject();
        storey.transform.SetParent(transform);
        
        clone.transform.SetParent(storey.transform);
        RandomScaleObject(clone);
        clone.GetComponent<MeshRenderer>().material = material;

        BeamGenerator beamGen = clone.GetComponent<BeamGenerator>();
        beamGen.seed = Random.Range(0, 100000);
        beamGen.Generate();

        return clone;
    }

    Vector3 RandomScale(float value)
    {
        return new Vector3(Random.Range(0, value), Random.Range(0, value), Random.Range(0, value));
    }

    void RandomScaleObject(GameObject clone)
    {
        clone.transform.localScale = new Vector3(clone.transform.localScale.x + NegativePositive() * Random.Range(0f, scaleStep), 
                                                 clone.transform.localScale.y + NegativePositive() * Random.Range(0f, scaleStep), 
                                                 clone.transform.localScale.z + NegativePositive() * Random.Range(0f, scaleStep));
    }

    Vector3 RandomScaleAxis(GameObject clone, char axis)
    {
        if (axis == 'x')
            return new Vector3(Random.Range(0, scaleStep), clone.transform.localScale.y, clone.transform.localScale.z);
        else if (axis == 'y')
            return new Vector3(clone.transform.localScale.x, Random.Range(0, scaleStep), clone.transform.localScale.z);
        else if (axis == 'z')
            return new Vector3(clone.transform.localScale.x, clone.transform.localScale.y, Random.Range(0, scaleStep));
        else
            return clone.transform.localScale;
    }

    void PositionShift(GameObject clone, float height)
    {
        float step = (Random.Range(0f, shift) * NegativePositive());
        clone.transform.parent.position += new Vector3(0, height, step);
    }

    void ComparePosition(GameObject clone, GameObject prev)
    {
        Vector3 clonePosition = clone.transform.position;
        Vector3 cloneScale = clone.transform.localScale;
        Vector3 prevPosition = prev.transform.position;
        Vector3 prevScale = prev.transform.localScale;

        float distanceClone, distancePrev;

        distancePrev = Mathf.Abs(prevPosition.x - prevScale.x);
        distanceClone = Mathf.Abs(clonePosition.x - cloneScale.x);

        var parent = clone.transform.parent;
        var position = parent.position;
        position = new Vector3(position.x + (distanceClone - distancePrev), position.y, position.z);
        parent.position = position;
    }

    bool RandomBool()
    {
        return Random.Range(0f, 1f) > .5f;
    }

    float NegativePositive()
    {
        return Random.Range(0f, 1f) > .5f ? 1 : -1;
    }

    void spawnDoor(GameObject clone)
    {
        GameObject Door = Instantiate(Doors[Random.Range(0, Doors.Length)], transform.position, transform.rotation);

        Door.transform.parent = clone.transform;

        float distanceClone = Mathf.Abs(clone.transform.position.x - clone.transform.localScale.x);
        Door.transform.position += new Vector3(distanceClone / 2, clone.transform.position.y / 4, (Random.Range(0f, .1f) * NegativePositive()));
        Door.transform.localScale = new Vector3(Door.transform.localScale.x / 7, Door.transform.localScale.y / 7, Door.transform.localScale.z / 7);
    }

    void spawnWindow(GameObject clone)
    {
        float nb = Random.Range(0, 2);
        for (int i = 0; i <= nb; i++)
        {
            GameObject Window = Instantiate(Windows[Random.Range(0, Windows.Length)], transform.position, transform.rotation);

            Window.transform.parent = clone.transform;
            //Window.transform.SetParent(clone.transform.parent);

            float distanceClone = Mathf.Abs(clone.transform.position.x - clone.transform.localScale.x);
            Window.transform.position += new Vector3(distanceClone / 2, clone.transform.position.y / 4, (Random.Range(0f, .1f) * NegativePositive()));
            Window.transform.localScale = new Vector3(Window.transform.localScale.x / 7, Window.transform.localScale.y / 7, Window.transform.localScale.z / 7);
        }
    }
}
