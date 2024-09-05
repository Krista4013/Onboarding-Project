using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    public Text nameTxt;  // 몬스터 이름 텍스트
    public Text gradeTxt;
    public Text speedTxt;
    public Text HpTxt;
    public Image monsterImage;

    public void ShowInfo(string name, string grade, string speed, string Hp, Sprite image)
    {
        nameTxt.text = name;
        gradeTxt.text = grade;
        speedTxt.text = speed;
        HpTxt.text = Hp;
        monsterImage.sprite = image;
    }
}