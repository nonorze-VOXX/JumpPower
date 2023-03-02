using Player;
using Player.Camera;
using UnityEngine;

public class player_ani : MonoBehaviour
{
    public PlayerData playerData;
    public CameraData cameraData;
    public SpriteRenderer playerSR;
    public Animator playerAni;
    public GameObject gravity;
    public GameObject endText;
    public GameObject speedLine;
    public GameObject lightup;
    public GameObject playerLine;
    public GameObject lightdown;
    public GameObject fuse;
    public GameObject cameraButton;
    public GameObject map;
    private bool isEnd;

    private Rigidbody2D playerRig;

    // Start is called before the first frame update
    private void Start()
    {
        isEnd = false;
        playerRig = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isEnd == false)
        {
            if (Input.GetKey(KeyCode.A))
                if (playerSR.flipX == false && playerData.status != Status.Jumping)
                    playerSR.flipX = true;
            if (Input.GetKey(KeyCode.D))
                if (playerSR.flipX && playerData.status != Status.Jumping)
                    playerSR.flipX = false;

            if (playerData.status == Status.Jumping)
            {
                if ((playerRig.velocity.y > 0.001 && playerData.gravityDirection.y == -1) ||
                    (playerRig.velocity.y < -0.001 && playerData.gravityDirection.y == 1) ||
                    (playerRig.velocity.x > 0.001 && playerData.gravityDirection.x == -1) ||
                    (playerRig.velocity.x < -0.001 && playerData.gravityDirection.x == 1))
                    playerAni.SetInteger("state", 3);
                else if ((playerRig.velocity.y > 0.001 && playerData.gravityDirection.y == 1) ||
                         (playerRig.velocity.y < -0.001 && playerData.gravityDirection.y == -1) ||
                         (playerRig.velocity.x > 0.001 && playerData.gravityDirection.x == 1) ||
                         (playerRig.velocity.x < -0.001 && playerData.gravityDirection.x == -1))
                    playerAni.SetInteger("state", 4);
            }
            else
            {
                if (playerData.powerTime != 0) //Input.GetKey(KeyCode.W))
                    playerAni.SetInteger("state", 2);
                else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl))
                    playerAni.SetInteger("state", 0);
                else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && Input.GetKey(KeyCode.LeftControl))
                    playerAni.SetInteger("state", 1);
                else
                    playerAni.SetInteger("state", 0);
            }
        }
    }

    public void issEnd()
    {
        isEnd = true;
        fuse.gameObject.SetActive(false);
        gravity.SetActive(false);
        playerData.gravityDirection = Vector2.down;
        playerAni.SetInteger("state", 5);

        cameraButton.SetActive(false);

        lightup.SetActive(false);
        lightdown.SetActive(true);
        Invoke("gameOver", 4);
    }

    private void gameOver()
    {
        cameraData.CameraStatus = CameraStatus.GameEnd;
        map.SetActive(false);
        speedLine.SetActive(true);
        playerLine.SetActive(false);
        Invoke("GameEndding", 5);
    }

    private void GameEndding()
    {
        endText.SetActive(true);
        playerData.gravityDirection = transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}