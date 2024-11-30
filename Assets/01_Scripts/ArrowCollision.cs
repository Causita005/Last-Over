using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowCollision : MonoBehaviour
{
    // Tiempo en segundos antes de destruir la flecha automáticamente
    public float destroyAfterSeconds = 6f;

    private void Start()
    {
        // Destruir la flecha después de 6 segundos
        Destroy(gameObject, destroyAfterSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la flecha colisiona con el jugador
        if (collision.CompareTag("Player"))
        {
            // Destruir la flecha
            Destroy(gameObject);

            // Reiniciar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
