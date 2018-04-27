using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public SpriteRenderer sRend;
    public Rigidbody2D rb2D;
    public float direction;
    public bool targetReached = false;
    public float speed = 10;
    public Vector2 startPos;
    public Vector2 targetPos;
    public PlayerScript shooter;
	// Use this for initialization
	void Start ()
    {
        sRend = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x < targetPos.x - speed / 20 || transform.position.x > targetPos.x + speed / 20)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime * direction, 0));
        }
        else
        {
            /*sRend.enabled = false;
            targetReached = true;
            transform.position = startPos;*/
            shooter.state = PlayerScript.MovePhase.EndTurn;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().DIE();
            shooter.state = PlayerScript.MovePhase.EndTurn;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            shooter.state = PlayerScript.MovePhase.EndTurn;
            Destroy(gameObject);
        }
    }
}
