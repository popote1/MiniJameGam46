using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDDialogue : MonoBehaviour {
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _txtDialogue;
    [SerializeField] private Button _bpContinue;
    [SerializeField] private float _fadeTime = 1;
    [SerializeField] private AudioElementSFX _aesOpen;

    private void Awake() {
        StaticEvent.OnPlayDialogue+= StaticEventOnOnPlayDialogue;
        _bpContinue.onClick.AddListener(UIContinue);
        _canvasGroup.alpha = 0;
        _canvasGroup.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        StaticEvent.OnPlayDialogue-= StaticEventOnOnPlayDialogue;
    }

    private void UIContinue() {
        _canvasGroup.gameObject.SetActive(false);
        StaticData.ChangerGameStat(StaticData.GameStat.Playing);
    }

    private void StaticEventOnOnPlayDialogue(object sender, string e) {
        Debug.Log("Try to play dialogue");
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOPause();
        _canvasGroup.DOFade(1, _fadeTime);
        _txtDialogue.text = e;
        _aesOpen.Play();
    }
}