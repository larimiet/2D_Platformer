using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Use this for initialization
    public int MovePhase;
    public GameObject controller;
    public LayerMask groundLayer;
    public Vector2 target;
    public bool isGrounded;
    public int airtime;
    public int suunta;
    public Vector2 Liikkuvuus;
    void Start()
    {
        Liikkuvuus = Vector2.zero;
        MovePhase = 0;
        groundLayer = LayerMask.GetMask("Ground");
        suunta = 1;
        target = transform.position;
        airtime = -1;
        if (GroundCheck(transform.position, Vector2.down, 1, groundLayer))
        {
            MovePhase = 2;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            suunta = 1;
            TurnStart(1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            suunta = -1;
            TurnStart(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            TurnStart(6);
        }
        
    }

    void TurnStart(int action)
    {
        MovePhase = 1;
        if (action >= 2)
        {
            moveUnit(suunta, action);
        }
        if (action == 3)
        {
            Flip();
        }
        if (action == 4)
        {
            jump(Vector2.up * 3);
        }
        if (action == 5)
        {
            crouch();
        }
        if (action == 6)
        {
            jump(Vector2.up * 2 + Vector2.right * suunta);
        }
        if (action == 7)
        {
            jump(Vector2.up * 1 + Vector2.right * suunta * 2);
        }

    }
    void turnEnd()
    {
        gravity(Liikkuvuus);

    }
    void crouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.5f, 1);
    }
    void FixedUpdate()
    {
        isGrounded = GroundCheck(transform.position, Vector2.down, 1, groundLayer);

        transform.position = Vector2.MoveTowards(transform.position, target, 0.05f);
        if (transform.position == (Vector3)target && MovePhase == 1)
        {
            MovePhase = 2;
            turnEnd();
        }
        if (!isGrounded)
        {

            gravity(Liikkuvuus);
        }
        if (transform.position == (Vector3)target && MovePhase == 3)
        {

            MovePhase = 0;
        }
        if (isGrounded&&MovePhase == 2)
        {
            GoToGrid();
        }



    }
    void gravity(Vector2 vel)
    {
        if (airtime < vel.x)
        {
            airtime++;
        }
		if(vel !=Vector2.zero){
			target += new Vector2(vel.x + (airtime * -suunta), -vel.y + airtime);
		}
        else{
			target += Vector2.down;
		}

        /* 
		if (!GroundCheck(transform.position, Vector2.down, 1, groundLayer))
        {
            target += new Vector2(vel.x,-vel.y);
			airtime++;
		}
		else 
        {
			target = transform.position;
			//MovePhase = 0;
			airtime= -1;
			Liikkuvuus = Vector2.zero;
		}
		*/

    }
    void jump(Vector2 vel)
    {
        Liikkuvuus = vel;
        target = transform.position + (Vector3)Liikkuvuus;

    }
    bool GroundCheck(Vector2 pos, Vector2 dir, float distance, LayerMask kerros)
    {


        if (Physics2D.Raycast(pos, dir, distance, kerros))
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    void Flip()
    {
        suunta = suunta * -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void moveUnit(int direction, int howMany)
    {
        target = transform.position + new Vector3(howMany * direction, 0, 0);
    }

    void GoToGrid()
    {
        MovePhase = 3;
        target = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }
}
