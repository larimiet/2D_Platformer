﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurnControl : MonoBehaviour
{

    // Use this for initialization
    public GameObject player;
    public int actionPlayer;
    public int currentplayer;
    public int currentComp;
    public List<Transform> targets = new List<Transform>();
    public List<GameObject> units;
    public List<int> actionList = new List<int>();
    public int[,] toimintolista;
    public bool exec;
    public int teams;
    public int currentTeam;
    void Awake()
    {
        units = new List<GameObject>();
        teams = 2;
        
        actionPlayer = -1;
        exec = false;
        currentComp = 0;
        currentTeam = 0;
        currentplayer = currentTeam;
    }
    void Start()
    {
        Debug.Log("unit count " + units.Count);
        toimintolista = new int[units.Count, 3];
		SortList();
        
		foreach(GameObject unit in units ){
			targets.Add(unit.transform);
		}
    }
    // Update is called once per frame
    
	public void SortList(){
		if (units.Count > 0)
        {
            units.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.GetComponent<PlayerScript>().finalIndex).CompareTo(b.GetComponent<PlayerScript>().finalIndex);
            });
        }
	}
    void Update()
    {

        if (currentplayer >= units.Count)
        {
            currentplayer = currentTeam;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            actionPlayer = -1;
            exec = true;
            currentplayer = -1;
            currentComp = 0;
            GetActions();
            SendAction();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentTeam ++;
            if(currentTeam>= teams){
                currentTeam = 0;
            }
            
            currentplayer = currentTeam;
        }
        if (exec)
        {
            player = units[actionPlayer];

        }
        else
        {
            player = units[currentplayer];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentplayer+= teams;
        }

    }
    public void SortByIndex(int index)
    {
        foreach(GameObject unit in units){
			if(unit.GetComponent<PlayerScript>().finalIndex > index){
				unit.GetComponent<PlayerScript>().finalIndex--;
			}
		}
    }
    public void GetActions()
    {
        foreach (GameObject unit in units)
        {
            unit.transform.GetChild(0).GetComponent<buttonActive>().getButtons();
        }
    }
    public void SendAction()
    {
        actionPlayer++;
        //units[actionPlayer].GetComponent<PlayerScript>().state = PlayerScript.MovePhase.executing;
        if (actionPlayer == units.Count)
        {
            EndTurn();
            actionPlayer = 0;
            currentComp++;
        }
        if (currentComp > 2)
        {
            exec = false;
            actionPlayer = -1;
            currentplayer = 0;
            Array.Clear(toimintolista, 0, toimintolista.Length);
        }
        if (exec)
        {
            foreach (GameObject unit in units)
            {
                //Debug.Log(actionPlayer+","+currentComp+","+toimintolista[actionPlayer,currentComp]);
                if (unit.GetComponent<PlayerScript>().finalIndex == actionPlayer)
                {

                    unit.GetComponent<PlayerScript>().TurnStart(toimintolista[actionPlayer, currentComp]);
                }
            }
        }
        
    }
    public void EndTurn(){
            foreach(GameObject unit in units){
                unit.GetComponent<PlayerScript>().turnEnd();
            }
        }

}
