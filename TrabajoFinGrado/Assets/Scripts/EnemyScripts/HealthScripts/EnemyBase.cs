using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    public float currentHp;
    public float knockBackTime;
    public int damage;
    public float knockBackCounter;
    private AIChase enemySpeed;
    private bool isDead = false; // Flag to ensure Die is called once

    private void Update()
    {
        //Debug.Log("Update called: knockBackCounter = " + knockBackCounter);

        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            Debug.Log("Knockback counter updated: " + knockBackCounter);

            if (enemySpeed.speed > 0)
            {
                enemySpeed.speed = -enemySpeed.speed * 20f; // Apply knockback
                Debug.Log("Knockback applied: " + enemySpeed.speed);
            }

            if (knockBackCounter <= 0)
            {
                enemySpeed.speed = Mathf.Abs(enemySpeed.speed * 0.5f); // Reset speed after knockback
                Debug.Log("Knockback ended: " + enemySpeed.speed);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        Debug.Log("Enemy took damage: " + damage);

        if (currentHp <= 0 && !isDead)
        {
            Die();
        }
        //DamageNumberController.instance.SpawnDamage(damage, transform.position);
    }

    public void TakeDamage(float damage, bool shouldKnockBack)
    {
        Debug.Log("TakeDamage called: " + damage + ", shouldKnockBack: " + shouldKnockBack);
        TakeDamage(damage);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
            Debug.Log("Knockback initiated");
        }
    }

    protected void Die()
    {
        if (isDead) return; // Prevent multiple executions
        isDead = true;

        EnemyDropGemTest dropGemScript = FindObjectOfType<EnemyDropGemTest>();

        if (dropGemScript != null)
        {
            EnemyDropGemTest.boxCounter++;
            if (EnemyDropGemTest.boxCounter % 10 == 0)
            {
                Instantiate(dropGemScript.box, transform.position, Quaternion.identity);
            }
            else
            {
                int randomGemIndex = Random.Range(0, dropGemScript.gemDropped.Length);
                Instantiate(dropGemScript.gemDropped[randomGemIndex], transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject); // Destroy this enemy upon death
    }

    public void IncreaseStats(int damageIncrease, float healthIncrease)
    {
        damage += damageIncrease;
        currentHp += healthIncrease;
        Debug.Log("Enemy stats increased: Damage = " + damage + ", Health = " + currentHp);
    }
}



