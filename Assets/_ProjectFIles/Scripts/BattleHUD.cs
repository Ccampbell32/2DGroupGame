using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;

	public void SetHUD(PlayerStats playerUnit)
	{
		nameText.text = playerUnit.unitname;
		levelText.text = "Lvl " + playerUnit.unitLevel;
		hpSlider.maxValue = playerUnit.maxHP;
		hpSlider.value = playerUnit.currentHP;
	}
    public void SetEnemyHUD(EnemyStats enemyUnit)
    {
        nameText.text = enemyUnit.unitname;
        levelText.text = "Lvl " + enemyUnit.unitLevel;
        hpSlider.maxValue = enemyUnit.maxHP;
        hpSlider.value = enemyUnit.currentHP;
    }
    public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
