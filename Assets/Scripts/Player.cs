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
    private bool FacingRight = true;
    
    public GameObject Go;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody2D rb2d;

    public static List<Color> playerColors;

    // Start is called before the first frame update
    void Start() {

        rb2d = GetComponent<Rigidbody2D>();

        if (!photonView.IsMine)
            return;
        
        photonView.RPC("SetRandomColor", RpcTarget.All, null);
        
    }

    // Update is called once per frame
    void Update() {

        if (!photonView.IsMine)
            return;
        
        

        if (photonView.IsMine) {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Fire1")) {
                //GameObject Go = PhotonNetwork.Instantiate(Projectil.name, bullet_transform.transform.position, Quaternion.identity, 0

                Go = PhotonNetwork.Instantiate(Projectil.name, bullet_transform.position, bullet_transform.rotation);
                Go.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right * projectil_speed);
                Debug.Log("Socorro");

                //Go.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.forward * projectil_speed);
                //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }

    }

    private void FixedUpdate() {

        if (horizontalInput < 0 && FacingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && !FacingRight) {
            Flip();
        }

        if (PauseManager.isPaused) {
            rb2d.velocity = Vector2.zero;
            return;
        }
        
        float velX = horizontalInput * speed * Time.deltaTime;
        float velY = verticalInput * speed * Time.deltaTime;
        Vector2 velocity = new Vector2(velX, velY);
        rb2d.velocity = velocity;
    }

    public static void RefreshPlayerInstance(ref Player player, Player prefab) {

        var position = Vector3.zero;
        var rotation = Quaternion.identity;

        if (player != null) {
            position = player.transform.position;    
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<Player>();
    }

    [PunRPC]
    public void SetRandomColor() { 
        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }

    private void Flip()
    {
        FacingRight = !FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada");
        Destroy(gameObject);
    }

}
