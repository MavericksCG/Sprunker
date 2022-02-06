using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    [Tooltip("How fast should the player move")] public float moveSpeed;

    private Rigidbody2D rb;

    [Header("Movement/Jumping")]

    [Tooltip("How high should the player jump")] public float jumpForce;
    [Tooltip("How high should the Physics2D.Raycast check to confirm whether the player can jump or not. Recommended value is anywhere between 0.1 - 0.3")] public float checkHeight;

    private bool isGrounded;

    [Tooltip("Where should the Physics2D Engine shoot the raycast. Place this right under the player's feet")] public Transform groundCheck;
    [Tooltip("Whichever Layer you pass in this layermask will be the layer on which the player can perform jumps and superjumps")] public LayerMask whatIsGround;


    [Header("Movement/Sprinting")]

    [Tooltip("The player's default speed value")] public float normalSpeed;
    [Tooltip("How fast should the player move if it's sprinting")] public float sprintSpeed;

    [Tooltip("How fast should the player's speed interpolate to the sprint or normal speed")] public float accelerationSpeed;


    [Header("Movement/Jumping/SuperJump")]

    [Tooltip("How high should the player jump when it performs a super jump")] public float superJumpForce;
    [Tooltip("How much time should elapse before you can perform another super jump after the player has already performed one")] [Range(0.1f, 10f)] public float superJumpCooldown;

    [HideInInspector] public bool canSuperJump = true;


    [Header("Movement/Miscellaneous")]
    public float pulldownForce;


    [Header("Teleportation")] 

    [Tooltip("Confirm Teleportation Prompt")] public GameObject prompt;
    private GameObject currentTeleporter;

    private bool canTeleport = true;


    [Header("Physics")]

    // Fall Speed
    public float minClampSpeed;
    public float maxClampSpeed;
    public float maxSpeedBeforeActivatingClamping; // That's quite a big variable name though...   
    private float rigidbodyVelocityY;


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

        #region Fall Speed Clamping

        if (rb.velocity.y >= maxSpeedBeforeActivatingClamping) {
            rigidbodyVelocityY = Mathf.Clamp(rb.velocity.y, minClampSpeed, maxClampSpeed);
        }

        #endregion

        #region Pulldown Ability 

        if ((Input.GetKey(Keybinds.instance.pulldownKey) || Input.GetKey(Keybinds.instance.altPulldownKey))) {
            rb.AddForce(Vector2.down * pulldownForce, ForceMode2D.Force);
        }

        #endregion

    }

    private IEnumerator SuperJump () {

        canSuperJump = false;
        rb.velocity = Vector2.up * superJumpForce;

        yield return new WaitForSeconds(superJumpCooldown);

        canSuperJump = true;

    }


    private void OnCollisionEnter2D (Collision2D col) {

        if (col.gameObject.CompareTag("Harmful Platform")) {
            Die();
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