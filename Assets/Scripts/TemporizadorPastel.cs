using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TemporizadorPastel : MonoBehaviour
{
    public Image imagenPastel;
    public float tiempoMaximo = 40f;
    private float tiempoActual;

    public bool timerActivo = true;
    public string nombreEscenaMenu = "Menu";

    void Start()
    {
        tiempoActual = tiempoMaximo;
    }

    void Update()
    {
        if (!timerActivo) return;

        tiempoActual -= Time.deltaTime;
        tiempoActual = Mathf.Clamp(tiempoActual, 0, tiempoMaximo);

        if (imagenPastel != null)
            imagenPastel.fillAmount = tiempoActual / tiempoMaximo;

        if (tiempoActual <= 0)
        {
            timerActivo = false;
            Debug.Log("¡Tiempo agotado!");
            RegresarAlMenu();
        }
    }

    public void AñadirTiempo(float segundos)
    {
        tiempoActual += segundos;
        tiempoActual = Mathf.Min(tiempoActual, tiempoMaximo);
    }

    public void QuitarTiempo(float segundos)
    {
        tiempoActual -= segundos;
        tiempoActual = Mathf.Max(0, tiempoActual);
    }

    private void RegresarAlMenu()
    {
     // Guardar puntaje
        Time.timeScale = 1f;
        timerActivo = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}
