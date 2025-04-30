using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance { get; private set; }

    [SerializeField] private string _gameSceneName, _shopSceneName, _mainMenuSceneName;
    [SerializeField] private Button _playButton, _shopButton;
    [SerializeField] private int _ratingGameCDInSec = 120;

    public string GameSceneName => _gameSceneName;
    public string ShopSceneName => _shopSceneName;
    public string MainMenuSceneName => _mainMenuSceneName;

    public void Initialize()
    {
        Debug.Log("SceneSwitcher Start Initialize");
        if (Instance != null)
        {
            throw new UnityException("One SceneSwitcher Instance only!");
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += DateManager.Instance.SaveToCloud;
        SceneManager.sceneLoaded += TryToSuggestGiveRating;

        Debug.Log("SceneSwitcher Start Initialized");
    }

    private void OnDestroy()
    {
        Debug.Log("Disabled");
        SceneManager.sceneLoaded -= DateManager.Instance.SaveToCloud;
        SceneManager.sceneLoaded -= TryToSuggestGiveRating;
    }

    public void TryToSuggestGiveRating(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == MainMenuSceneName && Time.time >= _ratingGameCDInSec)
        {
            YandexGame.ReviewShow(false);
        }
    }

    public void OpenGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(_shopSceneName);
    }
}
