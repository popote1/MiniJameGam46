using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SaisonFeedBackManager : MonoBehaviour
{
    [SerializeField] private Light2D _mainLight2D;
    [Header("Winter Effects")] 
    [SerializeField] private AudioClip _winterMusic;
    [SerializeField] private AudioClip _winterambiance;
    [SerializeField] private Color _winterLightColor;
    [SerializeField] private ParticleSystem _psWinter;
    
    [Header("Sunny Days")]
    [SerializeField] private AudioClip _sunnyDaysMusic;
    [SerializeField] private AudioClip _sunnyDaysAmbiance;
    [SerializeField] private Gradient _sunnyDaysLightColor;
    [SerializeField] private ParticleSystem _psSunnyDays;

    private void Awake()
    {
        StaticEvent.OnSaisonChange+= StaticEventOnOnSaisonChange;
    }
    private void OnDestroy() {
        StaticEvent.OnSaisonChange-= StaticEventOnOnSaisonChange;
    }

    private void Start() {
        PlaySunnyDays();
    }


    private void StaticEventOnOnSaisonChange(object sender, StaticData.Saison e)
    {
        if( e== StaticData.Saison.Winter)PlayWinter();
        else PlaySunnyDays();
    }

    private void PlayWinter() {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.PlayMusic(_winterMusic);
            AudioManager.Instance.PlayAmbiance(_winterambiance);
        }
        _psSunnyDays.Stop();
        _psWinter.Play();
    }

    private void PlaySunnyDays() {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.PlayMusic(_sunnyDaysMusic);
            AudioManager.Instance.PlayAmbiance(_sunnyDaysAmbiance);
        }
        _psSunnyDays.Play();
        _psWinter.Stop();
    }

    private void Update() {
        if (StaticData.CurrentSaison == StaticData.Saison.Winter) {
            _mainLight2D.color = _winterLightColor;
        }
        else {
            _mainLight2D.color = _sunnyDaysLightColor.Evaluate(StaticData.SaisonProgress);
        }
    }
}