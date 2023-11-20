using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyController : MonoBehaviour
{
    private static float Ground_Spacing = 6.7f;

    [SerializeField] private Quaternion upQuaternionBound;
    [SerializeField] private Quaternion downQuaternionBound;
    [SerializeField] private GameObject ground1;
    [SerializeField] private GameObject ground2;
    [SerializeField] private GameObject flappyVisual;
    [SerializeField] private Camera flappyCamera;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalFallSpeed;
    [SerializeField] private float verticalBoostSpeed;
    [SerializeField] private float boostDuration;
    [SerializeField] private float rotateSpeed;

    private Rigidbody2D rb;
    private FlappyState flappyState;
    private float groundTravelCounter = 0;
    private float boostTimer = 0;
    private bool isBoosting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        flappyState = FlappyState.Alive;
    }

    void Update()
    {
        if (flappyState == FlappyState.Alive)
        {
            if (isBoosting)
            {
                boostTimer += Time.deltaTime;
                if (boostTimer >= boostDuration)
                {
                    isBoosting = false;
                    rb.velocity = Vector2.zero;
                }
            }
            MoveBirdHorizontal();
            MoveBirdVertical();
            if (Input.GetMouseButtonDown(0))
            {
                BoostBirdVertical();
            }
        }
        RotateBird();
    }

    private void MoveBirdVertical()
    {
        var moveVector = Vector3.down * Time.deltaTime * verticalFallSpeed;
        transform.position += moveVector;
    }

    private void MoveBirdHorizontal()
    {
        var moveVector = Vector3.right * Time.deltaTime * horizontalSpeed;
        transform.position += moveVector;
        flappyCamera.transform.position += moveVector;

        groundTravelCounter += moveVector.x;
        ObstacleSpawner.Instance.UpdateFlappyDistance(moveVector.x, transform.position.x);

        if (groundTravelCounter > Ground_Spacing)
        {
            groundTravelCounter -= Ground_Spacing;
            SwapGround();
        }
    }

    private void BoostBirdVertical()
    {
        rb.velocity = Vector2.up * verticalBoostSpeed;
        isBoosting = true;
        boostTimer = 0;
    }

    private void RotateBird()
    {
        //var y = rb.velocity.y;
        if (isBoosting)
        {
            flappyVisual.transform.rotation = Quaternion.Lerp(flappyVisual.transform.rotation, upQuaternionBound, rotateSpeed * Time.deltaTime);
        }
        else
        {
            flappyVisual.transform.rotation = Quaternion.Lerp(flappyVisual.transform.rotation, downQuaternionBound, rotateSpeed * Time.deltaTime);
        }
    }

    private void SwapGround()
    {
        var swapGround1 = ground2.transform.position.x > ground1.transform.position.x;
        if (swapGround1)
        {
            ground1.transform.position += (Vector3)(Ground_Spacing * 2 * Vector2.right);
        }
        else
        {
            ground2.transform.position += (Vector3)(Ground_Spacing * 2 * Vector2.right);
        }
    }

    public void TriggerGameOver()
    {
        flappyState = FlappyState.Dead;
        rb.gravityScale = 1;
    }

    public void ResetGame()
    {
        flappyState = FlappyState.Alive;
        rb.gravityScale = 0;
    }
}

public enum FlappyState
{
    Alive, Dead
}
