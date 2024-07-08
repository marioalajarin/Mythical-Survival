using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRedExpScript : MonoBehaviour
{

    private PlayerRPG playerRPG;
    private PlayerController playerController;
    
    private AudioSource audioSource;
    public AudioClip pickupSound;

    void Start()
    {
        playerRPG = GetComponent<PlayerRPG>();
        playerController = GetComponent<PlayerController>();
        
        audioSource = GetComponent<AudioSource>();


        if (playerRPG == null || playerController == null)
        {
            Debug.LogError("PlayerRPG or PlayerController component not found on the player.");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RedGem"))
        {
            PlayPickupSound();
            IncreaseExperience(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    void IncreaseExperience(GameObject gem)
    {
        int playerLvl = PlayerRPG.lvlCount;
        if (playerLvl >= 0 && playerLvl < 5)
        {
            playerRPG.currentExperience += 10;
        }
        else if (playerLvl >= 5 && playerLvl < 10)
        {
            playerRPG.currentExperience += 5;
        }
        else if (playerLvl >= 10 && playerLvl < 20)
        {
            playerRPG.currentExperience += 2;
        }
        else if (playerLvl >= 20 && playerLvl < 50)
        {
            playerRPG.currentExperience += 1;
        }
    }
    void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
}
