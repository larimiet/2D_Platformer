using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonActive : MonoBehaviour {

	// Use this for initialization
	public GameObject Controller;
    public TurnControl turnController;
	public List<GameObject> buttons = new List<GameObject>();
	public GameObject player;
	public ButtonControl buttonCtrl;
	public PlayerScript CTRL;
	void Start () {
		Controller = GameObject.FindGameObjectWithTag("GameController");
        turnController = Controller.GetComponent<TurnControl>();
		player = transform.parent.gameObject;
		CTRL = player.GetComponent<PlayerScript>();
		foreach(Transform button in transform){
			buttons.Add(button.gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) ){
			foreach(GameObject button in buttons){
				turnController.actionList.Add(button.GetComponent<ButtonControl>().sIndex);
			}
		turnController.currentplayer++;
		}
		
	}
}
