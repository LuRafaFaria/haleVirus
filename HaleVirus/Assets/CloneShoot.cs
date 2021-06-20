using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneShoot : MonoBehaviour
{

    public Transform shootPoint;
    public GameObject clonePrefab;
    public float cloneForce = 20f;
    private int cloneCount = 0;
    private GameObject clone;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootClone();
        }
    }

    void ShootClone()
    {
        
        if (cloneCount == 0)
        {
            clone = Instantiate(clonePrefab, shootPoint.position, shootPoint.rotation);
            cloneCount++;
        }
        //Se já tiver clone destroi o anterior e cria um novo
        else
        {
            Destroy(clone.gameObject);
            clone = Instantiate(clonePrefab, shootPoint.position, shootPoint.rotation);

            
        }
        

    }
}
