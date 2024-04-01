using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    


    public string unitname;
    public int unitLevel;

    public int damage;
    public int maxHP;
    public int currentHP;

    public int XPheld;

    //animation
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        int randomNumber = Random.Range(1, 4);
        damage = randomNumber;


        if (currentHP == 0)
        {
            animator.SetBool("IsDefeated", true);

        }
        else
        {
            animator.SetBool("IsDefeated", false);
        }
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
