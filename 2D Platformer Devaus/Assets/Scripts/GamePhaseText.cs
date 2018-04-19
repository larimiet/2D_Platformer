using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhaseText : MonoBehaviour
{
    public GameObject controlObject;
    public Control control;
    public Text phaseText;
    // Use this for initialization
    void Start ()
    {
        control = controlObject.GetComponent<Control>();
        phaseText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (control.gamePhase == 1)
        {
            phaseText.text = ("Player1, make your plan. Then press space to continue...");
        }

        if (control.gamePhase == 2)
        {
            phaseText.text = ("Player2, make your plan. Then press space to continue...");
        }

        if (control.gamePhase == 3)
        {
            phaseText.text = ("Executing orders, please wait...");
        }
    }
}
