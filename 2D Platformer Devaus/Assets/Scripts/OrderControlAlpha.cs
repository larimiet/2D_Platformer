using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderControlAlpha : MonoBehaviour
{
    public CharacterAlpha characterToCommand;
    public Control control;
    public int order1 = 0;
    public int order2 = 0;
    public int order3 = 0;
    public bool selected;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (characterToCommand != null)
        {
            selected = characterToCommand.GetComponent<CharacterAlpha>().selected;
            if (control.frame == 1 && control.gamePhase == 3)
            {
                if (control.performActionNumber == 1)
                {
                    if (order1 == 1)
                    {
                        characterToCommand.numberOfSteps = 1;
                        characterToCommand.action = 1;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order1 = 0;
                    }
                    if (order1 == 2)
                    {
                        characterToCommand.numberOfSteps = 2;
                        characterToCommand.action = 1;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order1 = 0;
                    }
                    if (order1 == 3)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 0;
                        characterToCommand.compY = 3;
                        order1 = 0;
                    }
                    if (order1 == 4)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 1;
                        characterToCommand.compY = 2;
                        order1 = 0;
                    }
                    if (order1 == 5)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 2;
                        characterToCommand.compY = 1;
                        order1 = 0;
                    }
                    if (order1 == 6)
                    {
                        characterToCommand.action = 5;
                        characterToCommand.crouched = true;
                        characterToCommand.performAction = true;
                        //order1 = 10;
                    }
                    if (order1 == 7)
                    {
                        characterToCommand.action = 3;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order1 = 0;
                    }
                }
                    if (order1 == 8 && control.performActionNumber == 2)
                    {
                        characterToCommand.action = 4;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order1 = 0;
                    }
                if (control.performActionNumber == 3)
                {
                    characterToCommand.jumpPhase = 2;
                    characterToCommand.crouched = false;
                    if (order1 == 5)
                    {
                        characterToCommand.performAction = true;
                        order1 = 0;
                    }
                }
            }
            if (control.frame == 2 && control.gamePhase == 3)
            {
                if (control.performActionNumber == 1)
                {
                    if (order2 == 1)
                    {
                        characterToCommand.numberOfSteps = 1;
                        characterToCommand.action = 1;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order2 = 0;
                    }
                    if (order2 == 2)
                    {
                        characterToCommand.numberOfSteps = 2;
                        characterToCommand.action = 1;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order2 = 0;
                    }
                    if (order2 == 3)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 0;
                        characterToCommand.compY = 3;
                        order2 = 0;
                    }
                    if (order2 == 4)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 1;
                        characterToCommand.compY = 2;
                        order2 = 0;
                    }
                    if (order2 == 5)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 2;
                        characterToCommand.compY = 1;
                        order2 = 0;
                    }
                    if (order2 == 6)
                    {
                        characterToCommand.action = 5;
                        characterToCommand.crouched = true;
                        characterToCommand.performAction = true;
                        //order1 = 10;
                    }
                    if (order2 == 7)
                    {
                        characterToCommand.action = 3;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order2 = 0;
                    }
                }
                    if (order2 == 8 && control.performActionNumber == 2)
                    {
                        characterToCommand.action = 4;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order2 = 0;
                    }
                
                if (control.performActionNumber == 3)
                {
                    characterToCommand.jumpPhase = 2;
                    characterToCommand.crouched = false;
                    if (order2 == 5)
                    {
                        characterToCommand.performAction = true;
                        order2 = 0;
                    }
                }
            }
            if (control.frame == 3 && control.gamePhase == 3)
            {
                if (control.performActionNumber == 1)
                {
                    if (order3 == 1)
                    {
                        characterToCommand.numberOfSteps = 1;
                        characterToCommand.action = 1;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order3 = 0;
                    }
                    if (order3 == 2)
                    {
                        characterToCommand.numberOfSteps = 2;
                        characterToCommand.action = 1;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order3 = 0;
                    }
                    if (order3 == 3)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 0;
                        characterToCommand.compY = 3;
                        order3 = 0;
                    }
                    if (order3 == 4)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 1;
                        characterToCommand.compY = 2;
                        order3 = 0;
                    }
                    if (order3 == 5)
                    {
                        characterToCommand.action = 2;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        characterToCommand.jumpPhase = 1;
                        characterToCommand.compX = 2;
                        characterToCommand.compY = 1;
                        order3 = 0;
                    }
                    if (order3 == 6)
                    {
                        characterToCommand.action = 5;
                        characterToCommand.crouched = true;
                        characterToCommand.performAction = true;
                        //order3 = 10;
                    }
                    if (order3 == 7)
                    {
                        characterToCommand.action = 3;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order3 = 0;
                    }
                }
                    if (order3 == 8 && control.performActionNumber == 2)
                    {
                        characterToCommand.action = 4;
                        characterToCommand.setDestination = true;
                        characterToCommand.performAction = true;
                        order3 = 0;
                    }
                
                if (control.performActionNumber == 3)
                {
                    characterToCommand.jumpPhase = 2;
                    characterToCommand.crouched = false;
                    if (order3 == 5)
                    {
                        characterToCommand.performAction = true;
                        order3 = 0;
                    }
                }
            }
        }
    }
}
