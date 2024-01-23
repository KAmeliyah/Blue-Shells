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
        if(hasBeenPlayed == false)
        {
            transform.position = Vector3.up * 5;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            Invoke("MovetoDiscardPile", 2f);
        }
    }

    void MovetoDiscardPile()
    {
        cm.discardPile.Add(this);
        gameObject.SetActive(false);
    }
}
