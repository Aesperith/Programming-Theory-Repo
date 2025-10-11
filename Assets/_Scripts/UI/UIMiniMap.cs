using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;


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

    private RectTransform playerIconRect;

    [SerializeField]
    private List<EnemyDataInMiniMap> enemyList = new();

    private float radiusWorld;
    private float radiusMap;
    private float scaleRatio;

    [Serializable]
    public struct EnemyDataInMiniMap
    {
        public GameObject enemyIcon;
        public RectTransform enemyIconRect;
        public Transform enemy;

        /// <summary>
        /// Update the enemy icon position in the minimap.
        /// </summary>
        public void UpdateInMiniMap(float scaleRatio)
        {
            enemyIconRect.anchoredPosition = new Vector3
            (
                enemy.position.x * scaleRatio,
                enemy.position.z * scaleRatio,
                0
            );

            Quaternion enemyIconRotation = Quaternion.Euler
                (0f, 0f, -enemy.rotation.eulerAngles.y);
            enemyIconRect.rotation = enemyIconRotation;
        }
    }

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerIconRect = playerIcon.GetComponent<RectTransform>();

        radiusWorld = playArea.Radius;
        radiusMap = miniMap.rectTransform.rect.width / 2f;
        radiusMap -= (0.1f * radiusMap);    // Sprite radius != miniMap radius
        scaleRatio = radiusMap / radiusWorld;
    }

    private void LateUpdate()
    {
        UpdatePlayerInMiniMap();    // ABSTRACTION
        UpdateEnemyInMiniMap();     // ABSTRACTION
    }

    /// <summary>
    /// Register the enemy in the minimap.
    /// </summary>
    /// <returns>Enemy data in minimap.</returns>
    public EnemyDataInMiniMap RegisterToMiniMap(Transform enemy)
    {
        EnemyDataInMiniMap enemyData = new()
        {
            enemy = enemy,
            enemyIcon = Instantiate
            (
                enemyIcon, miniMap.transform
            )
        };
        enemyData.enemyIconRect = enemyData.enemyIcon
            .GetComponent<RectTransform>();

        enemyList.Add(enemyData);

        return enemyData;
    }

    /// <summary>
    /// Unregister the enemy from the minimap.
    /// </summary>
    /// <param name="enemyData">Enemy data to remove.</param>
    public void UnregisterToMiniMap(EnemyDataInMiniMap enemyData)
    {
        Destroy(enemyData.enemyIcon);
        enemyList.Remove(enemyData);
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
    /// Update the enemy(ies) icon position in the minimap.
    /// </summary>
    private void UpdateEnemyInMiniMap()
    {
        foreach (var enemy in enemyList)
        {
            enemy.UpdateInMiniMap(scaleRatio);
        }
    }
}
