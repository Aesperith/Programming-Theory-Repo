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
        playerRb.AddForce(horizontalInput * speed * Vector3.right, ForceMode.Impulse);
    }

    /// <summary>
    /// Move the player vertically (Y Axis).
    /// </summary>
    private void MoveInYAxis()
    {
        verticalInput = actionMove.ReadValue<Vector2>().y;
        playerRb.AddForce(verticalInput * speed * Vector3.forward, ForceMode.Impulse);
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
