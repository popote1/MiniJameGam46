using System;
using UnityEngine;

public class ParalaxBackGround: MonoBehaviour
{

    [SerializeField] private CameraControler _cameraameraControler;
    [SerializeField] private ParallaxPlane[] _planes;
    [SerializeField] private float _parrallaxGeneralPower;
    
    [Serializable]
    private class ParallaxPlane {
        [Range(-1,1)]public float ParallaxPower;
        public GameObject Plane;
        private Vector3 _originalPos;
        
        public Vector3 OriginalPos {
            get => _originalPos;
            set => _originalPos = value;
        }


        public void SetOriginalPos()
        {
            OriginalPos = Plane.transform.position;
            Debug.Log("Origianl Pos = "+ _originalPos);
        } 
        public void ApplyDelta(Vector2 delta) => Plane.transform.position = OriginalPos + (Vector3)(delta * ParallaxPower);
    }

    private void Start() {
        foreach (var plane in _planes) {
            if (plane.Plane == null) continue;
            plane.SetOriginalPos();
        }
    }

    private void Update() {
        if (_cameraameraControler == null) return;
        Vector2 delta = _cameraameraControler.GetCameraDelta();
        foreach (var plane in _planes) {
            if (plane.Plane == null) continue;
            plane.ApplyDelta(delta*_parrallaxGeneralPower);
        }
    }
}