using System;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using UnityEngine;
using Random = UnityEngine.Random;

public class CueManager : MonoBehaviour {
    [SerializeField] private GameObject _prfBuilding;
    [SerializeField] private GameObject _prfDestruction;
    [SerializeField] private GameObject _prfSick;
    [SerializeField] private GameObject _prfDead;
    [SerializeField] private GameObject _prfCure;
    [SerializeField] private GameObject _prfProdFarm;
    [SerializeField] private GameObject _prfProdFish;
    [SerializeField] private GameObject _prfProdWood;
    [SerializeField] private GameObject _prfGold;
    [SerializeField] private GameObject _prfMerchant;
    [Space(10)]
    [SerializeField] private AudioElementSFX _aesConstruction;
    [SerializeField] private AudioElementSFX _aesDestruction;
    [SerializeField] private AudioElementSFX _aesDead;
    [SerializeField] private AudioElementSFX _aesCure;
    [SerializeField] private AudioClip[] _audioClipSick;
    [SerializeField, Range(0, 1)] private float _sickVolume;
    [Space(10)]
    [SerializeField] private AudioElementSFX _aesSmallHouse;
    [SerializeField] private AudioElementSFX _aesBigHouse;
    [SerializeField] private AudioElementSFX _aesFarm;
    [SerializeField] private AudioElementSFX _aesFishingDock;
    [SerializeField] private AudioElementSFX _aesSawMill;
    [SerializeField] private AudioElementSFX _aesMerchantDock;
    [SerializeField] private AudioElementSFX _aesInfirmery;
    [SerializeField] private AudioElementSFX _aesWarehouse;
    
    public void Awake() {
        StaticEvent.OnPlayCue+= StaticEventOnOnPlayCue;
    }

    private void StaticEventOnOnPlayCue(object sender, StructCueInformation e) {

        switch (e.Type) {
            case StructCueInformation.CueType.Building:
                Instantiate(_prfBuilding, e.TargetPosition, Quaternion.identity);
                PlayerBuildingSFX(e);
                break;
            case StructCueInformation.CueType.Destroy:
                Instantiate(_prfDestruction, e.TargetPosition, Quaternion.identity);
                _aesDestruction.Play();
                break;
            case StructCueInformation.CueType.Sick:
                Instantiate(_prfSick, e.TargetPosition, Quaternion.identity);
                PlaySick();
                break;
            case StructCueInformation.CueType.Dead:
                Instantiate(_prfDead, e.TargetPosition, Quaternion.identity);
                _aesDead.Play();
                break;
            case StructCueInformation.CueType.Cure:
                Instantiate(_prfCure, e.TargetPosition, Quaternion.identity);
                _aesCure.Play();
                break;
            case StructCueInformation.CueType.ProdFram:Instantiate(_prfProdFarm, e.TargetPosition, Quaternion.identity);
                break;
            case StructCueInformation.CueType.ProdFish:Instantiate(_prfProdFish, e.TargetPosition, Quaternion.identity);
                break;
            case StructCueInformation.CueType.ProdWoof:Instantiate(_prfProdWood, e.TargetPosition, Quaternion.identity);
                break;
            case StructCueInformation.CueType.Gold:Instantiate(_prfGold, e.TargetPosition, Quaternion.identity);
                break;
            case StructCueInformation.CueType.Merchant:Instantiate(_prfMerchant, e.TargetPosition, Quaternion.identity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlayerBuildingSFX(StructCueInformation cue) {
        _aesConstruction.Play();

        switch (cue.CellType)
        {
            case Cell.TileType.Air:
                break;
            case Cell.TileType.Ground:
                break;
            case Cell.TileType.LittleHouse:
                _aesSmallHouse.Play();
                break;
            case Cell.TileType.BigHouse:
                _aesBigHouse.Play();
                break;
            case Cell.TileType.Sawmill:
                _aesSawMill.Play();
                break;
            case Cell.TileType.Farm:
                _aesFarm.Play();
                break;
            case Cell.TileType.Warehouse:
                _aesWarehouse.Play();
                break;
            case Cell.TileType.MerchantDock:
                _aesMerchantDock.Play();
                break;
            case Cell.TileType.Infirmary:
                _aesInfirmery.Play();
                break;
            case Cell.TileType.FishDocks:
                _aesFishingDock.Play();
                break;
            case Cell.TileType.Church:
                break;
            case Cell.TileType.PlaceableSquare:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlaySick()
    {
        if (AudioManager.Instance == null) return;
        AudioManager.Instance.PlaySFX(_audioClipSick[Random.Range(0,_audioClipSick.Length)], _sickVolume);
    }

    
}
