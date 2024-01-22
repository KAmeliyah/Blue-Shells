using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInformation : MonoBehaviour
{
    public static List<Card> cardList = new();



    void Awake()
    {
        cardList.Add(new Card(0, "Fireball", "Fire", 0));
        cardList.Add(new Card(1, "Fireball", "Fire", 0));
        cardList.Add(new Card(2, "Fireball", "Fire", 0));
        cardList.Add(new Card(3, "Fireball", "Fire", 0));
        cardList.Add(new Card(4, "Fireball", "Fire", 0));

        cardList.Add(new Card(5, "Wave", "Water", 0));
        cardList.Add(new Card(6, "Wave", "Water", 0));
        cardList.Add(new Card(7, "Wave", "Water", 0));
        cardList.Add(new Card(8, "Wave", "Water", 0));
        cardList.Add(new Card(9, "Wave", "Water", 0));

        cardList.Add(new Card(10, "Tree", "Grass", 0));
        cardList.Add(new Card(11, "Tree", "Grass", 0));
        cardList.Add(new Card(12, "Tree", "Grass", 0));
        cardList.Add(new Card(13, "Tree", "Grass", 0));
        cardList.Add(new Card(14, "Tree", "Grass", 0));

    }
}
