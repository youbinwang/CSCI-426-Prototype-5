using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyGameObjects;
    public float spawnTime = 1f;
    public float radiusMin;
    public float radiusMax;
    public Transform playerTransform;
    public Vector2 scaleRange = new Vector2(0.5f, 4f);

    //在音乐开始播放后才开始生成敌人
    //public BeatMatching beatMatching;
    //private bool isStartSpawn;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());

        //isStartSpawn = false;
    }

    private void Update()
    {
        //if (beatMatching.isStartPlay && !isStartSpawn)
        //{
        //    isStartSpawn = true;
        //    StartCoroutine(SpawnEnemies());
        //}
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            float spawnDistance = Random.Range(radiusMin, radiusMax);
            Vector2 spawnPosition = (Vector2)playerTransform.position + spawnDirection * spawnDistance;

          
            float randomScale = Random.Range(scaleRange.x, scaleRange.y);
            Vector3 spawnScale = new (randomScale, randomScale, 1);

           
            int randomIndex = Random.Range(0, enemyGameObjects.Length);
            GameObject enemyPrefab = enemyGameObjects[randomIndex];
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.localScale = spawnScale;
        }
    }
}
