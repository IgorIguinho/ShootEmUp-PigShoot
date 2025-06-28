using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configs : MonoBehaviour
{
    public static Configs Instance { get; private set; }

    public GameObject configGroup;

    [Header("VolumeUI Varible")]
    public Slider sliderVolumeGeral;
    public Slider sliderVolumeSoundTrack;
    public Slider sliderVolumeSoundEffcts;

    [Header("Sounds Variable")]
    public float volumeGeral;
    public float volumeSoundTrack;
    public float volumeSoundEffcts;
    public AudioSource soundTrackAudioSource;
   public List<AudioSource> soundEffctsAudioSource;

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
        volumeGeral = sliderVolumeGeral.value;
        volumeSoundTrack = sliderVolumeSoundTrack.value * volumeGeral;
        volumeSoundEffcts = sliderVolumeSoundEffcts.value * volumeGeral;
        if (soundEffctsAudioSource != null)
        {
            foreach(AudioSource source in soundEffctsAudioSource)
            {
                source.volume = volumeSoundTrack;
            }
        }
        if (soundTrackAudioSource != null)
        {
            soundTrackAudioSource.volume = volumeSoundTrack;
        }
    }

    public void OpenConfigGroup(bool open)
    {
        configGroup.SetActive(open);
        if (open)
        {
            Time.timeScale = 0f;
        }
        else { Time.timeScale = 1.0f; }
    }

  
}
