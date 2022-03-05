using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    private SlowMotion slowMotion;

    [Header("MOVEMENT")]
    public float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDir;


    [Header("MOVEMENT/JUMPING")]
    public float jumpForce;
    public float checkHeight;

    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    public AudioSource jumpSFX;


    [Header("MOVEMENT/SPRINTING")]
    public float normalSpeed;
    public float sprintSpeed;

    public float accelerationSpeed;


    [Header("MOVEMENT/JUMPING/SUPER-JUMP")]
    public float superJumpForce;
    [Range(0.1f, 100f)] public float superJumpCooldown;

    [HideInInspector] public bool canSuperJump = true;

    public AudioSource superJumpSFX;


    [Header("MOVEMENT/DASH")]
    [SerializeField] private float dashSpeed;
    [SerializeField] [Range(0f, 120f)] private float dashCooldown;
    
    private int dir;

    [HideInInspector] public bool canDash = true;

    [SerializeField] private AudioSource dashSFX;


    [Header("TELEPORTATION")]
    public GameObject prompt;
    private GameObject currentTeleporter;

    private bool canTeleport = true;


    [Header("GFX")]
    public float particleDestructionDelay;

    private Quaternion particleShapeQuat;
    
    #region Particles
    public GameObject groundJumpParticle;
    public GameObject groundLandCollisionParticle;
    public GameObject pillarCollisionParticle;
    public GameObject platformCollisionParticle;
    public GameObject harmfulPlatformCollisionParticle;
    public GameObject endTriggerPlatformCollisionParticle;
    public GameObject endTriggerCollisionParticle;
    public GameObject endTriggerCoverCollisionParticle;
    #endregion

    private void Awake () {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        slowMotion = GetComponent<SlowMotion>();
        
        // Set a quaternion for all of the particle rotations
        particleShapeQuat = Quaternion.Euler(90f, 90f, 0f);
        
    }


    private void Update () {
        // Shoot a raycast downwards from our groundCheck.position
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkHeight, whatIsGround);
    }


    private void FixedUpdate () {
        // Call Methods
        HandleMovement();
        HandleTeleportation();
    }


    private void HandleTeleportation () {

        if (Input.GetKey(Keybinds.instance.interact)) {
            if (currentTeleporter != null && canTeleport) {
                canTeleport = false;
                rb.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            }
        }

    }


    private void HandleMovement () {

        // GetAxis provides smoother but less snappier movement but GetAxisRaw made it look a little suspiciously snappy
        float horizontalMovement = Input.GetAxis("Horizontal");

        // Create a new Vector2 and send the position data to our rigidbody's position
        moveDir = new Vector2(horizontalMovement * moveSpeed, 0f) * Time.deltaTime;
        rb.position += moveDir;

        #region Jumping

        if ((Input.GetKey(Keybinds.instance.jump) || Input.GetKey(Keybinds.instance.altJump)) && isGrounded) {
            // Add an upwards force to the rigidbody's velocity
            rb.velocity = Vector2.up * jumpForce;
            GameObject jp = Instantiate(groundJumpParticle, groundCheck.position, particleShapeQuat);
            Destroy(jp, particleDestructionDelay);

            // Play the jump sound effect
            jumpSFX.Play();
        }

        #endregion


        #region Sprinting

        if ((Input.GetKey(Keybinds.instance.sprint) || Input.GetKey(Keybinds.instance.altSprint)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            // Interpolate moveSpeed to sprintSpeed
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, accelerationSpeed);
        }
        else {
            // Interpolate moveSpeed to normalSpeed
            moveSpeed = Mathf.Lerp(moveSpeed, normalSpeed, accelerationSpeed);
        }

        #endregion

        // Super Jump
        if (Input.GetKey(Keybinds.instance.superJump) && canSuperJump) {
            StartCoroutine(SuperJump());
        }

        #region Dashing

        if (Input.GetKey(Keybinds.instance.dashKey) && canDash) {
            StartCoroutine(Dash());
        }

        // If Statements to check which input key was pressed
        if (Input.GetKey(KeyCode.D)) {
            dir = 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            dir = 2;
        }
        
        // Set the dir variable to a random amount if no key is being pressed.
        if (dir == 0) {
            dir = Random.Range(1, 2);
        }


        #endregion
        
    }
    

    private IEnumerator Dash () {
        
        // Check current movement direction using
        switch (dir) {
            case 1:
                rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
                break;

            case 2:
                rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
                break;
        }
        
        dashSFX.Play();
        canDash = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }


    private IEnumerator SuperJump () {

        canSuperJump = false;
        rb.velocity = Vector2.up * superJumpForce;
        GameObject jp = Instantiate(groundJumpParticle, groundCheck.position, particleShapeQuat);
        Destroy(jp, particleDestructionDelay);
        superJumpSFX.Play();

        yield return new WaitForSeconds(superJumpCooldown);

        canSuperJump = true;

    }


    private void OnCollisionEnter2D (Collision2D col) {

        if (col.gameObject.CompareTag("Harmful Platform")) {
            Die();
        }

        #region Particles

        if (col.gameObject.CompareTag("GroundObject")) {
            GameObject cp = Instantiate(groundLandCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        if (col.gameObject.CompareTag("Pillar")) {
            GameObject cp = Instantiate(pillarCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        if (col.gameObject.CompareTag("Platform")) {
            GameObject cp = Instantiate(platformCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        if (col.gameObject.CompareTag("Harmful Platform")) {
            GameObject cp = Instantiate(harmfulPlatformCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        if (col.gameObject.CompareTag("End Platform Base")) {
            GameObject cp = Instantiate(endTriggerPlatformCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        if (col.gameObject.CompareTag("End Trigger")) {
            GameObject cp = Instantiate(endTriggerCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        if (col.gameObject.CompareTag("End Platform Cover")) {
            GameObject cp = Instantiate(endTriggerCoverCollisionParticle, groundCheck.position, particleShapeQuat);
            Destroy(cp, particleDestructionDelay);
        }

        #endregion
    }


    private void OnTriggerEnter2D (Collider2D col) {

        if (col.CompareTag("Teleporter") && canTeleport) {
            prompt.SetActive(true);
            currentTeleporter = col.gameObject;
        }

        else if (col.CompareTag("Teleporter") && !canTeleport)
            Destroy(prompt);


        if (col.CompareTag("End Trigger")) {
            GameManager.instance.Complete();
        }


        // Switching Virtual Camera Priority 
        if (col.CompareTag("Switch Virtual Camera"))
            SwitchCamera.instance.SetPriority();
        
        // Ground Contraction
        if (col.CompareTag("Contract Ground"))
            ContractGround.instance.Contract();
        
        // Switching Virtual Camera Priority... again
        if (col.CompareTag("Switch Virtual Camera 02"))
            ContractGround.instance.Contract();
    }


    private void OnTriggerExit2D (Collider2D col) {

        if (col.CompareTag("Teleporter") && canTeleport) {
            prompt.SetActive(false);
            currentTeleporter = null;
        }
        else if (col.CompareTag("Teleporter") && !canTeleport)
            Destroy(prompt);

    }


    private void Die () {
        // Invoke the EndGame method in the GameManager
        GameManager.instance.EndGame();
    }

    public bool IsSprinting () {
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && Input.GetKey(Keybinds.instance.sprint) || Input.GetKey(Keybinds.instance.altSprint)) {
            return true;
        }
        else {
            return false;
        }
    }
}