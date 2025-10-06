using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const float xRange = 14.0f;
    private const float zRange = 14.0f;

    [SerializeField]
    private float speed = 20.0f;

    [SerializeField]
    private float cooldownTime = 0.2f;
    private float cooldownUntilNextPress;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody playerRb;

    private InputAction actionMove;
    private InputAction actionShot;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        actionMove = InputSystem.actions.FindAction("Move");
        actionShot = InputSystem.actions.FindAction("Shot");
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        KeepInBoundary();
        //Move();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Move the player.
    /// </summary>
    private void Move()
    {
        MoveInXAxis();
        MoveInYAxis();
    }

    /// <summary>
    /// Move the player horizontally (X Axis).
    /// </summary>
    private void MoveInXAxis()
    {
        horizontalInput = actionMove.ReadValue<Vector2>().x;
        //transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);
        playerRb.AddForce(horizontalInput * speed * Vector3.right, ForceMode.Impulse);
    }

    /// <summary>
    /// Move the player vertically (Y Axis).
    /// </summary>
    private void MoveInYAxis()
    {
        verticalInput = actionMove.ReadValue<Vector2>().y;
        //transform.Translate(verticalInput * speed * Time.deltaTime * Vector3.forward);
        playerRb.AddForce(verticalInput * speed * Vector3.forward, ForceMode.Impulse);
    }

    /// <summary>
    /// Keep the player in boundaries.
    /// </summary>
    private void KeepInBoundary()
    {
        KeepInXBounds();
        KeepInZBounds();
    }

    /// <summary>
    /// Keep the player in X-axis bounds.
    /// </summary>
    private void KeepInXBounds()
    {
        // Check for left and right bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3
                (-xRange, transform.position.y, transform.position.z);
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3
                (xRange, transform.position.y, transform.position.z);
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Keep the player in Z-axis bounds.
    /// </summary>
    private void KeepInZBounds()
    {
        // Check for top and bottom bounds
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3
                (transform.position.x, transform.position.y, -zRange);
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3
                (transform.position.x, transform.position.y, zRange);
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }
    }

    private void Shoot()
    {
        if (actionShot.IsPressed())
        {
            // Get an object object from the pool
            GameObject pooledProjectile =
                ObjectPooler.SharedInstance.GetPooledObject();

            if (pooledProjectile != null && cooldownUntilNextPress < Time.time)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.SetPositionAndRotation
                    (transform.position, transform.rotation);
                cooldownUntilNextPress = Time.time + cooldownTime;
            }
        }
    }
}
