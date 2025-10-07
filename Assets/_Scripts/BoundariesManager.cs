using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class BoundariesManager : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private Vector3 center;
    private float radius;

    
    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        center = sphereCollider.center;
        radius = sphereCollider.radius;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                KeepInSphereBounds(other.gameObject);
            }
        }
    }

    private void KeepInSphereBounds(GameObject gameObject)
    {
        Vector3 pos = gameObject.transform.position;
        float angle = Mathf.Atan2(pos.z, pos.x);
        float distance = Mathf.Clamp(pos.magnitude, 0.0f, radius);
        pos.x = Mathf.Cos(angle) * distance;
        pos.z = Mathf.Sin(angle) * distance;
        gameObject.transform.position = pos;
    }
}
