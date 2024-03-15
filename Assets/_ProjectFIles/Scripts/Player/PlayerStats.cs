using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string unitname;
    public int unitLevel;

    public int damage = 2;
    public int damage2 = 3;
    public int damage3 = 4;
    public int damage4 = 5;

    public int maxHP;
    public int currentHP;

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
    public bool TakeDamage2(int dmg2)
    {
        currentHP -= dmg2;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }

    }
    public bool TakeDamage3(int dmg3)
    {
        currentHP -= dmg3;
        if (currentHP <= 0)
        {
            return true;

        }
        else
        {

            return false;
        }

    }
    public bool TakeDamage4(int dmg4)
    {
        currentHP -= dmg4;
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
