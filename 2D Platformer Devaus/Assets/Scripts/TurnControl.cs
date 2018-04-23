using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour {

	// Use this for initialization
	public int actionPlayer;
	public int currentplayer;
	public int currentComp;
	public List<GameObject> units;
	public List<int> actionList = new List<int>();
	
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
		
	}
	// Update is called once per frame
	
	void FixedUpdate () {
		if(currentplayer >= units.Count){
			currentplayer = -1;
			actionPlayer = 0;
			exec = true;
			currentComp =0;
			SendAction();
		}
		if(actionPlayer>= units.Count){
			currentComp++;
			actionPlayer = 0;
		}
		if(currentComp >2){
			exec = false;
			currentplayer = 0;
			currentComp = 0;
			actionPlayer = -1;
			actionList.RemoveRange(0,actionList.Count);
		}
	}
	public void SendAction(){
		
		//units[actionPlayer].GetComponent<PlayerScript>().state = PlayerScript.MovePhase.executing;
		
		foreach(GameObject unit in units){
			Debug.Log(actionPlayer+","+currentComp+","+actionList[(actionPlayer*3)+currentComp]);
			if(unit.GetComponent<PlayerScript>().playerIndex == actionPlayer){
				
				unit.GetComponent<PlayerScript>().TurnStart(actionList[(actionPlayer*3)+currentComp]);
			}
		}
		actionPlayer++;
	}
	
}
