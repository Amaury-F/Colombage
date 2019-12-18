using System.Globalization;
using UnityEngine;

public class RoofSpawner : MonoBehaviour {
    
    public int number;
    public GameObject clone;
    
    void Start() {
        Generate();
    }

    public void Kill() {
        for (int i = 0; i < 9; ++i) {
            foreach(Transform child in transform) {
                DestroyImmediate(child.gameObject);
            }
        }
        
    }
    public void Generate() {
        Kill();

        int size = (int) Mathf.Sqrt(number);
        for (int i = 0; i < size; ++i) {
            for (int j = 0; j < size; ++j) {
                GameObject story = new GameObject();
                story.transform.SetParent(transform);
                story.name = "roof_" + i + "," + j;
                story.transform.position = new Vector3(i * 1.1f, 0, j * 1.1f);
                
                GameObject obj = Instantiate(clone, new Vector3(i*2, 0, j*2), Quaternion.identity);
                obj.transform.SetParent(story.transform);

                RoofGenerator g = obj.GetComponent<RoofGenerator>();
                g.height = Random.Range(0.5f, 2f);
                g.semiHeight = Random.Range(0.5f, g.height);

                g.xSizeA = Random.Range(0.5f, 1.6f);
                g.xSizeB = Random.Range(0.5f, g.xSizeA);
                
                g.zSizeA = Random.Range(0.5f, 1.6f);
                g.zSizeB = Random.Range(0.5f, g.zSizeA);
                
                g.topSize = Random.Range(0f, g.zSizeB);
                
                g.GenerateRoof();
            }
        }
    }
}