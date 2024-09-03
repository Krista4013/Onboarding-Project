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

        string fullPath = "Prefabs/Monsters/" + monsterPrefabName;
        Debug.Log("Attempting to load prefab at path: " + fullPath);

        GameObject monsterPrefab = Resources.Load<GameObject>(fullPath);

        if (monsterPrefab != null)
        {
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);

            Monster monsterComponent = spawnedMonster.GetComponent<Monster>();
            if (monsterComponent != null)
            {
                // 몬스터 초기화
                monsterComponent.Initialize(selectedMonsterData);

                // 이벤트 핸들러 연결
                monsterComponent.OnMonsterDeath += HandleMonsterDeath;

                // 필요한 하위 컴포넌트 초기화
                var attackComponent = spawnedMonster.GetComponent<MonsterAttack>();
                var takeDamageComponent = spawnedMonster.GetComponent<MonsterTakeDamage>();
                var movementComponent = spawnedMonster.GetComponent<MonsterMovement>();

                if (attackComponent != null)
                {
                    attackComponent.InitializeAttack(1f); // 공격 딜레이를 설정합니다.
                }

                if (takeDamageComponent != null)
                {
                    takeDamageComponent.health = selectedMonsterData.health; // 체력 설정
                }

                if (movementComponent != null)
                {
                    movementComponent.InitializeMovement(selectedMonsterData.speed); // 속도 설정
                }
            }
        }
        else
        {
            Debug.LogError("Prefab not found at path: " + fullPath);
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