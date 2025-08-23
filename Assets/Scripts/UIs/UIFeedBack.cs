using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIFeedBack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private float _aniamtionTime = 0.3f;
    [SerializeField] private float _maxSize = 1.1f;
    [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private AudioElementSFX _onPointerSFX;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOPause();
        transform.DOScale(_maxSize, _aniamtionTime).SetEase(_animationCurve);
        _onPointerSFX.Play();
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.DOPause();
        transform.DOScale(1, _aniamtionTime).SetEase(_animationCurve);
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        transform.DOPause();
        transform.localScale = Vector3.one;
        
    }
}