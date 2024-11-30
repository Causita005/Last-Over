using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class FlagCollision : MonoBehaviour
{
    // Este m�todo se llama cuando otro objeto entra en el collider de la bandera
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si el objeto que colision� es el jugador (asegur�ndote de que el jugador tiene un tag 'Player')
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador toc� la bandera! Cambiando de escena...");
            // Cambiar a la escena siguiente (cambiar 'SceneName' por el nombre de la escena)
            SceneManager.LoadScene("BossNivel1");  // Reemplaza "SceneName" con el nombre real de tu escena
        }
    }
}
