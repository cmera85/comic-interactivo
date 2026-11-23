using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccesibilityManager : MonoBehaviour
{
    private const string KeyNivelTamano = "Accesibilidad_NivelTamano";
    private const string KeyColorTexto = "Accesibilidad_ColorTexto";

    [Header("Límites (puntos sobre el tamaño original)")]
    [SerializeField] private float maxDisminucion = 4f;
    [SerializeField] private float maxAumento = 8f;

    private readonly Dictionary<TMP_Text, float> tamanosOriginales = new Dictionary<TMP_Text, float>();
    private readonly Dictionary<TMP_Text, Color> coloresOriginales = new Dictionary<TMP_Text, Color>();

    private float nivelTamano;
    private bool usaColorPersonalizado;
    private Color colorPersonalizado;

    private void Awake()
    {
        foreach (TMP_Text texto in GetComponentsInChildren<TMP_Text>(true))
        {
            tamanosOriginales[texto] = texto.fontSize;
            coloresOriginales[texto] = texto.color;
        }

        CargarPreferencias();
        AplicarTamano();
        AplicarColor();
    }

    // ---------- TAMAÑO ----------
    public void CambiarTamanoTexto(float delta)
    {
        nivelTamano = Mathf.Clamp(nivelTamano + delta, -maxDisminucion, maxAumento);
        AplicarTamano();
        GuardarPreferencias();
    }

    public void AumentarTamano() => CambiarTamanoTexto(1f);
    public void DisminuirTamano() => CambiarTamanoTexto(-1f);

    private void AplicarTamano()
    {
        foreach (var par in tamanosOriginales)
        {
            if (par.Key == null) continue;
            par.Key.fontSize = Mathf.Max(1f, par.Value + nivelTamano);
        }
    }

    // ---------- COLOR ----------
    public void CambiarColorTexto(Color color)
    {
        usaColorPersonalizado = true;
        colorPersonalizado = color;
        AplicarColor();
        GuardarPreferencias();
    }

    public void PonerNaranja() => CambiarColorTexto(new Color32(255, 128, 0, 255));
    public void PonerAmarilloCalido() => CambiarColorTexto(new Color32(255, 210, 89, 255));

    private void AplicarColor()
    {
        if (!usaColorPersonalizado) return;

        foreach (TMP_Text texto in coloresOriginales.Keys)
        {
            if (texto != null) texto.color = colorPersonalizado;
        }
    }

    // ---------- RESTAURAR ----------
    public void RestaurarPredeterminados()
    {
        nivelTamano = 0f;
        usaColorPersonalizado = false;

        foreach (var par in tamanosOriginales)
            if (par.Key != null) par.Key.fontSize = par.Value;

        foreach (var par in coloresOriginales)
            if (par.Key != null) par.Key.color = par.Value;

        PlayerPrefs.DeleteKey(KeyNivelTamano);
        PlayerPrefs.DeleteKey(KeyColorTexto);
        PlayerPrefs.Save();
    }

    // ---------- PERSISTENCIA ----------
    private void GuardarPreferencias()
    {
        PlayerPrefs.SetFloat(KeyNivelTamano, nivelTamano);

        if (usaColorPersonalizado)
            PlayerPrefs.SetString(KeyColorTexto, "#" + ColorUtility.ToHtmlStringRGBA(colorPersonalizado));

        PlayerPrefs.Save(); // fuerza la escritura a disco
    }

    private void CargarPreferencias()
    {
        nivelTamano = Mathf.Clamp(PlayerPrefs.GetFloat(KeyNivelTamano, 0f), -maxDisminucion, maxAumento);

        if (PlayerPrefs.HasKey(KeyColorTexto) &&
            ColorUtility.TryParseHtmlString(PlayerPrefs.GetString(KeyColorTexto), out Color guardado))
        {
            usaColorPersonalizado = true;
            colorPersonalizado = guardado;
        }
    }
}
