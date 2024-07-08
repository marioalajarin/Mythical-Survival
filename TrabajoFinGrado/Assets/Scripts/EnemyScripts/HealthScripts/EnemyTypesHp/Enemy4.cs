using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : EnemyBase
{
    private Animator animator;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Enemy4");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            spriteRenderer.flipX = direction.x < 0;
        }
    }
}