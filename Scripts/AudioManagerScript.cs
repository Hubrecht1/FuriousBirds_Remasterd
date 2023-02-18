using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudioSource(AudioSource Audio, ulong delay = 0)
    {
        Audio.Play(delay);

    }

}
