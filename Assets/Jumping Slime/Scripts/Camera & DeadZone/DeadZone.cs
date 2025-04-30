using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private PlatformSpawner _platformSpawner;

    [SerializeField] private GameObject _RewardWindow, _topPanelUI;
    [SerializeField] private GameObject _joystick;
    private bool _isRewardSuggested = false;
    private Collider2D _playerCollision;

    public bool IsAnimOfDeadShowing = false;
    private bool _isDeadZoneMoved = false;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GiveSecondLive;
        YandexGame.ErrorVideoEvent += GameOver;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GiveSecondLive;
        YandexGame.ErrorVideoEvent -= GameOver;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerCollision = collision;

            if (!_isDeadZoneMoved)
            {
                if (!_isRewardSuggested)
                {
                    if (YandexGame.EnvironmentData.isMobile)
                    {
                        _joystick.SetActive(false);
                    }
                    _topPanelUI.SetActive(false);
                    _RewardWindow.SetActive(true);
                    Time.timeScale = 0;
                    _isRewardSuggested = true;
                }
                else
                {
                    GameOver();
                }
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (collision.gameObject.CompareTag("Green Platform"))
        {
            Destroy(collision.gameObject);
            _platformSpawner.GeneratePlatform();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    public void WatchRewardVid()
    {
        Time.timeScale = 0;
        // YandexGame.RewVideoShow(0); turned of ads for CV version
        GiveSecondLive(0); // for CV version
    }

    private void GiveSecondLive(int index)
    {
        if (index == 0)
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                _joystick.SetActive(true);
                _joystick.GetComponent<Joystick>().ResetInput();
            }

            _topPanelUI.SetActive(true);
            _RewardWindow.SetActive(false);
            Time.timeScale = 1;
            _playerCollision.gameObject.transform.position = _platformSpawner.gameObject.transform.GetChild(3).transform.position + Vector3.up;
        }
    }

    public void GameOver()
    {
        _RewardWindow.SetActive(false);
        _topPanelUI.SetActive(false);
        Time.timeScale = 1;

        IsAnimOfDeadShowing = true;
        this.transform.SetParent(null, true);
        this.transform.position = new Vector3(transform.position.x, transform.position.y - 15, transform.position.z);
        _playerCollision.gameObject.GetComponent<PlayerMovement>().IsDead = true;
        _isDeadZoneMoved = true;
    }
}
