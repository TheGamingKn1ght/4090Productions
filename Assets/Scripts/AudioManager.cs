using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Singleton;
    /*
    private void Start()
    {
        AudioManager.Singleton.PlaySoundEffect("Background Music");
    }*/
    public void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] public SoundEffect[] soundEffects;

    private AudioSource CreateAudioSource()
    {
        GameObject audioSourceGO = new GameObject();
        audioSourceGO.name = "audioSource";
        audioSourceGO.transform.parent = this.transform;
        AudioSource newAudioSource = audioSourceGO.AddComponent<AudioSource>();
        audioSources.Add(newAudioSource);

        return newAudioSource;
    }
    public void PlaySoundEffect(string soundName)
    {

        foreach (SoundEffect sfx in soundEffects)
        {
            if (sfx.soundName == soundName)
            {
                PlaySoundEffect(sfx);
            }
        }

    }
    public void PlaySoundEffect(SoundEffect sfx)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = sfx.audioClip;
                audioSource.volume = sfx.volume;

                audioSource.Play();
                return;
            }
        }
        AudioSource createdAudioSource = CreateAudioSource();
        createdAudioSource.clip = sfx.audioClip;
        createdAudioSource.volume = sfx.volume;
        createdAudioSource.Play();

    }
}
[System.Serializable]

public struct SoundEffect
{
    public string soundName;
    public AudioClip audioClip;
    public float volume;

}
