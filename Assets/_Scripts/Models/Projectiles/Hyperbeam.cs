using UnityEngine;

/// <summary>
/// Hyperbeam projectile from Special Power <see cref="ShootHyperBeam"/>.
/// Inherits from <see cref="Projectile"/>.
/// </summary>
public class Hyperbeam : Projectile // INHERITANCE
{
    private void Start()
    {
        speed = 40.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
