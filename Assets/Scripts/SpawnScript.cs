using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

    public GameObject Object;
    public float chance = 5;
    public float minY = 0;
    public float maxY = 0;
    public GameObject parent;

    private GameObject Clone;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CloudSpawner", 0, 1);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void CloudSpawner()
    {
        float random = Random.Range(0, 100);
        if (random <= chance)
        {
            Clone = Object;
            Instantiate(Clone, new Vector2(transform.position.x, Random.Range(minY, maxY)), transform.rotation);
        }
    }


}
