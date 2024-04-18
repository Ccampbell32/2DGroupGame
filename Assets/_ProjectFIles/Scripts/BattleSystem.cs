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
    public GameObject movesAnim;
    public Animator animator;

    #region Moves
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;
    #endregion

    #region Audio
    //atack sounds
    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource attackSound3;
    public AudioSource attackSound4;
    #endregion

    #region Player Animator Bools
    public bool PlayerAttacking;
    public bool PlayerHealing;

    #endregion region

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

        foreach (Transform child in enemyBattleSpawn)
        {
            Destroy(child.gameObject);
        }


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
        movesAnim = GameObject.FindWithTag("AttackAnim");

        if (GameObject.FindWithTag("AttackAnim") == null)
        {
            movesAnim = GameObject.FindWithTag("AttackAnim");
            Debug.Log("Found anim");

        }
        else
        {
            Debug.Log("No anim");
        }

       animator = movesAnim.GetComponent<Animator>();

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
       PlayerAttacking = false;
       yield return new WaitForSeconds(2f);

       Debug.Log("players turn");
       state = BattleState.PLAYERTURN;
       PlayerTurn();
    }

    #region PlayerAttacks
    IEnumerator PlayerAttack1()
    {
        gameManager.damage = 2;

        attackButton.gameObject.SetActive(false);   bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        moves.gameObject.SetActive(false);
        
        attackSound1.Play();
        PlayerAttacking = true;
        //anim
        movesAnim.gameObject.SetActive(true);
        animator.SetBool("Attack1", true);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);
        PlayerAttacking = false;
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
        

        int randomNumber2 = Random.Range(1, 4);
        gameManager.damage2 = randomNumber2;


        attackButton.gameObject.SetActive(false); bool isDead = enemyUnit.TakeDamage(playerUnit.damage2);
        moves.gameObject.SetActive(false);

        attackSound2.Play();
        PlayerAttacking = true;
        //anim
        movesAnim.gameObject.SetActive(true);
        animator.SetBool("Attack2", true);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);
        PlayerAttacking = false;
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
        int randomNumber3 = Random.Range(1, 4);
        gameManager.damage3 = randomNumber3;

        attackButton.gameObject.SetActive(false); bool isDead = enemyUnit.TakeDamage(playerUnit.damage3);
        moves.gameObject.SetActive(false);

        attackSound3.Play();
        PlayerAttacking = true;
        //anim
        movesAnim.gameObject.SetActive(true);
        animator.SetBool("Attack3", true);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);      
        dialogueText.text = "The attack is successful!";
        Debug.Log(enemyUnit.currentHP);
        yield return new WaitForSeconds(2f);
        PlayerAttacking = false;

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
        int randomNumber4 = Random.Range(1, 4);
        gameManager.damage4 = randomNumber4;

        attackButton.gameObject.SetActive(false); bool isDead = enemyUnit.TakeDamage(playerUnit.damage4);
        moves.gameObject.SetActive(false);

        attackSound4.Play();
        PlayerAttacking = true;
        //anim
        movesAnim.gameObject.SetActive(true);
        animator.SetBool("Attack4", true);

        enemyHUD.SetEnemyHP(enemyUnit.currentHP);

        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);
        PlayerAttacking = false;
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
        movesAnim.gameObject.SetActive(false);

        //anim
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Attack4", false);


        attackButton.gameObject.SetActive(false);
        healButton.gameObject.SetActive(false);
        dialogueText.text = enemyUnit.unitname + " attacks!";

        yield return new WaitForSeconds(1f);

        enemyUnit.animator.SetBool("IsAttacking", true);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);
        enemyUnit.animator.SetBool("IsAttacking", false);
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
        movesAnim.SetActive(false);
        //anim
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Attack4", false);

        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle! and gained " + enemyUnit.XPheld + "XP!";
            yield return new WaitForSeconds(2f);
            gameManager.AddXP(enemyUnit.XPheld);

            gameManager.ChangeGameState(GameState.Overworld);
            Debug.Log("Change to Overworld");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
            yield return new WaitForSeconds(3f);
            gameManager.ChangeGameState(GameState.Overworld);
        }
    }
    void OnDisable()
    {
        foreach (Transform child in enemyBattleSpawn)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("PrintOnDisable: script was disabled");
        movesAnim.gameObject.SetActive(true);
    }
     void OnEnable()
     {
        Debug.Log("PrintOnDisable: script was enabled");
       
        Start();
     }
    void PlayerTurn()
    {
        movesAnim.SetActive(false);
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
        PlayerHealing = true;
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        PlayerHealing = false;
        StartCoroutine(EnemyTurn());
    }
    
    public void OnAttackButton()
    {
        print("Attack Button");
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
