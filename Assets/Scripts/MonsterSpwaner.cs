using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;  // 몬스터 스폰 위치
    public float initialSpawnDelay = 8f;  // 초기 스폰 대기 시간

    private List<MonsterDataLoader.MonsterData> monsterDataList;
    private bool isSpawning = false;

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
        if (!isSpawning) return;

        int randomIndex = Random.Range(0, monsterDataList.Count);

        MonsterDataLoader.MonsterData selectedMonsterData = monsterDataList[randomIndex];
        string monsterPrefabName = selectedMonsterData.name;

        // Resources 폴더 내의 경로를 맞춰줍니다. 예를 들어, Prefabs 폴더 내에 있을 경우:
        string fullPath = "Prefabs/" + monsterPrefabName;
        GameObject monsterPrefab = Resources.Load<GameObject>(fullPath);

        if (monsterPrefab != null)
        {
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);

            Monster monsterComponent = spawnedMonster.GetComponent<Monster>();
            if (monsterComponent != null)
            {
                monsterComponent.Initialize(selectedMonsterData);
                monsterComponent.OnMonsterDeath += HandleMonsterDeath;
            }
        }
        else
        {
            Debug.LogError("not found" + monsterPrefabName);
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