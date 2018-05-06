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
	float sensitivity;
	public float zoomRatio;
	void Start () {
		control = GameObject.FindGameObjectWithTag("GameController");
		CTRL = control.GetComponent<TurnControl>();
		offset = new Vector3(0,0,-1);
		foreach (GameObject item in CTRL.units){
			targets.Add(item.transform);
		}
		SmoothTime = 0.25f;
		MinZoom = 5;
		zoomRatio = 1;
		sensitivity = 0.5f;
	}
	
	// Update is called once per frame
	void Update(){
		if(zoomRatio > 0.5){
			zoomRatio += Input.GetAxis("Mouse ScrollWheel")*sensitivity;
		}else{
			zoomRatio = 0.56f;
		}
		
	}
	void FixedUpdate () {
		player = CTRL.player;
		foreach(GameObject unit in GameObject.FindGameObjectsWithTag("Player")){
			if(unit.GetComponent<PlayerScript>().TeamID == CTRL.currentTeam&&!targets.Contains(unit.transform)){
				targets.Add(unit.transform);
			}
		}
		MoveCamera(getCenterPoint());
		MaxZoom = GetGreatestDistance();
		Zoom();
		
	}
	public void clearTargets(){
		targets.Clear();
	}
	void MoveCamera(Vector3 point){
		if(targets.Count == 0){
			return;
		}
		 
		transform.position = Vector3.SmoothDamp(transform.position, point + offset, ref velocity , SmoothTime) ;
		
	}
	void Zoom(){
		
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,(GetGreatestDistance()/2) *zoomRatio , SmoothTime);
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