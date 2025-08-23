using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    public event EventHandler OnPanelClose;
    [SerializeField] private Button _bpReturn;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _sliderAudioMaster;
    [SerializeField] private Slider _sliderAudioAmbiance;
    [SerializeField] private Slider _sliderAudioMusic;
    [SerializeField] private Slider _sliderAudioSFX;
    [SerializeField] private AudioElementSFX _OnOpenSFX;
    
    public void OpenPanel() {
        gameObject.SetActive(true);
        _OnOpenSFX.Play();
    }

    private void Start() {
        _bpReturn.onClick.AddListener(UIReturn);
        _sliderAudioMaster.onValueChanged.AddListener(UISliderAudioMasterChange);
        _sliderAudioAmbiance.onValueChanged.AddListener(UISliderAudioAmbianceChange);
        _sliderAudioMusic.onValueChanged.AddListener(UiSliderAudioMusicChange);
        _sliderAudioSFX.onValueChanged.AddListener(UISliderAudioSFXChange);
        SetUpSliders();
    }

    private void SetUpSliders()
    {
        _audioMixer.GetFloat("MasterVolume", out float masterVolume);
        _sliderAudioMaster.SetValueWithoutNotify(Mathf.Pow(10, masterVolume)*20);
        _audioMixer.GetFloat("MusicVolume", out float MusicVolume);
        _sliderAudioMusic.SetValueWithoutNotify(Mathf.Pow(10, MusicVolume)*20);
        _audioMixer.GetFloat("AmbianceVolume", out float AmbianceVolume);
        _sliderAudioAmbiance.SetValueWithoutNotify(Mathf.Pow(10, AmbianceVolume)*20);
        _audioMixer.GetFloat("SFXVolume", out float SFXVolume);
        _sliderAudioSFX.SetValueWithoutNotify(Mathf.Pow(10, SFXVolume)*20);
    }

    private void UIReturn() {
        OnPanelClose?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
    
    private void UISliderAudioAmbianceChange(float value) {
        _audioMixer.SetFloat("AmbianceVolume", Mathf.Log10(value) * 20);
    }
    private void UISliderAudioSFXChange(float value) {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }

    private void UiSliderAudioMusicChange(float value) {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    private void UISliderAudioMasterChange(float value) {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
}