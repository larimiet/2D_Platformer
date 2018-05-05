using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorVertical : MonoBehaviour
{
    // 2 = elvator goes up, 1 = elevator goes down.
    public int upOrDown = 1;
    public bool setUpOrDown = true;
    public LayerMask top;
    public LayerMask bottom;

	// Use this for initialization
	void Start ()
    {
        top = LayerMask.GetMask("ElevatorTop");
        bottom = LayerMask.GetMask("ElevatorBottom");
    }

    bool TrackEnd(Vector2 pos, Vector2 dir, float distance, LayerMask layer)
    {
        if (Physics2D.Raycast(pos, dir, distance, layer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if(TrackEnd(transform.position, Vector2.up, 0, top))
        {
            print("top");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CompareTag("ElevatorTop"))
        {
            print("ElevatorTop");
        }
    }
}
