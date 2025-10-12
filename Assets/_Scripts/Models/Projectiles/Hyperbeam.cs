using UnityEngine;

public class Hyperbeam : Projectile
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
