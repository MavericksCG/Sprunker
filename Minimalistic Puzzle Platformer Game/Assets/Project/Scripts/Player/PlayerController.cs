using System.Collections;
using UnityEngine;
using EZCameraShake;

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


    [Header("MOVEMENT/FALL")]
    public float fallThreshold;

    private bool previouslyGrounded;

    // Camera Shake
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;


    [Header("TELEPORTATION")] 
    public GameObject prompt;
    private GameObject currentTeleporter;

    private bool canTeleport = true;


    [Header("GFX")]
    public float particleDestructionDelay;

    private Quaternion particleShapeQuat;

    public GameObject groundJumpParticle;
    public GameObject groundLandingParticle;


    private void Awake () {
        rb = GetComponent<Rigidbody2D>();

        particleShapeQuat = Quaternion.Euler(90f, 90f, 0f);
    }

    
    private void Update () {
        // Shoot a raycast downwards from our groundCheck.position
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkHeight, whatIsGround);

        // Make a new previouslyGrounded variable for checking fall damage
        previouslyGrounded = isGrounded;
    }


    private void FixedUpdate () {
        // Call Methods
        HandleMovement();
        HandleTeleportation();
        HandleFalling();
    }


    private void HandleFalling () {
        if (!previouslyGrounded && isGrounded) {
            // Check whether the rigidbody's velocity was greater than our NEGATIVE fall damage threshold
            if (rb.velocity.y > -fallThreshold) {
                // Apply some slowness to the player if it has fallen higher than ou

                // Use EZCameraShake to shake the camera once
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);   
            }
        }
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

        // Create a new Vector2 and send the position to our rigidbody's position
        Vector2 moveDir = new Vector2(horizontalMovement * moveSpeed, 0f) * Time.deltaTime;
        rb.position += moveDir;

        #region Jumping

        if ((Input.GetKey(Keybinds.instance.jump) || Input.GetKey(Keybinds.instance.altJump)) && isGrounded) {
            // Add an upwards force to the rigidbody's velocity
            rb.velocity = Vector2.up * jumpForce;
            GameObject jp = Instantiate(groundJumpParticle, groundCheck.position, particleShapeQuat);
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

    }

    private IEnumerator SuperJump () {

        canSuperJump = false;
        rb.velocity = Vector2.up * superJumpForce;
        GameObject jp = Instantiate(groundJumpParticle, groundCheck.position, particleShapeQuat);
        Destroy(jp, particleDestructionDelay);

        yield return new WaitForSeconds(superJumpCooldown);

        canSuperJump = true;

    }


    private void OnCollisionEnter2D (Collision2D col) {

        if (col.gameObject.CompareTag("Harmful Platform")) {
            Die();
        }

        if (col.gameObject.CompareTag("GroundObject")) {
            GameObject lp = Instantiate(groundLandingParticle, groundCheck.position, particleShapeQuat);
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