using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChance : MonoBehaviour
{
    //Tämä koodi luo listan spriteistä, jotka voidaan sitten valita. Kätevä nappulalle esim.
    public Sprite[] spritePool;
    public int showSpriteNumber;
    public SpriteRenderer sRend;
	// Use this for initialization
	void Start ()
    {
        sRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpriteSelect();
	}

    public void SpriteSelect()
    {
        sRend.sprite = spritePool[showSpriteNumber];
    }
}
