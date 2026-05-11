using UnityEngine;

public class Luna : MonoBehaviour
{
    public Light moonLight;
    public Transform sunLight;
    public AnimationCurve sunIntensity;

    private float currentTime;

    void Update()
    {
        // Obtener el tiempo del sol desde el script principal
        currentTime = sunLight.GetComponent<DayNightCycle>().currentTime;

        // La luna está opuesta al sol
        moonLight.transform.rotation = Quaternion.Euler(
            sunLight.transform.eulerAngles.x + 180f, 170f, 0f
        );

        // Solo visible de noche
        float nightFactor = 1f - sunIntensity.Evaluate(currentTime);
        moonLight.intensity = nightFactor * 0.3f;
    }
}