using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Logic
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<GameObject> enemyPrefabs;
        public List<Planet> planets;
        
        public List<GameObject> enemies;

        public float enemyLimit;

        public float startSpawnDelay;
        public float endSpawnDelay;
        public float delayIncreaseTime;

        public float minSpawnDistance;
        public float maxSpawnDistance;

        public bool spawn = true;

        public float spawnDelay;
        float increaseDelayTimer;

        Coroutine spawnCoroutine;

        void Start()
        {
            spawnDelay = startSpawnDelay;
            increaseDelayTimer = delayIncreaseTime;
            
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            yield return new WaitForSeconds(5.0f);
            
            while (isActiveAndEnabled)    
            {
                Debug.Log("Try to spawn");
                
                if (spawn && enemies.Count < enemyLimit)
                {
                    GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                    Transform target = planets[Random.Range(0, planets.Count)].transform;
                    Vector2 position = Random.insideUnitCircle * Random.Range(minSpawnDistance, maxSpawnDistance);
                    position = (Vector2) target.position + position;

                    GameObject enemy = Instantiate(prefab, position, Quaternion.identity, transform);
                    enemy.GetComponent<EnemyLogic>().targetPlanet = target.gameObject;
                    enemies.Add(enemy);
            
                    yield return new WaitForSeconds(spawnDelay);
                }
                
                yield return new WaitForSeconds(0.1f);
            }
        }

        void Update()
        {
            increaseDelayTimer -= Time.deltaTime;

            if (increaseDelayTimer <= 0.0f)
            {
                spawnDelay -= 0.25f;
                spawnDelay = Mathf.Clamp(spawnDelay, endSpawnDelay, startSpawnDelay);
                increaseDelayTimer = delayIncreaseTime;
            }

            Debug.Log(spawnCoroutine);
            
            if (spawnCoroutine == null)
            {
                spawnCoroutine = StartCoroutine(SpawnEnemies());
            }
        }

        void LateUpdate()
        {
            enemies.RemoveAll(x => x == null);
        }
    }
}