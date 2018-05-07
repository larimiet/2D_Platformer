using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorVertical : MonoBehaviour
{
    // 1 = elvator goes up, 2 = elevator goes down.
    public int upOrDown = 1;
    public bool setUpOrDown = true;
    public LayerMask top;
    public LayerMask bottom;
    public Vector2 targetPos;
    public Vector2 targetPosUp;
    public Vector2 targetPosDown;
    public Vector2 currentPos;
    public GameObject passenger;
    public float speed = 10;

	// Use this for initialization
	void Start ()
    {
        top = LayerMask.GetMask("ElevatorTop");
        bottom = LayerMask.GetMask("ElevatorBottom");
        RaycastHit2D hitUp = Physics2D.Raycast(new Vector2(transform.position.x,
          transform.position.y + 1.05f), transform.up);
        if (hitUp.collider != null)
        {
            if (hitUp.collider.gameObject.tag == ("ElevatorTop") || hitUp.collider.gameObject.tag == ("ElevatorBottom"))
            {
                targetPosUp = (new Vector2(hitUp.collider.transform.position.x,
                hitUp.collider.transform.position.y));
            }
        }
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(transform.position.x,
         transform.position.y - 1.05f), -transform.up);
        if (hitDown.collider != null)
        {
            if (hitDown.collider.gameObject.tag == ("ElevatorTop") || hitDown.collider.gameObject.tag == ("ElevatorBottom"))
            {
                targetPosDown = (new Vector2(hitDown.collider.transform.position.x,
                hitDown.collider.transform.position.y));
            }
        }
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
        if (upOrDown == 1)
            targetPos = targetPosUp;
        if (upOrDown == 2)
            targetPos = targetPosDown;
        if (upOrDown > 2)
            upOrDown = 0;

        if (TrackEnd(transform.position, Vector2.up, 0, top))
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

        RaycastHit2D hitPlayer = Physics2D.Raycast(new Vector2(transform.position.x,
           transform.position.y + 1.05f), transform.up);
        if (hitPlayer.collider != null)
        {
            if (hitPlayer.collider.gameObject.tag == ("Player"))
            {
                passenger = hitPlayer.collider.gameObject;
                if (setUpOrDown)
                {
                    upOrDown += 1;
                    setUpOrDown = false;
                }
            }
            else
            {if(currentPos == targetPos)
                setUpOrDown = true;
            }
        }
        if (hitPlayer.collider == null)
        {
                passenger = null;
        }
    }

   public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.2f * speed * Time.deltaTime);
        if(passenger != null)
        {
            passenger.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
            passenger.GetComponent<PlayerScript>().targetPos = new Vector2(transform.position.x, transform.position.y + 1.5f);
        }
    }
}
