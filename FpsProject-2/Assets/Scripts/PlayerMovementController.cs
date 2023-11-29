using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController Instance;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRange = 0.2f;
    [SerializeField] LayerMask groundLayerMask;

    [Header("Camera look")]
    [SerializeField] float cameraSensitivity = 100f;
    [SerializeField] Camera lookCamera;
    [SerializeField] GameObject characterBody;

    private CharacterController controller;
    private float xRotation = 0;
    private Vector3 velocity;
    private bool isGrounded;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleLookInput();
        HandleMovementInput();
    }

    void HandleLookInput()
    {
        var dt = Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * dt;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * dt;

        xRotation -= mouseY;
        Debug.Log($"Mouse X {mouseX}, rotation{xRotation}");
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        lookCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovementInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRange, groundLayerMask);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
