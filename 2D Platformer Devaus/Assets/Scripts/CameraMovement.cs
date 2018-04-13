using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followedObject;
    public float smoothTime = 0.6F;
    private Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetPosition = new Vector3(followedObject.position.x, followedObject.position.y, followedObject.position.z - 10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
