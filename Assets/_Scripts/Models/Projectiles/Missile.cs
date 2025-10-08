using UnityEngine;

// INHERITANCE
public class Missile : Projectile
{
    public float speed = 20.0f;


    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
