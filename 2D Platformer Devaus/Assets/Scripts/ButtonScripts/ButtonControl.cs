using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{

    public Sprite[] spritePool;
    public SpriteRenderer sRend;
    // Use this for initialization
    public int sIndex;
    void Start()
    {
        sRend = GetComponent<SpriteRenderer>();
        sIndex = 0;
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
