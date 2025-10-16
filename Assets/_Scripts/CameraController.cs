using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the camera. It follows the player and can adjust the zoom
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float stickMinZoom = -20;

    [SerializeField]
    private float stickMaxZoom = -10;

    [SerializeField, Range(0f, 1f)]
    private float zoomSensitivity = 0.025f;

    private Camera mainCamera;

    private Transform swivel;

    private Transform stick;

    private GameObject player;

    private float zoom = 1f;

    private bool isReady;

    private InputAction zoomAction;


    private void Awake()
    {
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
        mainCamera = stick.GetComponentInChildren<Camera>();

        zoomAction = InputSystem.actions.FindAction("Zoom");
    }


    private void Update()
    {
        if (isReady)
        {
            float zoomDelta = zoomAction.ReadValue<Vector2>().y * zoomSensitivity;
            if (zoomDelta != 0f)
            {
                AdjustZoom(zoomDelta);
            }
        }
    }

    private void LateUpdate()
    {
        if (isReady)
        {
            FollowPlayer(); // ABSTRACTION
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

    /// <summary>
    /// Follow the player movement and rotation.
    /// </summary>
    private void FollowPlayer()
    {
        transform.position = player.transform.position;
        
        swivel.transform.eulerAngles = new Vector3
        (
            swivel.transform.eulerAngles.x,
            player.transform.eulerAngles.y,
            swivel.transform.eulerAngles.z
        );
    }

    /// <summary>
    /// Adjust the zoom of the camera.
    /// </summary>
    /// <param name="delta">Delta value to zoom.</param>
    private void AdjustZoom(float delta)
    {
        zoom = Mathf.Clamp01(zoom + delta);

        float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
        stick.localPosition = new Vector3(0f, 0f, distance);
    }
}
