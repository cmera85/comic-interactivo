using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Configuración Capítulo 1 (3 Estados)")]
    public Image imagenBotonCapitulo1;
    public Sprite spriteCapitulo1Disponible;
    public Sprite spriteCapitulo1EnProgreso;
    public Sprite spriteCapitulo1Completado;

    void Start()
    {
        ActualizarEstadoCapitulos();
    }

    public void ActualizarEstadoCapitulos()
    {
        if (imagenBotonCapitulo1 == null) return;

        int estado = PlayerPrefs.GetInt("Capitulo1_Estado", 0);

        switch (estado)
        {
            case 2:
                if (spriteCapitulo1Completado != null)
                    imagenBotonCapitulo1.sprite = spriteCapitulo1Completado;
                break;

            case 1:
                if (spriteCapitulo1EnProgreso != null)
                    imagenBotonCapitulo1.sprite = spriteCapitulo1EnProgreso;
                break;

            default:
                if (spriteCapitulo1Disponible != null)
                    imagenBotonCapitulo1.sprite = spriteCapitulo1Disponible;
                break;
        }
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    [ContextMenu("Borrar Progreso Guardado")]
    public void BorrarProgreso()
    {
        PlayerPrefs.DeleteKey("Capitulo1_Estado");
        PlayerPrefs.DeleteKey("Capitulo1_Completado");
        PlayerPrefs.DeleteKey("Cap1_Hueso");
        PlayerPrefs.DeleteKey("Cap1_Guitarra");
        PlayerPrefs.DeleteKey("Cap1_Medalla");
        PlayerPrefs.DeleteKey("Cap1_Hoja");
        PlayerPrefs.Save();

        if (imagenBotonCapitulo1 != null && spriteCapitulo1Disponible != null)
        {
            imagenBotonCapitulo1.sprite = spriteCapitulo1Disponible;
        }
    }
}