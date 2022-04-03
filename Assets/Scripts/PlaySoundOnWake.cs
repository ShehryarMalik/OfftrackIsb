using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnWake : MonoBehaviour
{
    [SerializeField] AudioSource sound;

    private void Awake()
    {
        if (sound)
            sound.Play();
    }
}
