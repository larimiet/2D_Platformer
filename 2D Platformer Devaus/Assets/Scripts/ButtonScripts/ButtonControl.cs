using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{

    public Sprite[] spritePool;
    public SpriteRenderer sRend;
    // Use this for initialization
    
    public GameObject control;
    public GameObject player;
    public TurnControl turnCtrl;
    public PlayerScript playerCTRL;
    public int sIndex;
    public int ButtonIndex;
    void Start()
    {
        player = gameObject.transform.parent.parent.gameObject;
        control= GameObject.FindGameObjectWithTag("GameController");
        sRend = GetComponent<SpriteRenderer>();
        sIndex = 0;
        turnCtrl= control.GetComponent<TurnControl>();
        playerCTRL = player.GetComponent<PlayerScript>();
        
    }

    // Update is called once per frame
    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (sIndex < spritePool.Length-1)
            {
                sIndex = sIndex + 1;
            }
            else if (sIndex == spritePool.Length-1)
            {
                sIndex = 0;
            }
        }
    }
	void Update(){
		sRend.sprite = spritePool[sIndex];

    }
}
