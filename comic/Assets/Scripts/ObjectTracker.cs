using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectTracker : MonoBehaviour
{
    public bool hueso = false;
    public GameObject blockHueso;
    public bool guitarra = false;
    public GameObject blockGuitarra;
    public bool medalla = false;
    public GameObject blockMedalla;
    public bool hoja = false;
    public GameObject blockHoja;

    public TextMeshProUGUI scoreTXT;
    public int score;

    public void UpdateScore()
    {
        scoreTXT.text = score.ToString() +" de 4";
    }

    public void ObtenerHueso()
    {
        hueso = true;
        blockHueso.SetActive(false);
        score += 1;
        UpdateScore();
    }

    public void ObtenerGuitarra()
    {
        guitarra = true;
        blockGuitarra.SetActive(false);
        score += 1;
        UpdateScore();
    }

    public void ObtenerMedalla()
    {
        medalla = true;
        blockMedalla.SetActive(false);
        score += 1;
        UpdateScore();
    }

    public void ObteneHoja()
    {
        hoja = true;
        blockHoja.SetActive(false);
        score += 1;
        UpdateScore();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Capitulo1");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
