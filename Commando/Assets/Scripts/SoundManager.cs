using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        none,
        gun1_shoot,
        player_move,
        shotgun_shoot,
        shotgun_reload,
        woosh1,
        pistol_reload,
        grenade_explosion,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.player_move] = 0f;
    }
    

    //spatial sound overload
    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (PlayCooldown(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);

            if(audioSource.clip != null)
                audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    //normal sound
    public static void PlaySound(Sound sound)
    {
        if(PlayCooldown(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            AudioClip audioClip = GetAudioClip(sound);
            if(audioClip != null)    
                oneShotAudioSource.PlayOneShot(audioClip);

        }
        
    }

    //can this sound be played
    private static bool PlayCooldown(Sound sound)
    {
        //TODO i don't like this solution, id rather just pass a timer and delay for every sound
        //(make an function overload so that i can just pass nothing) and if it's 0 that means sound will be played normally
        switch (sound)
        {
            case Sound.player_move:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveCooldown = 0.3f;
                    if (lastTimePlayed + playerMoveCooldown < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return true;
                }
            default:
                return true;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.Log("Sound " + sound + "not found!");
        return null;
    }
}
