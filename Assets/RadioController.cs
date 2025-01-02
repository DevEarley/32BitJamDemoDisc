
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

[Serializable]
public class PlaylistItem
{
    public AudioClip clip;
    public string SongName;
    public string Artist;

}
public class RadioController : MonoBehaviour
{
    public AudioSource AudioSource;
    [SerializeField]
    public List<PlaylistItem> Playlist;
    public int CurrentTrackIndex = 0;
    public TextMeshProUGUI SongName_GUI;
    public TextMeshProUGUI Artist_GUI;
    public int Volume_Level = 4;
    public float Volume_Level_0 = 0.0f;
    public float Volume_Level_1 = 0.2f;
    public float Volume_Level_2 = 0.33f;
    public float Volume_Level_3 = 0.66f;
    public float Volume_Level_4 = 1.00f;
    public Image Image_Mute;
    public Image Image_Volume_1;
    public Image Image_Volume_2;
    public Image Image_Volume_3;
    public Image Image_Volume_4;
    private Vector4 transparent = new Vector4(1, 1, 1, 0.3f);

    private void Start()
    {
        AudioSource.clip = Playlist[CurrentTrackIndex].clip;
        SongName_GUI.text = Playlist[CurrentTrackIndex].SongName;
        Artist_GUI.text = Playlist[CurrentTrackIndex].Artist;
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void Next()
    {
        AudioSource.pitch = 1.0f;
        Playing = false;
        Pausing = false;
        CurrentTrackIndex++;
        if (CurrentTrackIndex >= Playlist.Count)
        {
            CurrentTrackIndex = 0;
        }
        AudioSource.clip = Playlist[CurrentTrackIndex].clip;
        SongName_GUI.text = Playlist[CurrentTrackIndex].SongName;
        Artist_GUI.text = Playlist[CurrentTrackIndex].Artist;
        AudioSource.Play();

    }
    public void Previous()
    {
        AudioSource.pitch = 1.0f;
        Playing = false;
        Pausing = false;
        CurrentTrackIndex--;
        if (CurrentTrackIndex < 0)
        {
            CurrentTrackIndex = Playlist.Count - 1;
        }
        AudioSource.clip = Playlist[CurrentTrackIndex].clip;
        SongName_GUI.text = Playlist[CurrentTrackIndex].SongName;
        Artist_GUI.text = Playlist[CurrentTrackIndex].Artist;
        AudioSource.Play();
    }

    public void Play()
    {
        Pausing = false;
        Playing = true;

        AudioSource.pitch = 1.0f;
        AudioSource.Play();
    }
    private bool Playing = false;
    private bool Pausing = false;
    private float Pause_time = 0.0f;
    public void Pause()
    {
        Playing = false;
        Pausing = true;
        AudioSource.pitch = 1.0f;

        Pause_time = 1.0f;
    }

    public void Update()
    {
        if (Pausing == true)
        {
            Pause_time = Mathf.Lerp(Pause_time, 0.0f, Time.unscaledDeltaTime * 2.5f);
            if (Pause_time < 0.1f)
            {
                Pausing = false;
                AudioSource.Pause();

            }
            AudioSource.pitch = Pause_time;
        }
        if (Playing == true)
        {
            Pause_time = Mathf.Lerp(Pause_time, 1.0f, Time.unscaledDeltaTime * 4.5f);
            if (Pause_time > 0.95f)
            {
                Playing = false;
                AudioSource.pitch = 1.0f;
            }
            else
            {
                AudioSource.pitch = Pause_time;
            }
        }
    }

    public void SetVolume(int volume)
    {
        Volume_Level = volume;
        switch (Volume_Level)
        {
            case 0:
                AudioSource.volume = Volume_Level_0;
                Image_Mute.color = Color.white;
                Image_Volume_1.color = transparent;
                Image_Volume_2.color = transparent;
                Image_Volume_3.color = transparent;
                Image_Volume_4.color = transparent;
                break;
            case 1:
                AudioSource.volume = Volume_Level_1;
                Image_Mute.color = Color.white;
                Image_Volume_1.color = Color.white;
                Image_Volume_2.color = transparent;
                Image_Volume_3.color = transparent;
                Image_Volume_4.color = transparent;
                break;
            case 2:
                AudioSource.volume = Volume_Level_2;
                Image_Mute.color = Color.white;
                Image_Volume_1.color = Color.white;
                Image_Volume_2.color = Color.white;
                Image_Volume_3.color = transparent;
                Image_Volume_4.color = transparent;
                break;
            case 3:
                AudioSource.volume = Volume_Level_3;
                Image_Mute.color = Color.white;
                Image_Volume_1.color = Color.white;
                Image_Volume_2.color = Color.white;
                Image_Volume_3.color = Color.white;
                Image_Volume_4.color = transparent;
                break;
            case 4:
                AudioSource.volume = Volume_Level_4;
                Image_Mute.color = Color.white;
                Image_Volume_1.color = Color.white;
                Image_Volume_2.color = Color.white;
                Image_Volume_3.color = Color.white;
                Image_Volume_4.color = Color.white;
                break;
        }

    }

}
