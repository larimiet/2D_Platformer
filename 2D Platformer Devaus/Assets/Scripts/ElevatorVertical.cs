using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorVertical : MonoBehaviour
{
    // 1 = elvator goes up, -1 = elevator goes down.
    public int upOrDown = 1;
    public bool setUpOrDown = true;
    public LayerMask top;
    public LayerMask bottom;
    public Vector2 targetPos;
    public Vector2 currentPos;
    public float speed = 10;

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
        currentPos = new Vector2(transform.position.x, transform.position.y);

        if(TrackEnd(transform.position, Vector2.up, 0, top))
        {
            print("im at top");
        }

        if(currentPos != targetPos)
        {
            Move();
        }

       RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,
            transform.position.y + upOrDown * 1.05f), transform.up * upOrDown);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == ("ElevatorTop") || hit.collider.gameObject.tag == ("ElevatorBottom"))
            {
                targetPos = (new Vector2(hit.collider.transform.position.x,
                hit.collider.transform.position.y));
                print("top above");
            }
        }
        else
        {
            print("else");
        }
    }

   public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.2f * speed * Time.deltaTime);
    }
}
