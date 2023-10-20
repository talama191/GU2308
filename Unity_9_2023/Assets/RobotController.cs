using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    [SerializeField] private float collisionForce = 10;
    [SerializeField] private float currentFuel = 100;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        MoveControl();
        currentFuel -= Time.deltaTime;
    }

    public void PickUpGas()
    {
        currentFuel += 100;
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherRb = other.gameObject.GetComponent<Rigidbody>();
        if (other.gameObject.tag == "enemy")
        {
            Vector3 collisionVector = ((other.transform.position - transform.position).normalized + Vector3.up).normalized;
            otherRb.AddForce(collisionVector * collisionForce, ForceMode.Impulse);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     // Destroy(other.gameObject);
    //     other.gameObject.SetActive(false);
    // }

    private void MoveControl()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
