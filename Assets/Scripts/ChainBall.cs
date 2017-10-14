using UnityEngine;
using System.Collections;

public class ChainBall : MonoBehaviour {

    public float angle = 0;
    public float speed = (2 * Mathf.PI) / 5;
    public float radius = 5;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        angle += speed * Time.deltaTime;
        transform.rotation = new Quaternion(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0, 1);
    }
}
