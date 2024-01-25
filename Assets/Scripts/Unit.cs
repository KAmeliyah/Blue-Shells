using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{

    [SerializeField] private AudioClip deathSoundClip;
    [SerializeField] private AudioClip swordSoundClip;


    public string maxHP;
    public int currentHP;

    public int damage;
    public int attackPower;



    public void SetDamage(int _dmg)
    {
        damage = _dmg;
    }

  

    public bool TakeDamage(int _dmg, string element)
    {

        SoundFXManager.instance.PlaySoundFXClip(swordSoundClip, transform, 1f);
        currentHP -= _dmg;
        Debug.Log("Damage Taken");

      

        if(currentHP <= 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(deathSoundClip, transform, 1f);
       
            return true;

        }
        else
        {
            return false;
        }
    }

   
}
