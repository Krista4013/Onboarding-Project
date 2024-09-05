using System.Collections.Generic;
using UnityEngine;

public class MonsterDataLoader : MonoBehaviour
{
    public string fileName = "SampleMonster";

    public class MonsterData
    {
        public string name;
        public string grade;
        public float speed;
        public int health;
    }

    public List<MonsterData> Monster = new List<MonsterData>();

    public void LoadMonsterData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] values = lines[i].Split(',');

                try
                {
                    MonsterData monster = new MonsterData();
                    monster.name = values[0];
                    monster.grade = values[1];
                    monster.speed = float.Parse(values[2]);
                    monster.health = int.Parse(values[3]);

                    Monster.Add(monster);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed {i + 1}: {lines[i]}");
                    Debug.LogError($"¿¡·¯ {ex.Message}");
                }
            }
        }
    }
}
