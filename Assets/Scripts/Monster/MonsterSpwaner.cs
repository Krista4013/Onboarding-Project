using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;  // ���� ���� ��ġ
    public float initialSpawnDelay = 8f;  // �ʱ� ���� ��� �ð�

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
                // ���� �ʱ�ȭ
                monsterComponent.Initialize(selectedMonsterData);

                // �̺�Ʈ �ڵ鷯 ����
                monsterComponent.OnMonsterDeath += HandleMonsterDeath;

                // �ʿ��� ���� ������Ʈ �ʱ�ȭ
                var attackComponent = spawnedMonster.GetComponent<MonsterAttack>();
                var takeDamageComponent = spawnedMonster.GetComponent<MonsterTakeDamage>();
                var movementComponent = spawnedMonster.GetComponent<MonsterMovement>();

                if (attackComponent != null)
                {
                    attackComponent.InitializeAttack(1f); // ���� �����̸� �����մϴ�.
                }

                if (takeDamageComponent != null)
                {
                    takeDamageComponent.health = selectedMonsterData.health; // ü�� ����
                }

                if (movementComponent != null)
                {
                    movementComponent.InitializeMovement(selectedMonsterData.speed); // �ӵ� ����
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