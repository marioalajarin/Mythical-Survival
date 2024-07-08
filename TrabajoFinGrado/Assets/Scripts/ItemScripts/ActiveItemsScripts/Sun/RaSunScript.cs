using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaSunScript : MonoBehaviour
{
    public float regeneration=1f;

    private PlayerRPG playerRPG;

    private void Start()
    {
        playerRPG = GetComponent<PlayerRPG>();
    }

    public void RegenerateHealth()
    {
        playerRPG.currentHealthPoints -= regeneration;
    }
    
}
