using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurnControl : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public int actionPlayer;
	public int currentplayer;
	public int currentComp;
	public List<GameObject> units;
	public List<int> actionList = new List<int>();
	public int[,] toimintolista;
	public bool exec;
	void Awake () {
		units = new List<GameObject>();
		currentplayer = 0;
		actionPlayer = -1;
		exec = false;
		currentComp =0;
	}
	void Start(){
		Debug.Log("unit count "+ units.Count);
		toimintolista = new int[units.Count,3];
	}
	// Update is called once per frame
	void TurnShit(){
		 
	}
	void Update () {
		
		if(currentplayer >= units.Count){
			currentplayer = 0;
			
		}
		if(Input.GetKeyDown(KeyCode.X)){
			actionPlayer = -1;
			exec = true;
			currentplayer = -1;
			currentComp =0;
			GetActions();
			SendAction();
		}
		if(exec){
			player = units[actionPlayer];

		}else{
			player = units[currentplayer];
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			currentplayer++;
		}
		
	}
	public void GetActions(){
		foreach(GameObject unit in units){
			unit.transform.GetChild(0).GetComponent<buttonActive>().getButtons();
		}
	}
	public void SendAction(){
		actionPlayer++;
		//units[actionPlayer].GetComponent<PlayerScript>().state = PlayerScript.MovePhase.executing;
		if(actionPlayer == units.Count){
			actionPlayer = 0;
			currentComp++;
		}if(currentComp> 2){
			exec = false;
			actionPlayer = -1;
			currentplayer = 0;
			Array.Clear(toimintolista, 0, toimintolista.Length);
		}
		if(exec){
			foreach(GameObject unit in units){
			//Debug.Log(actionPlayer+","+currentComp+","+toimintolista[actionPlayer,currentComp]);
			if(unit.GetComponent<PlayerScript>().playerIndex == actionPlayer){
				
				unit.GetComponent<PlayerScript>().TurnStart(toimintolista[actionPlayer,currentComp]);
			}
		}
		}
		
		
	}
	
}
