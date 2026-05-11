using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public float totalTime = 3600f; // 60 minuti in secondi
    private float timeLeft;

    [Header("Obiettivo")]
    public TextMeshProUGUI objectiveText;

    void Start()
    {
        timeLeft = totalTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Max(timeLeft, 0f);
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Diventa rosso sotto i 5 minuti
        timerText.color = timeLeft < 300f
            ? new Color(0.94f, 0.39f, 0.27f)
            : Color.white;
    }

    // Chiama questo metodo da altri script quando cambia l'obiettivo
    public void SetObjective(string text)
    {
        objectiveText.text = text;
    }
}