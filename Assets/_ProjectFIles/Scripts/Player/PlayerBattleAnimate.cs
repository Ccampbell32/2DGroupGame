using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleAnimate : MonoBehaviour
{
    private Animator animator;
    public BattleSystem battleSystem;
    private void Start()
    {
        animator = GetComponent<Animator>();
        battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
    }

     void Update()
     {
        animator = GetComponent<Animator>();
        battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();

        if (battleSystem.PlayerAttacking == true)
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }

        if (battleSystem.PlayerHealing == true)
        {
            animator.SetBool("IsHealing", true);
        }
        else
        {
            animator.SetBool("IsHealing", false);
        }
     }









}
