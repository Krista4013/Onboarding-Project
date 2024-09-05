using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public string grade;
    public float speed;
    public int health;
    public Sprite monsterImage;

    public GameObject monsterInfo;  // MonsterInfo UI 오브젝트

    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        monsterInfo = canvas.transform.Find("MonsterInfo").gameObject;
    }

    public void Initialize(MonsterDataLoader.MonsterData data)
    {
        monsterName = data.name;
        grade = data.grade;
        speed = data.speed;
        health = data.health;

        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterImage = spriteRenderer.sprite;
    }

    private void OnMouseDown()
    {
        if (monsterInfo != null)
        {
            MonsterInfo infoComponent = monsterInfo.GetComponent<MonsterInfo>();
            if (infoComponent != null)
            {
                infoComponent.ShowInfo(monsterName, grade, speed.ToString(), health.ToString(), monsterImage);
                monsterInfo.SetActive(true);
            }
        }
    }

    public void HideInfo()
    {
            monsterInfo.SetActive(false);
    }
}