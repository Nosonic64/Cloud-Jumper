using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region private variables
    private Rigidbody rb;
    private CapsuleCollider colliderPlayer;
    private MeshRenderer meshRenderer;
    private bool hasBell;
    private bool gameOver;
    private bool touchingYClamp;
    private bool hasDoubleJump;
    private float hasInvulnerable;
    private float movementSpeed;
    private float lastInputDir;
    private float horizontalInput;
    private float distToGround;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float rightWrapAroundThreshold = 17f;
    private float leftWrapAroundThreshold = 0f;
    private int playerLives;
    private int retryCount;
    #endregion

    #region serialized fields
    [Header("Character Stat Variables")]
    [SerializeField] private int maxPlayerLives;
    [SerializeField] private int maxRetrys;
    [Space]
    [Header("Character Movement Variables")]
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float decceleration = 15f;
    [SerializeField] private float maxSpeed = 8.5f;
    [SerializeField] private float jumpSpeed = 20f;
    [Space]
    [Header("Coyote Time Variables")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [Space]
    [Header("Misc")]
    [SerializeField] private Material defaultMat;
    [SerializeField] private GameObject fallPlat;
    [SerializeField] private GameObject yClamp;
    [SerializeField] private Vector3 respawnPoint = new Vector3(8, 10, 0);
    #endregion

    #region getters setters
    public Rigidbody GetRigidbody { get => rb; }
    public int RetryCount { get => retryCount; set => retryCount = value; }
    public int PlayerLives { get => playerLives; set => playerLives = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool TouchingYClamp { get => touchingYClamp; }
    #endregion
    private void OnEnable()
    {
        Instantiate(fallPlat, transform.position - new Vector3(2, 3, 0), transform.rotation);
    }

    void Start()
    {
        colliderPlayer = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        meshRenderer = GetComponent<MeshRenderer>();
        playerLives = maxPlayerLives; 
        retryCount = maxRetrys;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput != 0)
        {
            lastInputDir = horizontalInput;
        }

        if(horizontalInput != 0 && movementSpeed < maxSpeed)
        {
            movementSpeed += acceleration * Time.deltaTime;
        }
        else if (horizontalInput == 0 && movementSpeed > 0)
        {
            movementSpeed -= decceleration * Time.deltaTime; 
        }

        movementSpeed = Mathf.Clamp(movementSpeed, 0, maxSpeed);

        if (rb.velocity.y < 0 && hasBell) // Explain what this does
        {
            colliderPlayer.enabled = true;
            hasBell = false;
        }

        if (transform.position.y >= yClamp.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, yClamp.transform.position.y, transform.position.z);
            touchingYClamp = true;
        }
        else
        {
            touchingYClamp = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        if (Input.GetButtonUp("Jump"))
        {
            coyoteTimeCounter = 0f;
        }

        if (GroundCheck())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if (Input.GetButtonDown("Jump") && hasDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
                hasDoubleJump = false;
            }
        }

        if(Mathf.Floor(transform.position.x) == rightWrapAroundThreshold) //Wrap around code 
        {
            transform.position = new Vector3(leftWrapAroundThreshold + 1, transform.position.y, transform.position.z);
        }
        if(Mathf.Floor(transform.position.x) == leftWrapAroundThreshold)
        {
            transform.position = new Vector3(rightWrapAroundThreshold - 1, transform.position.y, transform.position.z);
        }

        if(hasInvulnerable > 0f) 
        {
             hasInvulnerable -= Time.deltaTime;
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

        if (!gameOver && !hasBell)
        {
            if (horizontalInput != 0)
            {
                rb.velocity = (new Vector3(horizontalInput * movementSpeed, rb.velocity.y, rb.velocity.z));
            }
            else
            {
                rb.velocity = (new Vector3(lastInputDir * movementSpeed, rb.velocity.y, rb.velocity.z));
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            // Play particle burst
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PowerUp")
        {
            var powerUpID = other.GetComponent<PowerUp>().Id;
            PowerUp(powerUpID);
            Destroy(other.gameObject);
        }
    }

    public bool GroundCheck()
    {
        return (Physics.Raycast(transform.position, Vector3.down, distToGround));
    }

    public void PowerUp(int id) // 0 == DoubleJump | 1 == MoonCake(Invulnerable) | 2 == Bell(Sprite comes down and flys player up) |
    {
        switch (id) 
        {
            case 0:
                hasDoubleJump = true;
                break;

            case 1:
                hasInvulnerable = 5f;
                break;

            case 2:
                GameManager.instance.bellSprite.StartAnim();
                BellPower(true, true, false);
                break;
        }
    }

    public void BellPower(bool a, bool b, bool c)
    {
        rb.isKinematic = a;
        hasBell = b;
        colliderPlayer.enabled = c;
        if (c)
        {
            Instantiate(fallPlat, transform.position - new Vector3(2, 3, 0), transform.rotation);
        }
    }

    public void GameOverRespawn()
    {
        playerLives = maxPlayerLives;
        gameOver = false; 
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
        Instantiate(fallPlat, transform.position - new Vector3(2, 3, 0), transform.rotation); //We offset the platform otherwise it would spawn to the left of the player
    }

    public void NormalRespawn()
    {
        playerLives--;
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
        Instantiate(fallPlat, transform.position - new Vector3(2, 3, 0), transform.rotation);
    }

    public void GoBackToInitial()
    {
        playerLives = maxPlayerLives;
        retryCount = maxRetrys;
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
    }
}

