using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lisään testi kommentin.
public class Character : MonoBehaviour
{   //Tämä scripti voidaan asettaa hahmolle pelikentällä.
    public bool groundBelow;
    public Camera cam;
    public GameObject controlObject;
    public DetectControl DetectCollisions;
    public Control control;

    public float speed = 30f;
    public bool performAction;
    public string action = ("nothing");
    public bool setDestination = true;
    public Vector2 targetPos;
    public Vector2 startPos;
    public Vector2 jumpHighPoint;
    public float xMovementInJump;

    //actions: 0=nothing, 1=walk_forward, 2=jump, 3=turn around 

    // Use this for initialization
    void Start ()
    {
        DetectCollisions = cam.GetComponent<DetectControl>();
        control = controlObject.GetComponent<Control>();
        targetPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        groundBelow = DetectCollisions.DetectCollisionAtPosition(new Vector3
        (transform.position.x, transform.position.y - control.gridSize - 0.05f, transform.position.z));
        
        if(performAction)
        {
            if (action == ("walk_forward"))
            {
                if (setDestination)
                {
                    startPos = transform.position;
                    targetPos = new Vector2(transform.position.x
                    + control.gridSize * transform.localScale.x, transform.position.y);
                    setDestination = false;
                }
                MoveOneStep(speed, transform.localScale.x);
            }
            if (action == ("jump"))
            {
                if (setDestination)
                {
                    startPos = transform.position;
                    targetPos = new Vector2(transform.position.x
                    + control.gridSize * 2 * transform.localScale.x, transform.position.y);
                    jumpHighPoint = new Vector2(transform.position.x
                    + control.gridSize * transform.localScale.x, transform.position.y);
                    setDestination = false;
                }
                Jump(speed, transform.localScale.x);
            }
            if (action == ("turn_around"))
            {
                Turn180();
            }
        }
    }

    public void MoveOneStep(float spd, float direction)
    {
        if (transform.position.x < targetPos.x - speed / 20 || transform.position.x > targetPos.x + speed / 20)
        {
            transform.Translate(new Vector2 (speed * Time.deltaTime * direction, 0));
        }
        else
        {
            transform.position = targetPos;
            action = ("nothing");
            performAction = false;
        }
    }

    public void Jump(float spd, float direction)
    {
        
        if (transform.position.x < targetPos.x - speed / 20 || transform.position.x > targetPos.x + speed / 20)
        {
            xMovementInJump += speed * 2 * Time.deltaTime * direction;
            transform.position = new Vector2(startPos.x + xMovementInJump, startPos.y + control.gridSize * 2);
        }
        else
        {
            transform.position = targetPos;
            xMovementInJump = 0;
            action = ("nothing");
            performAction = false;
        }
    }
    public void Turn180()
    {
        transform.localScale = new Vector2(transform.localScale.x * (-1), 1);
        action = ("nothing");
        performAction = false;
    }
}
