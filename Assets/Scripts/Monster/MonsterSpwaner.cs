using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float initialSpawnDelay;

    private List<MonsterDataLoader.MonsterData> monsterDataList;
    private bool isSpawning = false;
    private int currentMonsterIndex = 0;

    public void InitializeSpawner(List<MonsterDataLoader.MonsterData> monsterData)
    {
        monsterDataList = monsterData;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(InitialSpawn());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    IEnumerator InitialSpawn()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        SpawnMonster();
    }

    public void SpawnMonster()
    {
        if (!isSpawning || monsterDataList == null || monsterDataList.Count == 0)
            return;

        MonsterDataLoader.MonsterData selectedMonsterData = monsterDataList[currentMonsterIndex];
        string monsterPrefabName = selectedMonsterData.name;

        string fullPath = "Prefabs/Monsters/" + monsterPrefabName;
        Debug.Log("Attempting to load prefab at path: " + fullPath);

        GameObject monsterPrefab = Resources.Load<GameObject>(fullPath);

        if (monsterPrefab != null)
        {
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);

            Monster monsterComponent = spawnedMonster.GetComponent<Monster>();
            if (monsterComponent != null)
            {
                monsterComponent.Initialize(selectedMonsterData);

                var attackComponent = spawnedMonster.GetComponent<MonsterAttack>();
                var takeDamageComponent = spawnedMonster.GetComponent<MonsterTakeDamage>();
                var movementComponent = spawnedMonster.GetComponent<MonsterMovement>();

                if (attackComponent != null)
                {
                    attackComponent.InitializeAttack(1f);
                }

                if (takeDamageComponent != null)
                {
                    takeDamageComponent.health = selectedMonsterData.health;

                    takeDamageComponent.OnMonsterDeath += HandleMonsterDeath;
                }

                if (movementComponent != null)
                {
                    movementComponent.InitializeMovement(selectedMonsterData.speed);
                }
            }
            currentMonsterIndex++;
            if (currentMonsterIndex >= monsterDataList.Count)
            {
                currentMonsterIndex = 0;
            }
        }
    }

    void HandleMonsterDeath()
    {
        if (isSpawning)
        {
            SpawnMonster();
        }
    }
}