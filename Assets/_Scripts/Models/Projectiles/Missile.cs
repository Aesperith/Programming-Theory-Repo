using UnityEngine;

/// <summary>
/// Missile.
/// Inherits from <see cref="Projectile"/>.
/// </summary>
public class Missile : Projectile   // INHERITANCE
{
    private void Start()
    {
        speed = 20.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
