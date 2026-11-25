using UnityEngine;

public class ScaleEfect : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.1f;

    [SerializeField] private float speed = 2f;

    [SerializeField] private float offset = 0f;

    [SerializeField] private bool useUnscaledTime = false;

    [SerializeField] private bool randomizeOffset = false;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = transform.localScale;

        if (randomizeOffset)
            offset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        float time = useUnscaledTime ? Time.unscaledTime : Time.time;

        float factor = 1f + Mathf.Sin(time * speed + offset) * amplitude;

        transform.localScale = initialScale * factor;
    }

    private void OnDisable()
    {
        transform.localScale = initialScale;
    }
}
