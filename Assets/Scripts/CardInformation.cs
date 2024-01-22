using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInformation : MonoBehaviour
{
    public static List<Card> cardList = new();



    void Awake()
    {
        cardList.Add(new Card(0, "Fireball", "Fire", 14));
        cardList.Add(new Card(1, "Fireball", "Fire", 2));
        cardList.Add(new Card(2, "Fireball", "Fire", 3));
        cardList.Add(new Card(3, "Fireball", "Fire", 4));
        cardList.Add(new Card(4, "Fireball", "Fire", 5));
        cardList.Add(new Card(5, "Fireball", "Fire", 6));
        cardList.Add(new Card(6, "Fireball", "Fire", 7));
        cardList.Add(new Card(7, "Fireball", "Fire", 8));
        cardList.Add(new Card(8, "Fireball", "Fire", 9));
        cardList.Add(new Card(9, "Fireball", "Fire", 10));
        cardList.Add(new Card(10, "Fireball", "Fire", 11));
        cardList.Add(new Card(11, "Fireball", "Fire", 12));
        cardList.Add(new Card(12, "Fireball", "Fire", 13));

        cardList.Add(new Card(13, "Wave", "Water", 14));
        cardList.Add(new Card(14, "Wave", "Water", 2));
        cardList.Add(new Card(15, "Wave", "Water", 3));
        cardList.Add(new Card(16, "Wave", "Water", 4));
        cardList.Add(new Card(17, "Wave", "Water", 5));
        cardList.Add(new Card(18, "Wave", "Water", 6));
        cardList.Add(new Card(19, "Wave", "Water", 7));
        cardList.Add(new Card(20, "Wave", "Water", 8));
        cardList.Add(new Card(21, "Wave", "Water", 9));
        cardList.Add(new Card(22, "Wave", "Water", 10));
        cardList.Add(new Card(23, "Wave", "Water", 11));
        cardList.Add(new Card(24, "Wave", "Water", 12));
        cardList.Add(new Card(25, "Wave", "Water", 13));

        cardList.Add(new Card(26, "Tree", "Grass", 14));
        cardList.Add(new Card(27, "Tree", "Grass", 2));
        cardList.Add(new Card(28, "Tree", "Grass", 3));
        cardList.Add(new Card(29, "Tree", "Grass", 4));
        cardList.Add(new Card(30, "Tree", "Grass", 5));
        cardList.Add(new Card(31, "Tree", "Grass", 6));
        cardList.Add(new Card(32, "Tree", "Grass", 7));
        cardList.Add(new Card(33, "Tree", "Grass", 8));
        cardList.Add(new Card(34, "Tree", "Grass", 9));
        cardList.Add(new Card(35, "Tree", "Grass", 10));
        cardList.Add(new Card(36, "Tree", "Grass", 11));
        cardList.Add(new Card(37, "Tree", "Grass", 12));
        cardList.Add(new Card(38, "Tree", "Grass", 13));

    }
}
