using System;
using UnityEngine;

[Serializable]
public class AudioElementSFX {
    public AudioClip Clip;
    [Range(0,1)]public float Volume =0.75f;

    public void Play() {
        if (AudioManager.Instance == null) {
            Debug.LogWarning(" Audio Element is trying to play but there no AudioManager in the scene");
            return;
        }
        if( Clip==null) return;
        AudioManager.Instance.PlaySFX(Clip, Volume);
    }
}