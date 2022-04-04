using Cinemachine;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;
using Sprunker.Managing;
using Sprunker.Miscellaneous;
using Sprunker.PuzzleElements;

namespace Sprunker.Player {

    public class PlayerController : MonoBehaviour {

        [Header("Movement")] 
        public float moveSpeed;

        private Rigidbody2D rb;
        private Vector2 moveDir;

        [SerializeField] private AnimationCurve speedCurve = new AnimationCurve();


        [Header("Movement/Jumping")] 
        public float jumpForce;
        [SerializeField] private float checkHeight;

        private bool isGrounded;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask whatIsGround;


        [Header("Movement/Sprinting")] 
        public float normalSpeed;
        public float sprintSpeed;

        [SerializeField] private float accelerationSpeed;


        [Header("Movement/Jumping/Super-Jump")]
        [SerializeField] private float superJumpForce;

        [Range(0.1f, 100f)] [SerializeField] private float superJumpCooldown;

        [HideInInspector] public bool canSuperJump = true;


        [Header("Movement/Dashing")] 
        [SerializeField] private float dashSpeed;
        [SerializeField] [Range(0f, 120f)] private float dashCooldown;

        private int dir;

        [HideInInspector] public bool canDash = true;


        [Header("Teleportation")] 
        [SerializeField] private GameObject prompt;
        private GameObject currentTeleporter;

        private bool canTeleport = true;


        [Header("GFX")] 
        [SerializeField] private float particleDestructionDelay;

        private Quaternion particleShapeQuat;

        #region Particles

        // Landing/Collision Particles
        [SerializeField] private GameObject groundJumpParticle;
        [SerializeField] private GameObject groundLandCollisionParticle;
        [SerializeField] private GameObject pillarCollisionParticle;
        [SerializeField] private GameObject platformCollisionParticle;
        [SerializeField] private GameObject harmfulPlatformCollisionParticle;
        [SerializeField] private GameObject endTriggerPlatformCollisionParticle;
        [SerializeField] private GameObject endTriggerCollisionParticle;
        [SerializeField] private GameObject endTriggerCoverCollisionParticle;


        // Other Particles
        [SerializeField] private GameObject checkpointSetParticle;

        #endregion
        
        
        [Header("Audio")]
        [SerializeField] private AudioSource deathSFX;
        [SerializeField] private AudioSource dashSFX;
        [SerializeField] private AudioSource superJumpSFX;
        [SerializeField] private AudioSource jumpSFX;
        
        
        [Header("Other")]
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        [SerializeField] private bool logMainCameraPriority;

        private PlayerUtilities utils;

        [HideInInspector] public Vector3 startPos;

        private Checkpoint checkpoint;

        // TODO : Bring back the pulldown ability as a debug option 
        // Debugging Options - 
        [Header("Debugging/Pulldown")]
        public bool usePulldown = false;
        public float pulldownForce;
        [SerializeField] private KeyCode pulldownKey = KeyCode.S;


        private void Awake () {
            // Get components
            rb = GetComponent<Rigidbody2D>();
            utils = GetComponent<PlayerUtilities>();
            checkpoint = FindObjectOfType<Checkpoint>();

            // Set a quaternion for all of the particle rotations
            particleShapeQuat = Quaternion.Euler(90f, 90f, 0f);
        }


        private void Start () {
            // Set the startPos(Vector3)
            startPos = transform.position;   
            
            // Invoke OnSpawn(); from PlayerUtilities
            // First check if the PlayerUtilities Script in not null
            if (utils != null) {
                utils.OnSpawn();
            }
            else {
                return;
            }
        }


        private void Update () {
            // Shoot a raycast downwards from our groundCheck.position
            isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkHeight, whatIsGround);

            if (checkpoint != null) {
                if (checkpoint.hasSetCheckpoint && HasDied()) {
                    // Revert the change in the priority if the player has set a checkpoint and the player has died
                    SwitchCamera.instance.RevertPriorityChange();
                }
            }

            if (logMainCameraPriority) {
                // Write the main camera's priority each frame if the logMainCameraPriority bool is true
                Debug.Log(mainCamera.Priority);
            }
        } 
        

        private void FixedUpdate () {
            // Call Methods
            HandleMovement();
            HandleTeleportation();
            HandleDebugMovement();
        }

        private void HandleDebugMovement () {
            // Check if usePulldown boolean in true
            if (usePulldown) {
                if (Input.GetKey(pulldownKey)) {
                    // Add a downwards force
                    rb.AddForce(Vector2.down * pulldownForce, ForceMode2D.Force);
                }
            }
            else return;
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

            // Updating graphs
            speedCurve.AddKey(Time.realtimeSinceStartup, moveSpeed);

        }


        private IEnumerator Dash () {

            // Check current movement direction using dir(int)
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

            // Set canSuperJump back to true 
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


            if (col.CompareTag("Checkpoint")) {
                GameObject csp = Instantiate(checkpointSetParticle, transform.position, Quaternion.identity);
                Destroy(csp, particleDestructionDelay);
            }


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
            if (col.CompareTag("Switch Virtual Camera 02")) {
                // ContractGround.instance.Contract();
                SwitchCamera.instance.SetPriority();
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
            
            // Play the death sound effect
            deathSFX.Play();
        }


        #region Boolean Methods

        public bool IsSprinting () {
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && Input.GetKey(Keybinds.instance.sprint) || Input.GetKey(Keybinds.instance.altSprint)) return true;

            else return false;
        }

        public bool IsWalking () {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) return true;

            else return false;
        }

        public bool HasSuperJumped () {
            if (!canSuperJump && !isGrounded) return true;

            else return false;
        }

        public bool HasDashed () {
            if (!canDash && !isGrounded) return true;

            else return false;
        }

        public bool HasJumped () {
            if (!isGrounded) return true;

            else return false;
        }

        private bool HasDied () {
            if (gameObject == null)
                return true;
            else
                return false;
        }

        #endregion
    }
}