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

    private bool isReady;


    private void LateUpdate()
    {
        if (isReady)
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

    /// <summary>
    /// Set the player for the camera to follow.
    /// </summary>
    /// <param name="player">Player to follow.</param>
    public void SetPlayer(GameObject player)
    {
        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            this.player = player;
            isReady = true;
        }
    }
}
