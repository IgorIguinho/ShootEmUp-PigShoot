using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfoStats : MonoBehaviour
{
    public static InfoStats Instance { get; private set; }

    public float elapsedTime;
    public Text timer;
    

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerGame();
    }

    void timerGame()
    {
        elapsedTime += Time.deltaTime;
        timer.text = FormatTime(elapsedTime);
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Calcula os minutos
        int seconds = Mathf.FloorToInt(time % 60); // Calcula os segundos
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Formata para exibição
    }
}
