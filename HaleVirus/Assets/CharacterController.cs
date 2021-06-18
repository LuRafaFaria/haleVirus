using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private float jumpforce = 100f;
    [SerializeField] public Vector3 vel = Vector3.zero;
    [SerializeField] public LayerMask groundLayers;
    [SerializeField] public Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;

    bool inGround;
    bool wasInGround;
    Rigidbody2D rigidbody2D;
    bool right = true;
    float groundRadius = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //atualizar o estado anterior
        wasInGround = inGround;
        inGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, groundLayers);

        //verificar os colliders
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                inGround = true;
            }
        }

    }

    public void Move(float move, bool jump)
    {
        if (inGround)
        {   //
            //Vector3 tVel = new Vector2(move * 10f, rigidbody2D.velocity.y);
            //rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, tVel, ref vel, 0.05f);
            //
            //if (move > 0 && right == false)
            //{
            //    Flip();
            //}
            //else if (move < 0 && right)
            //{
            //    Flip();
            //}


        }
        Vector3 tVel = new Vector2(move * 10f, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, tVel, ref vel, 0.05f);

        if (move > 0 && right == false)
        {
            Flip();
        }
        else if (move < 0 && right)
        {
            Flip();
        }

        if (inGround && jump)
        {
            inGround = false;
            rigidbody2D.AddForce(new Vector2(0f, jumpforce));
        }
    }

    void Flip()
    {
        right = !right;

        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }
}
