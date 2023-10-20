using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform model;
    [SerializeField] private Animator animator;
    [SerializeField] private Bullet bulletPrefab;

    private Rigidbody2D rb;
    private bool isJumping;
    private bool isFalling;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessMove();
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 bulletDirection = (mouseWorldPos - new Vector2(transform.position.x, transform.position.y)).normalized;
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.Shoot(bulletDirection);
        }
    }

    private void ProcessMove()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * hor, rb.velocity.y);

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);

        animator.SetFloat("move_speed", Mathf.Abs(hor));
        if (hor < 0)
        {
            model.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (hor > 0)
        {
            model.transform.localScale = Vector3.one;
        }

        bool isGrounded = hit2D.collider != null;
        if (isJumping && rb.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
            animator.SetTrigger("fall");
        }

        if (isFalling && isGrounded)
        {
            isFalling = false;
            animator.SetTrigger("ground");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
            animator.SetTrigger("jump");
        }
    }
}
