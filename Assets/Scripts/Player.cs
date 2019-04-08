using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class Player : MonoBehaviourPun {

    public float speed = 350f;
    public GameObject Projectil;
    public int projectil_speed = 200;
    public Transform bullet_transform;
    public GameObject Go;
    //public Bullet bullet;
    private float horizontalInput;
    private Rigidbody2D rb2d;
    public bool isHurt = false;
    private Animator anim;


    // Start is called before the first frame update
    void Start() {

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bullet_transform = GetComponent<Transform>();


        if (!photonView.IsMine)
            return;
        
       
        
    }

    // Update is called once per frame
    void Update() {

        if (!photonView.IsMine)
            return;      

        if (photonView.IsMine) {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            //bullet.Spawn();

            if (Input.GetButtonDown("Fire1")) {
               
                Go = PhotonNetwork.Instantiate(Projectil.name, bullet_transform.position, bullet_transform.rotation);
                Go.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right * projectil_speed);
                Debug.Log("Socorro");

            }
        }

    }

    private void FixedUpdate() {

        if (PauseManager.isPaused) {
            rb2d.velocity = Vector2.zero;
            return;
        }
        float velX = horizontalInput * speed * Time.deltaTime;      
        Vector2 velocity = new Vector2(velX, rb2d.velocity.y);
        rb2d.velocity = velocity;
    }

    public static void RefreshPlayerInstance(ref Player player, Player prefab) {

        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        var scale = Vector3.zero;

        if (player != null) {
            position = player.transform.position;    
            rotation = player.transform.rotation;
            scale = player.transform.lossyScale;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "balla")
        {
            photonView.RPC("hurt", RpcTarget.All, null);
            Debug.Log(collision.gameObject.tag);
        }
    }

    [PunRPC]
    void hurt()
    {
        anim.SetTrigger("isHurt");
    }

}
