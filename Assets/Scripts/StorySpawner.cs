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
                GameObject story = new GameObject();
                story.transform.SetParent(transform);
                story.name = "story_" + i + "," + j;
                story.transform.position = new Vector3(i * 2, 0, j * 2);
                
                GameObject obj = Instantiate(clone, new Vector3(i*2, 0, j*2), Quaternion.identity);
                obj.transform.SetParent(story.transform);
                obj.transform.localScale = new Vector3(Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f));
                
                obj.GetComponent<BeamGenerator>().seed = (int) (Random.value * 100000);
                obj.GetComponent<BeamGenerator>().width = Random.Range(0.04f, 0.13f);
                obj.GetComponent<BeamGenerator>().Generate();
            }
        }
    }
}
