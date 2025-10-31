using UnityEngine;

/// <summary>
/// Missile.
/// Inherits from <see cref="Projectile"/>.
/// </summary>
public class Missile : Projectile   // INHERITANCE
{
    [SerializeField]
    private MissileController controller;

    [SerializeField]
    private ParticleSystem explosionVFX;

    private string targetTag;

    public float explosionDuration;

    private void Start()
    {
        speed = 20.0f;
        explosionDuration = explosionVFX.main.duration;
        targetTag = source == "Enemy" ? "Player" : "Enemy";
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            PlayExplosion();
        }
    }

    /// <summary>
    /// Play the explosion VFX of the missile.
    /// </summary>
    public void PlayExplosion()
    {
        explosionVFX.Play();
    }

    /// <summary>
    /// Activate the MissileController.
    /// </summary>
    public void ActivateController()
    {
        controller.Activate();
    }
}
