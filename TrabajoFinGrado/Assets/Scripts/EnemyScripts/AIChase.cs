using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIChase : MonoBehaviour
{
    public Transform player;
    public float speed;
    public Rigidbody2D rb;
    private bool canTrigger = true;
    public Vector2 moveDirection;

    private EnemyBase enemy;
    private PlayerRPG decreaseHealthPoints;
    public float hitWaitTime = 2f;
    private float hitCounter;

    void Start()
    {
        enemy = GetComponent<EnemyBase>();
        player = GameObject.Find("Player").transform;
        decreaseHealthPoints = FindObjectOfType<PlayerRPG>();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        moveDirection = direction;

        if (canTrigger)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (enemy.knockBackCounter > 0)
        {
            enemy.knockBackCounter -= Time.deltaTime;

            if (speed > 0)
            {
                speed = -speed * 2f; // Apply knockback
            }

            if (enemy.knockBackCounter <= 0)
            {
                speed = Mathf.Abs(speed * 0.5f); // Reset speed after knockback
            }
        }

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
            ApplyDamage();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
            ApplyDamage();
        }
    }

    private void ApplyDamage()
    {
        int damage = enemy != null ? enemy.damage : 5; // Use the damage value from the enemy if available
        decreaseHealthPoints.currentHealthPoints += damage;
        hitCounter = hitWaitTime;
        Debug.Log("Te he pegado " + hitCounter + " " + hitWaitTime + " Damage: " + damage);
    }
}
