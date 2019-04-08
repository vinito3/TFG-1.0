using UnityEngine;
using Photon.Pun;


public class PlayerAnimationManager : MonoBehaviourPun
{
    private Animator anim;
    public float maxSpeed = 10f;
    bool facingRight = true;
    public float horizontalInput;
    private Rigidbody2D rb2d;
    public float velX;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("disparoTrue", RpcTarget.All, null);
                
                Debug.Log("space");

            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                photonView.RPC("disparoFalse", RpcTarget.All, null);
                Debug.Log("space");
            }

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                photonView.RPC("saltoTrue", RpcTarget.All, null);

                Debug.Log("saltando");

            }

            if (Input.GetKeyUp(KeyCode.Insert))
            {
                photonView.RPC("saltoFalse", RpcTarget.All, null);
                Debug.Log("no saltando");
            }


            horizontalInput = Input.GetAxisRaw("Horizontal");
            velX = horizontalInput * maxSpeed * Time.deltaTime;
            
            if (velX > 0 || velX < 0)
            {
                Debug.Log("velocidad de x dwentro de if: " + velX);
                photonView.RPC("correrTrue", RpcTarget.All, null);
            }
            else
            {
                Debug.Log("velocidad de x dwentro de else: " + velX);
                photonView.RPC("correrFalse", RpcTarget.All, null);
            }

               
            
           
            if (horizontalInput > 0 && !facingRight)
                Flip();
            else if (horizontalInput < 0 && facingRight)
                Flip();

        }   
    }

    private void FixedUpdate()
    {    
        

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.lossyScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    [PunRPC]
    void disparoTrue() {

        anim.SetBool("isShotting", true);
    }

    [PunRPC]
    void disparoFalse()
    {

        anim.SetBool("isShotting", false);
    }

    [PunRPC]
    void correrTrue()
    {

        anim.SetBool("isRunning", true);
    }

    [PunRPC]
    void correrFalse()
    {
        Debug.Log("is running a false " );
        anim.SetBool("isRunning", false);
    }

    [PunRPC]
    void saltoTrue()
    {

        anim.SetBool("isJumping", true);
    }

    [PunRPC]
    void saltoFalse()
    {

        anim.SetBool("isJumping", false);
    }

}
