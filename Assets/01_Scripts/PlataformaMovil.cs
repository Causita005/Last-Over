using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento;

    private int siguientePlataforma = 1;
    private bool ordenPlataforma = true;

    private void Update()
    {
        // Verificar si el índice está dentro de los límites antes de cambiar de dirección
        if (ordenPlataforma)
        {
            if (siguientePlataforma + 1 >= puntosMovimiento.Length)
            {
                ordenPlataforma = false;
            }
        }
        else
        {
            if (siguientePlataforma <= 0)
            {
                ordenPlataforma = true;
            }
        }

        // Comprobar si la plataforma ha llegado al siguiente punto de movimiento
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePlataforma].position) < 0.1f)
        {
            if (ordenPlataforma)
            {
                siguientePlataforma += 1;
            }
            else
            {
                siguientePlataforma -= 1;
            }
        }

        // Asegurarse de que el índice esté dentro del rango antes de mover la plataforma
        if (siguientePlataforma >= 0 && siguientePlataforma < puntosMovimiento.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePlataforma].position, velocidadMovimiento * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Establecer la plataforma como el "padre" del jugador para que se mueva con ella
            collision.transform.SetParent(this.transform);

            // Obtener el Rigidbody del jugador para ajustar su movimiento
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Al entrar, mantendremos el movimiento horizontal en sincronía con la plataforma
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Asegurarse de que solo se mueva horizontalmente con la plataforma (si la plataforma se mueve horizontalmente)
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Solo ajustamos la velocidad horizontal para que el jugador se mueva con la plataforma en X
                float velocidadHorizontal = rb.velocity.x;
                rb.velocity = new Vector2(velocidadHorizontal, rb.velocity.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cuando el jugador sale de la plataforma, ya no se mueve con ella
            collision.transform.SetParent(null);
        }
    }
}
