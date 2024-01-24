using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string maxHP;
    public int currentHP;

    public int damage;
    public string attackElement;
    public int attackPower;


    public void SetDamage(int _dmg)
    {
        damage = _dmg;
    }

  

    public bool TakeDamage(int _dmg)
    {
        currentHP -= _dmg;

        if(currentHP <= 0)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
