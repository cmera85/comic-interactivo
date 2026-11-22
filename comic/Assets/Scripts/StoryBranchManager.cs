using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryBranchManager : MonoBehaviour
{
    [Header("Identificador Único de Decisión")]
    public string idDecision = "Decision_1";

    [Header("Viñeta de Decisiones Original")]
    public GameObject viñetaDecisionBase;

    [Header("Viñetas de Consecuencia")]
    public GameObject ruta1Container;
    public GameObject ruta2Container;
    public GameObject ruta3Container;

    [Header("Viñetas Target para los botones")]
    public GameObject viñetaTarget1;
    public GameObject viñetaTarget2;
    public GameObject viñetaTarget3;
    public float posicionYTarget = 1500f;

    [Header("Botones de Decisión Originales")]
    public Button buttonDecision1;
    public Button buttonDecision2;
    public Button buttonDecision3;

    [Header("Imagen de desenfoque")]
    public GameObject imagenDesenfoque;

    public static Dictionary<string, int> decisionesGuardadas = new Dictionary<string, int>();

    void Start()
    {
        if (ruta1Container != null)
            ruta1Container.SetActive(false);

        if (ruta2Container != null)
            ruta2Container.SetActive(false);

        if (ruta3Container != null)
            ruta3Container.SetActive(false);

        CrearBotonesTarget();

        if (buttonDecision1 != null)
            buttonDecision1.onClick.AddListener(() => TomarDecision(1));

        if (buttonDecision2 != null)
            buttonDecision2.onClick.AddListener(() => TomarDecision(2));

        if (buttonDecision3 != null)
            buttonDecision3.onClick.AddListener(() => TomarDecision(3));

        CheckSavedDecisions();
    }

    private void CrearBotonesTarget()
    {
        if (buttonDecision1 != null && viñetaTarget1 != null)
        {
            Button nuevoBoton1 = Instantiate(
                buttonDecision1,
                viñetaTarget1.transform
            );

            nuevoBoton1.name = "Button Decision 1 Target";
            nuevoBoton1.interactable = false;

            RectTransform rectTransform = nuevoBoton1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    posicionYTarget
                );
            }
        }

        if (buttonDecision2 != null && viñetaTarget2 != null)
        {
            Button nuevoBoton2 = Instantiate(
                buttonDecision2,
                viñetaTarget2.transform
            );

            nuevoBoton2.name = "Button Decision 2 Target";
            nuevoBoton2.interactable = false;

            RectTransform rectTransform = nuevoBoton2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    posicionYTarget
                );
            }
        }

        if (buttonDecision3 != null && viñetaTarget3 != null)
        {
            Button nuevoBoton3 = Instantiate(
                buttonDecision3,
                viñetaTarget3.transform
            );

            nuevoBoton3.name = "Button Decision 3 Target";
            nuevoBoton3.interactable = false;

            RectTransform rectTransform = nuevoBoton3.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(
                    rectTransform.anchoredPosition.x,
                    posicionYTarget
                );
            }
        }
    }

    private void TomarDecision(int opcion)
    {
        LockDecisions();

        decisionesGuardadas[idDecision] = opcion;

        if (imagenDesenfoque != null)
            imagenDesenfoque.SetActive(false);

        if (viñetaDecisionBase != null)
            viñetaDecisionBase.SetActive(false);

        if (opcion == 1 && ruta1Container != null)
            ruta1Container.SetActive(true);
        else if (opcion == 2 && ruta2Container != null)
            ruta2Container.SetActive(true);
        else if (opcion == 3 && ruta3Container != null)
            ruta3Container.SetActive(true);
    }

    private void LockDecisions()
    {
        if (buttonDecision1 != null)
        {
            buttonDecision1.interactable = false;
            buttonDecision1.gameObject.SetActive(false);
        }

        if (buttonDecision2 != null)
        {
            buttonDecision2.interactable = false;
            buttonDecision2.gameObject.SetActive(false);
        }

        if (buttonDecision3 != null)
        {
            buttonDecision3.interactable = false;
            buttonDecision3.gameObject.SetActive(false);
        }
    }

    private void CheckSavedDecisions()
    {
        if (decisionesGuardadas.ContainsKey(idDecision))
        {
            int opcionGuardada = decisionesGuardadas[idDecision];

            if (viñetaDecisionBase != null)
                viñetaDecisionBase.SetActive(false);

            if (imagenDesenfoque != null)
                imagenDesenfoque.SetActive(false);

            if (opcionGuardada == 1 && ruta1Container != null)
                ruta1Container.SetActive(true);

            if (opcionGuardada == 2 && ruta2Container != null)
                ruta2Container.SetActive(true);

            if (opcionGuardada == 3 && ruta3Container != null)
                ruta3Container.SetActive(true);

            LockDecisions();
        }
    }
}