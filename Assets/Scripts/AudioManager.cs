using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    [SerializeField]private  AudioMixerGroup _mixerSfx;
    [SerializeField]private IndependentAudioSource _musicAudioSource;
    [SerializeField] private IndependentAudioSource _ambienceAudioSource;
    

    public void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            Debug.Log("There already a AudioManager in the scene");
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySFX(AudioClip audioClip, float volume) {
        if (audioClip == null) return;
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup =_mixerSfx ;
        audioSource.Play();
        Destroy(audioSource, audioClip.length+1);
    }

    public void PlayMusic(AudioClip clip) => _musicAudioSource.PlaySong(clip);
    public void PlayAmbiance(AudioClip clip) => _ambienceAudioSource.PlaySong(clip);
    
    
}