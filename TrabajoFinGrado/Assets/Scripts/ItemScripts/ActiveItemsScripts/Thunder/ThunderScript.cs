using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : MonoBehaviour
{
    public GameObject spawnObject; // The object to spawn
    public float spawnInterval = 2f; // Interval in seconds
    public float initialDamageAmount = 10f; // Initial damage amount
    public float areaDamageAmount = 1f; // Damage amount for stepping into the area
    public float damageInterval = 0.3f; // Interval between damage ticks for stepping into the area

    private Camera mainCamera;
    
    private AudioSource audioSource;
    public AudioClip hitSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Start the repeating spawn method
        InvokeRepeating(nameof(SpawnAndDamage), spawnInterval, spawnInterval);

        // Get the main camera
        mainCamera = Camera.main;
    }

    private void SpawnAndDamage()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Create a list to hold enemies within the camera's view
        List<GameObject> visibleEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (IsInView(enemy))
            {
                visibleEnemies.Add(enemy);
            }
        }

        // Only proceed if there are visible enemies in the scene
        if (visibleEnemies.Count > 0)
        {
            // Select a random enemy from the list of visible enemies
            GameObject enemy = visibleEnemies[Random.Range(0, visibleEnemies.Count)];

            // Spawn the object below the selected enemy
            Vector3 spawnPosition = enemy.transform.position + new Vector3(0, -1, 0);
            GameObject spawnedObject = Instantiate(spawnObject, spawnPosition, Quaternion.identity);

            // Set up the damage area component
            DamageArea damageArea = spawnedObject.GetComponent<DamageArea>();
            if (damageArea != null)
            {
                damageArea.damageAmount = areaDamageAmount;
                damageArea.damageInterval = damageInterval;
            }

            // Apply initial damage to the selected enemy
            PlayHitSound();
            enemy.GetComponent<EnemyBase>().TakeDamage(initialDamageAmount);
            //Debug.Log("Has infligido " + damage1 + " de daño a el enemigo " + enemy.name);
        }
    }

    private bool IsInView(GameObject enemy)
    {
        // Get the position of the enemy in the viewport
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(enemy.transform.position);

        // Check if the enemy is within the viewport bounds
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0;
    }
    
    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
