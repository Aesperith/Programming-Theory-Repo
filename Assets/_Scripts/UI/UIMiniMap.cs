using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI Minimap.
/// </summary>
public class UIMiniMap : MonoBehaviour
{
    [SerializeField]
    private Image miniMap;

    [SerializeField]
    private BoundariesManager playArea;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject playerIcon;

    [SerializeField]
    private GameObject enemyIcon;

    [SerializeField]
    private GameObject allyIcon;

    private RectTransform playerIconRect;

    [SerializeField]
    private List<ShipDataInMiniMap> shipsList = new();

    private float radiusWorld;
    private float radiusMap;
    private float scaleRatio;

    /// <summary>
    /// Type of icon.
    /// </summary>
    public enum TypeIcon
    {
        Enemy,
        Ally
    }

    /// <summary>
    /// Other ship data in the minimap.
    /// </summary>
    [Serializable]
    public struct ShipDataInMiniMap
    {
        public GameObject shipIcon;
        public RectTransform shipIconRect;
        public Transform ship;

        /// <summary>
        /// Update the enemy icon position in the minimap.
        /// </summary>
        public readonly void UpdateInMiniMap(float scaleRatio)
        {
            if (shipIcon != null && shipIconRect != null && ship != null)
            {
                shipIconRect.anchoredPosition = new Vector3
                (
                    ship.position.x * scaleRatio,
                    ship.position.z * scaleRatio,
                    0
                );

                Quaternion shipIconRotation = Quaternion.Euler
                    (0f, 0f, -ship.rotation.eulerAngles.y);
                shipIconRect.rotation = shipIconRotation;
            }
        }
    }


    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        playerIconRect = playerIcon.GetComponent<RectTransform>();

        radiusWorld = playArea.Radius;
        radiusMap = miniMap.rectTransform.rect.width / 2f;
        radiusMap *= 0.9f;   // Sprite radius != miniMap radius
        scaleRatio = radiusMap / radiusWorld;
    }

    private void LateUpdate()
    {
        UpdatePlayerInMiniMap();    // ABSTRACTION
        UpdateShipsInMiniMap();     // ABSTRACTION
    }

    /// <summary>
    /// Set the player for the minimap to follow.
    /// </summary>
    /// <param name="player">Player to follow.</param>
    public void SetPlayer(GameObject player)
    {
        this.player = player.transform;
    }

    /// <summary>
    /// Register the ship in the minimap.
    /// <param name="ship">The transform of the ship.</param>
    /// <param name="type">The type of icon on the minimap.</param>
    /// <returns>Ship data in the minimap.</returns>
    public ShipDataInMiniMap RegisterToMiniMap(Transform ship, TypeIcon type)
    {
        ShipDataInMiniMap shipData = new()
        {
            ship = ship,
            shipIcon = type switch
            {
                TypeIcon.Enemy => Instantiate(enemyIcon, miniMap.transform),
                TypeIcon.Ally => Instantiate(allyIcon, miniMap.transform),
                _ => Instantiate(enemyIcon, miniMap.transform)
            }
        };

        shipData.shipIconRect = shipData.shipIcon
            .GetComponent<RectTransform>();

        shipsList.Add(shipData);

        return shipData;
    }

    /// <summary>
    /// Unregister the ship from the minimap.
    /// </summary>
    /// <param name="shipData">Ship data to remove.</param>
    public void UnregisterToMiniMap(ShipDataInMiniMap shipData)
    {
        Destroy(shipData.shipIcon);
        shipsList.Remove(shipData);
    }

    /// <summary>
    /// Update the player icon position in the minimap;
    /// </summary>
    private void UpdatePlayerInMiniMap()
    {
        playerIconRect.anchoredPosition = new Vector3
        (
            player.transform.position.x * scaleRatio,
            player.transform.position.z * scaleRatio,
            0
        );

        Quaternion playerIconRotation = Quaternion.Euler
            (0f, 0f, -player.rotation.eulerAngles.y);
        playerIconRect.rotation = playerIconRotation;
    }

    /// <summary>
    /// Update the other ship(s) icon(s) position in the minimap.
    /// </summary>
    private void UpdateShipsInMiniMap()
    {
        foreach (var ship in shipsList)
        {
            ship.UpdateInMiniMap(scaleRatio);
        }
    }
}
