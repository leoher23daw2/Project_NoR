using UnityEngine;

[RequireComponent(typeof(Light))]
public class ParpadeoVela : MonoBehaviour
{
    private Light _light; 

    [Header("Ajustes de Parpadeo")]
    public float intensidadMinima = 0.8f;
    public float intensidadMaxima = 1.2f;
    [Range(0f, 0.2f)]
    public float suavizado = 0.05f;

    private float _intensidadBase;
    private float _targetIntensidad;
    private float _velVariacion;

    void Start()
    {
        _light = GetComponent<Light>();
        _intensidadBase = _light.intensity;
        _targetIntensidad = _intensidadBase;
    }

    void Update()
    {
        if (Mathf.Abs(_light.intensity - _targetIntensidad) < 0.01f)
        {
            float variacion = Random.Range(intensidadMinima, intensidadMaxima);
            _targetIntensidad = _intensidadBase * variacion;
        }

        _light.intensity = Mathf.SmoothDamp(_light.intensity, _targetIntensidad, ref _velVariacion, suavizado); 
    }
}