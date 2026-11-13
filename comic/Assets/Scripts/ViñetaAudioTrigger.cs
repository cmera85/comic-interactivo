using UnityEngine;

public class ViñetaAudioTrigger : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioClip clipDeAudio;
    public bool reproducirEnBucle;
    
    [Header("UI (Arrastra el Viewport aquí)")]
    public RectTransform viewport; 
    
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (viewport == null || clipDeAudio == null || AudioManager.instancia == null) return;

        // 1. Obtener coordenadas del panel
        Vector3[] panelCorners = new Vector3[4];
        rectTransform.GetWorldCorners(panelCorners);
        float panelTopY = panelCorners[1].y;
        float panelBottomY = panelCorners[0].y;

        // 2. Obtener el centro exacto de la pantalla (Viewport)
        Vector3[] viewportCorners = new Vector3[4];
        viewport.GetWorldCorners(viewportCorners);
        float viewportCenterY = (viewportCorners[0].y + viewportCorners[1].y) / 2f;

        // 3. Si el centro de la pantalla cruza esta viñeta...
        if (viewportCenterY < panelTopY && viewportCenterY > panelBottomY)
        {
            // Y no es la viñeta que ya está registrada como activa...
            if (AudioManager.instancia.viñetaActiva != this)
            {
                AudioManager.instancia.viñetaActiva = this;
                AudioManager.instancia.ReproducirAudioViñeta(clipDeAudio, reproducirEnBucle);
            }
        }
    }
}