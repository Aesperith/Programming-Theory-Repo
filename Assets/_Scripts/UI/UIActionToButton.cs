using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIActionToButton : MonoBehaviour
{
    public string actionName;

    private Button button;
    private InputAction action;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        button = GetComponent<Button>();
        action = InputSystem.actions.FindAction(actionName);
    }

    // Update is called once per frame
    private void Update()
    {
        if (action.WasPressedThisFrame())
        {
            button.onClick.Invoke();
        }
    }
}
