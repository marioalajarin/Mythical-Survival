using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTheme : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip inGameTheme;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = inGameTheme;
        audioSource.Play();
        
    }

}
