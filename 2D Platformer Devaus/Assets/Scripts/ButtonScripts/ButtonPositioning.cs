using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPositioning : MonoBehaviour
{
    //Aasettaa objektin annettuihin x, y -coordinaatteihin,
    //jotka ovat suhteessa annettuun Transform -komponenttiin.
    public float xPos;
    public float yPos;
    public Transform originTransform;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(originTransform.position.x + xPos,
        originTransform.position.y + yPos);
	}
}
