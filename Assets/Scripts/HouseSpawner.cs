using System.Globalization;
using UnityEngine;

public class HouseSpawner : MonoBehaviour {

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
                GameObject house = new GameObject();
                house.transform.SetParent(transform);
                house.name = "house_" + i + "," + j;
                house.transform.position = new Vector3(i * 3, 0, j * 3);
                
                GameObject obj = Instantiate(clone, new Vector3(i * 3, 0, j * 3), Quaternion.identity);
                obj.transform.SetParent(house.transform);
                obj.transform.localScale = new Vector3(Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f));
                
                obj.GetComponent<HouseGen>().Build();
            }
        }
    }
}
