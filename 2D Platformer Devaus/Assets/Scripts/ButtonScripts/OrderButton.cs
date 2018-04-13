using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButton : MonoBehaviour
{
    //Tällä koodilla voi antaa character luokalle käskyjä.
    public int orderToSend;
    public GameObject sendOrderTo;
    // Use this for initialization
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            sendOrderTo.GetComponent<Character>().action = orderToSend;
            sendOrderTo.GetComponent<Character>().setDestination = true;
            sendOrderTo.GetComponent<Character>().performAction = true;
        }
    }
}
