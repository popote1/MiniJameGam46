using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IndependentAudioSource : MonoBehaviour
{
    [SerializeField] private float _fadeTime;
    private AudioSource _audioSource;

    private AudioClip _nextSong;
    
    private enum Stat {
        Stop , Playing, FadeIn, FadeOut
    }

    private Stat _stat;
    private float _timer;
    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong(AudioClip audioClip) {
        if (_stat == Stat.Stop) {
            _audioSource.clip = audioClip;
            _audioSource.Play();
            _stat = Stat.FadeIn;
            return;
        }

        if (_stat == Stat.FadeOut) {
            _nextSong = audioClip;
            return;
        }
        _nextSong = audioClip;
        _stat = Stat.FadeOut;
    }

    private void Update()
    {
        if (_stat == Stat.Playing && _stat == Stat.Stop) return;
        if( _stat==Stat.FadeIn) ManageFadeIn();
        if( _stat==Stat.FadeOut) ManageFadeOut();
        
    }

    private void ManageFadeIn() {
        _timer += Time.deltaTime;
        _audioSource.volume = _timer / _fadeTime;
        if (_timer >= _fadeTime) {
            _audioSource.volume = 1;
            _stat = Stat.Playing;
        }
    }
    private void ManageFadeOut() {
        _timer -= Time.deltaTime;
        _audioSource.volume = _timer / _fadeTime;
        if (_timer >= _fadeTime) {
            _audioSource.volume = 0;
            if (_nextSong == null) {
                _stat = Stat.Stop;
                _audioSource.Stop();
            }
            else
            {
                _audioSource.clip = _nextSong;
                _audioSource.Play();
                _stat = Stat.FadeIn;
            }
            
            
        }
    }
    
    
    
    
}