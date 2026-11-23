using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccesibilityManager : MonoBehaviour
{
    // ─── Configuración ────────────────────────────────────────────────────
    private const float MIN_SIZE = 36f;
    private const float MAX_SIZE = 40f;

    // Tag que identifica los elementos del diálogo (debe existir en Tag Manager, escrita igual)
    private const string DIALOG_TAG = "Dialogo";

    // ─── Estado que se conserva al cambiar de escena ──────────────────────
    // static = sobrevive a la carga de escenas (se reinicia solo al cerrar la aplicación)
    private static float sizeOffset = 0f;
    private static bool highContrastEnabled = false;

    // ─── Estado de ESTA escena ────────────────────────────────────────────
    // Tamaño original de cada texto, antes de aplicar el ajuste
    private readonly Dictionary<TMP_Text, float> baseSizes = new Dictionary<TMP_Text, float>();
    // Color original de cada Image/Texto modificado por el alto contraste
    private readonly Dictionary<Graphic, Color> originalColors = new Dictionary<Graphic, Color>();

    private void Awake()
    {
        // Aplica lo que el usuario ya había configurado en escenas anteriores
        if (sizeOffset != 0f) ApplySize();
        if (highContrastEnabled) ApplyHighContrast();
    }


    // Imágenes con tag "dialogo"  negro | Textos con tag "dialogo"  blanco
    public void ActivarAltoContraste()
    {
        highContrastEnabled = true;
        ApplyHighContrast();
    }

    // Devuelve las imágenes y textos a sus colores originales
    public void DesactivarAltoContraste()
    {
        highContrastEnabled = false;

        foreach (KeyValuePair<Graphic, Color> par in originalColors)
        {
            if (par.Key != null) par.Key.color = par.Value;
        }
        originalColors.Clear();
    }

    private void ApplyHighContrast()
    {
        foreach (Image imagen in GetComponentsInChildren<Image>(true))
        {
            if (imagen.CompareTag(DIALOG_TAG)) SetHighContrastColor(imagen, Color.black);
        }

        foreach (TMP_Text texto in GetComponentsInChildren<TMP_Text>(true))
        {
            if (texto.CompareTag(DIALOG_TAG)) SetHighContrastColor(texto, Color.white);
        }
    }

    // Guarda el color original (solo la primera vez) y aplica el nuevo conservando la transparencia
    private void SetHighContrastColor(Graphic elemento, Color nuevo)
    {
        if (!originalColors.ContainsKey(elemento)) originalColors[elemento] = elemento.color;

        float alfaOriginal = originalColors[elemento].a;
        elemento.color = new Color(nuevo.r, nuevo.g, nuevo.b, alfaOriginal);
    }

    // Cambia el tamaño de todos los TMP hijos
    public void Resize(float delta)
    {
        RegisterTexts();

        float minBase = float.MaxValue;
        float maxBase = float.MinValue;
        foreach (KeyValuePair<TMP_Text, float> par in baseSizes)
        {
            if (par.Key == null) continue;
            minBase = Mathf.Min(minBase, par.Value);
            maxBase = Mathf.Max(maxBase, par.Value);
        }
        if (minBase > maxBase) return;   // no hay textos

        // Se limita el ajuste al rango que realmente se puede alcanzar. Así, al llegar al
        // tope, un clic en el sentido contrario se nota de inmediato (no se "acumula").
        float lo = MIN_SIZE - maxBase;
        float hi = MAX_SIZE - minBase;
        sizeOffset = Mathf.Clamp(Mathf.Clamp(sizeOffset, lo, hi) + delta, lo, hi);

        ApplySize();
    }

    public void Aumentar() => Resize(1f);
    public void Disminuir() => Resize(-1f);

    private void ApplySize()
    {
        RegisterTexts();

        foreach (KeyValuePair<TMP_Text, float> par in baseSizes)
        {
            if (par.Key == null) continue;
            par.Key.fontSize = Mathf.Clamp(par.Value + sizeOffset, MIN_SIZE, MAX_SIZE);
        }
    }

    // Guarda el tamaño original de los textos que aún no están registrados
    private void RegisterTexts()
    {
        foreach (TMP_Text texto in GetComponentsInChildren<TMP_Text>(true))
        {
            if (!baseSizes.ContainsKey(texto)) baseSizes[texto] = texto.fontSize;
        }
    }
}
