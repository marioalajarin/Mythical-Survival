using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTheme : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip resultTheme;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = resultTheme;
        audioSource.Play();
        
    }
    
}
