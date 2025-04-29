using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class DateManager : MonoBehaviour
{
    public static DateManager Instance { get; private set; }

    private int _coinsCount;
    public int CoinsCount => _coinsCount;
    private int _bestScore;
    public int BestScore => _bestScore;
    [SerializeField] private SkinInfo[] _players, _backgrounds;
    public SkinInfo[] Players => _players;
    public SkinInfo[] Backgrounds => _backgrounds;
    private SkinInfo _currentPlayerSkin, _currentBackgroundSkin;
    public SkinInfo CurrentPlayerSkin => _currentPlayerSkin;
    public SkinInfo CurrentBackgroundSkin => _currentBackgroundSkin;

    private bool _isDateLoad = false;
    public bool IsDateLoad => _isDateLoad;

    public void Initialize() 
    {
        Debug.Log("Date Manager Start Initialize");
        if(Instance != null)
        {
            throw new UnityException("One Date Manager Instance only!");
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Date Manager Start Initialized");

        StartCoroutine(LoadDateFromCloud());
    }

    public void SaveToCloud(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == SceneSwitcher.Instance.MainMenuSceneName && _isDateLoad)
        {
            Debug.Log("SAVE TO CLOUD");

            YandexGame.savesData.CoinsCount = _coinsCount;
            YandexGame.savesData.BestScore = _bestScore;

            YandexGame.savesData.CurrentPlayerSkin = JsonUtility.ToJson(_currentPlayerSkin);
            YandexGame.savesData.CurrentBackgroundSkin = JsonUtility.ToJson(_currentBackgroundSkin);
            YandexGame.savesData.PlayerSkins = JsonHelper.ToJson(_players);
            YandexGame.savesData.BackgroundSkins = JsonHelper.ToJson(_backgrounds);

            YandexGame.SaveProgress();
        }
    }

    private IEnumerator LoadDateFromCloud()
    {
        while(!YandexGame.SDKEnabled)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("LOAD FROM CLOUD FIRST VISIT == " + YandexGame.savesData.IsFirstVisit);

        _coinsCount = YandexGame.savesData.CoinsCount;
        _bestScore = YandexGame.savesData.BestScore;

        if(!YandexGame.savesData.IsFirstVisit)
        {
            _players = JsonHelper.FromJson<SkinInfo>(YandexGame.savesData.PlayerSkins);
            _backgrounds = JsonHelper.FromJson<SkinInfo>(YandexGame.savesData.BackgroundSkins);
            _currentPlayerSkin = JsonUtility.FromJson<SkinInfo>(YandexGame.savesData.CurrentPlayerSkin);
            _currentBackgroundSkin = JsonUtility.FromJson<SkinInfo>(YandexGame.savesData.CurrentBackgroundSkin);
        }
        else
        {
            _currentPlayerSkin = _players[0];
            _currentPlayerSkin.IsBought = true;
            _currentPlayerSkin.IsChoosen = true;
            _currentBackgroundSkin = _backgrounds[0];
            _currentBackgroundSkin.IsBought = true;
            _currentBackgroundSkin.IsChoosen = true;

            YandexGame.savesData.PlayerSkins = JsonHelper.ToJson(_players);
            YandexGame.savesData.BackgroundSkins = JsonHelper.ToJson(_backgrounds);
            YandexGame.savesData.CurrentPlayerSkin = JsonUtility.ToJson(_currentPlayerSkin);
            YandexGame.savesData.CurrentBackgroundSkin = JsonUtility.ToJson(_currentBackgroundSkin);

            YandexGame.savesData.IsFirstVisit = false;
            YandexGame.SaveProgress();
        }

        _isDateLoad = true;
    }

    public void SaveCoins(int coins)
    {
        _coinsCount = coins;
    }

    public void SaveBestScore(int score)
    {
        _bestScore = score;
    }    

    public void UpdateCurrentPlayerSkin(SkinInfo currentPlayerSkin)
    {
        _currentPlayerSkin = currentPlayerSkin;
    }

    public void UpdateCurrentBackgroundSkin(SkinInfo currentBackgroundSkin)
    {
        _currentBackgroundSkin = currentBackgroundSkin;
    }
}
