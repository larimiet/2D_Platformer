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
	public GameObject backGround;
	public float zoomRatio;
	void Start () {
		control = GameObject.FindGameObjectWithTag("GameController");
		CTRL = control.GetComponent<TurnControl>();
		offset = new Vector3(0,0,-1);
		backGround = GameObject.FindGameObjectWithTag("Background");
		foreach (GameObject item in CTRL.units){
			targets.Add(item.transform);
		}
		SmoothTime = 0.25f;
		MinZoom = 5;
		zoomRatio = 1.1f;
		sensitivity = 0.05f;
	}
	
	// Update is called once per frame
	void Update(){
		if(zoomRatio > 0.5 ){
			zoomRatio += Input.GetAxis("Mouse ScrollWheel")*sensitivity;
		}else{
			zoomRatio = 0.51f;
		}if(zoomRatio < 1.5 ){
			zoomRatio += Input.GetAxis("Mouse ScrollWheel")*sensitivity;
		}else{
			zoomRatio = 1.49f;
		}
		
	}
	void FixedUpdate () {
		player = CTRL.player;
		foreach(GameObject unit in GameObject.FindGameObjectsWithTag("Player")){
			if(CTRL.currentTeam != -1){
				if(unit.GetComponent<PlayerScript>().TeamID == CTRL.currentTeam&&!targets.Contains(unit.transform)){
				targets.Add(unit.transform);
			}
			}else if(!targets.Contains(unit.transform)){
				targets.Add(unit.transform);
			}
			
		}
		MoveCamera(getCenterPoint());
		MaxZoom = GetGreatestDistance();
		Zoom();
		backGround.transform.position = gameObject.transform.position *0.5f;
	}
	public void clearTargets(){
		targets.Clear();
		zoomRatio = 1.1f;
	}
	void MoveCamera(Vector3 point){
		if(targets.Count == 0){
			return;
		}
		 
		transform.position = Vector3.SmoothDamp(transform.position, point + offset, ref velocity , SmoothTime) ;
		
	}
	void Zoom(){
		
		if(targets.Count <= 1){
			Camera.main.orthographicSize = MinZoom;
		}else{
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,(GetGreatestDistance()/2) *zoomRatio , SmoothTime);
		}
		
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