using UnityEngine;

public class DestruirBotella : MonoBehaviour
{
    public float tiempoAEliminar = 5f; // Tiempo a quitar cuando se destruye una botella
    public float tiempoAAñadir = 0f;  // Tiempo a añadir si es necesario (puedes configurarlo)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Botella"))
        {
            // Notificar al script de interacción que la botella será destruida
            InteractuarConObjeto interactor = FindObjectOfType<InteractuarConObjeto>();
            if (interactor != null)
            {
                interactor.ForzarSoltarSiEs(other.gameObject);
            }

            // Interactuar con el temporizador
            TemporizadorPastel temporizador = FindObjectOfType<TemporizadorPastel>();
            if (temporizador != null)
            {
                if (tiempoAEliminar > 0)
                {
                    temporizador.QuitarTiempo(tiempoAEliminar);
                }

                if (tiempoAAñadir > 0)
                {
                    temporizador.AñadirTiempo(tiempoAAñadir);
                }
            }
            else
            {
                Debug.LogWarning("No se encontró un componente TemporizadorPastel en la escena.");
            }

            // Destruir la botella
            Destroy(other.gameObject);
        }
    }
}
