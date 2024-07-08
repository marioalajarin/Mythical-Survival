using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsScript : MonoBehaviour
{
    private PlayerController pc;

    public float increasedSpeed=1.05f;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    public void increaseSpeed()
    {
        pc.speed = pc.speed * increasedSpeed;
    }
}
