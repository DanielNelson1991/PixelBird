using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {

    public GameObject objToTrack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(objToTrack.transform.position.x, 0, -10);
	}
}
