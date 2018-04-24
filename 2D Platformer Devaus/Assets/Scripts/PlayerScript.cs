using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Use this for initialization
    public enum MovePhase { Plan, InAir, EndTurn, executing, waiting };
    public MovePhase state;
    public GameObject controller;
    public LayerMask groundLayer;
    public Vector2 target;
    public float speed;
    public bool isGrounded;
    public bool done;
    public int airtime;
    public int suunta;
    public int playerIndex;
    public Vector2 Liikkuvuus;
    public TurnControl turnCRTL;
    void Start()
    {
        speed = 5;
        controller = GameObject.FindGameObjectWithTag("GameController");
        turnCRTL = controller.GetComponent<TurnControl>();
        turnCRTL.units.Add(gameObject);
        playerIndex = turnCRTL.units.IndexOf(gameObject);

        Liikkuvuus = Vector2.zero;

        groundLayer = LayerMask.GetMask("Ground");
        suunta = 1;
        target = transform.position;
        airtime = -1;


    }

    public void TurnStart(int action)
    {
        state = MovePhase.executing;
        if(action == 0){
            state = MovePhase.EndTurn;
            Debug.Log("waited turn");
        }
        
        else if (action <= 2)
        {
            moveUnit(suunta, action);
        }
        
        if (action == 3)
        {
            Flip();
            state = MovePhase.EndTurn;
        }
        if (action == 4)
        {
            jump(Vector2.up * 2 + Vector2.right * suunta);
        }
        if (action == 5)
        {
            crouch();
            state = MovePhase.EndTurn;
        }
        if (action == 6)
        {
            jump(Vector2.up * 2 + Vector2.right * suunta);
        }
        if (action == 7)
        {
            jump(Vector2.up * 1 + Vector2.right * suunta * 2);
        }
        if(action == 8)
        {
            shoot(transform.position);
            state = MovePhase.EndTurn;
        }

    }
    void turnEnd()
    {
        if (turnCRTL.exec == true)
        {
            turnCRTL.SendAction();
        }
        Debug.Log("TurnEnded "+ playerIndex);

        state = MovePhase.waiting;

    }

    void Update()
    {
        if (playerIndex != turnCRTL.currentplayer)
        {

            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<buttonActive>().buttonState(false);
            }
        }
        else if(state == MovePhase.Plan)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<buttonActive>().buttonState(true);
            }

        }


    }
    void shoot(Vector2 pos)
    {
        //ampumisen koodi
    }
    
    void crouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.5f, 1);
    }
    void FixedUpdate()
    {
        isGrounded = CollisionCheck(transform.position, Vector2.down, 1, groundLayer);

        if (state == MovePhase.executing || state == MovePhase.InAir)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 0.01f*speed);
        }
        if (state == MovePhase.executing && (Vector2)transform.position == target && isGrounded)
        {
            state = MovePhase.EndTurn;
        }
        if (state == MovePhase.executing && (Vector2)transform.position == target && !isGrounded)
        {
            state = MovePhase.InAir;
            gravity(Liikkuvuus);
        }
        if (state == MovePhase.InAir && isGrounded)
        {
            GoToGrid();
            airtime = -1;
            state = MovePhase.EndTurn;
        }
        if (state == MovePhase.InAir && !isGrounded && (Vector2)transform.position == target)
        {
            gravity(Liikkuvuus);
        }
        if (state == MovePhase.EndTurn)
        {
            turnEnd();
        }
        if (state == MovePhase.waiting && !turnCRTL.exec)
        {
            state = MovePhase.Plan;
        }
        /*
        if(!isGrounded&&(Vector2)transform.position == target){
            gravity(Liikkuvuus);
            state = MovePhase.InAir;
        }
       
        if (state == MovePhase.InAir && isGrounded)
        {
            GoToGrid();
            airtime = -1;
            done = true;
        }
        if(state == MovePhase.executing&&(Vector2) transform.position == target){
            state = MovePhase.EndTurn;
        }
        */
        //Debug.Log(state+", "+playerIndex);
    }
    void gravity(Vector2 vel)
    {
        if (airtime < vel.x)
        {
            airtime++;
        }
        if (vel != Vector2.zero)
        {
            target += new Vector2(vel.x + (airtime * -suunta), -vel.y);
        }
        else
        {
            target += Vector2.down;
        }



    }
    void jump(Vector2 vel)
    {
        Liikkuvuus = vel;
        target = transform.position + (Vector3)Liikkuvuus;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            GoToGrid();
        }
    }
    bool CollisionCheck(Vector2 pos, Vector2 dir, float distance, LayerMask kerros)
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
        target = new Vector2(transform.position.x + howMany * direction, transform.position.y);
    }

    void GoToGrid()
    {

        transform.position= new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }
}
