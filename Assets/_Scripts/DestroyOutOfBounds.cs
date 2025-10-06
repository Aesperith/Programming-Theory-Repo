using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private const float zRange = 20;
    private const float xRange = 40;


    // Update is called once per frame
    private void Update()
    {
        if (transform.position.z > zRange)
        {
            // Instead of destroying the projectile when it leaves the screen
            //Destroy(gameObject);

            // Just deactivate it
            gameObject.SetActive(false);
        }

        if (transform.position.z < -zRange)
        {
            // Instead of destroying the projectile when it leaves the screen
            //Destroy(gameObject);

            // Just deactivate it
            gameObject.SetActive(false);
        }

        if (transform.position.x > xRange)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -xRange)
        {
            Destroy(gameObject);
        }
    }
}
