using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectTracker : MonoBehaviour
{
    [Header("Estado de Objetos")]
    public bool hueso = false;
    public GameObject blockHueso;
    public bool guitarra = false;
    public GameObject blockGuitarra;
    public bool medalla = false;
    public GameObject blockMedalla;
    public bool hoja = false;
    public GameObject blockHoja;

    [Header("Interfaz y Botones")]
    public TextMeshProUGUI scoreTXT;
    public int score;
    public int objetosRequeridos = 4;
    public Button botonVolverAlMenu;

    void Start()
    {
        if (botonVolverAlMenu != null)
        {
            botonVolverAlMenu.interactable = true;
        }

        CargarObjetosGuardados();
        UpdateScore();
    }

    private void CargarObjetosGuardados()
    {
        score = 0;

        if (PlayerPrefs.GetInt("Cap1_Hueso", 0) == 1)
        {
            hueso = true;
            if (blockHueso != null) blockHueso.SetActive(false);
            score += 1;
        }

        if (PlayerPrefs.GetInt("Cap1_Guitarra", 0) == 1)
        {
            guitarra = true;
            if (blockGuitarra != null) blockGuitarra.SetActive(false);
            score += 1;
        }

        if (PlayerPrefs.GetInt("Cap1_Medalla", 0) == 1)
        {
            medalla = true;
            if (blockMedalla != null) blockMedalla.SetActive(false);
            score += 1;
        }

        if (PlayerPrefs.GetInt("Cap1_Hoja", 0) == 1)
        {
            hoja = true;
            if (blockHoja != null) blockHoja.SetActive(false);
            score += 1;
        }
    }

    public void UpdateScore()
    {
        if (scoreTXT != null)
        {
            scoreTXT.text = score.ToString() + " de " + objetosRequeridos.ToString();
        }
    }

    public void ObtenerHueso()
    {
        if (hueso) return;
        hueso = true;
        if (blockHueso != null) blockHueso.SetActive(false);
        
        PlayerPrefs.SetInt("Cap1_Hueso", 1);
        PlayerPrefs.Save();

        score += 1;
        UpdateScore();
    }

    public void ObtenerGuitarra()
    {
        if (guitarra) return;
        guitarra = true;
        if (blockGuitarra != null) blockGuitarra.SetActive(false);

        PlayerPrefs.SetInt("Cap1_Guitarra", 1);
        PlayerPrefs.Save();

        score += 1;
        UpdateScore();
    }

    public void ObtenerMedalla()
    {
        if (medalla) return;
        medalla = true;
        if (blockMedalla != null) blockMedalla.SetActive(false);

        PlayerPrefs.SetInt("Cap1_Medalla", 1);
        PlayerPrefs.Save();

        score += 1;
        UpdateScore();
    }

    public void ObteneHoja()
    {
        if (hoja) return;
        hoja = true;
        if (blockHoja != null) blockHoja.SetActive(false);

        PlayerPrefs.SetInt("Cap1_Hoja", 1);
        PlayerPrefs.Save();

        score += 1;
        UpdateScore();
    }

    public void Reiniciar()
    {
        // Borra decisiones, estado del capítulo y los 4 objetos guardados
        StoryBranchManager.decisionesGuardadas.Clear();
        PlayerPrefs.DeleteKey("Capitulo1_Estado");
        PlayerPrefs.DeleteKey("Capitulo1_Completado");
        PlayerPrefs.DeleteKey("Cap1_Hueso");
        PlayerPrefs.DeleteKey("Cap1_Guitarra");
        PlayerPrefs.DeleteKey("Cap1_Medalla");
        PlayerPrefs.DeleteKey("Cap1_Hoja");
        PlayerPrefs.Save();

        SceneManager.LoadScene("Capitulo1");
    }

    public void VolverAlMenu()
    {
        int estadoActual = PlayerPrefs.GetInt("Capitulo1_Estado", 0);

        if (score >= objetosRequeridos)
        {
            PlayerPrefs.SetInt("Capitulo1_Estado", 2);
        }
        else if (estadoActual < 2)
        {
            PlayerPrefs.SetInt("Capitulo1_Estado", 1);
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void CargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}