using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    
    GameManager gameManager;

    public string unitname;
    public int unitLevel;

    public int damage;
    public int maxHP;
    public int currentHP;

    public int XPheld;

    //animation
    public Animator animator;
    public BattleSystem battleSystem;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();



        if (GameManager.manager != null)
        {
            gameManager = GameManager.manager;

        }
        else
        {
            Debug.Log("GameManager not found! - Please add a GameManager to the scene!");
        }

        if (GameObject.FindWithTag("BattleSystem") != null)
        {
            battleSystem = GameObject.FindWithTag("BattleSystem").GetComponent<BattleSystem>();
            Debug.Log("Found Battle System");

        }
        else
        {
            Debug.Log("No Battle System");
        }




        if (battleSystem.enemyUnit == GameObject.FindWithTag("HellBoss"))
        {
            XPheld = gameManager.MaxXP;

        }
        if (battleSystem.enemyUnit == GameObject.FindWithTag("BattleHellEnemy"))
        {
            XPheld = 5;

        }

    }

    private void Update()
    {
        

        if (battleSystem.enemyUnit == GameObject.FindWithTag("HellBoss")) 
        { 
          XPheld = gameManager.MaxXP;

            int randomNumber = Random.Range(1, 4);
            damage = randomNumber;

        }
        if (battleSystem.enemyUnit == GameObject.FindWithTag("BattleHellEnemy"))
        {
            XPheld = 5;

            
            damage = 2;

        }


        if (battleSystem.state == BattleState.WON)
        {
            animator.SetBool("IsDefeated", true);

        }
        else
        {
            animator.SetBool("IsDefeated", false);
        }

        if (gameObject.tag == ("HellBoss") && currentHP == 0)
        {
            gameManager.BossBeaten = true;

        }
        else
        {
            gameManager.BossBeaten= false;  
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
