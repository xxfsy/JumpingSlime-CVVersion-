using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class ShopManager : MonoBehaviour
{
    private SkinInfo[] _players;
    private SkinInfo[] _backgrounds;
    [SerializeField] private Image _backgroundUIImage, _playerUIImage;
    [SerializeField] private TextMeshProUGUI _backgroundBuyButtonText, _playerBuyButtonText, _backgroundCostText, _playerCostText;

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private int _coinsCount;

    private int _currentPlayersIndex = 0, _currentBackgroundIndex = 0;
    private SkinInfo _currentChosenPlayerSkin, _currentChosenBackgroundSkin;

    [SerializeField] RewardManager _rewardManager;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += CheckAfterReward;
        YandexGame.ErrorVideoEvent += () => CheckAfterReward(100);
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= CheckAfterReward;
        YandexGame.ErrorVideoEvent -= () => CheckAfterReward(100);
    }

    public void CheckAfterReward(int index)
    {
        _coinsCount = DateManager.Instance.CoinsCount;
        SetPlayerInfoOnUi(_currentPlayersIndex);
        SetBackgroundInfoOnUi(_currentBackgroundIndex);
    }

    private IEnumerator Start()
    {
        while (!YandexGame.SDKEnabled)
        {
            yield return new WaitForSeconds(0.2f);
        }

        _players = DateManager.Instance.Players;
        _backgrounds = DateManager.Instance.Backgrounds;
        _coinsCount = DateManager.Instance.CoinsCount;
        _currentChosenPlayerSkin = DateManager.Instance.CurrentPlayerSkin;
        _currentChosenBackgroundSkin = DateManager.Instance.CurrentBackgroundSkin;

        _coinsText.SetText($"{_coinsCount}");

        _currentPlayersIndex = _currentChosenPlayerSkin.IndexInArray;
        _currentBackgroundIndex = _currentChosenBackgroundSkin.IndexInArray;
        SetPlayerInfoOnUi(_currentPlayersIndex);
        SetBackgroundInfoOnUi(_currentBackgroundIndex);
    }

    private void SetPlayerInfoOnUi(int index)
    {
        _playerUIImage.sprite = _players[index].SkinSprite;
        _playerUIImage.color = _players[index].SkinColor;

        if (_players[index].IsBought == false)
        {
            _playerCostText.SetText($"{_players[index].Cost}");

            switch (YandexGame.lang)
            {
                case "ru":
                    _playerBuyButtonText.SetText("Купить");
                    break;

                case "en":
                    _playerBuyButtonText.SetText("Buy");
                    break;

                case "tr":
                    _playerBuyButtonText.SetText("Almak");
                    break;

                default:
                    goto case "en";
            }
        }
        else if (_players[index].IsBought == true && _players[index].IsChoosen == false)
        {
            switch (YandexGame.lang)
            {
                case "ru":
                    _playerCostText.SetText("Куплено");
                    _playerBuyButtonText.SetText("Выбрать");
                    break;

                case "en":
                    _playerCostText.SetText("Purchased");
                    _playerBuyButtonText.SetText("Select");
                    break;

                case "tr":
                    _playerCostText.SetText("Satin Alindi");
                    _playerBuyButtonText.SetText("Seсmek");
                    break;

                default:
                    goto case "en";
            }
        }
        else if (_players[index].IsBought == true && _players[index].IsChoosen == true)
        {
            switch (YandexGame.lang)
            {
                case "ru":
                    _playerCostText.SetText("Куплено");
                    _playerBuyButtonText.SetText("Выбрано");
                    break;

                case "en":
                    _playerCostText.SetText("Purchased");
                    _playerBuyButtonText.SetText("Selected");
                    break;

                case "tr":
                    _playerCostText.SetText("Satin Alindi");
                    _playerBuyButtonText.SetText("Secme");
                    break;

                default:
                    goto case "en";
            }
        }
    }

    private void SetBackgroundInfoOnUi(int index)
    {
        _backgroundUIImage.sprite = _backgrounds[index].SkinSprite;

        if (_backgrounds[index].IsBought == false)
        {
            _backgroundCostText.SetText($"{_backgrounds[index].Cost}");

            switch (YandexGame.lang)
            {
                case "ru":
                    _backgroundBuyButtonText.SetText("Купить");
                    break;

                case "en":
                    _backgroundBuyButtonText.SetText("Buy");
                    break;

                case "tr":
                    _backgroundBuyButtonText.SetText("Almak");
                    break;

                default:
                    goto case "en";
            }
        }
        else if (_backgrounds[index].IsBought == true && _backgrounds[index].IsChoosen == false)
        {
            switch (YandexGame.lang)
            {
                case "ru":
                    _backgroundCostText.SetText("Куплено");
                    _backgroundBuyButtonText.SetText("Выбрать");
                    break;

                case "en":
                    _backgroundCostText.SetText("Purchased");
                    _backgroundBuyButtonText.SetText("Select");
                    break;

                case "tr":
                    _backgroundCostText.SetText("Satin Alindi");
                    _backgroundBuyButtonText.SetText("Secmek");
                    break;

                default:
                    goto case "en";
            }

        }
        else if (_backgrounds[index].IsBought == true && _backgrounds[index].IsChoosen == true)
        {
            switch (YandexGame.lang)
            {
                case "ru":
                    _backgroundCostText.SetText("Куплено");
                    _backgroundBuyButtonText.SetText("Выбрано");
                    break;

                case "en":
                    _backgroundCostText.SetText("Purchased");
                    _backgroundBuyButtonText.SetText("Selected");
                    break;

                case "tr":
                    _backgroundCostText.SetText("Satin Alindi");
                    _backgroundBuyButtonText.SetText("Secme");
                    break;

                default:
                    goto case "en";
            }
        }
    }

    public void NextPlayerSkin()
    {
        if (_currentPlayersIndex < _players.Length - 1)
        {
            _currentPlayersIndex++;
            SetPlayerInfoOnUi(_currentPlayersIndex);
        }
        else
        {
            _currentPlayersIndex = 0;
            SetPlayerInfoOnUi(_currentPlayersIndex);
        }
    }

    public void NextBackgroundSkin()
    {
        if (_currentBackgroundIndex < _backgrounds.Length - 1)
        {
            _currentBackgroundIndex++;
            SetBackgroundInfoOnUi(_currentBackgroundIndex);
        }
        else
        {
            _currentBackgroundIndex = 0;
            SetBackgroundInfoOnUi(_currentBackgroundIndex);
        }
    }

    public void PreviousPlayerSkin()
    {
        if (_currentPlayersIndex > 0)
        {
            _currentPlayersIndex--;
            SetPlayerInfoOnUi(_currentPlayersIndex);
        }
        else
        {
            _currentPlayersIndex = _players.Length - 1;
            SetPlayerInfoOnUi(_currentPlayersIndex);
        }
    }

    public void PreviousBackgroundSkin()
    {
        if (_currentBackgroundIndex > 0)
        {
            _currentBackgroundIndex--;
            SetBackgroundInfoOnUi(_currentBackgroundIndex);
        }
        else
        {
            _currentBackgroundIndex = _backgrounds.Length - 1;
            SetBackgroundInfoOnUi(_currentBackgroundIndex);
        }
    }

    public void BuyPlayerSkin()
    {
        if (!_players[_currentPlayersIndex].IsBought && _coinsCount >= _players[_currentPlayersIndex].Cost)
        {
            _coinsCount -= _players[_currentPlayersIndex].Cost;
            _coinsText.SetText($"{_coinsCount}");

            DateManager.Instance.SaveCoins(_coinsCount);

            _players[_currentChosenPlayerSkin.IndexInArray].IsChoosen = false;

            _players[_currentPlayersIndex].IsBought = true;
            _players[_currentPlayersIndex].IsChoosen = true;
            _currentChosenPlayerSkin = _players[_currentPlayersIndex];

            DateManager.Instance.UpdateCurrentPlayerSkin(_currentChosenPlayerSkin);

            SetPlayerInfoOnUi(_currentPlayersIndex);
        }
        else if (_players[_currentPlayersIndex].IsBought)
        {
            _players[_currentChosenPlayerSkin.IndexInArray].IsChoosen = false;

            _players[_currentPlayersIndex].IsChoosen = true;
            _currentChosenPlayerSkin = _players[_currentPlayersIndex];

            DateManager.Instance.UpdateCurrentPlayerSkin(_currentChosenPlayerSkin);

            SetPlayerInfoOnUi(_currentPlayersIndex);
        }
        else
        {
            _rewardManager.SuggestReward();
        }
    }

    public void BuyBackgroundSkin()
    {
        if (!_backgrounds[_currentBackgroundIndex].IsBought && _coinsCount >= _backgrounds[_currentBackgroundIndex].Cost)
        {
            _coinsCount -= _backgrounds[_currentBackgroundIndex].Cost;
            _coinsText.SetText($"{_coinsCount}");

            DateManager.Instance.SaveCoins(_coinsCount);

            _backgrounds[_currentBackgroundIndex].IsBought = true;

            _backgrounds[_currentChosenBackgroundSkin.IndexInArray].IsChoosen = false;

            _backgrounds[_currentBackgroundIndex].IsBought = true;
            _backgrounds[_currentBackgroundIndex].IsChoosen = true;
            _currentChosenBackgroundSkin = _backgrounds[_currentBackgroundIndex];

            DateManager.Instance.UpdateCurrentBackgroundSkin(_currentChosenBackgroundSkin);

            SetBackgroundInfoOnUi(_currentBackgroundIndex);
        }
        else if (_backgrounds[_currentBackgroundIndex].IsBought)
        {
            _backgrounds[_currentChosenBackgroundSkin.IndexInArray].IsChoosen = false;

            _backgrounds[_currentBackgroundIndex].IsChoosen = true;
            _currentChosenBackgroundSkin = _backgrounds[_currentBackgroundIndex];

            DateManager.Instance.UpdateCurrentBackgroundSkin(_currentChosenBackgroundSkin);

            SetBackgroundInfoOnUi(_currentBackgroundIndex);
        }
        else
        {
            _rewardManager.SuggestReward();
        }
    }

    public void ExitFromShop()
    {
        SceneManager.LoadScene(0);
    }
}
