using UnityEngine;
// Codigo de Kristian.


public class DayNightCycle : MonoBehaviour
{
    [Header("Configuración")]
    public float dayDuration = 120f; 
    public Light sunLight;

    public float currentTime = 0f;

    void Update()
    {
        currentTime += Time.deltaTime / dayDuration;
        currentTime %= 1f;

        float sunAngle = currentTime * 360f - 90f;
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
    }
    [Header("Color del sol")]
    public Gradient sunColor;      
    public AnimationCurve sunIntensity; 

    void UpdateLighting()
    {
        sunLight.color = sunColor.Evaluate(currentTime);
        sunLight.intensity = sunIntensity.Evaluate(currentTime);

        sunLight.gameObject.SetActive(sunLight.intensity > 0.01f);
    }
}
