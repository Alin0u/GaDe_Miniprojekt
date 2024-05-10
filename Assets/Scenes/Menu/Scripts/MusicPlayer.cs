using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MusicPlayerMenu : MonoBehaviour
{
    public AudioSource menuSource;
    public Slider volumeSlider;
    private string filePath;

    private float musicVolume = 1f;

    void Start()
    {
        filePath = Application.persistentDataPath + "/volume.txt";
        LoadVolume();
        menuSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    void Update()
    {
        LoadVolume();
        menuSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
        menuSource.volume = musicVolume;
        File.WriteAllText(filePath, musicVolume.ToString());
    }

    private void LoadVolume()
    {
        if (File.Exists(filePath))
        {
            string volumeString = File.ReadAllText(filePath); 
            if (float.TryParse(volumeString, out float loadedVolume))
            {
                musicVolume = loadedVolume;
            }
        }
    }
}
