using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{

    enum Estado { Clone, Fire, Ice, Eletric, ThroughWalls };
    Estado estado;

    public Sprite cDefault;
    public Sprite fire;
    public Sprite ice;
    public Sprite eletric;
    public Sprite goThroughWall;
    public Sprite FrozenWater;
    public Sprite Water;
    public Sprite Button;
    public Sprite PressedButton;
    public Sprite PressedButtonEletric;
    public Sprite DoorOpen;
    public Sprite DoorClose;
    float timer = 2f;
    float timerIce = 10f;
    float timerDoor = 3f;

    //public float offset;
    //public GameObject clone;
    //public GameObject dClone;
    //public Transform shotPoint;
    //CloneShoot cloneShoot;
    //
    //private float count;
    public float speed;
    public Rigidbody2D rb;
    private float desiredScale = 0.9f;
    Vector2 newPos;

    private bool frostedIce = false;
    private bool openDoor = false;
    bool isWall;

    Collider2D iceCol;
    Collider2D playerCol;
    Collider2D waterCol;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        estado = Estado.Clone;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = fire;
            transform.localScale = new Vector2(1.02f * desiredScale, 1.02f * desiredScale);
            estado = Estado.Fire;
        }

        else if (collision.CompareTag("Ice"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ice;
            transform.localScale = new Vector2(2f * desiredScale, 2f * desiredScale);
            estado = Estado.Ice;
        }
        else if (collision.CompareTag("GoThroughFat"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = goThroughWall;
            transform.localScale = new Vector2(1f * desiredScale, 1f * desiredScale);
            estado = Estado.ThroughWalls;
        }
        else if (collision.CompareTag("Eletric"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = eletric;
            transform.localScale = new Vector2(2.5f * desiredScale, 2.5f * desiredScale);
            estado = Estado.Eletric;
        }



    }

    //Ao entrar numa colisão chama isto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //Verifica o estado da bola
        switch (estado)
        {
            //Se for clone não faz nada
            case Estado.Clone:

                Physics2D.IgnoreLayerCollision(7, 10, false);

                if (collision.collider.CompareTag("StaticWall"))
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    //transform.position = new Vector2(0, -500f);


                }

                if (collision.collider.CompareTag("Button") && GameObject.Find("botao").GetComponent<SpriteRenderer>().sprite.name != "botaoPressEletric")
                {
                    openDoor = true;
                    timerDoor = 3f;

                    //botao
                    GameObject buttonPressed = GameObject.Find("botao");
                    buttonPressed.GetComponent<SpriteRenderer>().sprite = PressedButton;
                    transform.localScale = new Vector2(5.45f * desiredScale, 5.45f * desiredScale);

                    //clone
                    gameObject.GetComponent<SpriteRenderer>().sprite = cDefault;
                    transform.localScale = new Vector2(1f * desiredScale, 1f * desiredScale);

                    //porta
                    GameObject doorOpened = GameObject.Find("doorClosed");
                    doorOpened.GetComponent<SpriteRenderer>().sprite = DoorOpen;
                    transform.localScale = new Vector2(6.9f * desiredScale, 6.9f * desiredScale);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;

                    //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }


                break;
            //Se for fogo
            case Estado.Fire:

                //Se colidir com uma parede com a tag Wall
                if (collision.collider.CompareTag("Gordura"))
                {

                    timer = 2f;
                    //Vai buscar os componentes e desativa o collider e o objeto
                    //collision.collider.GetComponent<BoxCollider2D>().CompareTag("Gordura");

                    collision.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    collision.collider.gameObject.GetComponent<Collider2D>().enabled = false;


                    //Transforma no clone
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = cDefault;
                    transform.localScale = new Vector2(1.02f * desiredScale, 1.02f * desiredScale);
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    estado = Estado.Clone;



                }

                if (collision.collider.CompareTag("FrostedWater"))
                {

                    Physics2D.IgnoreLayerCollision(6, 9, true);
                    Physics2D.IgnoreLayerCollision(6, 11, true);
                    collision.collider.gameObject.GetComponent<SpriteRenderer>().sprite = Water;
                    transform.localScale = new Vector2(15.7f * desiredScale, 6.2f * desiredScale);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    timerIce = 50f;
                    frostedIce = false;
                    collision.collider.gameObject.tag = "Water";
                    
                }

                if (collision.collider.CompareTag("Water"))
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }

                if (collision.collider.CompareTag("StaticWall"))
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }

                break;
            case Estado.Ice:

                //gameObject.AddComponent<BoxCollider2D>();
                if (collision.collider.CompareTag("Water"))
                {
                    frostedIce = true;
                    timerIce = 50f;
                    Debug.Log("ENTROOU");
                    collision.collider.gameObject.GetComponent<SpriteRenderer>().sprite = FrozenWater;
                    transform.localScale = new Vector2(15.7f * desiredScale, 6.2f * desiredScale);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;

                    iceCol = collision.collider;

                    Physics2D.IgnoreLayerCollision(6, 9, false);
                    collision.collider.gameObject.tag = "FrostedWater";
                    Debug.Log(collision.collider.gameObject.tag);
                }
                break;
            case Estado.Eletric:
                if (collision.collider.CompareTag("Button"))
                {
                    openDoor = true;
                    timerDoor = 3000f;

                    collision.collider.gameObject.GetComponent<SpriteRenderer>().sprite = PressedButtonEletric;
                    transform.localScale = new Vector2(5.45f * desiredScale, 5.45f * desiredScale);

                    gameObject.GetComponent<SpriteRenderer>().sprite = cDefault;
                    transform.localScale = new Vector2(1f * desiredScale, 1f * desiredScale);

                    GameObject doorOpened = GameObject.Find("doorClosed");

                    doorOpened.GetComponent<SpriteRenderer>().sprite = DoorOpen;
                    transform.localScale = new Vector2(6.9f * desiredScale, 6.9f * desiredScale);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Collider2D>().enabled = false;

                }


                break;
            case Estado.ThroughWalls:
                Physics2D.IgnoreLayerCollision(7, 10, true);
                break;
            default:
                break;
        }
    }


    private void Update()
    {

        DefrostIce();
        OpenDoor();

        timer -= Time.deltaTime;
        timerIce -= Time.deltaTime;
        timerDoor -= Time.deltaTime;

        if (estado == Estado.ThroughWalls)
        {
            Physics2D.IgnoreLayerCollision(7, 10, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(7, 10, false);
        }
    }

    void OpenDoor()
    {

        Debug.Log("Entra no opendoor");
        if (openDoor && estado != Estado.Eletric)
        {
            Debug.Log("Não é eletrico");
            if (timerDoor > 0)
            {
                Debug.Log($"Porta ainda está aberta {timerDoor}");
                GameObject doorOpened = GameObject.Find("doorClosed");

                doorOpened.GetComponent<SpriteRenderer>().sprite = DoorOpen;
                transform.localScale = new Vector2(6.9f * desiredScale, 6.9f * desiredScale);

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            else
            {
                Debug.Log($"Porta está fechada {timerDoor}");
                GameObject doorOpened = GameObject.Find("doorClosed");

                doorOpened.GetComponent<SpriteRenderer>().sprite = DoorClose;
                transform.localScale = new Vector2(6.9f * desiredScale, 6.9f * desiredScale);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;


                GameObject buttonUnpressed = GameObject.Find("botao");

                buttonUnpressed.GetComponent<SpriteRenderer>().sprite = Button;
                transform.localScale = new Vector2(5.45f * desiredScale, 5.45f * desiredScale);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                timerDoor = 3f;
                openDoor = false;
            }
        }

        else
        {

        }
    }


    void DefrostIce()
    {

        if (frostedIce)
        {
            Debug.Log(timerIce);

            if (timerIce > 0)
            {
                Physics2D.IgnoreLayerCollision(6, 9, false);
                //iceCol.gameObject.SetActive(false);

            }
            else
            {

                Physics2D.IgnoreLayerCollision(6, 9, true);
                Physics2D.IgnoreLayerCollision(6, 11, true);
                iceCol.gameObject.GetComponent<SpriteRenderer>().sprite = Water;
                transform.localScale = new Vector2(15.7f * desiredScale, 6.2f * desiredScale);

                timerIce = 50f;
                frostedIce = false;
                iceCol.gameObject.tag = "Water";

            }
        }
    }

}
