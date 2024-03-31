using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject background;
    public GameObject attackButton;
    public GameObject healButton;
    public GameObject moves;

    //moves
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;


    public Transform playerBattleSpawn;
    public Transform enemyBattleSpawn;

    GameManager playerUnit;
    public EnemyStats enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        state = BattleState.START;
        StartCoroutine(SetupBattle());


        //finding moves in battleSys
        attack1 = GameObject.FindWithTag("Attack1");
        attack2 = GameObject.FindWithTag("Attack2");
        attack3 = GameObject.FindWithTag("Attack3");
        attack4 = GameObject.FindWithTag("Attack4");
    }
    

    IEnumerator SetupBattle()
    {
       Debug.Log("Setup Battle");
       attackButton.gameObject.SetActive(false);
       healButton.gameObject.SetActive(false);
       moves.gameObject.SetActive(false);
       GameObject playerBattle = Instantiate(playerPrefab, playerBattleSpawn);
       playerUnit = gameManager.GetComponent<GameManager>();
       background.gameObject.SetActive(true);

       GameObject enemy = Instantiate(enemyPrefab, enemyBattleSpawn);
       enemyUnit = enemy.GetComponent<EnemyStats>();

       dialogueText.text = "A deadly " + enemyUnit.unitname + " approaches...";

       playerHUD.SetHUD(playerUnit);
       enemyHUD.SetEnemyHUD(enemyUnit);
       enemyUnit.currentHP = enemyUnit.maxHP;
       yield return new WaitForSeconds(2f);

       Debug.Log("players turn");
       state = BattleState.PLAYERTURN;
       PlayerTurn();
    }

    #region PlayerAttacks
    IEnumerator PlayerAttack1()
    {
        attackButton.gameObject.SetActive(false);   bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        moves.gameObject.SetActive(false);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);
        
        if (isDead)
        {
            Debug.Log("Won");
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;

            dialogueText.text = "You deal " + playerUnit.damage + " damage...";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
        
    }
    IEnumerator PlayerAttack2()
    {
        attackButton.gameObject.SetActive(false); bool isDead = enemyUnit.TakeDamage(playerUnit.damage2);
        moves.gameObject.SetActive(false);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            Debug.Log("Won");

            state = BattleState.WON;

            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;

            dialogueText.text = "You deal " + playerUnit.damage2 + " damage...";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }

    }
    IEnumerator PlayerAttack3()
    {
        attackButton.gameObject.SetActive(false); bool isDead = enemyUnit.TakeDamage(playerUnit.damage3);
        moves.gameObject.SetActive(false);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);      
        dialogueText.text = "The attack is successful!";
        Debug.Log(enemyUnit.currentHP);
        yield return new WaitForSeconds(2f);
        
        if (isDead)
        {
            Debug.Log("Won");

            state = BattleState.WON;

            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;

            dialogueText.text = "You deal " + playerUnit.damage3 + " damage...";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }

    }
    IEnumerator PlayerAttack4()
    {
        attackButton.gameObject.SetActive(false); bool isDead = enemyUnit.TakeDamage(playerUnit.damage4);
        moves.gameObject.SetActive(false);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            Debug.Log("Won");

            state = BattleState.WON;

            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;

            dialogueText.text = "You deal " + playerUnit.damage4 + " damage...";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }

    }
    #endregion

    IEnumerator EnemyTurn()
    {
        attackButton.gameObject.SetActive(false);
        healButton.gameObject.SetActive(false);
        dialogueText.text = enemyUnit.unitname + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;

            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        if (isDead && state == BattleState.WON)
        {
            StartCoroutine(EndBattle());
        }
    }
    
    IEnumerator EndBattle()
    {

        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle! and gained " + enemyUnit.XPheld + "XP!";
            yield return new WaitForSeconds(2f);
            gameManager.CurrentXP += enemyUnit.XPheld;

            gameManager.ChangeGameState(GameState.Overworld);
            Debug.Log("Change to Overworld");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }
    void OnDisable()
    {
        Destroy(enemyUnit);
        Debug.Log("PrintOnDisable: script was disabled");
    }
     void OnEnable()
    {
        Debug.Log("PrintOnDisable: script was enabled");
        Start();
    }
    void PlayerTurn()
    {
        attackButton.gameObject.SetActive(true);
        healButton.gameObject.SetActive(true);
        

        dialogueText.text = "Choose an action:";

    }
    
    IEnumerator PlayerHeal()
    {
        healButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);
        playerHUD.SetHP(playerUnit.currentHP = playerUnit.maxHP);
        dialogueText.text = "You feel your injuries fading!";
        gameManager.currentamountofPotions --;
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    
    public void OnAttackButton()
    {
        healButton.gameObject.SetActive(false);
        AttacksMenu();
    }
    
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
             return;
        if (gameManager.currentamountofPotions >=1)
        {
            StartCoroutine(PlayerHeal());
        }
        else
        {
            StartCoroutine(NoHealingItems());
         
        }
    }
    IEnumerator NoHealingItems() 
    {
        dialogueText.text = "You have no Healing Items";
        healButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    #region Different Attacks
    void AttacksMenu()
    {
        attackButton.gameObject.SetActive(false);
        moves.gameObject.SetActive(true);

        attack1 = GameObject.FindWithTag("Attack1");
        attack2 = GameObject.FindWithTag("Attack2");
        attack3 = GameObject.FindWithTag("Attack3");
        attack4 = GameObject.FindWithTag("Attack4");

        attack1.gameObject.SetActive(true);
        attack2.gameObject.SetActive(true);
        if (attack3 != null)
        {
            if (gameManager.unitLevel >= 3)
            {
                attack3.gameObject.SetActive(true);
            }
            else
            {
                attack3.gameObject.SetActive(false);
            }
        }
        if (attack4 != null)
        {

            if (gameManager.unitLevel >= 5)
            {
                attack4.gameObject.SetActive(true);
            }
            else
            {
                attack4.gameObject.SetActive(false);
            }
        }


    }

    
    public void OnAttack1()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack1());
    }

    public void OnAttack2()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack2());
    }

    public void OnAttack3()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack3());
    }
    public void OnAttack4()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack4());
    }
    #endregion
}
