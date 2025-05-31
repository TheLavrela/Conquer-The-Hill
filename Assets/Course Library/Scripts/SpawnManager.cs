using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;  
    public Transform enemyParent;
    public Transform powerupParent;

    private float randomRange = 9f;

    private int waveNumber = 1;
    private int powerupsLimit = 5;
    private GameObject[] enemyCount;
    private GameObject[] powerupCount;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;


    void Start() 
    {
       
    }

    private void Update()
    {
        checkWaves();


    }

    void checkWaves()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        powerupCount = GameObject.FindGameObjectsWithTag("Powerup");

        // if all enemies are died, spawn a new wave of enemies and increase the wave number
        if (enemyCount.Length <= 0)
        {
            SpawnEnemies(waveNumber);
            waveNumber++;

            if (powerupCount.Length < powerupsLimit)
            {
                SpawnPowerup(1);
            }
            Debug.Log("WAVE: " + waveNumber);

            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
           

        }
        
      
    }

    void SpawnEnemies(int enemies)
    {
        for (int i = 0; i < enemies; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], RandomPos(false), enemyPrefabs[randomEnemy].transform.rotation);
            
        }
    }

    void SpawnPowerup(int powerups)
    {
        for (int i = 0; i < powerups; i++)
        {
            //spawn a powerUP
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], RandomPos(false), powerupPrefabs[randomPowerup].transform.rotation);
        }
    }

    private Vector3 RandomPos(bool integrer)
    {
        float randomX;
        float randomZ;

        if (integrer)
        {
            randomX = Random.Range((int)randomRange, (int)-randomRange);
            randomZ = Random.Range((int)randomRange, (int)-randomRange);
        }
        else
        {
            randomX = Random.Range(randomRange, -randomRange);
            randomZ = Random.Range(randomRange, -randomRange);
        }

        Vector3 randomPos = new Vector3(randomX,transform.position.y, randomZ);
        return randomPos;
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;

        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab,RandomPos(true),bossPrefab.transform.rotation);

    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], RandomPos(true), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}