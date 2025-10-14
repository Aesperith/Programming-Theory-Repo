using UnityEngine;

/// <summary>
/// Make UI not rotate because of the GameObject rotation.
/// </summary>
public class UINonRotate : MonoBehaviour
{
    private Camera mainCamera;


    private void Awake()
    {
        mainCamera = GameObject.FindFirstObjectByType<Camera>();
    }

    private void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
