using TMPro;
using UnityEngine;

public class AccesibilityManager : MonoBehaviour
{
    // Cambia el tamaño de todos los TMP hijos
    public void Resize(float delta)
    {
        TMP_Text[] textos = GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text texto in textos)
        {
            texto.fontSize = Mathf.Clamp(texto.fontSize + delta, 36f, 40f);

        }
    }

    public void Aumentar() => Resize(1f);
    public void Disminuir() => Resize(-1f);
}
