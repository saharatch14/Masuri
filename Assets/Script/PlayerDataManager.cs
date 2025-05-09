using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private static PlayerDataManager _inter;
    public static PlayerDataManager inter
    {
        get
        {
            if (_inter == null)
            {
                GameObject obj = new GameObject();
                obj.name = "PlayerDataManger";
                _inter = obj.AddComponent<PlayerDataManager>();
                DontDestroyOnLoad(obj);
                _inter.Load();
            }
            return _inter;
        }
    }

    private int _gold;
    private int _item;
    private int _unlock;
    private int _startonce;

    //public int _ScoreRingTossInbottle;
    public int _ScoreRingToss;
    public int _ScoreKnockOut;
    public int _ScoreGunBlast;
    public int _FinalScore;
    private Vector3 lastlocate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _FinalScore = PlayerPrefs.GetInt("FinalScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int gold
    {
        get { return _gold; }
        set
        {
            SetValidate(ref _gold, value);
        }
    }

    public int startalready
    {
        get { return _startonce; }
        set
        {
            SetGameStartValidate(ref _startonce, value);
        }
    }

    public int ScoreRingToss
    {
        get { return _ScoreRingToss; }
        set
        {
            SetRingTossScoreValidate(ref _ScoreRingToss, value);
        }
    }
    public int ScoreKnockOut
    {
        get { return _ScoreKnockOut; }
        set
        {
            SetKnockOutScoreValidate(ref _ScoreKnockOut, value);
        }
    }
    public int ScoreGunBlast
    {
        get { return _ScoreGunBlast; }
        set
        {
            SetGunBlastScoreValidate(ref _ScoreGunBlast, value);
        }
    }

    protected void SetValidate(ref int currentValue, int newValue)
    {
        int curValue = newValue;
        curValue = curValue < 0 ? 0 : curValue;

        //int oldValue = currentValue;
        currentValue = curValue;
        Save();
    }

    protected void SetGameStartValidate(ref int currentValue, int newValue)
    {
        int curValue = newValue;
        currentValue = curValue;
        SaveGameAlredayStart();
    }

    protected void SetRingTossScoreValidate(ref int currentValue, int newValue)
    {
        int curValue = newValue;
        curValue = curValue < 0 ? 0 : curValue;

        //int oldValue = currentValue;
        currentValue = curValue;
        SaveRingTossScore();
    }

    protected void SetKnockOutScoreValidate(ref int currentValue, int newValue)
    {
        int curValue = newValue;
        curValue = curValue < 0 ? 0 : curValue;

        //int oldValue = currentValue;
        currentValue = curValue;
        SaveKnockOutScore();
    }

    protected void SetGunBlastScoreValidate(ref int currentValue, int newValue)
    {
        int curValue = newValue;
        curValue = curValue < 0 ? 0 : curValue;

        //int oldValue = currentValue;
        currentValue = curValue;
        SaveGunBlastScore();
    }

    protected void SetFinalScoreValidate(ref int currentValue, int newValue)
    {
        int curValue = newValue;
        curValue = curValue < 0 ? 0 : curValue;

        //int oldValue = currentValue;
        currentValue = curValue;
        SaveFinalScore();
    }

    public void Load()
	{
        //_gold = PlayerPrefs.GetInt("PlayerGold");
        //lastlocate = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
    }

    public Vector3 LoadLocate()
    {
        lastlocate = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        PlayerPrefs.SetInt("PlayerHaveLocate", 0);
        return lastlocate;
    }

    public void Save()
	{
        PlayerPrefs.SetInt("PlayerGold", _gold);
    }
    public void SaveGameAlredayStart()
    {
        PlayerPrefs.SetInt("GameAlredayStart", _startonce);
    }

    public void SaveRingTossScore()
    {
        PlayerPrefs.SetInt("ScoreRingToss", _ScoreRingToss);
#if UNITY_EDITOR
        Debug.Log($"||RingToss: {_ScoreRingToss} || KnockOut: {_ScoreKnockOut} || GunBlast: {_ScoreGunBlast}");
#endif
    }

    public void SaveKnockOutScore()
    {
        PlayerPrefs.SetInt("ScoreKnockOut", _ScoreKnockOut);
#if UNITY_EDITOR
        Debug.Log($"||RingToss: {_ScoreRingToss} || KnockOut: {_ScoreKnockOut} || GunBlast: {_ScoreGunBlast}");
#endif
    }
    public void SaveGunBlastScore()
    {
        PlayerPrefs.SetInt("ScoreGunBlast", _ScoreGunBlast);
#if UNITY_EDITOR
        Debug.Log($"||RingToss: {_ScoreRingToss} || KnockOut: {_ScoreKnockOut} || GunBlast: {_ScoreGunBlast}");
#endif
    }

    public void SaveFinalScore()
    {
        _FinalScore = _ScoreRingToss + _ScoreKnockOut + _ScoreGunBlast;
        PlayerPrefs.SetInt("FinalScore", _FinalScore);
#if UNITY_EDITOR
        Debug.Log($"||RingToss: {_ScoreRingToss} || KnockOut: {_ScoreKnockOut} || GunBlast: {_ScoreGunBlast}");
        Debug.Log($"||FinalScore: {_FinalScore}");
#endif
    }

    public void SaveLocate(Transform player)
    {
        PlayerPrefs.SetInt("PlayerHaveLocate", 1);
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
    }
    public static int skinPickedID
    {
        set { PlayerPrefs.SetInt("skinPickedID", value); }
        get { return PlayerPrefs.GetInt("skinPickedID", 0); }
    }

    public static void UnlockSkin(int id)
    {
        PlayerPrefs.SetInt("UnlockSkin" + id, 1);
    }

    public static void PickSkin(int id)
    {
        PlayerPrefs.SetInt("PickSkin" + id, 1);
    }
    public static bool isSkinUnlocked(int id)
    {
        return PlayerPrefs.GetInt("UnlockSkin" + id, 0) == 1 ? true : false;
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("FinalScore", 0);
        PlayerPrefs.SetInt("ScoreRingToss", 0);
        PlayerPrefs.SetInt("ScoreKnockOut", 0);
        PlayerPrefs.SetInt("ScoreGunBlast", 0);
        PlayerPrefs.SetInt("GameAlredayStart", 0);
        PlayerPrefs.SetInt("UnlockSkin" + 1, 0);
        PlayerPrefs.SetInt("PlayerGold", 0);
        Debug.Log("Reset Complete");
    }
}
