using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> spawnPoints;
    public int enemyChose;
    public int enemyChose1;
    public int spawnChose;
    public float cooldownForSpawn;

    public GameObject bossObject;
    public Transform bossSpawnTranform;

    public bool canSpawnEnemies;

    float cooldownCount;
    bool can = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyChose = 1;
        //canSpawnEnemies = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSpawnEnemies == true)
        {
            spawnMultiEnemy();
            chanceCooldown();
            Cooldown();
            StartBoss();
        }
        
    }

    void spawnMultiEnemy()
    {
        if (can == true) 
        {

             if  (InfoStats.Instance.elapsedTime >60)
            {
                enemyChose = Random.Range(0,enemies.Count);
            }else if (InfoStats.Instance.elapsedTime > 30)
            {
                enemyChose = Random.Range(0, enemies.Count - 1);
            }

            spawnChose = Random.Range(0, spawnPoints.Count);
            GameObject newEnemy = Instantiate(enemies[enemyChose], spawnPoints[spawnChose].transform.position, Quaternion.identity);

            if (InfoStats.Instance.elapsedTime > 30)
            {
                if (enemyChose == 0)
                {
                newEnemy.GetComponent<RangedBehaviorEnemy>().objectLimits = spawnPoints[spawnChose];
                }

            }
            if (InfoStats.Instance.elapsedTime > 120)
            {
                enemyChose1 = Random.Range(0, enemies.Count);
                GameObject newEnemy1 = Instantiate(enemies[enemyChose1], spawnPoints[spawnChose].transform.position, Quaternion.identity);
                if (enemyChose1 == 0)
                {
                    newEnemy1.GetComponent<RangedBehaviorEnemy>().objectLimits = spawnPoints[spawnChose];
                }
            }

                can = false;
            cooldownCount = 0;
        }
    }

    void chanceCooldown()
    {
        if (InfoStats.Instance.elapsedTime > 10)
        {
            cooldownForSpawn = 2.5f;
        }
        else if (InfoStats.Instance.elapsedTime > 60)
        {
            cooldownForSpawn = 1;
        }
        else if (InfoStats.Instance.elapsedTime > 90)
        {
            cooldownForSpawn = 0.5f;
        }
    }
    
    void Cooldown()
    {
        if (can == false && cooldownCount > cooldownForSpawn)
        {
            can = true;

        }
        else
        {
            cooldownCount += Time.deltaTime;
        }
    }

    void StartBoss()
    {
        if (InfoStats.Instance.elapsedTime >= 120 && HudManager.Instance.points >= 50)
        {
            GameObject newBoss = Instantiate(bossObject,bossSpawnTranform);
            canSpawnEnemies = false;
        }
    }
}
