using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource MusicAudioSource;

    public AudioClip GameSelection_Music;
    public AudioClip TitleScreen_Music;
    public AudioClip _32BitJamInfoScreen_Music;
    public AudioClip OptionsScreen_Music;

    private void OnApplicationFocus(bool focus)
    {
        if(focus == false)
        {
            Pause(); 
        }
        else
        {

        Resume();
        }

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
             Pause(); 
        }
        else
        {

            Resume();
        }
    }

   

    public void Pause()
    {
        MusicAudioSource.Pause();
    }
    public void Resume()
    {
        MusicAudioSource.Play();
    }
    public void Play_GameSelection_Music() 
    {
        MusicAudioSource.Stop();
        MusicAudioSource.clip = (GameSelection_Music);
        MusicAudioSource.Play();
    }

    public void Play_TitleScreen_Music() 
    {
        MusicAudioSource.Stop();
        MusicAudioSource.clip = (TitleScreen_Music);
        MusicAudioSource.Play();
    }

    public void Play_32BitJamInfoScreen_Music() 
    {

        MusicAudioSource.Stop();
        MusicAudioSource.clip = (_32BitJamInfoScreen_Music);
        MusicAudioSource.Play();
    }

    public void Play_OptionsScreen_Music() 
    {
        MusicAudioSource.Stop();
        MusicAudioSource.clip = (OptionsScreen_Music);
        MusicAudioSource.Play();
    }

}
