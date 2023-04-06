using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public Rigidbody playerRB;

    public float speed;
    public float speedMultiplier;
    public float smoothing = 0.9f;

    Vector3 moveDirection;
    Vector3 slopeMoveDir;

    public float tempPlayerx;
    public float tempPlayerz;

    public bool grounded;
    public bool groundedslope;
    public LayerMask groundMask;

    RaycastHit slopeHit;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool SlopeCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 2))
        {
            if (slopeHit.normal != Vector3.up)
            {
                groundedslope = true;
                return true;
            }else
            {
                groundedslope = false;
                return false;
            }
        }
        groundedslope = false;
        return false;
    }

    void FixedUpdate()
    {

        SlopeCheck();

        grounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), 0.4f, groundMask);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float y = playerRB.velocity.y;

        slopeMoveDir = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        moveDirection = transform.forward * vertical + transform.right * horizontal;

        if (grounded)
        {
            playerRB.AddForce(Vector3.down * 25, ForceMode.Acceleration);
            playerRB.AddForce(moveDirection.normalized * speed * speedMultiplier * Time.deltaTime, ForceMode.Impulse);
        }
        if (groundedslope)
        {
            grounded = false;
            Debug.Log("slope!");
            playerRB.AddForce(Vector3.down * 25, ForceMode.Acceleration);
            playerRB.AddForce(slopeMoveDir.normalized * speed * speedMultiplier * Time.deltaTime, ForceMode.Impulse);
        }
        

        if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)
        {
            tempPlayerx = playerRB.velocity.x;
            tempPlayerz = playerRB.velocity.z;
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;

            playerRB.velocity = new Vector3(tempPlayerx, playerRB.velocity.y, tempPlayerz);

            playerRB.velocity = playerRB.velocity * smoothing;
        }

        if (playerRB.velocity.magnitude > speed)
        {
            playerRB.velocity = playerRB.velocity.normalized * speed;
        }

        playerRB.velocity = new Vector3(playerRB.velocity.x, y, playerRB.velocity.z);
    }
}