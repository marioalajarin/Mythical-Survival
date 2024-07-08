using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{

    public static DamageNumberController instance;

    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;

    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(location);
        
        int rounded = Mathf.RoundToInt(damageAmount);

        DamageNumber newDamage=Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);
        
        Debug.Log("Damage Number Spawned at World Position: " + location + ", Screen Position: " + screenPosition);
    }
}
