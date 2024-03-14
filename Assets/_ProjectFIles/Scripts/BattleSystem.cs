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

    public Transform playerBattleSpawn;
    public Transform enemyBattleSpawn;

    PlayerStats playerUnit;
    EnemyStats enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        attackButton.gameObject.SetActive(false);
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

    IEnumerator PlayerAttack()
    {
        attackButton.gameObject.SetActive(false);   bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

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
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    /*
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    */
}
