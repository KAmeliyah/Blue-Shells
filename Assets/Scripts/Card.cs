using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{

    public int id;
    public string cardName;
    public string element;
    public int power;
   




    public Card()
    {

    }


    public Card(int _id, string _cardName, string _element, int _power)
    {
        id = _id;
        cardName = _cardName;
        element = _element;
        power = _power;
        
        
    }

}
