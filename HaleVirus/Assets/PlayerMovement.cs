using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    float horizontalMove = 0f;

    public Rigidbody2D rb;

    public float speed = 40f;
    bool jump = false;
    public Camera cam;

    Vector2 mousePos;
    Collider2D playerCol;
    Collider2D waterCol;
    int inCollision = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Door") && GameObject.Find("doorClosed").GetComponent<SpriteRenderer>().sprite.name == "doorOpen")
        {

            gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector2(-11f, 1.48f);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder") && Input.GetKey(KeyCode.W))
        {
            Debug.Log("sobe");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6f);
            
        }

        else if (collision.CompareTag("Ladder") && Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6f);
        }
    }

    // Update is called once per frame
    void Update()
    {


        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }


    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }
}
