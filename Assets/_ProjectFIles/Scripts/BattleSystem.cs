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
    public GameObject moves;

    //moves
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;


    public Transform playerBattleSpawn;
    public Transform enemyBattleSpawn;

    PlayerStats playerUnit;
    public EnemyStats enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
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
        attackButton.gameObject.SetActive(false);
        moves.gameObject.SetActive(false);
        GameObject playerBattle = Instantiate(playerPrefab, playerBattleSpawn);
        playerUnit = playerBattle.GetComponent<PlayerStats>();
        background.gameObject.SetActive(true);

        GameObject enemy = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemyUnit = enemy.GetComponent<EnemyStats>();

       dialogueText.text = "A deadly " + enemyUnit.unitname + " approaches...";

       playerHUD.SetHUD(playerUnit);
       enemyHUD.SetEnemyHUD(enemyUnit);
       
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack1()
    {
        attackButton.gameObject.SetActive(false);   bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        moves.gameObject.SetActive(false);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);
        
        if (isDead)
        {
            state = BattleState.WON;
           
            EndBattle();
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
            state = BattleState.WON;

            EndBattle();
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

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;

            EndBattle();
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
            state = BattleState.WON;

            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;

            dialogueText.text = "You deal " + playerUnit.damage4 + " damage...";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }

    }
    IEnumerator EnemyTurn()
    {
        attackButton.gameObject.SetActive(false);    
        dialogueText.text = enemyUnit.unitname + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;

            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        if (isDead && state == BattleState.WON)
        {
            EndBattle();
        }
    }
    
    IEnumerator EndBattle()
    {

        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            yield return new WaitForSeconds(2f);
            gameManager.ChangeGameState(GameState.Overworld);
            Debug.Log("Change to Overworld");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }
    
    void PlayerTurn()
    {
        attackButton.gameObject.SetActive(true);

        

        dialogueText.text = "Choose an action:";

    }
    /*
    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

       // playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    */
    public void OnAttackButton()
    {
        AttacksMenu();
    }
    /*
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    */

    void AttacksMenu()
    {
        attackButton.gameObject.SetActive(false);
        moves.gameObject.SetActive(true);

        attack1.gameObject.SetActive(true);
        attack2.gameObject.SetActive(true);
        attack3.gameObject.SetActive(true);
        attack4.gameObject.SetActive(true);


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
}
