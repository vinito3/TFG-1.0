using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject projectil;
    public int projectil_speed = 200;
    public Transform bullet_transform;
    public Bullet Go;


    void Start()
    {

        

       

    }

    // Update is called once per frame
    public void Spawn()
    {

            if (Input.GetButtonDown("Fire1"))
            {

                Go = PhotonNetwork.Instantiate(projectil.name, bullet_transform.position, bullet_transform.rotation).GetComponent<Bullet>();
                Go.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right * projectil_speed);
                Debug.Log("Socorro");

            }
        

    }

    //Método que detecta si la bala ha tenido alguna colisión
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Destruimos la bala en caso de que detecte alguna colisión
        Destroy(gameObject);
    }
}
