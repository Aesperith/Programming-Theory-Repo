using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Spaceship spaceship;

    [SerializeField]
    private float halfAngleView = 15;

    [SerializeField]
    private float halfAngleShoot = 30;

    [SerializeField]
    private float delay = 0.5f;

    private UIMiniMap miniMap;
    private UIMiniMap.EnemyDataInMiniMap dataInMiniMap;

    private GameObject target;

    private SphereCollider detection;
    private float radiusDetection;

    private Vector2 input;
    private Coroutine followPlayer;
    private Coroutine shootPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        detection = GetComponent<SphereCollider>();
        radiusDetection= detection.radius;
        
        miniMap = GameObject.FindFirstObjectByType<UIMiniMap>();
        dataInMiniMap = miniMap.RegisterToMiniMap(spaceship.transform);
    }

    private void FixedUpdate()
    {
        spaceship.Move(input);  // ABSTRACTION
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
            followPlayer = StartCoroutine(FollowPlayer());
            shootPlayer = StartCoroutine(ShootPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(followPlayer);
            StopCoroutine(shootPlayer);
            target = null;
            input = Vector2.zero;
            spaceship.Stop();
        }
    }

    /// <summary>
    /// Coroutine to follow player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FollowPlayer()
    {
        while (target != null)
        {
            yield return new WaitForSeconds(delay);

            Vector3 direction = (target.transform.position
                - this.transform.position);

            float targetDistance = Vector3.Distance
                (this.transform.position, target.transform.position);

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
    /// Coroutine to shoot player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootPlayer()
    {
        while (target != null)
        {
            yield return new WaitForSeconds(delay);
            Vector3 direction = (target.transform.position
                - this.transform.position).normalized;

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
        miniMap.UnregisterToMiniMap(dataInMiniMap);
    }
}
