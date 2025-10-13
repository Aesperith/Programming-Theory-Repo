using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// Controls the behavior of the <see cref="Missile"/> projectile.
/// </summary>
public class MissileController : MonoBehaviour
{
    [SerializeField]
    private Missile missile;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float halfAngleView = 10;

    [SerializeField]
    private float delay = 0.1f;

    private Vector3 angleVelocity;

    private SphereCollider detection;
    private float radiusDetection;
 
    private float timeLimit;
    private float elapsedTime;

    private float inputX;

    private string sourceTag;
    private string targetTag;

    private GameObject target;

    private bool isActive;

    private Coroutine followTarget;


    private void Awake()
    {
        detection = GetComponent<SphereCollider>();
        radiusDetection = detection.radius;
    }

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        angleVelocity = new(0, 240, 0);
    }

    private void OnEnable()
    {
        sourceTag = missile.source;
        targetTag = sourceTag == "Enemy" ? "Player" : "Enemy";

        inputX = 0f;

        elapsedTime = 0;
        timeLimit = 5f;
        isActive = true;

        StartCoroutine(TimerLimit());
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Orientate();
        }
        else
        {
            SelfDestruct();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            target = other.gameObject;
            followTarget = StartCoroutine(FollowTarget());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            StopCoroutine(followTarget);
            inputX = 0f;
            target = null;
        }
    }

    /// <summary>
    /// Orientate the missile.
    /// </summary>
    private void Orientate()
    {
        Quaternion deltaRotation = Quaternion.Euler
            (inputX * Time.fixedDeltaTime * angleVelocity);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    /// <summary>
    /// Coroutine to follow target.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FollowTarget()
    {
        while (target != null)
        {
            Vector3 position = target.transform.position;
            
            yield return new WaitForSeconds(delay);

            Vector3 direction = (position - this.transform.position);

            float targetAngle = Vector3.Angle
                (direction, this.transform.forward);

            float angleDirection = AngleDirection
                (this.transform.forward, direction, this.transform.up);

            if (targetAngle > halfAngleView)
            {
                inputX = angleDirection;
            }
            else
            {
                inputX = 0f;
            }
        }
    }

    /// <summary>
    /// Method to know the direction of target.
    /// </summary>
    /// <param name="fwd">Forward direction.</param>
    /// <param name="targetDir">Target direction.</param>
    /// <param name="up">Up direction.</param>
    /// <returns>+1: Right; -1: Left; 0: Ahead or Behind.</returns>
    private float AngleDirection(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 right = Vector3.Cross(up, fwd);        // right vector
        float dir = Vector3.Dot(right, targetDir);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    /// <summary>
    /// Timer for the duration limit of the missile.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimerLimit()
    {
        while (isActive)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeLimit)
            {
                isActive = false;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Destroy the missile.
    /// </summary>
    private void SelfDestruct()
    {
        missile.gameObject.SetActive(false);
    }
}
