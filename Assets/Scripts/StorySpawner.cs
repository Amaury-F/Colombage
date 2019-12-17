using System.Globalization;
using UnityEngine;

public class StorySpawner : MonoBehaviour {

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
                GameObject obj = Instantiate(clone, new Vector3(i*2, 0, j*2), Quaternion.identity);
                obj.transform.SetParent(transform);
                obj.GetComponent<BeamGenerator>().seed = Random.Range(0, 100000);
                obj.GetComponent<BeamGenerator>().Generate();
            }
        }
    }
}
