using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
	public GameObject control;
	public TurnControl CTRL;
	public List<Transform> targets = new List<Transform>();
	private Vector3 velocity;
	public GameObject player;
	public float SmoothTime;
	public float MinZoom;
	public float MaxZoom;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		control = GameObject.FindGameObjectWithTag("GameController");
		CTRL = control.GetComponent<TurnControl>();
		offset = transform.position - player.transform.position;
		foreach (GameObject item in CTRL.units){
			targets.Add(item.transform);
		}
		SmoothTime = 0.25f;
		MinZoom = 5;
		MaxZoom = 12;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		player = CTRL.player;
		targets = CTRL.targets;
		MoveCamera();
		Zoom();
	
	}

	void MoveCamera(){
		if(targets.Count == 0){
			return;
		}
		Vector3 centerPoint = getCenterPoint();
		transform.position = Vector3.SmoothDamp(transform.position, centerPoint + offset, ref velocity , SmoothTime) ;
		
	}
	void Zoom(){
		Camera.main.orthographicSize = Mathf.Lerp(MinZoom, MaxZoom,GetGreatestDistance()/25);
	}
	float GetGreatestDistance(){
		Bounds Koko = new Bounds(targets[0].position, Vector3.zero);
		foreach(Transform item in targets){
			Koko.Encapsulate(item.position);
		}
		return Koko.size.x;
	}
	Vector3 getCenterPoint(){
		if(targets.Count == 0){
			return targets[0].position;
		}
		Bounds Rajat = new Bounds(targets[0].position,Vector3.zero );
		foreach (Transform item in targets)
		{
			Rajat.Encapsulate(item.position);
		}
		return Rajat.center;
	}
}