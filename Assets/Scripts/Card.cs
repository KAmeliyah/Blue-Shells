using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

[System.Serializable]

public class Card: MonoBehaviour
{
    public int id;
    public string element;
    public int power;

    public bool hasBeenPlayed;

    public bool player;
  
    public int handIndex;
    private CombatManager cm;

  
    private void Awake()
    {
        cm = FindObjectOfType<CombatManager>();

    }

    private void OnMouseDown()
    {
        
        if(hasBeenPlayed == false && cm.state == BattleState.PLAYERTURN )
        {
            transform.position = cm.playerCardLoc.position;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            cm.enemyPlay.transform.position = cm.enemyCardLoc.position;
            GetPlayedCard();
            Invoke("MovetoDiscardPile", 5f);
           
        }

    }

  
    void GetPlayedCard()
    {
       cm.playedCard = this;
       cm.state = BattleState.RESOLVE;
       return;
    }



    public void MovetoDiscardPile()
    {
        if(player)
        {
            cm.discardPile.Add(this);
        }
        else
        {
            cm.eDiscardPile.Add(this);
        }
       
        gameObject.SetActive(false);

    }
}
