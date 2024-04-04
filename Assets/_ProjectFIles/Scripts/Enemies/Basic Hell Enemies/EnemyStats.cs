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
    public BattleSystem battleSystem;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

  

        if (GameObject.FindWithTag("BattleSystem") != null)
        {
            battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
            Debug.Log("Found Battle System");

        }
        else
        {
            Debug.Log("No Battle System");
        }
      

    }

    private void Update()
    {
        int randomNumber = Random.Range(1, 4);
        damage = randomNumber;


        if (battleSystem.state == BattleState.WON)
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
