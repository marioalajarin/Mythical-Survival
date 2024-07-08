using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaRotationScript : MonoBehaviour
{
    public GameObject target; // Objeto objetivo (no se utiliza actualmente)
    public float speed = 100f; // Velocidad de rotación
    
    void Update()
    {
        // Actualiza la rotación del objeto en el eje Z
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (speed * Time.deltaTime));
    }
}