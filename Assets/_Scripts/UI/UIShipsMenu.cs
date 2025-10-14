using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the Ships menu for selecting the player's spaceship.
/// </summary>
public class UIShipsMenu : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttons;

    private List<PlayerDataManager.SpaceShipType> types = new();


    private void Awake()
    {
        GetTypes();
    }

    private void OnEnable()
    {
        UpdateShipsSelection();
    }

    /// <summary>
    /// Get all the type of ship.
    /// </summary>
    private void GetTypes()
    {
        types.Add(PlayerDataManager.SpaceShipType.Fighter);
        types.Add(PlayerDataManager.SpaceShipType.Bomber);
        types.Add(PlayerDataManager.SpaceShipType.Corvette);
        types.Add(PlayerDataManager.SpaceShipType.Destroyer);
        types.Add(PlayerDataManager.SpaceShipType.Carrier);
        types.Add(PlayerDataManager.SpaceShipType.Battlecruiser);
    }

    /// <summary>
    /// Update the selectable ship for the player.
    /// Only ship unlocked by the player is selectable.
    /// </summary>
    private void UpdateShipsSelection()
    {
        for (int i = 0; i < types.Count; i++)
        {
            if (PlayerDataManager.Instance.CurrentShip != types[i])
            {
                if (PlayerDataManager.Instance.CheckUnlocked(types[i]))
                {
                    buttons[i].interactable = true;
                }
                else
                {
                    buttons[i].interactable = false;
                }
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
}
