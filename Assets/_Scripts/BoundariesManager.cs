using UnityEngine;

/// <summary>
/// Manages the play area limit.
/// <para>Keeps player and others inside the boundaries.</para> 
/// Disables all projectiles that leave the boundaries.
/// </summary>
[RequireComponent (typeof(SphereCollider))]
public class BoundariesManager : MonoBehaviour
{
    private SphereCollider sphereCollider;

    public Vector3 Center
    {
        get { return sphereCollider.center; }
    }

    public float Radius
    {
        get { return sphereCollider.radius; }
    }


    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider> ();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Player") 
            || other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                KeepInSphereBounds(other.gameObject);
            }
        }
    }

    /// <summary>
    /// Keep player inside the sphere bounds.
    /// </summary>
    /// <param name="gameObject">The player.</param>
    private void KeepInSphereBounds(GameObject gameObject)
    {
        Vector3 pos = gameObject.transform.position;       
        float angle = Mathf.Atan2(pos.z, pos.x);     
        float distance = Mathf.Clamp(pos.magnitude, 0.0f, Radius);       
        pos.x = Mathf.Cos(angle) * distance;
        pos.z = Mathf.Sin(angle) * distance;
        gameObject.transform.position = pos;
    }
}
