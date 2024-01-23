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

    public TextMeshProUGUI deckSizeText;

    private void Start()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    private void SetUpBattle()
    {
        Instantiate(playerPrefab,playerBattleStation);
        Instantiate(enemyPrefab,enemyBattleStation);
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

    private int CalculateDamage(Card _card)
    {


        return 0;
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