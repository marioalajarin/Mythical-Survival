using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f; // Velocidad del jugador
    public Rigidbody2D rb; // Referencia al Rigidbody2D del jugador
    public Animator animator; // Referencia al Animator del jugador
    public SpriteRenderer sr; // Referencia al SpriteRenderer del jugador
    
    void Update()
    {
        MovePlayer(); // Llama a la función para mover al jugador
        sr = GetComponent<SpriteRenderer>(); // Obtiene el componente SpriteRenderer
    }

    void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal"); // Obtiene la entrada horizontal
        float y = Input.GetAxisRaw("Vertical"); // Obtiene la entrada vertical
        
        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = false; // Voltea el sprite a la izquierda

        } else if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = true; // Voltea el sprite a la derecha

        }
        
        Vector2 movement = new Vector2(x, y); // Crea un vector de movimiento
        
        movement.Normalize(); // Normaliza el vector de movimiento

        // Verifica si el Rigidbody2D no es nulo y si la velocidad es válida
        if (rb != null && !float.IsInfinity(speed) && !float.IsNaN(speed))
        {
            rb.velocity = movement * speed; // Aplica la velocidad al Rigidbody2D
        }
        else
        {
            Debug.LogError("Rigidbody2D is null or speed is invalid."); // Mensaje de error si el Rigidbody2D es nulo o la velocidad no es válida
        }
        
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * speed)); // Actualiza el parámetro de velocidad en el Animator
    }
}