using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTheme : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip titleTheme;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = titleTheme;
        audioSource.Play();
        
    }
    
}
