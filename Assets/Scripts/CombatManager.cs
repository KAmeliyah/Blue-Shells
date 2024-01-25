using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public enum BattleState { START, PLAYERTURN, ENEMYTURN,RESOLVE, WON, LOST}
public class CombatManager : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Card playedCard = null;

    public List <Card> enemyDeck = new List<Card>();
    public List<Card> eDiscardPile = new List<Card>();
    public Card enemyPlay = null;


    [SerializeField] private AudioClip GoToCritSoundClip;


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
        enemyPlay = enemyDeck[Random.Range(0, deck.Count)];
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

        DrawCard();

        Debug.Log("Player Turn Ended");

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

        //resolve winner

        if(playedCard.element == "Fire" && enemyPlay.element == "Grass")
        {
            playerwin = true;
            ewin = false;
            
        }
        else if (playedCard.element == "Water" && enemyPlay.element == "Fire")
        {
            playerwin = true;
            ewin = false;
            
        }
        else if (playedCard.element == "Grass" && enemyPlay.element == "Water")
        {
            playerwin = true;
            ewin = false;
         
           
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
           
        }

        //calculate damage
        if (playerwin && !ewin) 
        {
            
            int damage = Mathf.Abs(playedCard.power - enemyPlay.power);
            eDead = enemyUnit.TakeDamage(damage,playedCard.element);
       

        }
        else if (!playerwin && ewin)
        {
            
            int damage = Mathf.Abs(enemyPlay.power - playedCard.power);
            pDead = playerUnit.TakeDamage(damage,enemyPlay.element);
         
        }

        yield return new WaitForSeconds(5f);


        //check win
        if(eDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if(pDead)
        {
          
           
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
           
            
            state = BattleState.ENEMYTURN;
            enemyPlay.MovetoDiscardPile();
            StartCoroutine(EnemyTurn());
        }

    }



    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            
            SceneManager.LoadSceneAsync("Overworld1");
        }
        else if(state == BattleState.LOST)
        {

            SoundFXManager.instance.PlaySoundFXClip(GoToCritSoundClip, transform, 1f);
            SceneManager.LoadSceneAsync("GameOver");
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

