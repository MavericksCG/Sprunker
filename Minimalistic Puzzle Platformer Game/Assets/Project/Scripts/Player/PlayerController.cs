using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    private Rigidbody2D rb;

    [Header("Movement/Jumping")]
    public float jumpForce;
    public float checkHeight;

    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask whatIsGround;


    [Header("Movement/Sprinting")]
    public float normalSpeed;
    public float sprintSpeed;

    public float accelerationSpeed;


    [Header("Movement/Jumping/SuperJump")]
    public float superJumpForce;
    public float superJumpCooldown;

    [HideInInspector] public bool canSuperJump = true;


    [Header("Teleportation")] 
    public GameObject prompt;
    private GameObject currentTeleporter;

    private bool canTeleport = true;
    
    
    private void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void Update () {
        // Shoot a raycast downwards from our groundCheck.position
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkHeight, whatIsGround);
    }


    private void FixedUpdate () {
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

        if (Input.GetKeyDown(Keybinds.instance.superJump) && canSuperJump) {
            StartCoroutine(SuperJump());
        }

    }


    private IEnumerator SuperJump () {

        canSuperJump = false;
        rb.velocity = Vector2.up * superJumpForce;

        yield return new WaitForSeconds(superJumpCooldown);

        canSuperJump = true;

    }


    private void OnCollisionEnter2D(Collision2D col) {
        
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
        GameManager.instance.EndGame();
    }
}

/*

using UnityEngine

public class YourScriptName : MonoBehaviour {
    
    public Vector3 followOffset;
    public Transform target; // This is supposed to be the object which the camera is going to follow

    private void Update () {
        transform.position = target.position + followOffset;
    }
    
}

*/