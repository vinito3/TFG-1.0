using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //Método que detecta si la bala ha tenido alguna colisión
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Destruimos la bala en caso de que detecte alguna colisión
        Destroy(gameObject);
    }
}
