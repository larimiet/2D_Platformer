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
    public int action = 0;
    public bool setDestination = true;
    public Vector2 targetPos;
    public Vector2 startPos;
    public Vector2 jumpHighPoint;
    public int jumpPhase;
    public float compX = 2;
    public float compY = 1;
    //public float xMovementInJump;

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
            if (action == 1)
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
            if (action == 2)
            {
                if (setDestination)
                {
                    startPos = transform.position;
                    targetPos = new Vector2(transform.position.x
                    + control.gridSize * compX * 2 * transform.localScale.x, transform.position.y);
                    jumpHighPoint = new Vector2(transform.position.x
                    + control.gridSize * compX * transform.localScale.x, transform.position.y + 
                    control.gridSize * compY);
                    setDestination = false;
                    jumpPhase = 1;
                }
                Jump(speed, transform.localScale.x);
            }
            if (action == 3)
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
            action = 0;
            performAction = false;
        }
    }

    public void Jump(float spd, float direction)
    {
        if (jumpPhase == 1)
        {
            if (transform.position.x < jumpHighPoint.x - speed / 20 || transform.position.x > jumpHighPoint.x + speed / 20 ||
                transform.position.y < jumpHighPoint.y - speed / 20 || transform.position.y > jumpHighPoint.y + speed / 20)
            {
                transform.Translate(new Vector2(speed * Time.deltaTime * direction * compX,
                speed * Time.deltaTime * direction * compY));
            }
            else
            {
                transform.position = jumpHighPoint;
            }
        }

        if (jumpPhase == 2)
        {
            if (transform.position.x < targetPos.x - speed / 20 || transform.position.x > targetPos.x + speed / 20 ||
                transform.position.y < targetPos.y - speed / 20 || transform.position.y > targetPos.y + speed / 20)
            {
                transform.Translate(new Vector2(speed * Time.deltaTime * direction * compX,
                speed * Time.deltaTime * direction * -compY));
            }
            else
            {
                transform.position = targetPos;
                action = 0;
                performAction = false;
            }
        }
    }
    
    
    public void Turn180()
    {
        transform.localScale = new Vector2(transform.localScale.x * (-1), 1);
        action = 0;
        performAction = false;
    }
}
