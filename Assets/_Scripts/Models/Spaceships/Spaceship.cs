using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for Spaceship. 
/// </summary>
public abstract class Spaceship : MonoBehaviour
{
    [SerializeField]
    protected AudioSource deathAudioSource;

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

    protected int price;
    protected int scorePoint;
    public UnityEvent<int> onDestroyed;

    protected int mass;

    protected float horizontalInput;
    protected float verticalInput;

    protected Vector3 angleVelocity;

    protected GameManager gameManager;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 1;
        rb.angularDamping = 1;

        deathAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Move the Spaceship.
    /// </summary>
    public virtual void Move(Vector2 input)
    {
        horizontalInput = input.x;
        verticalInput = input.y;
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

    /// <summary>
    /// Stop the spaceship.
    /// </summary>
    public virtual void Stop()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    [SerializeField]
    protected Transform innerRotation;
    protected float tiltAngleMax;
    private float tiltAngle = 0f;

    /// <summary>
    /// Tilt the Spaceship in the direction of the horizontal movement.
    /// </summary>
    protected virtual void Tilt()
    {
        if (horizontalInput > 0)
        {
            if (tiltAngle > 0)
            {
                tiltAngle--;
            }

            if (tiltAngle > -tiltAngleMax)
            {
                tiltAngle--;
            }
        }
        else if (horizontalInput < 0)
        {
            if (tiltAngle < 0)
            {
                tiltAngle++;
            }

            if (tiltAngle < tiltAngleMax)
            {
                tiltAngle++;
            }
        }
        else
        {
            if (tiltAngle > 0)
            {
                tiltAngle--;
            }
            else if(tiltAngle < 0)
            {
                tiltAngle++;
            }
            else
            {
                tiltAngle = 0f;
            }
        }

        innerRotation.eulerAngles = new Vector3
        (
            innerRotation.eulerAngles.x,
            innerRotation.eulerAngles.y,
            tiltAngle
        );
    }

    /// <summary>
    /// Shoot with the spaceship's weapon(s).
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// Activate special power 1.
    /// </summary>
    public abstract void Activate1();

    /// <summary>
    /// Check state of death of the spaceship.
    /// </summary>
    public virtual void CheckDeath()
    {
        if (healthPoint <= 0)
        {
            AudioSource.PlayClipAtPoint
                (deathAudioSource.clip, transform.position );

            if (gameObject.CompareTag("Enemy"))
            {
                onDestroyed.Invoke(scorePoint);
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
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
                if (!this.CompareTag(projectile.source))
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
}
