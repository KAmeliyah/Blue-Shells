using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{

    [SerializeField] private AudioClip deathSoundClip;
    [SerializeField] private AudioClip swordSoundClip;
    [SerializeField] private AudioClip fireSoundClip;
    [SerializeField] private AudioClip waterSoundClip;


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


        if(element == "Fire")
        {
            SoundFXManager.instance.PlaySoundFXClip(fireSoundClip, transform, 1f);
            
        }
        else if (element == "Water")
        {
            SoundFXManager.instance.PlaySoundFXClip(waterSoundClip, transform, 1f);
        }
      
        SoundFXManager.instance.PlaySoundFXClip(swordSoundClip, transform, 1f);



        if (currentHP <= 0)
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
