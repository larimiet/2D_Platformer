﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaButton : MonoBehaviour
{
    public GameObject masterObject;
    public int order;
    public int actionNumber = 1;
    public SpriteRenderer sRend;
    public Sprite[] spritePool;
    public bool selected;

    // Use this for initialization
    void Start ()
    {
        sRend = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        SpriteSelect();
        selected = masterObject.GetComponent<CharacterAlpha>().selected;
        if(selected)
        {
            sRend.enabled = true;
        }
        else
        {
            sRend.enabled = false;
        }
        if (masterObject == null)
        {
            Destroy(gameObject);
        }
    }

    public void SpriteSelect()
    {
        sRend.sprite = spritePool[actionNumber];
    }

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0) && selected)
        {
            if (order <= 8)
            {
                order = order + 1;
            }
            if (order > 8)
            {
                order = 0;
            }
        }
    }
}
