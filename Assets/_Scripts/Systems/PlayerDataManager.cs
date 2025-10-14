using System;
using UnityEngine;
using System.IO;

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

    /// <summary>
    /// Spaceship unlocked by the player.
    /// </summary>
    [Flags]
    public enum UnlockedShip
    {
        None = 0,
        Fighter = 1,
        Bomber = 1 << 1,
        Corvette = 1 << 2,
        Destroyer = 1 << 3,
        Carrier = 1 << 4,
        Battlecruiser = 1 << 5,
    }

    public string PlayerName { get; set; }

    public SpaceShipType CurrentShip { get; set; }

    public int Credits { get; set; }

    public UnlockedShip Unlocked { get; private set; }


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

        Unlocked |= UnlockedShip.Fighter;
        //LoadPlayerData();
    }

    /// <summary>
    /// Data to save.
    /// </summary>
    private class SaveData
    {
        public string PlayerName;
        public int Credits;
        public int CurrentShip;
        public int Unlocked;
    }

    /// <summary>
    /// Save the player data in a .json file.
    /// </summary>
    public void SavePlayerData()
    {
        SaveData data = new()
        {
            PlayerName = PlayerName,
            Credits = Credits,
            CurrentShip = (int) CurrentShip,
            Unlocked = (int) Unlocked
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText
            (Application.persistentDataPath + "/savefile.json", json);
    }

    /// <summary>
    /// Load the player data from a .json file.
    /// </summary>
    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
            Credits = data.Credits;
            CurrentShip = (SpaceShipType) data.CurrentShip;
            Unlocked |= (UnlockedShip) data.Unlocked;
        }
    }

    /// <summary>
    /// Unlock the ship for the player.
    /// </summary>
    /// <param name="shipType">The type of ship to unlock.</param>
    public void UnlockShip(SpaceShipType shipType)
    {
        switch (shipType)
        {
            case SpaceShipType.Fighter:
                Unlocked |= UnlockedShip.Fighter;
                break;
            case SpaceShipType.Bomber:
                Unlocked |= UnlockedShip.Bomber;
                break;
            case SpaceShipType.Corvette:
                Unlocked |= UnlockedShip.Corvette;
                break;
            case SpaceShipType.Destroyer:
                Unlocked |= UnlockedShip.Destroyer;
                break;
            case SpaceShipType.Carrier:
                Unlocked |= UnlockedShip.Carrier;
                break;
            case SpaceShipType.Battlecruiser:
                Unlocked |= UnlockedShip.Battlecruiser;
                break;
            default:
                Unlocked |= UnlockedShip.None;
                break;
        }
    }

    /// <summary>
    /// Check if the type of ship is unlocked.
    /// </summary>
    /// <param name="shipType">Type of ship.</param>
    /// <returns>True: unlocked; False: locked.</returns>
    public bool CheckUnlocked(SpaceShipType shipType)
    {
        switch (shipType)
        {
            case SpaceShipType.Fighter:
                return (Unlocked & UnlockedShip.Fighter) != 0;
            case SpaceShipType.Bomber:
                return (Unlocked & UnlockedShip.Bomber) != 0;
            case SpaceShipType.Corvette:
                return (Unlocked & UnlockedShip.Corvette) != 0;
            case SpaceShipType.Destroyer:
                return (Unlocked & UnlockedShip.Destroyer) != 0;
            case SpaceShipType.Carrier:
                return (Unlocked & UnlockedShip.Carrier) != 0;
            case SpaceShipType.Battlecruiser:
                return (Unlocked & UnlockedShip.Battlecruiser) != 0;
            default:
                return false;
        }
    }
}
