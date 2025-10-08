using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    protected int healthPoint;

    // ENCAPSULATION
    public virtual int HealthPoint
    {
        get { return healthPoint; }
        set 
        {
            if (value >= 0)
            {
                healthPoint = value;
            }
        }
    }

    protected float speed;

    // ENCAPSULATION
    public virtual float Speed
    {
        get { return speed; }
        set 
        {
            if (value >= 0)
            {
                speed = value;
            }
        }
    }

    protected int mass;

    protected Rigidbody rb;

    protected float horizontalInput;
    protected float verticalInput;

    protected Vector3 angleVelocity;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    /// <summary>
    /// Move the Spaceship.
    /// </summary>
    public virtual void Move(Vector2 playerInput)
    {
        horizontalInput = playerInput.x;
        verticalInput = playerInput.y;
        MoveInXAxis();
        MoveInYAxis();
        Tilt();
    }

    /// <summary>
    /// Move the Spaceship horizontally (X Axis).
    /// </summary>
    protected virtual void MoveInXAxis()
    {
        Quaternion deltaRotation = Quaternion.Euler
            (horizontalInput * Time.fixedDeltaTime * angleVelocity);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    /// <summary>
    /// Move the Spaceship vertically (Y Axis).
    /// </summary>
    protected virtual void MoveInYAxis()
    {
        rb.AddForce
            (verticalInput * speed * transform.forward, ForceMode.Impulse);
    }


    [SerializeField]
    protected Transform innerRotation;
    private float tiltAngle = 45.0f;

    /// <summary>
    /// Tilt the Spaceship in the direction of the horizontal movement.
    /// </summary>
    protected virtual void Tilt()
    {
        float zAngle = 0.0f;
        if (horizontalInput > 0)
        {
            zAngle = -tiltAngle;
        }

        if (horizontalInput < 0)
        {
            zAngle = tiltAngle;
        }

        innerRotation.eulerAngles = new Vector3
        (
            innerRotation.eulerAngles.x,
            innerRotation.eulerAngles.y, 
            zAngle
        );
    }

    public abstract void Shoot();

    public virtual void CheckDeath()
    {
        if (healthPoint <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Game Over!");
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (other.gameObject.TryGetComponent<Projectile>
                (out var projectile))
            {
                Debug.Log(gameObject.name + ": Take " + projectile.Damage + " damages");
                healthPoint -= projectile.Damage;
                Debug.Log(gameObject.name + ": HP: " + healthPoint);
                
                other.gameObject.SetActive(false);

                CheckDeath();
            }
        }
    }
}
