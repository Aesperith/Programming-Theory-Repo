using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private Vector3 offset = new(0, 20, 0);


    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
