using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // phases for the player
     public List<int> NoMoves = new List<int>();
    public enum MovePhase { Plan, InAir, EndTurn, executing, waiting, shooting };
    public MovePhase state;
    //important variables
    public GameObject controller;
    public LayerMask groundLayer;
    public LayerMask DeathLayer;
    public Animator anim;
    public Vector2 target;
    public float speed;
    public bool isGrounded;
    public bool done;
    public int airtime;
    public int suunta;
    public int playerIndex;
    public Vector2 Liikkuvuus;
    public TurnControl turnCRTL;
    public bool CanMove;
    public bool IsDead;
    public GameObject ammoPrefab;
    public float shootingRange = 8;
    public int actionLocal;
    public GameObject cameraFollow;

    void Start()
    {
        //Initialize all critical variables
        speed = 5;
        controller = GameObject.FindGameObjectWithTag("GameController");
        turnCRTL = controller.GetComponent<TurnControl>();
        anim = GetComponent<Animator>();
        turnCRTL.units.Add(gameObject);
        //playerIndex = turnCRTL.units.IndexOf(gameObject);
        DeathLayer = LayerMask.GetMask("DeathLayer");
        Liikkuvuus = Vector2.zero;
        IsDead = false;
        groundLayer = LayerMask.GetMask("Ground");
        suunta = 1;
        target = transform.position;
        airtime = -1;
        cameraFollow = GameObject.FindGameObjectWithTag("CameraFollow");

    }
    //Tells the player to start the turn and what to do
    public void TurnStart(int action)
    {
        state = MovePhase.executing;
        //if no action given, wait a turn
        if (action == 0)
        {
            state = MovePhase.EndTurn;
            //Debug.Log("waited turn");
        }

        else if (action <= 2)
        {
            //move unit 1 or 2 squares
            moveUnit(suunta, action);
            
        }

        if (action == 3)
        {
            //flip the player and end turn
            Flip();
            state = MovePhase.EndTurn;
        }
        if (action == 4)
        {
            //Starts the jump
            jump(Vector2.up * 3);
        }
        if (action == 5)
        {
            //starts a different jump
            jump(Vector2.up * 2 + Vector2.right * suunta);
        }
        if (action == 6)
        {
            jump(Vector2.up * 1 + Vector2.right * suunta * 2);
        }
        if (action == 7)
        {
            //Makes the player Crouch
            crouch();
            state = MovePhase.EndTurn;
        }
        if (action == 8)
        {
            //starts the shooting function
            //TODO: implement shooting and move ending turn to the ammo
            
                state = MovePhase.shooting;
                shoot(suunta);  
        }
        actionLocal = action;
    }
    //Handles all the turn logic
    void TurnLogic()
    {
        isGrounded = CollisionCheck(transform.position, Vector2.down, 1, groundLayer);
        CanMove = !CollisionCheck(transform.position - new Vector3(0, 0.25f, 0), Vector2.right * suunta, 0.5f, groundLayer) && !CollisionCheck(transform.position + new Vector3(0, 0.25f, 0), Vector2.right * suunta, 0.5f, groundLayer);
        IsDead = CollisionCheck(transform.position, Vector2.down, 1, DeathLayer);
        //Debug.Log("CanMove: "+ CanMove+ " Player: "+ playerIndex);
        if (IsDead)
        {
            DIE();
        }
        if (state == MovePhase.executing || state == MovePhase.InAir)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 0.2f * speed * Time.deltaTime);
        }
        if(state == MovePhase.executing && actionLocal <= 2)
        {
            speed = 10;
            anim.SetBool("Walk", true);
        }
        if (state == MovePhase.executing && actionLocal > 3 && actionLocal < 7)
        {
            speed = 30;
            anim.SetBool("Jump", true);
        }
        if (state != MovePhase.executing)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Jump", false);
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
        if (!CanMove)
        {
            GoToGrid();
            state = MovePhase.EndTurn;
        }
        if (state == MovePhase.EndTurn)
        {
            turnEnd();
        }
        if (state == MovePhase.waiting && !turnCRTL.exec)
        {
            state = MovePhase.Plan;
        }
    }
    //ENds turn and sends action to next player if needed
    void turnEnd()
    {
        //If execution is on, send action to the next player
        if (turnCRTL.exec == true)
        {
            turnCRTL.SendAction();
        }
       // Debug.Log("TurnEnded " + playerIndex);
        //sets self to wait
        state = MovePhase.waiting;

    }

    void Update()
    {
        //Handles activation and deactivation of buttons when player is inactive
        if (playerIndex != turnCRTL.currentplayer)
        {

            foreach (Transform child in transform)
            {
                if (child.tag == "ButtonControl")
                {
                    child.gameObject.GetComponent<buttonActive>().buttonState(false);
                }

            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "ButtonControl")
                {
                    child.gameObject.GetComponent<buttonActive>().buttonState(true);
                    cameraFollow.transform.position = transform.position;
                }
            }

        }


    }
    public void DIE()
    {
        state = MovePhase.EndTurn;
        turnCRTL.targets.Remove(transform);
        Debug.Log("DIED");
        turnCRTL.units.Remove(gameObject);
        turnCRTL.SortByIndex(playerIndex);
        turnCRTL.SortList();
        GameObject.Destroy(gameObject);
    }
    void shoot(int dir)
    {
        GameObject ammoCreation = Instantiate(ammoPrefab, new Vector2(transform.position.x +
        dir,
        transform.position.y + 0.5f), Quaternion.identity);
        ammoCreation.GetComponent<Ammo>().direction = dir;
        ammoCreation.GetComponent<Ammo>().startPos = transform.position;
        ammoCreation.GetComponent<Ammo>().targetPos = new Vector2(transform.position.x +
        shootingRange * dir, transform.position.y);
        ammoCreation.GetComponent<Ammo>().shooter = gameObject.GetComponent<PlayerScript>();
    }

    void crouch()
    {
        anim.SetInteger("CrouchControl", 1);
        //Courching code TODO: Implement crouching
        //transform.localScale = new Vector3(transform.localScale.x, 0.5f, 1);
    }
    void FixedUpdate()
    {
        TurnLogic();




    }
    //Handles gravity 
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
    //handles a jump, input is the velocity you want the character to jump with
    void jump(Vector2 vel)
    {
        Liikkuvuus = vel;
        target = transform.position + (Vector3)Liikkuvuus;

    }

    //Kutsutaan vain animaatiosta käsin. CrouchControl Arvolla 1 saadaan hahmo kyykkäämään,
    //arvolla 3 nousemaan ylös.
    public void IntoCrouch()
    {
        anim.SetInteger("CrouchControl", 2);
    }
    public void FromCrouch()
    {
        anim.SetInteger("CrouchControl", 4);
    }

    //I think this does nothing ATM
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            GoToGrid();
        }
        /*if (collision.gameObject.tag == "Ammo1")
        {
            DIE();
        }*/
    }
    //Checks collision at a position, to a direction, with a distance and only checks for a certain layer
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
    //Flips the player
    void Flip()
    {
        suunta = suunta * -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //Moves the players target on the  X axis with a direction of left or right and a distance
    void moveUnit(int direction, int howMany)
    {
        target = new Vector2(transform.position.x + howMany * direction, transform.position.y);
    }
    //Snaps the unit to grid
    void GoToGrid()
    {

        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    }
}
