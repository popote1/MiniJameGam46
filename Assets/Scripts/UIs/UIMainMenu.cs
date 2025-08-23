using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour {
    [SerializeField] private Button _bpPlay;
    [SerializeField] private Button _bpOptions;
    [SerializeField] private Button _bpCredits;
    [SerializeField] private Button _bpQuit;
    [Space(10)]
    [SerializeField] private UILevelSelection _uiLevelSelection;
    [SerializeField] private UIOption _uiOption;
    [SerializeField] private UICredit _uiCredit;

    [SerializeField] private AudioElementSFX _OnOpenSFX;

    [SerializeField] private AudioClip _MusiqueToPlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _bpPlay.onClick.AddListener(UIOnStart);
        _bpOptions.onClick.AddListener(UIOnOption);
        _bpCredits.onClick.AddListener(UIOnCredits);
        _bpQuit.onClick.AddListener(UIOnQuite);
        
        _uiLevelSelection.OnPanelClose+= UiLevelSelectionOnOnPanelClose;
        _uiOption.OnPanelClose+= UiOptionOnOnPanelClose;
        _uiCredit.OnPanelClose+= UiCreditOnOnPanelClose;
        
        AudioManager.Instance.PlayMusic(_MusiqueToPlay);
    }

    private void OnDestroy()
    {
        _uiLevelSelection.OnPanelClose-= UiLevelSelectionOnOnPanelClose;
        _uiOption.OnPanelClose-= UiOptionOnOnPanelClose;
        _uiCredit.OnPanelClose-= UiCreditOnOnPanelClose;
    }

    private void UiCreditOnOnPanelClose(object sender, EventArgs e) {
        gameObject.SetActive(true);
        _bpCredits.Select();
        _OnOpenSFX.Play();
    }

    private void UiOptionOnOnPanelClose(object sender, EventArgs e) {
        gameObject.SetActive(true);
        _bpOptions.Select();
        _OnOpenSFX.Play();
    }

    private void UiLevelSelectionOnOnPanelClose(object sender, EventArgs e) {
        gameObject.SetActive(true);
        _bpPlay.Select();
        _OnOpenSFX.Play();
    }

    private void UIOnStart() {
        gameObject.SetActive(false);
        _uiLevelSelection.OpenPanel();
    }

    private void UIOnOption()
    {
        gameObject.SetActive(false);
        _uiOption.OpenPanel();
    }

    private void UIOnCredits()
    {
        gameObject.SetActive(false);
        _uiCredit.OpenPanel();
    }

    private void UIOnQuite()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}