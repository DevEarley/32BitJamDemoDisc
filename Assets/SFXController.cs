using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource SFXAudioSource;

    public AudioClip Startup_SFX;
    public AudioClip Continue_SFX;
    public AudioClip SelectGame_SFX;
    public AudioClip ChangeSelection_SFX;
    public AudioClip Cancel_SFX;
    public AudioClip OptionTick_SFX;
    public AudioClip GoBack_SFX;

    public void Play_Startup_SFX()
    {
        SFXAudioSource.PlayOneShot(Startup_SFX);
    }

    public void Play_Continue_SFX()
    {
        SFXAudioSource.PlayOneShot(Continue_SFX);
    }

    public void Play_SelectGame_SFX()
    {
        SFXAudioSource.PlayOneShot(SelectGame_SFX);
    }

    public void Play_ChangeSelection_SFX()
    {
        SFXAudioSource.PlayOneShot(ChangeSelection_SFX);
    }

    public void Play_Cancel_SFX()
    {
        SFXAudioSource.PlayOneShot(Cancel_SFX);
    }

    public void Play_OptionTick_SFX()
    {
        SFXAudioSource.PlayOneShot(OptionTick_SFX);
    }

    public void Play_GoBack_SFX()
    {
        SFXAudioSource.PlayOneShot(GoBack_SFX);
    }
}
