using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    private IEnumerator blinkCoroutine;
    private bool hasBell;
    private bool gameOver;
    private bool beforeStart;
    private bool touchingYClamp;
    private bool hasDoubleJump;
    private bool grounded;
    private float hasInvulnerable;
    private float movementSpeed;
    private float lastInputDir;
    private float horizontalInput;
    private float distToGround;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float hitStopAmountCounter;
    private float rightWrapAroundThreshold = 17f;
    private float leftWrapAroundThreshold = -1f;
    private int playerLives;
    private int retryCount;

    #endregion

    #region serialized fields
    [Header("Character Stat Variables")]
    [SerializeField] private int maxPlayerLives;
    [SerializeField] private int maxRetrys;
    [SerializeField] private float hitStopAmountSet;
    [SerializeField] private float hitBlinkingRate;
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
    [SerializeField] private Vector3 startPoint = new Vector3(8, 1, 0);
    [SerializeField] private GameObject fallPlat;
    [SerializeField] private GameObject startPlat;
    [SerializeField] private GameObject yClamp;
    [SerializeField] private GameObject platLandParticleSystem;
    [SerializeField] private GameObject groundedChecker;
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject doubleJumpDrum;
    [SerializeField] private LayerMask groundMask;
    #endregion

    #region getters setters
    public Rigidbody GetRigidbody { get => rb; }
    public int RetryCount { get => retryCount; set => retryCount = value; }
    public int PlayerLives { get => playerLives; set => playerLives = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    public bool TouchingYClamp { get => touchingYClamp; set => gameOver = value; }
    public PlayerSounds PlayerSounds { get => playerSounds;}
    public bool Grounded { get => grounded;}
    public float HorizontalInput { get => horizontalInput;}
    public float LastInputDir { get => lastInputDir;}
    public bool BeforeStart { get => beforeStart;}
    #endregion

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
        mesh.SetActive(false);
        rb.useGravity = false;
        beforeStart = true;
    }

    private void Update()
    {
        if (!gameOver && !beforeStart)
        {
             horizontalInput = Input.GetAxisRaw("Horizontal");

            if(horizontalInput != 0 && movementSpeed < maxSpeed)
            {
                movementSpeed += acceleration * Time.deltaTime;
                lastInputDir = horizontalInput;
            }
            else if (horizontalInput == 0 && movementSpeed > 0)
            {
                movementSpeed -= decceleration * Time.deltaTime; 
            }

            movementSpeed = Mathf.Clamp(movementSpeed, 0, maxSpeed);

            if (transform.position.y >= yClamp.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, yClamp.transform.position.y, transform.position.z);
                touchingYClamp = true;
            }
            else
            {
                touchingYClamp = false;
            }

            if (Input.GetButton("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            if (Input.GetButtonUp("Jump"))
            {
                coyoteTimeCounter = 0f;
            }
        }

        if (GroundCheck())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if (Input.GetButtonDown("Jump") && hasDoubleJump && !hasBell)
            {
                playerParticles.ParticleObjects[2].Stop();
                coyoteTimeCounter = coyoteTime;
                jumpBufferCounter = jumpBufferTime;
                Instantiate(doubleJumpDrum, transform.position, Quaternion.identity);
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

        if (hitStopAmountCounter > 0)
        {
            hitStopAmountCounter -= Time.unscaledDeltaTime;
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

        if (!gameOver && !hasBell && !beforeStart)
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
        if (other.gameObject.CompareTag("Platform"))
        {
            platLandParticleSystem.transform.SetParent(other.gameObject.transform);
            platLandParticleSystem.transform.position = transform.position;
            var particlePlay = platLandParticleSystem.GetComponent<ParticleSystem>();
            particlePlay.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PowerUp"))
        {
            var powerUp = other.GetComponent<PowerUp>();
            PowerUp(powerUp.Id);
            powerUp.PowerUpObtained();
        }

        if (other.gameObject.CompareTag("Hazard") && hasInvulnerable <= 0f)
        {
            PlayParticle(0);
            PlayAudio(playerSounds.Sounds[1], 0.5f);
            playerLives--;
            hasInvulnerable = 1f;
            PlayerHit();
            Destroy(other.gameObject);
            HitStop(hitStopAmountSet);
            if (playerLives <= 0)
            { 
                GameOverSetup();
            }
        }
    }

    #region ground check function
    public bool GroundCheck()
    {
        Ray ray = new Ray(groundedChecker.transform.position, Vector3.down);
        return grounded = (Physics.SphereCast(ray, sphereCastRadius, sphereCastMaxDistance, groundMask)); 
    }
    #endregion

    //Normal respawns (From falling with a life) and respawns from a game over require different variables to change
    #region respawn functions
    public void GameOverRespawn()
    {
        mesh.SetActive(true);
        hasInvulnerable = 3f;
        PlayerHit();
        PlayParticle(1);
        playerLives = maxPlayerLives;
        gameOver = false; 
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
        rb.useGravity = true;
        colliderPlayer.enabled = true;
        Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation); //We offset the platform otherwise it would spawn to the left of the player
    }

    public void NormalRespawn()
    {
        mesh.SetActive(true);
        hasInvulnerable = 3f;
        PlayerHit();
        PlayParticle(1);
        playerLives--;
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
        Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation);
    }
    #endregion 

    //Different points of the game require the player to have different variables on or off
    #region state change functions
    public void PlayerGameStart()
    {
        transform.position = startPoint;
        mesh.SetActive(true);
        rb.useGravity = true;
        colliderPlayer.enabled = true;
        beforeStart = false;
        var startingPlatform = Instantiate(startPlat, transform.position - new Vector3(0,2,0), transform.rotation);
        startingPlatform.transform.parent = FindObjectOfType<LevelChunk>().transform;  

    }

    public void GoBackToInitial()
    {
        gameOver = false;
        beforeStart = true;
        mesh.SetActive(false);
        rb.useGravity = false;
        playerLives = maxPlayerLives;
        retryCount = maxRetrys;
        transform.position = startPoint;
        rb.velocity = Vector3.zero; //Reset the players velocity to zero 
    }
    public void ResetFromBell()
    {
        hasBell = false;
        colliderPlayer.enabled = true;
        rb.useGravity = true;
        hasInvulnerable = 2f;
        Ray ray = new Ray(transform.position + new Vector3(0,1,0), Vector3.down);
        if (Physics.SphereCast(ray, 0.5f, 0.5f, groundMask))
        {
            transform.position += new Vector3(0, 3, 0);
        }
        Instantiate(fallPlat, transform.position - fallPlatSpawnOffset, transform.rotation);
    }
    public void GameOverSetup()
    {
        if (GameManager.instance.scoreManager.Distance > GameManager.instance.scoreManager.CurrentPlayerTopDistance)
        {
            GameManager.instance.scoreManager.CurrentPlayerTopDistance = GameManager.instance.scoreManager.Distance;
        }
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        GameManager.instance.gameOverUI.SetActive(true);
        mesh.SetActive(false);
        colliderPlayer.enabled = false;
        gameOver = true;
        rb.useGravity = false;
        playerLives = -1;
    }
    #endregion

    //Pass in functions include playing audio, player particles, powerup handling
    #region pass in functions
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

    public void PlayParticle(int particleToPlay) // 0 == Pain particle (Player has been hit) | 1 == Invulnerability particle | 2 == Double Jump particle (Player currently has double jump)
    {
        switch (particleToPlay) 
        {
            case 0:
                playerParticles.ParticleObjects[0].Play();
                break;

            case 1:
                playerParticles.ParticleObjects[1].Stop();
                playerParticles.ParticleObjects[1].Clear();
                var main = playerParticles.ParticleObjects[1].main;
                main.duration = hasInvulnerable;
                playerParticles.ParticleObjects[1].Play();
                PlayAudio(playerSounds.Sounds[2], 0.8f);
                break;
            case 2:
                playerParticles.ParticleObjects[2].Play();
                break;
        }
    }

    public void PowerUp(int id) // 0 == DoubleJump | 1 == MoonCake(Invulnerable) | 2 == Bell(Sprite comes down and flys player up) |
    {
        switch (id)
        {
            case 0:
                hasDoubleJump = true;
                PlayParticle(2);
                break;

            case 1:
                hasInvulnerable = 5f;
                PlayParticle(1);
                break;

            case 2:
                GameManager.instance.bellSprite.StartAnim();
                rb.velocity = Vector3.zero;
                hasBell = true;
                colliderPlayer.enabled = false;
                rb.useGravity = false;
                break;
        }
    }
    #endregion

    //Functions to do with when the player is hit
    #region playerhit functions
    private void HitStop(float amount)
    {
        hitStopAmountCounter = amount;
        Time.timeScale = 0;
    }

    private IEnumerator Blink(float waitTime)
    {
        while(waitTime > 0f)
        {
            mesh.SetActive(false);
            yield return new WaitForSeconds(hitBlinkingRate);
            mesh.SetActive(true);
            yield return new  WaitForSeconds(hitBlinkingRate);
            waitTime -= hitBlinkingRate * 2; //this is stupid
        }
    }

    private void PlayerHit()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = Blink(hasInvulnerable);
        StartCoroutine(blinkCoroutine);
    }
    #endregion
}

