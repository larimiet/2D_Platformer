using System.Collections;
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
    {if(actionNumber == 1)
        sRend.sprite = spritePool[masterObject.GetComponent<OrderControlAlpha>().order1];
    if(actionNumber == 2)
        sRend.sprite = spritePool[masterObject.GetComponent<OrderControlAlpha>().order2];
    if(actionNumber == 3)
        sRend.sprite = spritePool[masterObject.GetComponent<OrderControlAlpha>().order3];
    }

    private void OnMouseOver()
    {
        if (actionNumber == 1)
        {
            masterObject.GetComponent<OrderControlAlpha>().order1 = order;
        }
        if (actionNumber == 2)
        {
            masterObject.GetComponent<OrderControlAlpha>().order2 = order;
        }
        if (actionNumber == 3)
        {
            masterObject.GetComponent<OrderControlAlpha>().order3 = order;
        }
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
