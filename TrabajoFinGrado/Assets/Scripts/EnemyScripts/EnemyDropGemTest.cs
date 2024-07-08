using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class EnemyDropGemTest : MonoBehaviour
{

    public GameObject[] gemDropped;
    
    public GameObject box;

    public Text killsText;

    public static int boxCounter; //Variable global
    
    public Animator animator;

    private void Update()
    {
        killsText.text = boxCounter.ToString();
    }
    
    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(HandleHurtAnimation());
        }
    }

    private void OnCollisionStay2D(Collision2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(HandleHurtAnimation());
        }
    }
    
    private IEnumerator HandleHurtAnimation()
    {
        
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isHurt", false);
        yield return new WaitForSeconds(0.2f);
    }

}
