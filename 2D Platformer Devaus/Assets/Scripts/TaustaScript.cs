using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaustaScript : MonoBehaviour {
    public GameObject camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 100);
	}
}
