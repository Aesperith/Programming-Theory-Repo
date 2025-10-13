using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the behavior of other NPC Spaceships.
/// </summary>
public class IAController : MonoBehaviour
{
    /// <summary>
    /// Target of the IAController.
    /// </summary>
    [Serializable]
    public enum Target
    {
        Player,
        Enemy
    }

    [SerializeField]
    private Target targetSelect;

    private string targetTag;

    [SerializeField]
    private Spaceship spaceship;

    [SerializeField]
    private float halfAngleView = 10f;

    [SerializeField]
    private float halfAngleShoot = 10f;

    [SerializeField]
    private float delay = 0.1f;

    private UIMiniMap miniMap;
    private UIMiniMap.ShipDataInMiniMap dataInMiniMap;

    private GameObject target;

    private SphereCollider detection;
    private float radiusDetection;

    private Vector2 input;
    private Coroutine followTarget;
    private Coroutine shootTarget;

    private bool isInit = false;

    private void Awake()
    {
        detection = GetComponent<SphereCollider>();
        radiusDetection = detection.radius;

        miniMap = GameObject.FindFirstObjectByType<UIMiniMap>();
    }

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        if (!isInit)
        {
            if (spaceship.CompareTag("Enemy"))
            {
                dataInMiniMap = miniMap.RegisterToMiniMap
                    (spaceship.transform, UIMiniMap.TypeIcon.Enemy);
            }
            else
            {
                dataInMiniMap = miniMap.RegisterToMiniMap
                    (spaceship.transform, UIMiniMap.TypeIcon.Ally);
            }

            targetTag = targetSelect switch
            {
                Target.Player => nameof(Target.Player),
                Target.Enemy => nameof(Target.Enemy),
                _ => nameof(Target.Player),
            };

            isInit = true;
        }
    }

    private void OnEnable()
    {
        if (!isInit)
        {
            if (spaceship.CompareTag("Enemy"))
            {
                dataInMiniMap = miniMap.RegisterToMiniMap
                    (spaceship.transform, UIMiniMap.TypeIcon.Enemy);
            }
            else
            {
                dataInMiniMap = miniMap.RegisterToMiniMap
                    (spaceship.transform, UIMiniMap.TypeIcon.Ally);
            }

            targetTag = targetSelect switch
            {
                Target.Player => nameof(Target.Player),
                Target.Enemy => nameof(Target.Enemy),
                _ => nameof(Target.Player),
            };

            isInit = true;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            input = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        spaceship.Move(input);  // ABSTRACTION
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            target = other.gameObject;
            followTarget = StartCoroutine(FollowTarget());
            shootTarget = StartCoroutine(ShootTarget());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            StopCoroutine(followTarget);
            StopCoroutine(shootTarget);
            target = null;
            input = Vector2.zero;
            spaceship.Stop();
        }
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

            float targetDistance = Vector3.Distance
                (this.transform.position, position);

            float targetAngle = Vector3.Angle
                (direction, this.transform.forward);

            float angleDirection = AngleDirection
                (this.transform.forward, direction, this.transform.up);

            if (targetAngle > halfAngleView)
            {
                input = new(angleDirection, 0);
            }
            else if(targetDistance > radiusDetection / 2.0f)
            {
                input = new(0, 1);
            }
            else
            {
                input = Vector2.zero;
            }
        }
    }

    /// <summary>
    /// Coroutine to shoot target.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootTarget()
    {
        while (target != null)
        {
            Vector3 position = target.transform.position;

            yield return new WaitForSeconds(delay);
            
            Vector3 direction = (position - this.transform.position);

            float targetAngle = Vector3.Angle
               (direction, this.transform.forward);

            if (targetAngle <= halfAngleShoot)
            {
                spaceship.Shoot();
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

    private void OnDestroy()
    {
        if (miniMap != null)
        {
            miniMap.UnregisterToMiniMap(dataInMiniMap);
            isInit = false;
        }       
    }

    private void OnDisable()
    {
        if (miniMap != null)
        {
            miniMap.UnregisterToMiniMap(dataInMiniMap);
            isInit = false;
        }
    }
}
