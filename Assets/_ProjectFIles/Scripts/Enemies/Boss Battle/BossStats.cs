using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    


    public string unitname;
    public int unitLevel;

    public int damage;
    public int maxHP;
    public int currentHP;

    private void Update()
    {
        int randomNumber = Random.Range(0, 4);
        damage = randomNumber;
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }

    }
}
