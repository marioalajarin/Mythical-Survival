using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float damageAmount = 1f; // Damage amount per tick
    public float damageInterval = 0.3f; // Interval between damage ticks
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private AudioSource audioSource;
    public AudioClip hitSound;

    private Animator animator;
    private void Start()
    {
        animator=GetComponent<Animator>();
        animator.Play("ThunderFire");
        audioSource = GetComponent<AudioSource>();
        // Destroy the object after 3 seconds
        Destroy(gameObject, 3f);

        // Start dealing damage at intervals
        StartCoroutine(DealDamageOverTime());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy entered damage area: " + other.name); // Debug log
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy exited damage area: " + other.name); // Debug log
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);

            // Create a copy of the list to iterate over
            List<GameObject> enemiesToDamage = new List<GameObject>(enemiesInRange);

            foreach (GameObject enemy in enemiesToDamage)
            {
                if (enemy != null) // Ensure the enemy is still valid
                {
                    Debug.Log("Dealing damage to enemy: " + enemy.name); // Debug log
                    PlayHitSound();
                    enemy.GetComponent<EnemyBase>().TakeDamage(damageAmount);
                }
            }
        }
    }
    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}