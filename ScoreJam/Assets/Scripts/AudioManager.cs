using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
}
