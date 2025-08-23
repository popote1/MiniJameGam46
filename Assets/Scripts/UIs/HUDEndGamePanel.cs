using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDEndGamePanel : MonoBehaviour {
    [SerializeField] private TMP_Text _txtLabel;
    [SerializeField] private TMP_Text _txtEndGameMessage;
    [SerializeField] private Button _bpMainMenu;
    [SerializeField] private Button _bpGame;
    [SerializeField] private TMP_Text _txtButtonLabel;
    
    [Space(10)]
    [SerializeField] private float _fadeInTime  =1;
    [SerializeField] private CanvasGroup _canvasGroup;


    private bool _isWin;
    private void Start() {
        _bpGame.onClick.AddListener(UIGame);
        _bpMainMenu.onClick.AddListener(ReturnToMainMenu);
        _canvasGroup.alpha = 0;
        StaticEvent.OnEndGame+= StaticEventOnOnEndGame;
        _canvasGroup.gameObject.SetActive(false);
        
    }

    private void OnDestroy() {
        StaticEvent.OnEndGame-= StaticEventOnOnEndGame;
    }

    private void StaticEventOnOnEndGame(object sender, EndGameMessage e) {
        DisplayEndGamePanel(e);
    }

    private void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }

    private void UIGame() {
        if (_isWin) {
            StaticData.ChangerGameStat(StaticData.GameStat.Playing);
            _canvasGroup.gameObject.SetActive(false);
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void DisplayEndGamePanel(EndGameMessage message) {
        _isWin = message.IsWin;
        if (_isWin)
        {
            _txtLabel.text = "YOU SURVIVE";
            _txtButtonLabel.text = "CONTINUE PLAYING";
        }
        else {
            _txtLabel.text = "FALLEN TO THE PLAGUE";
            _txtButtonLabel.text = "RESTART";
        }

        _txtEndGameMessage.text = message.TxtEndGameMessage;
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, _fadeInTime);
    }
}