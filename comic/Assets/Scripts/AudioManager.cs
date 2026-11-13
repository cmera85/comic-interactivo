using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Necesario para las corrutinas de tiempo

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    [Header("UI y Botones")]
    public Image buttonIMG;
    public Sprite soundOnBtn;
    public Sprite soundOffBtn;
    
    [Header("Reproductores")]
    public AudioSource bgm;
    public AudioSource fx;

    public bool isMuted = false;
    public AudioClip[] clips;

    [HideInInspector] public MonoBehaviour viñetaActiva;
    
    // Guardamos la corrutina actual para poder detenerla si hay un cambio rápido
    private Coroutine transicionActual;

    void Awake()
    {
        if (instancia == null) instancia = this;
    }

    // ---------- LÓGICA DEL BOTÓN MUTE (INTACTA) ----------
    public void ToggleMute()
    {
        isMuted = !isMuted;
        if (isMuted == true)
        {
            bgm.mute = true;
            fx.mute = true;
            buttonIMG.sprite = soundOffBtn;
        }
        else
        {
            bgm.mute = false;
            fx.mute = false;
            buttonIMG.sprite = soundOnBtn;
        }
    }

    public void PlayFX(string clipName)
    {
        AudioClip clip = System.Array.Find(clips, c => c.name == clipName);
        if (clip != null) fx.PlayOneShot(clip);
        else Debug.Log("No se encontró el audio");
    }

    // ---------- NUEVA LÓGICA DE TRANSICIONES ----------
    public void ReproducirAudioViñeta(AudioClip nuevoClip, bool enBucle)
    {
        if (nuevoClip == null) return;
        
        // Si hay un fade ejecutándose, lo detenemos para evitar choques de volumen
        if (transicionActual != null) StopCoroutine(transicionActual);

        if (enBucle)
        {
            // Si es un ambiente/bucle, iniciamos el proceso de Fade de 2 segundos
            transicionActual = StartCoroutine(FadeBGM(nuevoClip));
        }
        else
        {
            // Si es efecto corto (ej. Mateo despierta), corte instantáneo de TODO
            bgm.Stop();
            fx.Stop();
            
            fx.clip = nuevoClip;
            fx.loop = false;
            fx.volume = 1f; // Aseguramos volumen al máximo
            fx.Play();
        }
    }

    private IEnumerator FadeBGM(AudioClip nuevoClip)
    {
        // 1. FADE OUT: Apagar progresivamente el audio actual durante 1 segundo
        if (bgm.isPlaying)
        {
            float volumenInicial = bgm.volume;
            float tiempo = 0f;

            while (tiempo < 1f)
            {
                tiempo += Time.deltaTime;
                bgm.volume = Mathf.Lerp(volumenInicial, 0f, tiempo / 1f);
                yield return null;
            }
        }

        // Aseguramos que quede en silencio y detenemos efectos sueltos
        bgm.volume = 0f;
        bgm.Stop();
        fx.Stop();

        // 2. CAMBIO DE PISTA: Cargamos el nuevo ambiente
        bgm.clip = nuevoClip;
        bgm.loop = true;
        bgm.Play();

        // 3. FADE IN: Subir progresivamente el nuevo audio durante 1 segundo
        float tiempoIn = 0f;
        while (tiempoIn < 1f)
        {
            tiempoIn += Time.deltaTime;
            bgm.volume = Mathf.Lerp(0f, 1f, tiempoIn / 1f);
            yield return null;
        }

        // Asegurar que el volumen final quede al 100%
        bgm.volume = 1f;
    }

    // Método del hueso interactivo
    public void ReproducirAudioInteractivo(AudioClip clipInteractivo)
    {
        // Si estaba a mitad de un fade ambiental, lo cancelamos de golpe
        if (transicionActual != null) StopCoroutine(transicionActual);
        
        bgm.Stop();
        fx.Stop();
        
        fx.clip = clipInteractivo;
        fx.loop = false;
        fx.volume = 1f;
        fx.Play();
    }
}