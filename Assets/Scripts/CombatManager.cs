using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class CombatManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;


    public TextMeshProUGUI deckSizeText;

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    private IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerGO.GetComponent<Unit>();


        GameObject enemyGo = Instantiate(enemyPrefab,enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();


    }

   
    //logic during the players turn
    public void PlayerTurn()
    {


        return;

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

    public void EnemyTurn()
    {

    }
   

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        DrawCard();
    }

}


/*
 * states:
 * start
 * player turn
 * enemy turn
 * won/lost
 * */