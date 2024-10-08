using UnityEngine;
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource SoundFXObject;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioclip, Transform spawnTransform, float volume)
    {
        //create a sound object
        AudioSource audioSource = Instantiate(SoundFXObject,spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioclip;
        audioSource.volume = volume;

        //may add delay depending on length of animation
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        //Destroy when it's done
        Destroy(audioSource.gameObject,clipLength);

    }

}


