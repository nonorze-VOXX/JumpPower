using Player;
using Unity.Mathematics;
using UnityEngine;

public class player_ani : MonoBehaviour
{
    public PlayerData playerData;
    public SpriteRenderer playerSR;
    public Animator playerAni;
    private Rigidbody2D playerRig;
    private bool isSquat;
    // Start is called before the first frame update
    void Start()
    {
        isSquat = false;
        playerRig = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            if (playerSR.flipX == false && playerData.status == Status.Idle)
                playerSR.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (playerSR.flipX == true && playerData.status == Status.Idle)
                playerSR.flipX = false;
        }

        if (playerRig.velocity.y > 0 && playerAni.GetInteger("state") != 3)
        {
            playerAni.SetInteger("state", 3);
        }
        else if (playerRig.velocity.y < 0)
        {
            playerAni.SetInteger("state", 4);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            playerAni.SetInteger("state", 2);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            Debug.Log(playerRig.velocity.x);
            playerAni.SetInteger("state", 1);
        }
        else
        {
            playerAni.SetInteger("state", 0);
        }
    }

}
