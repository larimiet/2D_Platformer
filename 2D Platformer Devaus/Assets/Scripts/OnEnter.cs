using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEnter : MonoBehaviour {
    public GameObject mainMenu;
    public MainMenu Menu;
    public List<string> buttonText = new List<string>();
    public int buttonIndex;
    public int active;
    public Text teksti;
	// Use this for initialization
	void Start () {

        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        Menu = mainMenu.GetComponent<MainMenu>();
        buttonText = Menu.changedText;
        active = 0;
        teksti = gameObject.GetComponentInChildren<Text>();
    }
    public void activate()
    {
        active = 1;
        
    }
    public void deactivate()
    {
        active = 0;

    }
    void Update () {
        Debug.Log(buttonText[buttonIndex * 2 + active]);
        teksti = gameObject.GetComponentInChildren<Text>();
        teksti.text = buttonText[buttonIndex * 2 + active];
    }
}
