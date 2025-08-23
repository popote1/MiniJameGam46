using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HUDSaisonAnnonser : MonoBehaviour {
    [SerializeField] private float _fadeInAniamtionTime = 0.5f;
    [SerializeField] private float _delayBeforeEnd = 2f;
    [SerializeField] private float _fadeOutAnimationTime = 1.5f;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _txtLable;
    void Start() {
        StaticEvent.OnSaisonChange+= StaticEventOnOnSaisonChange;
        _canvasGroup.alpha = 0;
    }

    private void OnDestroy()
    {
        StaticEvent.OnSaisonChange-= StaticEventOnOnSaisonChange;
    }

    private void StaticEventOnOnSaisonChange(object sender, StaticData.Saison e) {

        if (e == StaticData.Saison.Winter)
        {PlayAnnonser("WINTER");
            
        }
        else
        {
            PlayAnnonser("WINTER ENDED"); 
        }
        
    }

    private void PlayAnnonser(string label) {
        _txtLable.text = label;
        _canvasGroup.transform.DOPause();
        _canvasGroup.transform.localEulerAngles = new Vector3(0, 90, 0);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, _fadeInAniamtionTime);
        _canvasGroup.transform.DOLocalRotate(new Vector3(0, 0, 0), _fadeInAniamtionTime);
        _canvasGroup.DOFade(0, _fadeOutAnimationTime).SetDelay(_delayBeforeEnd);
    }

    
    void Update()
    {
        
    }
}
