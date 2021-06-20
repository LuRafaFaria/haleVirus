using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    public float timer = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("botao").GetComponent<SpriteRenderer>().sprite.name == "botaoPress")
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SetTimer();
        }
        
    }

    public void SetTimer()
    {
        timer = 3f;
    }
}
