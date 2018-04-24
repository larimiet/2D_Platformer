﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {
	public GameObject control;
	public TurnControl CTRL;
	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		control = GameObject.FindGameObjectWithTag("GameController");
		CTRL = control.GetComponent<TurnControl>();
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		player = CTRL.player;
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, 0.25f ) ;
	}
}