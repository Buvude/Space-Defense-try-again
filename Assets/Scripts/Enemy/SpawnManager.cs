using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int maxNumOfEnemies;
    public GameObject[] spawnPoints;
    private int numOfEnemiesLeft;
    private int numOfEnemies = 5;//might need to make this public
    public GameObject enemy;
    private Vector3 whereToSpawn;
    public int enemyCount;
    private float spawnRange = 9;
    public int waveNumber = 1;
    private int randomChoice;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
       /* whereToSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        SpawnEnemyWave(numOfEnemies + waveNumber);*/
        StartCoroutine(SpawnNewEnemy());
    }
    //This will probably need to change to specific spawn points once we have the map implimented, but good start
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            GameObject[] tempEnemyList;
            tempEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject e in tempEnemyList)
            {
                if (e.GetComponent<NMA>().CurrentState != NMA.EnemyState.Dead)
                {
                    numOfEnemiesLeft++;
                }
            }
            if (enemyCount == 0||gameManager.getSecondsLeft()<=0 && gameManager.isGameActive)
            {
                waveNumber++;
                SpawnEnemyWave(numOfEnemies + waveNumber);
                gameManager.resetRountTimer();
            }
            yield return new WaitForSeconds(5);
        }
        
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            randomChoice = Random.Range(0, spawnPoints.Length);
            whereToSpawn = spawnPoints[randomChoice].transform.position;
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
        }
    }
}
