using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnubisScript : MonoBehaviour
{
    public float damage = 1f;
    public float damageCooldown = 2f;
    public float damageDelay = 0.1f;

    public EnemyDropGemTest dropGemScript; // Referencia al script EnemyDropGemTest

    private Dictionary<EnemyBase, float> enemiesInRange = new Dictionary<EnemyBase, float>();

    public bool shouldKnockBack;
    
    private AudioSource audioSource;
    public AudioClip hitSound;
    
    private Animator animator;

    private void Start()
    {
        animator=GetComponent<Animator>();
        animator.Play("AnubisAura");
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D enemyTrigger)
    {
        if (enemyTrigger.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = enemyTrigger.GetComponent<EnemyBase>(); // Obtén el componente EnemyBase del enemigo
            if (enemy != null && !enemiesInRange.ContainsKey(enemy))
            {
                enemiesInRange.Add(enemy, Time.time);
                StartCoroutine(DealDamage(enemy));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D enemyTrigger)
    {
        if (enemyTrigger.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = enemyTrigger.GetComponent<EnemyBase>();
            if (enemy != null && enemiesInRange.ContainsKey(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }

    private IEnumerator DealDamage(EnemyBase enemy)
    {
        while (enemiesInRange.ContainsKey(enemy))
        {
            float entryTime = enemiesInRange[enemy];
            if (Time.time >= entryTime + damageCooldown)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, shouldKnockBack);
                    PlayHitSound();
                    Debug.Log("Has infligido " + damage + " de daño a el enemigo " + enemy.name);
                }
                yield return new WaitForSeconds(damageDelay); // Espera antes de aplicar el siguiente daño
            }
            yield return null; // Espera un frame antes de chequear nuevamente
        }
    }
    
    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
