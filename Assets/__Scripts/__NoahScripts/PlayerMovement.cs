using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region private variables
    private Rigidbody rb;
    private BoxCollider colliderPlayer;
    private PlayerSounds playerSounds;
    private AudioSource[] audioSources = new AudioSource[0];
    private PlayerParticles playerParticles;
    private Vector3 fallPlatSpawnOffset = new Vector3(2,4,0);
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
    private float leftWrapAroundThreshold = -1f;
    private int playerLives;
    private int retryCount;
    #endregion

    #region serialized fields
    [Header("Character Stat Variables")]
    [SerializeField] private int maxPlayerLives;
    [SerializeField] private int maxRetrys;
    [SerializeField] private float hitStopAmount;
    [Space]
    [Header("Character Movement Variables")]
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float decceleration = 60f;
    [SerializeField] private float maxSpeed = 8.5f;
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float sphereCastRadius = 0.17f;
    [SerializeField] private float sphereCastMaxDistance = 0.5f;
    [Space]
    [Header("Coyote Time Variables")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [Space]
    [Header("Misc")]
    [SerializeField] private Vector3 respawnPoint = new Vector3(8, 10, 0);
    [SerializeField] private Material defaultMat;
    [SerializeField] private GameObject fallPlat;
    [SerializeField] private GameObject yClamp;
    [SerializeField] private GameObject platLandParticleSystem;
    [SerializeField] private GameObject groundedChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool grounded;
    #endregion

    #region getters setters
    public Rigidbody GetRigidbody { get => rb; }
    public int RetryCount { get => retryCount; set => retryCount = value; }
    public int PlayerLives { get => playerLives; set => playerLives = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool TouchingYClamp { get => touchingYClamp; set => gameOver = value; }
    public PlayerSounds PlayerSounds { get => playerSounds;}
    #endregion
    private void OnEnable()
    {
        Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation);
    }

    void Start()
    {
        playerParticles = GetComponent<PlayerParticles>();
        playerSounds = GetComponent<PlayerSounds>();
        audioSources = GetComponents<AudioSource>();
        colliderPlayer = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        distToGround = colliderPlayer.bounds.size.y / 2;
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
                coyoteTimeCounter = coyoteTime;
                jumpBufferCounter = jumpBufferTime;
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

        if (hitStopAmount > 0)
        {
            hitStopAmount -= Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void FixedUpdate()
    {
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            PlayAudio(playerSounds.Sounds[0], 1f);
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
            platLandParticleSystem.transform.SetParent(other.gameObject.transform);
            platLandParticleSystem.transform.position = transform.position;
            var particlePlay = platLandParticleSystem.GetComponent<ParticleSystem>();
            particlePlay.Play();
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

        if (other.gameObject.tag == "Hazard" && hasInvulnerable <= 0f)
        {
            playerParticles.ParticleObjects[0].Play();
            PlayAudio(playerSounds.Sounds[1], 0.5f);
            playerLives--;
            hasInvulnerable = 1f;
            Destroy(other.gameObject);
            if (playerLives > 0)
            {
                HitStop(0.1f);
            }
            else
            {
                GameManager.instance.scoreManager.PlayerDeathScoreChange();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
    }

    public bool GroundCheck()
    {
        Ray ray = new Ray(groundedChecker.transform.position, Vector3.down);
        return grounded = (Physics.SphereCast(ray, sphereCastRadius, sphereCastMaxDistance, groundMask)); 
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
                playerParticles.ParticleObjects[1].Play();
                break;

            case 2:
                GameManager.instance.bellSprite.StartAnim();
                hasInvulnerable = 7f;
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
            Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation);
        }
    }

    public void GameOverRespawn()
    {
        hasInvulnerable = 3f;
        playerLives = maxPlayerLives;
        gameOver = false; 
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
        Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation); //We offset the platform otherwise it would spawn to the left of the player
    }

    public void NormalRespawn()
    {
        hasInvulnerable = 3f;
        playerLives--;
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
        Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation);
    }

    public void GoBackToInitial()
    {
        playerLives = maxPlayerLives;
        retryCount = maxRetrys;
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
    }

    public void PlayAudio(AudioClip clip, float volume)
    {
        if (audioSources[0].isPlaying && clip != audioSources[0].clip)
        {
            audioSources[1].volume = volume;
            audioSources[1].clip = clip;
            audioSources[1].Play();
        }
        else
        {
            audioSources[0].volume = volume;
            audioSources[0].clip = clip;
            audioSources[0].Play();
        }
    }
    private void HitStop(float amount)
    {
        hitStopAmount = amount;
        Time.timeScale = 0;
    }
}

