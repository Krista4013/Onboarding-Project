using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image hpBarFill;
    public MonsterTakeDamage monsterTakeDamage;

    void Start()
    {
        UpdateHpBar();
    }

    void Update()
    {
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        if (monsterTakeDamage != null && hpBarFill != null)
        {
            float healthPercentage = (float)monsterTakeDamage.currenthealth / monsterTakeDamage.health;
            hpBarFill.fillAmount = Mathf.Clamp01(healthPercentage);
        }
    }
}
