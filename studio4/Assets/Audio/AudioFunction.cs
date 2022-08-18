using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFunction : MonoBehaviour
{

    // Our Audio Assets 

    public AudioClip OAA_BoosterCardSoundSample;
    public AudioClip OAA_CorruptionCardSoundSample;
    public AudioClip OAA_CreatureSwapAttackHurtDiscardedCreatureSoundSample;
    public AudioClip OAA_HypnosisSoundSample;
    public AudioClip OAA_PlayerDamagedSoundSample;
    public AudioClip OAA_RandomSelectionSoundSample;
    public AudioClip OAA_RegenerationSoundSample;
    public AudioClip OAA_SacrificeSoundSample;
    public AudioClip OAA_SwiftRecoverySoundSample;
    public AudioClip OAA_TheSkullsChoiceSoundSample;

    // Audio Collaborator's Audio Assets

    public AudioClip CAA_riffle;
    public AudioClip CAA_Riffle2;
    public AudioClip CAA_Riffel3;
    public AudioClip CAA_Riffel4;
    public AudioClip CAA_Deckshuffel;
    public AudioClip CAA_Cardflip;
    public AudioClip CAA_Cardflip2;
    public AudioClip CAA_CARDEFX;

    public AudioSource Audio;

    // Our Audio Assets - Functions

    public void BoosterCardSoundSample()
    {
        {
            Audio.clip = OAA_BoosterCardSoundSample;
            Audio.Play();
        }
    }

    public void CorruptionCardSoundSample()
    {
        {
            Audio.clip = OAA_CorruptionCardSoundSample; ;
            Audio.Play();
        }
    }

    public void CreatureSwap()
    {
        {
            Audio.clip = OAA_CreatureSwapAttackHurtDiscardedCreatureSoundSample;
            Audio.Play();
        }
    }

    public void HypnosisSoundSample()
    {
        {
            Audio.clip = OAA_HypnosisSoundSample;
            Audio.Play();
        }
    }

    public void PlayerDamagedSoundSample()
    {
        {
            Audio.clip = OAA_PlayerDamagedSoundSample;
            Audio.Play();
        }
    }

    public void RandomSelectionSoundSample()
    {
        {
            Audio.clip = OAA_RandomSelectionSoundSample;
            Audio.Play();
        }
    }

    public void RegenerationSoundSample()
    {
        {
            Audio.clip = OAA_RegenerationSoundSample;
            Audio.Play();
        }
    }

    public void SacrificeSoundSample()
    {
        {
            Audio.clip = OAA_SacrificeSoundSample;
            Audio.Play();
        }
    }

    public void SwiftRecoverySoundSample()
    {
        {
            Audio.clip = OAA_SwiftRecoverySoundSample;
            Audio.Play();
        }
    }

    public void TheSkullsChoiceSoundSample()
    {
        {
            Audio.clip = OAA_TheSkullsChoiceSoundSample;
            Audio.Play();
        }
    }

    // Collaborator Audio Assets - Functions

    public void riffle()
    {
        {
            Audio.clip = CAA_riffle;
            Audio.Play();
        }
    }

    public void Riffle2()
    {
        {
            Audio.clip = CAA_Riffle2;
            Audio.Play();
        }
    }

    public void Riffel3()
    {
        {
            Audio.clip = CAA_Riffel3;
            Audio.Play();
        }
    }

    public void Riffel4()
    {
        {
            Audio.clip = CAA_Riffel4;
            Audio.Play();
        }
    }

    public void Deckshuffel()
    {
        {
            Audio.clip = CAA_Deckshuffel;
            Audio.Play();
        }
    }

    public void Cardflip()
    {
        {
            Audio.clip = CAA_Cardflip;
            Audio.Play();
        }
    }

    public void Cardflip2()
    {
        {
            Audio.clip = CAA_Cardflip2;
            Audio.Play();
        }
    }

    public void CARDEFX()
    {
        {
            Audio.clip = CAA_CARDEFX;
            Audio.Play();
        }
    }
}
