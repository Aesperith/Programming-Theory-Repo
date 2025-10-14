using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the shop menu for unlocking new spaceship 
/// for the player.
/// </summary>
public class UIShopMenu : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttons;
    
    private GameManager gameManager;

    private List<Item> items = new();

    /// <summary>
    /// The item for sale. 
    /// </summary>
    private class Item
    {
        public string Name;
        public int Price;
        public PlayerDataManager.SpaceShipType Type;
    }

    private void Awake()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        InitShop();     // ABSTRACTION
    }

    private void OnEnable()
    {
        UpdateShop();   // ABSTRACTION
    }

    /// <summary>
    /// Purchase the ship.
    /// </summary>
    /// <param name="index">Index of the item.</param>
    public void PurchaseIt(int index)
    {
        buttons[index].gameObject.SetActive(false);
        PlayerDataManager.Instance.Credits -= items[index].Price;
        PlayerDataManager.Instance.UnlockShip(items[index].Type);

        gameManager.UpdateScore();
        UpdateShop();
    }

    /// <summary>
    /// Initialize all items for sale.
    /// </summary>
    private void InitItems()
    {
        Item item = new()
        {
            Name = nameof(Bomber),
            Price = Bomber.Price,
            Type = PlayerDataManager.SpaceShipType.Bomber
        };

        items.Add(item);

        item = new()
        {
            Name = nameof(Corvette),
            Price = Corvette.Price,
            Type = PlayerDataManager.SpaceShipType.Corvette
        };

        items.Add(item);

        item = new()
        {
            Name = nameof(Destroyer),
            Price = Destroyer.Price,
            Type = PlayerDataManager.SpaceShipType.Destroyer
        };

        items.Add(item);

        item = new()
        {
            Name = nameof(Carrier),
            Price = Carrier.Price,
            Type = PlayerDataManager.SpaceShipType.Carrier
        };

        items.Add(item);

        item = new()
        {
            Name = nameof(Battlecruiser),
            Price = Battlecruiser.Price,
            Type = PlayerDataManager.SpaceShipType.Battlecruiser
        };

        items.Add(item);
    }

    /// <summary>
    /// Initialize all item names and prices in the shop menu.
    /// </summary>
    private void InitShop()
    {
        InitItems(); // ABSTRACTION

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = items[i].Name
                + Environment.NewLine + $"{items[i].Price}";
        }
    }

    /// <summary>
    /// Update the purchasable items.
    /// </summary>
    private void UpdateShop()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (buttons[i].IsActive())
            {
                if (PlayerDataManager.Instance.Credits >= items[i].Price)
                {
                    buttons[i].interactable = true;
                }
                else
                {
                    buttons[i].interactable = false;
                }
            }
        }
    }
}
