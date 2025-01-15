
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

[Serializable]
public class PlaylistItem
{
    public AudioClip clip;
    public string SongName;
    public string Artist;
    public Color ColorTint;
}
public class RadioController : MonoBehaviour
{
    public AudioSource AudioSource;
    [SerializeField]
    public List<PlaylistItem> Playlist;
    public int CurrentTrackIndex = 0;
    public TextMeshProUGUI SongName_GUI;
    public TextMeshProUGUI Artist_GUI;
    public TextMeshProUGUI Time_GUI;
    public Material bars;
    public MeshRenderer grid;
    public Material ui;
    public int Volume_Level = 4;
    public float Volume_Level_0 = 0.0f;
    public float Volume_Level_1 = 0.2f;
    public float Volume_Level_2 = 0.33f;
    public float Volume_Level_3 = 0.66f;
    public float Volume_Level_4 = 1.00f;
    public Image Play_button;
    public Image Pause_button;
    public Image Image_Mute;
    public Image Image_Volume_1;
    public Image Image_Volume_2;
    public Image Image_Volume_3;
    public Image Image_Volume_4;
    private Vector4 transparent = new Vector4(1, 1, 1, 0.3f);

    private bool StartingToPlay = false;
    private bool Pausing = false;
    private bool Paused = true;
    public bool waiting_to_play = false;
    private float Pause_time = 0.0f;
    private void Start()
    {
        AudioSource.clip = Playlist[CurrentTrackIndex].clip;
        SongName_GUI.text = Playlist[CurrentTrackIndex].SongName;
        Artist_GUI.text = Playlist[CurrentTrackIndex].Artist;
        bars.SetColor("_Color", Playlist[CurrentTrackIndex].ColorTint);
        ui.SetColor("_Color", Playlist[CurrentTrackIndex].ColorTint);
        grid.material.SetColor("_GridLineColor", Playlist[CurrentTrackIndex].ColorTint);
        StartCoroutine(Delayed_start());
    }

    private IEnumerator Delayed_start()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        Paused = false;
        AudioSource.Play();
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

    }

    private void OnApplicationFocus(bool focus)
    {
        //if (focus == false)
        //{
        //    Paused = true;
        //}
        //else
        //{
        //    Paused = false;
        //    AudioSource.UnPause();
        //}
    }
    public IEnumerator Delayed_previous()
    {
        Paused = true;
        AudioSource.Stop();
        CurrentTrackIndex--;
        if (CurrentTrackIndex < 0)
        {
            CurrentTrackIndex = Playlist.Count - 1;
        }
        SongName_GUI.text = "...Loading Track " + (CurrentTrackIndex + 1) +"/" + Playlist.Count;
        Artist_GUI.text = "";
        var song_1 = Playlist[CurrentTrackIndex];
        var length_int_seconds = song_1.clip.length;
        var length_seconds = length_int_seconds % 60;
        var length_minutes = (length_int_seconds - length_seconds) / 60;
        Time_GUI.text = $"00:00 | {length_minutes.ToString("00")}:{length_seconds.ToString("00")}";
        yield return new WaitForSecondsRealtime(1.9f);
        var song = Playlist[CurrentTrackIndex];
        AudioSource.clip = song.clip;
        SongName_GUI.text = song.SongName;
        Artist_GUI.text = song.Artist;
        waiting_to_play = true;
        AudioSource.pitch = 1.0f;
        StartingToPlay = false;
        Paused = false;
        Pausing = false;
        AudioSource.Play();
        bars.SetColor("_Color", song.ColorTint);
        ui.SetColor("_Color", song.ColorTint);
        grid.material.SetColor("_GridLineColor", song.ColorTint);
    }
    public IEnumerator Delayed_next()
    {
        Paused = true;
        AudioSource.Stop();
        CurrentTrackIndex++;
        if (CurrentTrackIndex >= Playlist.Count)
        {
            CurrentTrackIndex = 0;
        }
        SongName_GUI.text = "...Loading Track " + (CurrentTrackIndex + 1) + "/" + Playlist.Count;
        var song_1 = Playlist[CurrentTrackIndex];
        var length_int_seconds = song_1.clip.length;
        var length_seconds = length_int_seconds % 60;
        var length_minutes = (length_int_seconds - length_seconds) / 60;
        Time_GUI.text = $"00:00 | {length_minutes.ToString("00")}:{length_seconds.ToString("00")}";

        Artist_GUI.text = "";
        yield return new WaitForSecondsRealtime(1.9f);
        var song = Playlist[CurrentTrackIndex];
        AudioSource.clip = song.clip;
        loading = false;
        SongName_GUI.text = song.SongName;
        Artist_GUI.text = song.Artist;
        waiting_to_play = true;
        AudioSource.pitch = 1.0f;
        StartingToPlay = false;
        Paused = false;
        Pausing = false;
        AudioSource.Play();
        bars.SetColor("_Color", song.ColorTint);
        ui.SetColor("_Color", song.ColorTint);
        grid.material.SetColor("_GridLineColor", song.ColorTint);
    }
    private bool loading = false;
    private Coroutine loading_routine;
    public void Previous()
    {
        if (loading == true)
        {
            StopCoroutine(loading_routine);
        }

        if (waiting_to_play == true) return;
        loading = true;
        loading_routine = StartCoroutine(Delayed_previous());


    }
    public void Next()
    {
        if (loading == true)
        {

            StopCoroutine(loading_routine);
        }

        if (waiting_to_play == true) return;
        loading = true;
        loading_routine = StartCoroutine(Delayed_next());

    }
    public void Play()
    {
        if (Paused == false) return;

        Pausing = false;
        StartingToPlay = true;
        Paused = false;
        AudioSource.pitch = 1.0f;
        Pause_time = 0.1f;


        Play_button.color = transparent;
        Pause_button.color = Color.white;
        AudioSource.UnPause();

    }

    public void Pause()
    {
        StartingToPlay = false;
        Pausing = true;
        AudioSource.pitch = 1.0f;

        Play_button.color = Color.white;
        Pause_button.color = transparent;
        Pause_time = 1.0f;
    }
    public void Update()
    {
        if (AudioSource.isPlaying)
        {
            var length_int_seconds = Mathf.FloorToInt(AudioSource.clip.length);
            var length_seconds = length_int_seconds % 60;
            var length_minutes = (length_int_seconds - length_seconds) / 60;
            var elapsed_int_seconds = Mathf.FloorToInt(AudioSource.time);
            var elapsed_seconds = (elapsed_int_seconds % 60);
            var elapsed_minutes = (elapsed_int_seconds - elapsed_seconds) / 60;
            if (AudioSource.time > 0.25)
            {
                waiting_to_play = false;
            }
            Time_GUI.text = $"{elapsed_minutes.ToString("00")}:{elapsed_seconds.ToString("00")} | {length_minutes.ToString("00")}:{length_seconds.ToString("00")}";
        }
        if (AudioSource.isPlaying == false && Paused == false && waiting_to_play == false)
        {
            Next();
        }

        if (Pausing == true)
        {
            Paused = true;
            Pause_time = Mathf.Lerp(Pause_time, 0.0f, Time.unscaledDeltaTime * 2.5f);
            if (Pause_time < 0.1f)
            {
                Pausing = false;
                AudioSource.Pause();

            }
            AudioSource.pitch = Pause_time;
        }
        if (StartingToPlay == true)
        {
            Pause_time = Mathf.Lerp(Pause_time, 1.0f, Time.unscaledDeltaTime * 4.5f);
            if (Pause_time > 0.95f)
            {
                StartingToPlay = false;
                Paused = false;

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
