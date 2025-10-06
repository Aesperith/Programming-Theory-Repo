using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;
    

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
