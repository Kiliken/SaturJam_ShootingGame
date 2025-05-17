using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform orientation;
    [SerializeField] float moveSpeed;
    float _sprintSpeed = 1;

    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    [Header("GroundCheck")]
    [SerializeField] float playerHeight;
    [SerializeField] public LayerMask ground;

    [Header("Combat")]
    [SerializeField] int _hp;


    bool grounded;

    float horizontalInput;

    Rigidbody rb;
    Vector2 moveDirection;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        PlayerInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
            Jump();


        _sprintSpeed = (Input.GetButton("Sprint") ? 3f : 1f);

    }

    private void MovePlayer()
    {
        moveDirection = orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * _sprintSpeed, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f * _sprintSpeed, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector2 flatVet = new Vector2(rb.velocity.x, 0f);

        if (flatVet.magnitude > moveSpeed)
        {
            Vector2 limitedVel = flatVet.normalized * moveSpeed;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _hp -= 1;

            //Fix Direction and use AddForce
            rb.AddForceAtPosition(Vector2.one * 2f, rb.transform.position, ForceMode.Impulse);
            if (_hp <= 0f)
                SceneManager.LoadScene("MainScene");
        }
            
    }

}
