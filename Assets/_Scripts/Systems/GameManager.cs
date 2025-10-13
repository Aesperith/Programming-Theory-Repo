using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Main Game.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private List<GameObject> playerShipsPrefabs;

    [SerializeField]
    private List<GameObject> smallShipsPrefabs;

    [SerializeField]
    private List<GameObject> mediumShipsPrefabs;

    [SerializeField]
    private List<GameObject> heavyShipsPrefabs;

    [SerializeField]
    private BoundariesManager spawnZone;

    [SerializeField]
    private Transform parentSpawn;

    private PauseSystem pauseSystem;

    private UIMiniMap minimap;

    public bool isGameActive;
    public bool isGameOver;

    private int points;
    private int waveNumber = 1;
    private int enemyCount;

    private const int smallShipMax = 3;
    private const int mediumShipMax = 3;


    private void Awake()
    {
        pauseSystem = GameObject.FindFirstObjectByType<PauseSystem>();
        minimap = GameObject.FindFirstObjectByType<UIMiniMap>();

        SetupPlayer();
    }

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        isGameActive = true;
        SpawnRandomEnemyWave(waveNumber);   // ABSTRACTION
    }

    // Update is called once per frame
    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0 && !isGameOver)
        {
            if (waveNumber < 9)
            {
                waveNumber++;
            }

            SpawnRandomEnemyWave(waveNumber);   // ABSTRACTION
        }
    }

    /// <summary>
    /// Add point to the total points
    /// and update the UI label text.
    /// </summary>
    /// <param name="point">Point to add to total.</param>
    public void AddPoint(int point)
    {
        points += point;
        score.text = $"Credits: {points}";
    }

    /// <summary>
    /// Manage the GameOver screen.
    /// </summary>
    public void GameOver()
    {
        if (pauseSystem != null)
        {
            pauseSystem.PauseGame();
        }
        
        isGameActive = false;
        isGameOver = true;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Restart game by reloading the scene.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Set the player ship.
    /// </summary>
    /// <param name="ship">0: Fighter, 1: Bomber, 2: Corvette,
    /// 3: Destroyer, 4: Carrier, 5: Battlecruiser.</param>
    public void SetPlayerShip(int ship)
    {
        PlayerDataManager.Instance.SpaceShip = (PlayerDataManager.SpaceShipType)ship;
        RestartGame();
    }

    /// <summary>
    /// Spawn random X enemy by wave.
    /// </summary>
    /// <param name="enemiesToSpawn">Number of ennemies to spawn.</param>
    private void SpawnRandomEnemyWave(int enemiesToSpawn)
    {
        if (enemiesToSpawn <= smallShipMax)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                InstantiateRandomSmallShip();   // ABSTRACTION
            }
        }
        else if(enemiesToSpawn <= smallShipMax + mediumShipMax)
        {
            for (int i = 0; i < smallShipMax; i++)
            {
                InstantiateRandomSmallShip();   // ABSTRACTION
            }

            int mediumShipToSpawn = enemiesToSpawn - smallShipMax;

            for (int i = 0; i < mediumShipToSpawn; i++)
            {
                InstantiateRandomMediumShip();  // ABSTRACTION
            }
        }
        else
        {
            for (int i = 0; i < smallShipMax; i++)
            {
                InstantiateRandomSmallShip();   // ABSTRACTION
            }

            for (int i = 0; i < mediumShipMax; i++)
            {
                InstantiateRandomMediumShip();  // ABSTRACTION
            }

            int heavyShipToSpawn = enemiesToSpawn - smallShipMax - mediumShipMax;

            for (int i = 0; i < heavyShipToSpawn; i++)
            {
                InstantiateRandomHeavyShip();   // ABSTRACTION
            }
        }
    }

    /// <summary>
    /// Instantiate a random SmallShip 
    /// in a random position in the play area.
    /// </summary>
    private void InstantiateRandomSmallShip()
    {
        int index = Random.Range(0, smallShipsPrefabs.Count);
        GameObject smallship = smallShipsPrefabs[index];

        var spaceship = Instantiate
        (
            smallship,
            RandomPositionInPlayArea(),
            Quaternion.identity,
            parentSpawn
        );

        spaceship.GetComponent<Spaceship>().onDestroyed.AddListener(AddPoint);
    }

    /// <summary>
    /// Instantiate a random MediumShip 
    /// in a random position in the play area.
    /// </summary>
    private void InstantiateRandomMediumShip()
    {
        int index = Random.Range(0, mediumShipsPrefabs.Count);
        GameObject mediumship = mediumShipsPrefabs[index];

        var spaceship = Instantiate
        (
            mediumship,
            RandomPositionInPlayArea(),
            Quaternion.identity,
            parentSpawn
        );

        spaceship.GetComponent<Spaceship>().onDestroyed.AddListener(AddPoint);
    }

    /// <summary>
    /// Instantiate a random HeavyShip 
    /// in a random position in the play area.
    /// </summary>
    private void InstantiateRandomHeavyShip()
    {
        int index = Random.Range(0, heavyShipsPrefabs.Count);
        GameObject heavyship = heavyShipsPrefabs[index];

        var spaceship = Instantiate
        (
            heavyship,
            RandomPositionInPlayArea(),
            Quaternion.identity,
            parentSpawn
        );

        spaceship.GetComponent<Spaceship>().onDestroyed.AddListener(AddPoint);
    }

    /// <summary>
    /// Return a random position in the center of 
    /// the play area
    /// </summary>
    /// <returns>Random position.</returns>
    private Vector3 RandomPositionInPlayArea()
    {
        Vector3 randPos = new
        (
            Random.Range(-spawnZone.Radius * 0.8f, spawnZone.Radius * 0.8f),
            0f,
            Random.Range(-spawnZone.Radius * 0.8f, spawnZone.Radius * 0.8f)
        );

        return randPos;
    }

    /// <summary>
    /// Setup the player spaceship, camera and minimap. 
    /// </summary>
    private void SetupPlayer()
    {
        GameObject player = PlayerDataManager.Instance.SpaceShip switch
        {
            PlayerDataManager.SpaceShipType.Fighter => Instantiate(playerShipsPrefabs[0], parentSpawn),
            PlayerDataManager.SpaceShipType.Bomber => Instantiate(playerShipsPrefabs[1], parentSpawn),
            PlayerDataManager.SpaceShipType.Corvette => Instantiate(playerShipsPrefabs[2], parentSpawn),
            PlayerDataManager.SpaceShipType.Destroyer => Instantiate(playerShipsPrefabs[3], parentSpawn),
            PlayerDataManager.SpaceShipType.Carrier => Instantiate(playerShipsPrefabs[4], parentSpawn),
            PlayerDataManager.SpaceShipType.Battlecruiser => Instantiate(playerShipsPrefabs[5], parentSpawn),
            _ => Instantiate(playerShipsPrefabs[0], parentSpawn),
        };

        mainCamera.GetComponent<FollowPlayer>().SetPlayer(player);
        minimap.SetPlayer(player);
    }
}
