using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab;  // Prefab de la flecha
    public Transform shootPoint;    // Punto desde donde dispara la flecha
    public float shootInterval = 1f;  // Intervalo de tiempo entre disparos (en segundos)

    private float timeSinceLastShot = 0f;  // Temporizador para controlar el intervalo de disparo

    void Update()
    {
        // Contabilizar el tiempo desde el último disparo
        timeSinceLastShot += Time.deltaTime;

        // Si ha pasado suficiente tiempo desde el último disparo, disparar una flecha
        if (timeSinceLastShot >= shootInterval)
        {
            ShootArrow();
            timeSinceLastShot = 0f;  // Reiniciar el temporizador
        }
    }

    void ShootArrow()
    {
        // Instanciar la flecha en la posición del punto de disparo
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

        // Agregar movimiento a la flecha (puedes ajustar la velocidad de la flecha aquí)
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(10f, 0f);  // Cambia 10f por la velocidad deseada
        }
    }
}
