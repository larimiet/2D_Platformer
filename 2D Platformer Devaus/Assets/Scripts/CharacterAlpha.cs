using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lisään testi kommentin.
public class CharacterAlpha : MonoBehaviour
{   //Tämä scripti voidaan asettaa hahmolle pelikentällä.
    public bool groundBelow;
    public Camera cam;
    public GameObject controlObject;
    public GameObject cameraFollow;
    public DetectControl DetectCollisions;
    public Control control;

    public float speed = 2f;
    public bool performAction;
    public bool performFallDown = false;
    public int action = 0;
    public bool setDestination = true;
    public Vector2 targetPos;
    public Vector2 startPos;
    public Vector2 jumpHighPoint;
    public int jumpPhase;
    public bool crouched;
    public float compX = 2;
    public float compY = 1;
    public int numberOfSteps;

    public float shootingRange = 8;
    public GameObject ammoPrefab;
    public int shooting = 0;

    public int side = 1;
    public int number = 0;
    public bool selected;

    //public float xMovementInJump;

    //actions: 0=nothing, 1=walk_forward, 2=jump, 3=turn around , 4=shoot, 5=crouch

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
        (transform.position.x, transform.position.y - control.gridSize /*- 0.05f*/, transform.position.z));

        if (side == 1)
        {
            if(control.gamePhase != 1)
            {
                selected = false;
            }
            if (Input.GetKeyDown("1") && number == 1 && control.gamePhase == 1)
            {
                GameObject[] allside1Robots = GameObject.FindGameObjectsWithTag("Robo1");

                foreach (GameObject ConnectorCube in allside1Robots)
                {
                    ConnectorCube.GetComponent<CharacterAlpha>().selected = false;
                }

                selected = true;
                cameraFollow.transform.position = transform.position;
            }
            if (Input.GetKeyDown("2") && number == 2 && control.gamePhase == 1)
            {
                GameObject[] allside1Robots = GameObject.FindGameObjectsWithTag("Robo1");

                foreach (GameObject ConnectorCube in allside1Robots)
                {
                    ConnectorCube.GetComponent<CharacterAlpha>().selected = false;
                }
                selected = true;
                cameraFollow.transform.position = transform.position;
            }
            if (Input.GetKeyDown("3") && number == 3 && control.gamePhase == 1)
            {
                GameObject[] allside1Robots = GameObject.FindGameObjectsWithTag("Robo1");

                foreach (GameObject ConnectorCube in allside1Robots)
                {
                    ConnectorCube.GetComponent<CharacterAlpha>().selected = false;
                }
                selected = true;
                cameraFollow.transform.position = transform.position;
            }
        }

        if (side == 2)
        {
            if (control.gamePhase != 2)
            {
                selected = false;
            }
            if (Input.GetKeyDown("1") && number == 1 && control.gamePhase == 2)
            {
                GameObject[] allside2Robots = GameObject.FindGameObjectsWithTag("Robo2");

                foreach (GameObject ConnectorCube in allside2Robots)
                {
                    ConnectorCube.GetComponent<CharacterAlpha>().selected = false;
                }

                selected = true;
                cameraFollow.transform.position = transform.position;
            }
            if (Input.GetKeyDown("2") && number == 2 && control.gamePhase == 2)
            {
                GameObject[] allside2Robots = GameObject.FindGameObjectsWithTag("Robo2");

                foreach (GameObject ConnectorCube in allside2Robots)
                {
                    ConnectorCube.GetComponent<CharacterAlpha>().selected = false;
                }
                selected = true;
                cameraFollow.transform.position = transform.position;
            }
            if (Input.GetKeyDown("3") && number == 3 && control.gamePhase == 2)
            {
                GameObject[] allside2Robots = GameObject.FindGameObjectsWithTag("Robo2");

                foreach (GameObject ConnectorCube in allside2Robots)
                {
                    ConnectorCube.GetComponent<CharacterAlpha>().selected = false;
                }
                selected = true;
                cameraFollow.transform.position = transform.position;
            }
        }

        if (performAction)
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
        if (action == 0 && groundBelow == false && !performFallDown)
        {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,
                transform.position.y - control.gridSize - 0.05f), -transform.up);
            if (hit.collider != null)
            {
                targetPos = (new Vector2(hit.collider.transform.position.x,
                hit.collider.transform.position.y + control.gridSize * 1.5f));
            }
            else
            {
                targetPos = new Vector2(0, -1000f);
            }
                performFallDown = true;
        }

        if (action == 0 && groundBelow == true && !performFallDown && numberOfSteps > 0 && performAction)
        {
            action = 1;
            setDestination = true;
            performAction = true;
        }

            if (action == 4)
        {
            if (setDestination && shooting == 0)
            {
                startPos = transform.position;
                targetPos = new Vector2(transform.position.x
                + control.gridSize * shootingRange * transform.localScale.x, transform.position.y);
                setDestination = false;
                shooting = 1;
            }
           Shoot(speed, transform.localScale.x);
        }
        if (action == 5)
        {
            Crouch();
        }

        if (performFallDown == true)
        {
            FallDown(speed * 3);
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
            numberOfSteps -= 1;
            action = 0;
            if(numberOfSteps == 0)
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
                speed * Time.deltaTime * compY));
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
                speed * Time.deltaTime * -compY));
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

    public void Crouch()
    {
        if(crouched && performAction && transform.localScale.y == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, 1);
            transform.Translate(new Vector3(0, - 0.5f, 0));
            performAction = false;
        }
        if (!crouched && performAction && transform.localScale.y == 0.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            transform.Translate(new Vector3(0, 0.5f, 0));
            performAction = false;
        }
    }

    public void Shoot(float spd, float direction)
    {
        if (shooting == 1)
        {
            GameObject ammoCreation = Instantiate(ammoPrefab, new Vector2(transform.position.x + transform.localScale.x * control.gridSize,
            transform.position.y + control.gridSize / 2), Quaternion.identity);
            ammoCreation.GetComponent<Ammo>().direction = transform.localScale.x;
            ammoCreation.GetComponent<Ammo>().startPos = transform.position;
            ammoCreation.GetComponent<Ammo>().targetPos = new Vector2(transform.position.x + control.gridSize
            * shootingRange * transform.localScale.x, transform.position.y);
            shooting = 0;
            action = 0;
            performAction = false;
        }
    }

    public void FallDown(float spd)
    {
        if (transform.position.y < targetPos.y - speed / 20 || transform.position.y > targetPos.y + speed / 20)
        {
            transform.Translate(new Vector2(0, -spd * Time.deltaTime));
        }
        else
        {
            transform.position = targetPos;
            action = 0;
            performFallDown = false;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ammo1") && side == 2)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == ("Ammo2") && side == 1)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
