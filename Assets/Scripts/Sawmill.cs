using System;
using UnityEngine;

public class Sawmill : MonoBehaviour {
    [SerializeField] private float _productionSpeed=3;
    [SerializeField] private int _productionAmout =2;

    private float _timer;
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer >= _productionSpeed) {
            _timer = 0;
            StaticData.ChangeWoodValue(_productionAmout);
        }
    }
}