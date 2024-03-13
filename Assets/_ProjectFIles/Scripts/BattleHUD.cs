using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleHUD : MonoBehaviour
{

	public TMP_Text nameText;
	public TMP_Text levelText;

    public TMP_Text enemynameText;
    public TMP_Text enemylevelText;

    public Slider hpSlider;
    public Slider enemyhpSlider;

    public void SetHUD(PlayerStats playerUnit)
	{
		nameText.text = playerUnit.unitname;
		levelText.text = "Lvl " + playerUnit.unitLevel;
		hpSlider.maxValue = playerUnit.maxHP;
		hpSlider.value = playerUnit.currentHP;
	}
    public void SetEnemyHUD(EnemyStats enemyUnit)
    {
        enemynameText.text = enemyUnit.unitname;
        enemylevelText.text = "Lvl " + enemyUnit.unitLevel;
        enemyhpSlider.maxValue = enemyUnit.maxHP;
        enemyhpSlider.value = enemyUnit.currentHP;
    }
    public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}
    public void SetEnemyHP(int hp)
    {
        enemyhpSlider.value = hp;
    }

}
