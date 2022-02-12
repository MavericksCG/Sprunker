using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("MOVEMENT")]
    public float moveSpeed;

    private Rigidbody2D rb;


    [Header("MOVEMENT/JUMPING")]
    public float jumpForce;
    public float checkHeight;

    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask whatIsGround;


    [Header("MOVEMENT/SPRINTING")]
    public float normalSpeed;
    public float sprintSpeed;

    public float accelerationSpeed;


    [Header("MOVEMENT/JUMPING/SUPER-JUMP")]
    public float superJumpForce;
    [Range(0.1f, 100f)] public float superJumpCooldown;

    [HideInInspector] public bool canSuperJump = true;


    [Header("MOVEMENT/PULLDOWN")]
    public float pulldownForce;


    [Header("TELEPORTATION")] 
    public GameObject prompt;
    private GameObject currentTeleporter;

    private bool canTeleport = true;


    [Header("PHYSICS")]

    // Clamped Fall Speed
    public float fallSpeed;
    public float maxPhysicsFallSpeed;

    private float rigidbodyVelocityY;


    [Header("GFX")]
    public float particleDestructionDelay;

    public GameObject groundJumpParticle;
    public GameObject groundLandingParticle;


    private void Awake () {
        rb = GetComponent<Rigidbody2D>();
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

        // Set the rigidbodyVelocityY variable as the rb.velocity.y
        rigidbodyVelocityY = rb.velocity.y;


        // GetAxis provides smoother but less snappier movement but GetAxisRaw made it look a little suspiciously snappy
        float horizontalMovement = Input.GetAxis("Horizontal"); 

        // Create a new Vector2 and send the position to our rigidbody's position
        Vector2 moveDir = new Vector2(horizontalMovement * moveSpeed, 0f) * Time.deltaTime;
        rb.position += moveDir;

        #region Jumping

        if ((Input.GetKey(Keybinds.instance.jump) || Input.GetKey(Keybinds.instance.altJump)) && isGrounded) {
            // Add an upwards force to the rigidbody's velocity
            rb.velocity = Vector2.up * jumpForce;
            GameObject jp = Instantiate(groundJumpParticle, groundCheck.position, Quaternion.identity);
            Destroy(jp, particleDestructionDelay);
        }

        #endregion


        #region Sprinting
        
        if ((Input.GetKey(Keybinds.instance.sprint) || Input.GetKey(Keybinds.instance.altSprint))) {
            // Interpolate moveSpeed to sprintSpeed
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, accelerationSpeed); 
        } else {
            // Interpolate moveSpeed to normalSpeed
            moveSpeed = Mathf.Lerp(moveSpeed, normalSpeed, accelerationSpeed);
        }

        #endregion

        // Super Jump
        if (Input.GetKey(Keybinds.instance.superJump) && canSuperJump) {
            StartCoroutine(SuperJump());
        }

        #region Pulldown Ability 

        if ((Input.GetKey(Keybinds.instance.pulldownKey) || Input.GetKey(Keybinds.instance.altPulldownKey))) {
            rb.AddForce(Vector2.down * pulldownForce, ForceMode2D.Force);
        }

        #endregion

        #region Clamp Fall Speed

        if (rigidbodyVelocityY >= maxPhysicsFallSpeed)
            rigidbodyVelocityY = fallSpeed;

        #endregion

    }

    private IEnumerator SuperJump () {

        canSuperJump = false;
        rb.velocity = Vector2.up * superJumpForce;
        GameObject jp = Instantiate(groundJumpParticle, groundCheck.position, Quaternion.identity);
        Destroy(jp, particleDestructionDelay);

        yield return new WaitForSeconds(superJumpCooldown);

        canSuperJump = true;

    }


    private void OnCollisionEnter2D (Collision2D col) {

        if (col.gameObject.CompareTag("Harmful Platform")) {
            Die();
        }

        if (col.gameObject.CompareTag("GroundObject")) {
            GameObject lp = Instantiate(groundLandingParticle, groundCheck.position, Quaternion.identity);
            Destroy(lp, particleDestructionDelay);
        }

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
}