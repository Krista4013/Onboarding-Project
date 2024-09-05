using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;  // 몬스터 스폰 위치
    public float initialSpawnDelay;  // 초기 스폰 대기 시간

    private List<MonsterDataLoader.MonsterData> monsterDataList;
    private bool isSpawning = false;
    private int currentMonsterIndex = 0;  // 현재 몬스터 인덱스

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
                // 몬스터 초기화
                monsterComponent.Initialize(selectedMonsterData);

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

                    takeDamageComponent.OnMonsterDeath += HandleMonsterDeath;
                }

                if (movementComponent != null)
                {
                    movementComponent.InitializeMovement(selectedMonsterData.speed); // 속도 설정
                }
            }

            // 다음 몬스터를 위한 인덱스 증가
            currentMonsterIndex++;
            if (currentMonsterIndex >= monsterDataList.Count)
            {
                // 마지막 몬스터가 생성된 후 인덱스를 0으로 리셋하여 반복하도록 설정
                currentMonsterIndex = 0;
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