using UnityEngine;
using UnityEngine.Splines;

public class MarchantShipEvent:MonoBehaviour
{

    
    [SerializeField]private SplineAnimate _splineAnimateComing;
    [SerializeField]private SplineAnimate _splineAnimateLeaving;
    [SerializeField]private float _stayTime;
    [SerializeField] private Vector2 _offsetPos;

    [SerializeField]private bool _hadLanded;

    [Header("Trader Ressources")]
    [SerializeField] private int _goldGiven =7;
    [SerializeField] private int _goldTaken =10;
    [SerializeField] private int _ressourceGiven = 7;
    [SerializeField] private int _ressourcesTaken =10;
    [SerializeField] private AudioElementSFX _sfxMerchant;
    [SerializeField, Range(0,100)] private float _chanceToInfect = 40;
    private MerchantDocks _merchantDocksTarget;
    private void Start() {
        _splineAnimateComing.Completed+= SplineAnimateOnCompleted;
        _splineAnimateLeaving.Completed+= SplineAnimateLeavingOnCompleted;
        _splineAnimateLeaving.gameObject.SetActive(false);
    }

    private void SplineAnimateLeavingOnCompleted() {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _splineAnimateLeaving.Completed-= SplineAnimateLeavingOnCompleted;
        _splineAnimateComing.Completed-= SplineAnimateOnCompleted;
    }

    private void SplineAnimateOnCompleted() {
        
        if (_hadLanded) {
            
        }
        else
        {
            DoTrade();
            _splineAnimateComing.gameObject.SetActive( false);
            _splineAnimateLeaving.gameObject.SetActive(true);
            _hadLanded = true;
            Invoke("StartPlayingreturn", _stayTime);   
        }
    }

    public void StartMarchantAnimation(MerchantDocks merchantDocks) {
        transform.position = (Vector2)(Vector3)merchantDocks.cell.position + _offsetPos;
        _merchantDocksTarget = merchantDocks;
        _splineAnimateLeaving.gameObject.SetActive(false);
        _hadLanded = false;
        _splineAnimateComing.Play();
    }

    private void StartPlayingreturn() {
        Debug.Log("PlayReturn;");
        _splineAnimateLeaving.Play();
    }

    private void DoTrade() {
        
        if(_merchantDocksTarget.tradeType == StaticData.MerchantStat.FoodToGold) {
            StaticEvent.DoPlayCue(new StructCueInformation(transform.position, StructCueInformation.CueType.Gold, Cell.TileType.Air));
            _sfxMerchant.Play();
            StaticData.ChangeFoodValue(-_ressourcesTaken);
            StaticData.ChangeGoldValue(_goldGiven);
        }
        else if (_merchantDocksTarget.tradeType == StaticData.MerchantStat.WoodToGold)
        {
            StaticEvent.DoPlayCue(new StructCueInformation(transform.position, StructCueInformation.CueType.Gold, Cell.TileType.Air));
            _sfxMerchant.Play();
            StaticData.ChangeWoodValue(-_ressourcesTaken);
            StaticData.ChangeGoldValue(_goldTaken);
        }
        else if (_merchantDocksTarget.tradeType == StaticData.MerchantStat.GoldToFood)
        {
            StaticEvent.DoPlayCue(new StructCueInformation(transform.position, StructCueInformation.CueType.ProdFram, Cell.TileType.Air));
            _sfxMerchant.Play();
            StaticData.ChangeFoodValue(_ressourceGiven);
            StaticData.ChangeGoldValue(-_goldTaken);
        }
        else if (_merchantDocksTarget.tradeType == StaticData.MerchantStat.GoldToWood)
        {
            StaticEvent.DoPlayCue(new StructCueInformation(transform.position, StructCueInformation.CueType.ProdWoof, Cell.TileType.Air));
            _sfxMerchant.Play();
            StaticData.ChangeWoodValue(_ressourceGiven);
            StaticData.ChangeGoldValue(-_goldTaken);
        }

        if (Random.Range(0, 100) < _chanceToInfect) {
            _merchantDocksTarget.SetMerchantSick();
        }
    }
    
    
}