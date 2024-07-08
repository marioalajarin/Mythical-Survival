using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaDamageScript : MonoBehaviour
{
    public float damage = 1f; // Daño infligido por Ra
    public float damageCooldown = 1f; // Tiempo de enfriamiento entre daños
    public float damageDelay = 0.1f; // Retardo entre daños consecutivos
    
    public EnemyDropGemTest dropGemScript; // Referencia al script EnemyDropGemTest

    private EnemyBase currentEnemy; // Enemigo actual en rango
    
    private List<EnemyBase> enemiesInRange = new List<EnemyBase>(); // Lista de enemigos en rango

    public bool shouldKnockBack; // Indica si debe haber retroceso
    
    private AudioSource audioSource;
    public AudioClip hitSound; // Sonido de impacto

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Se activa cuando un enemigo entra en el rango del trigger
    private void OnTriggerEnter2D(Collider2D enemyTrigger)
    {
        if (enemyTrigger.gameObject.CompareTag("Enemy"))
        {
            currentEnemy = enemyTrigger.GetComponent<EnemyBase>(); // Obtén el componente EnemyBase del enemigo
            if (currentEnemy != null && !enemiesInRange.Contains(currentEnemy))
            {
                enemiesInRange.Add(currentEnemy);
                // Si es el primer enemigo, inicia la invocación de DealDamage
                if (enemiesInRange.Count == 1)
                {
                    InvokeRepeating("DealDamage", 0f, damageCooldown);
                }
            }
        }
    }

    // Se activa cuando un enemigo sale del rango del trigger
    private void OnTriggerExit2D(Collider2D enemyTrigger)
    {
        if (enemyTrigger.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = enemyTrigger.GetComponent<EnemyBase>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
                // Si no quedan enemigos en rango, cancela la invocación de DealDamage
                if (enemiesInRange.Count == 0)
                {
                    CancelInvoke("DealDamage");
                }
            }
        }
    }
    
    // Invoca la corrutina para aplicar daño con retardo
    private void DealDamage()
    {
        StartCoroutine(ApplyDamageWithDelay());
    }

    // Aplica daño con retardo a los enemigos en rango
    private IEnumerator ApplyDamageWithDelay()
    {
        List<EnemyBase> enemiesCopy = new List<EnemyBase>(enemiesInRange);
        
        foreach (EnemyBase enemy in enemiesCopy)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage, shouldKnockBack);
                PlayHitSound();
                Debug.Log("Has infligido " + damage + " de daño al enemigo " + enemy.name);
                yield return new WaitForSeconds(damageDelay); // Espera antes de aplicar el siguiente daño
            }
        }
    }
    
    // Reproduce el sonido de impacto
    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
