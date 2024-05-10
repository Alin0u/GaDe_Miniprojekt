using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicPlayerGame : MonoBehaviour
{
    public AudioSource gameSource;
    private string filePath;

    private float musicVolume = 1f;

    void Start()
    {
        filePath = Application.persistentDataPath + "/volume.txt";
        LoadVolume();
    }

    void Update()
    {
        LoadVolume();
        gameSource.volume = musicVolume;
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
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
