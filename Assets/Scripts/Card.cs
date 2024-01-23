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
  
    public int handIndex;
    private CombatManager cm;

  
    private void Start()
    {
        cm = FindObjectOfType<CombatManager>();

    }

    private void OnMouseDown()
    {
        if(hasBeenPlayed == false && cm.state == BattleState.PLAYERTURN )
        {
            transform.position = Vector3.up * 0.5f;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            Invoke("MovetoDiscardPile", 5f);
            cm.state = BattleState.ENEMYTURN;
        }

    }

    void MovetoDiscardPile()
    {
        cm.discardPile.Add(this);
        gameObject.SetActive(false);
    }
}
