using UnityEngine;

/// <summary>
/// Camera's component for following the player.
/// </summary>
[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new(0, 20, 0);

    private GameObject player;
    

    private void Start()
    {
        player = GameObject.Find("Player");
    }


    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.eulerAngles = new Vector3
        (
            transform.eulerAngles.x, 
            player.transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }
}
