using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicPlayerMenu : MonoBehaviour
{
    public AudioSource menuSource;
    private string filePath;

    private float musicVolume = 1f;

    void Start()
    {
        filePath = Application.persistentDataPath + "/volume.txt";
    }

    void Update()
    {
        menuSource.volume = musicVolume;
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
        File.WriteAllText(filePath, musicVolume.ToString());
    }
}
