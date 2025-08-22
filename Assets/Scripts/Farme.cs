using System;
using UnityEngine;

public class Farme : MonoBehaviour {
    [SerializeField] private float _productionSpeed=2;
    [SerializeField] private int _productionAmout = 3;

    private float _timer;
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer >= _productionSpeed) {
            _timer = 0;
            StaticData.ChangeFoodValue(_productionAmout);
        }
    }
}