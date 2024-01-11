using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10, jumpHeight = 3, gravity = -50f, rotationSpeed = 0.10f;
    public Transform model;
    public int health = 5;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isGrounded;
    private Vector3 velocity;

    public Animator animator;

    private void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        controller = GetComponent<CharacterController>();
        GroundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Death();
    }

    private void Death()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Move()
    {
        //declares inputs using unitys built-in axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //sets the move Vector to the inputs
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //player is looking at the direction they are moving in
        Quaternion.LookRotation(move, Vector3.up);

        //makes sure the player doesnt turn in to the "default rotation" if no movement input
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), rotationSpeed);
            transform.Translate(move * rotationSpeed * Time.deltaTime, Space.World);
        }

        //if (move == Vector3.zero)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.2f);
        //}
        //if (move != Vector3.zero)
        //{
        //    rigidbody.constraints = RigidbodyConstraints.None;
        //}
        //else
        //{
        //    rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        //}

        //checks if object is grounded in selected layer
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //jump when spacebar is pressed, starts the animation
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //sets running animation when player is moving, disables when player is giving no move input
        if (move != Vector3.zero)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}