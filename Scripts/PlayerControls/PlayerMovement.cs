using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Controls")]
    [SerializeField] private float haste;
    [SerializeField] private float maxSpeed;

    [Header("Jumping Controls")]
    [SerializeField] private float jumpForce;
    private bool jump;


    [Header("Ground Handleing")]
    [SerializeField] private LayerMask ground;
    private bool grounded;

    //[Header("Animation")]
    private Animator anim;
    private bool isWalking;

    private Vector3 moveDirection;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0,-9.81f * 2.5f,0);
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maxSpeed = 10f;
        jump = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //handing Inputs
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        rb.AddForce(moveDirection.normalized * maxSpeed* 5 * haste, ForceMode.Force);//moves player proportional to camera
        speedControl();//limits players speed

        //moving animation
        if (moveDirection.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        anim.SetBool(name: "isWalking", isWalking);

        //handles player jump
        if (Input.GetAxisRaw("Jump")>0.5f && jump)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, 1.6f, ground);
            if (grounded) {
                jump = false; //flag is used so the player cannot jump continusly.
                Invoke(nameof(resetJump), 0.6f);//the player can jump again in 0.5 seconds
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(haste* transform.up* jumpForce, ForceMode.Impulse);
            }
            if (!jump)//when on jump cooldown is prepeled upwards
            {
                rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);
            }
        }
        else if (jump && moveDirection.magnitude <= 0.1)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, 1.6f, ground);
            if (grounded) {
                rb.velocity = new Vector3(rb.velocity.x/2, rb.velocity.y, rb.velocity.z/2); 
            }
        }
    }

    private void resetJump()//resets jump
    {
        jump = true;
    }
    private void speedControl()
    {
            Vector3 velLim = new(rb.velocity.x, 0f, rb.velocity.z);//velocity limiter

            // limit velocity if needed so the player cannot go faster
            if (velLim.magnitude > maxSpeed*haste)
            {
                Vector3 limitedVel = velLim.normalized * maxSpeed * haste;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
    }

    public void setHaste(int newHaste)
    {
        haste = newHaste;
    }
}
