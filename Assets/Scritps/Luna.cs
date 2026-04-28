using UnityEngine;
// Codigo de Kristian.


public class Luna : MonoBehaviour
{
    public Light moonLight;
    public Transform sunLight;
    public AnimationCurve sunIntensity;

    private float currentTime;

    void Update()
    {
        currentTime = sunLight.GetComponent<DayNightCycle>().currentTime;

        moonLight.transform.rotation = Quaternion.Euler(
            sunLight.transform.eulerAngles.x + 180f, 170f, 0f
        );

        float nightFactor = 1f - sunIntensity.Evaluate(currentTime);
        moonLight.intensity = nightFactor * 0.3f;
    }
}