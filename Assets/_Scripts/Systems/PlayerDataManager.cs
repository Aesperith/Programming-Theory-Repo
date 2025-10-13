using UnityEngine;

/// <summary>
/// Singleton who manages player data.
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    /// <summary>
    /// Type of spaceship of the player.
    /// </summary>
    public enum SpaceShipType
    {
        Fighter,
        Bomber,
        Corvette,
        Destroyer,
        Carrier,
        Battlecruiser
    }

    public string PlayerName { get; set; }

    public SpaceShipType SpaceShip { get; set; }

    public int Credits { get; set; }


    private void Awake()
    {
        // PlayerDataManager is a Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private class SaveData
    {
        public string PlayerName;
        public int Spaceship;
        public int Credits;
    }

    public void SavePlayerData()
    {
    }

    public void LoadPlayerData()
    {
    }
}
