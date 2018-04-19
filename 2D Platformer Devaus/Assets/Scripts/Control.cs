using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    //Tänne kerätään pelissä tarvittavaa dataa, sekä asetetaan yleisiä arvoja
    public float gridSize = 1f;
    public string orderToPlayer;

    public int gamePhase = 1;
    public int frame = 1;
    public int performActionNumber = 1;
    public float timer = 0;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown("space"))
        {
            if(gamePhase <= 2)
            gamePhase = gamePhase + 1;
             if(gamePhase > 3)
            gamePhase = 1;
        }

        if (gamePhase == 3)
        {
            //1 = move, turn, crouch, jump 
            //2 = fire  
            //3 = jump (landing), crouch up.
            timer = timer + 1 * Time.deltaTime;
            if (timer > 1.5)
            {
                performActionNumber += 1;
                if (performActionNumber > 3 && frame < 3)
                {
                    performActionNumber = 1;
                    frame = frame + 1;
                    timer = 0;
                }
                else if (performActionNumber > 3 && frame == 3)
                {
                    performActionNumber = 1;
                    frame = 1;
                    gamePhase = 1;
                    timer = 0;
                }
                timer = 0;
            }
        }
	}
}
