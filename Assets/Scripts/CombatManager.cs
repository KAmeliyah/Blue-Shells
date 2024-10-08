using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

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


    public Animator playerAnimator;
    public Animator enemyAnimator;

    public bool playerwin;
    public bool ewin;
    public string element;

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
      

       while(playedCard == null)
        {
            yield return null;
        }

        DrawCard();

       

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
        playerwin = false;
        ewin = false;
        bool pDead = false;
        bool eDead = false;

        //resolve winner

        element = playedCard.element;

        if(element == "Fire" && enemyPlay.element == "Grass")
        {
            playerwin = true;
            ewin = false;
            
                        
        }
        else if (element == "Water" && enemyPlay.element == "Fire")
        {
            playerwin = true;
            ewin = false;
                    

        }
        else if (element == "Grass" && enemyPlay.element == "Water")
        {
            playerwin = true;
            ewin = false;

        }
        else if (element == enemyPlay.element)
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
            playerAnimator.SetBool("isDead", true);
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
            
            SceneManager.LoadSceneAsync("CriticalHit");
        }
        else if(state == BattleState.LOST)
        {

            SoundFXManager.instance.PlaySoundFXClip(GoToCritSoundClip, transform, 1f);
            SceneManager.LoadSceneAsync("GameOver");
        }

    }
    
    public void DrawCard()
    {
        // if there are cards left
        if (deck.Count >= 1)
        {
            //pick a random card
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                //find an available slot
                if (availableCardSlots[i] == true)
                {
                    //set card to active and move to the right position
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;

                    //add card to hand and remove from deck
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
 

    

}

