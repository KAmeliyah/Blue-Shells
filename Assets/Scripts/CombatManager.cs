using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public enum BattleState { START, PLAYERTURN, ENEMYTURN,RESOLVE, WON, LOST}
public class CombatManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Card playedCard = null;

    public List <Card> enemyDeck = new List<Card>();
    public Card enemyPlay = null;   


    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Transform playerCardLoc;
    public Transform enemyCardLoc;

    Unit playerUnit;
    Unit enemyUnit;


    public TextMeshProUGUI deckSizeText;

    private void Awake()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
       
    }
    
    private IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerGO.transform.position = playerBattleStation.transform.position;   
        playerUnit = playerGO.GetComponent<Unit>();


        GameObject enemyGo = Instantiate(enemyPrefab);
        enemyGo.transform.position = enemyBattleStation.transform.position;
        enemyUnit = enemyGo.GetComponent<Unit>();


        for(int i = 0; i<4; i++)
        {
            DrawCard();
        }

     

        yield return new WaitForSeconds(0.5f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    

    }
    IEnumerator EnemyTurn()
    {
        Card enemyPlay = enemyDeck[Random.Range(0, deck.Count)];
        enemyDeck.Remove(enemyPlay);

        yield return new WaitForSeconds(0.5f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(WaitWhilePlayerTurn());
    }

    IEnumerator WaitWhilePlayerTurn()
    {
        playedCard = null;
        Debug.Log("Player turn start");

       while(playedCard == null)
        {
            yield return null;
        }

     

        Resolve();
        
    }

    public void Resolve()
    {
        if (state != BattleState.RESOLVE)
            return;

        StartCoroutine(ResolveRound());

    }

    IEnumerator ResolveRound()
    {
        bool playerwin = false;
        bool ewin = false;
        bool pDead = false;
        bool eDead = false;

        if(playedCard.element == "Fire" && enemyPlay.element == "Grass")
        {
            playerwin = true;
            ewin = false;
            Debug.Log("Player Win with Fire");
        }
        else if (playedCard.element == "Water" && enemyPlay.element == "Fire")
        {
            playerwin = true;
            ewin = false;
            Debug.Log("Player Win with Water");
        }
        else if (playedCard.element == "Grass" && enemyPlay.element == "Water")
        {
            playerwin = true;
            ewin = false;
            Debug.Log("Player Win with Grass");
           
        }
        else if (playedCard.element == enemyPlay.element)
        {
            playerwin = false;
            ewin = false;
        }
        else
        {
            ewin = true;
            playerwin= false;
            Debug.Log("Player Lost with" + enemyPlay.element);
        }


        if (playerwin && !ewin) 
        {
            Debug.Log("Start calc");
            int damage = Mathf.Abs(playedCard.power - enemyPlay.power);
            eDead = enemyUnit.TakeDamage(damage);
            Debug.Log(enemyUnit.currentHP);

        }
        else if (!playerwin && ewin)
        {
            Debug.Log("Start calc");
            int damage = Mathf.Abs(enemyPlay.power - playedCard.power);
            pDead = playerUnit.TakeDamage(damage);
            Debug.Log(playerUnit.currentHP);
        }
        else
        {
            Debug.Log("Draw");
        }
      


        yield return new WaitForSeconds(1f);

        if(eDead)
        {
            state = BattleState.WON;
            Debug.Log("player wins");
            EndBattle();
        }
        else if(pDead)
        {
            Debug.Log("player lost");
           
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            Debug.Log("loop");
         
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("Win");
        }
        else if(state == BattleState.LOST)
        {
            //lose- load game over screen
        }

    }
    
    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;

                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }

        }
       
    }
 

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
       
       
    }

}

