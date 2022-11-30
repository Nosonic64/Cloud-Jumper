using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 30f;
    [SerializeField]
    private float movementSpeed;
    private float maxSpeed = 8.5f;
    private float jumpSpeed = 20f;
    private float lastInputDir;
    private GameObject lastPlat;
    public GameObject fallPlat;
    private int playerLives;
    private Rigidbody rb;
    private float horizontal;
    public float wrapAroundCheck;
    private float distToGround;
    public MeshRenderer meshRenderer;
    private Vector3 lastStablePos;
    private CapsuleCollider collider;
    private bool hasBell;
    public GameObject gameOverUI;
    public Material defaultMat;
    private float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        wrapAroundCheck = 1f;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        meshRenderer = GetComponent<MeshRenderer>();
        playerLives = 2;
        PlayerInfo.playerLives = playerLives;
        PlayerInfo.retryCount = 1;
    }

    private void OnEnable()
    {
        if (PlayerInfo.respawn)
        {
            PlayerInfo.playerLives = playerLives;
            PlayerInfo.gameOver = false;
            transform.position = new Vector3(8, 10, 0);
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
            Instantiate(fallPlat, transform.position - new Vector3(2, 3, 0), transform.rotation);
            rb.isKinematic = false;
            PlayerInfo.respawn = false;
        }
    }

    private void Update()
    {
        PlayerInfo.playerX = transform.position.x;
        PlayerInfo.playerY = transform.position.y;
        PlayerInfo.playerYVelocity = rb.velocity.y;
        horizontal = Input.GetAxisRaw("Horizontal");

        if(!PlayerInfo.gameOver && !hasBell)
        {
            if (horizontal != 0)
            {
                lastInputDir = horizontal;
                rb.velocity = new Vector3(horizontal * movementSpeed, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(lastInputDir * movementSpeed, rb.velocity.y, rb.velocity.z);
            }
        }


        if(horizontal != 0 && movementSpeed < maxSpeed)
        {
            movementSpeed += speed * Time.deltaTime;
        }
        else if (horizontal == 0 && movementSpeed > 0)
        {
            movementSpeed -= (speed * 0.5f) * Time.deltaTime;
        }

        if (movementSpeed >= maxSpeed)
        {
            movementSpeed = maxSpeed;
        }
        else if(movementSpeed <= 0)
        {
            movementSpeed = 0;
        }

        if (rb.velocity.y < 0 && hasBell)
        {
            collider.enabled = true;
            hasBell = false;
        }

        if(transform.position.y >= 12f)
        {
            transform.position = new Vector3(transform.position.x, 12, transform.position.z);
            PlayerInfo.touchingCeiling = true;
            Mathf.Floor(ScoreHandler.distance += 1 * Time.deltaTime);
        }
        else
        {
            PlayerInfo.touchingCeiling = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            coyoteTimeCounter = 0f;
        }

        if (GroundCheck())
        {
            coyoteTimeCounter = coyoteTime;
            PlayerInfo.playerGrounded = true;
            lastStablePos = transform.position;
        }
        else
        {
            PlayerInfo.playerGrounded = false;
            coyoteTimeCounter -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.W) && PlayerInfo.hasDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
                PlayerInfo.hasDoubleJump = false; 
            }
        }

        if(Mathf.Floor(transform.position.x) == 17f)
        {
            transform.position = new Vector3(1f, transform.position.y, transform.position.z);
        }
        if(Mathf.Floor(transform.position.x) == -1f)
        {
            transform.position = new Vector3(16f, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -0.5f)
        {
                PlayerInfo.playerLives--;
                transform.position = new Vector3(8,10,0);
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            if (PlayerInfo.playerLives != 0)
            {
                Instantiate(fallPlat, transform.position - new Vector3(2, 3, 0), transform.rotation);
            }
        }

        if (PlayerInfo.playerLives == 0 && !PlayerInfo.gameOver)
        {
            if (ScoreHandler.distance > ScoreHandler.currentPlayerTopDistance)
            {
                ScoreHandler.currentPlayerTopDistance = ScoreHandler.distance;
            }
            gameOverUI.SetActive(true);
            PlayerInfo.gameOver = true;
            gameObject.SetActive(false);

        }

        if(PlayerInfo.hasInvulnerable > 0f)
        {
            PlayerInfo.hasInvulnerable -= Time.deltaTime;
        }
        else
        {
            meshRenderer.material = defaultMat;
        }
    }

    private void FixedUpdate()
    {
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
            jumpBufferCounter = 0f;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            lastPlat = collision.gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hazard" && PlayerInfo.hasInvulnerable <= 0f)
        {
            PlayerInfo.playerLives--;
            PlayerInfo.hasInvulnerable = 1f;
            Destroy(other.gameObject);
        }
    }

    public bool GroundCheck()
    {
        return (Physics.Raycast(transform.position, Vector3.down, distToGround));
    }

    //Unused Bell function
    public void BellPower()
    {
        collider.enabled = false;
        rb.AddForce(transform.up * jumpSpeed * 4, ForceMode.VelocityChange);
        hasBell = true;
    }

    public void BellPowerNew(bool a, bool b, bool c)
    {
        rb.isKinematic = a;
        hasBell = b;
        collider.enabled = c;
        if (c)
        {
            Instantiate(fallPlat, transform.position - new Vector3(2, 2, 0), transform.rotation);
        }
    }
}

