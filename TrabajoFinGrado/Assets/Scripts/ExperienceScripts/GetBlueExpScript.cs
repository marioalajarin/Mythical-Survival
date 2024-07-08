using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBlueExpScript : MonoBehaviour
{
    private PlayerRPG playerRPG; // Referencia al script PlayerRPG
    private PlayerController playerController; // Referencia al script PlayerController
    
    private AudioSource audioSource;
    public AudioClip pickupSound; // Sonido de recogida
    void Start()
    {
        playerRPG = GetComponent<PlayerRPG>();
        playerController = GetComponent<PlayerController>();
        
        audioSource = GetComponent<AudioSource>();

        // Verifica que los componentes PlayerRPG y PlayerController no sean nulos
        if (playerRPG == null || playerController == null)
        {
            Debug.LogError("PlayerRPG or PlayerController component not found on the player.");
        }
    }

    // Se activa cuando otro objeto entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BlueGem"))
        {
            PlayPickupSound(); // Reproduce el sonido de recogida
            IncreaseExperience(other.gameObject); // Aumenta la experiencia del jugador
            Destroy(other.gameObject); // Destruye el objeto BlueGem
        }
    }

    // Aumenta la experiencia del jugador según su nivel
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
    
    // Reproduce el sonido de recogida
    void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
}
