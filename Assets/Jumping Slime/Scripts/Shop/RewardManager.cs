using TMPro;
using UnityEngine;
using YG;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewardWindow, _shopPanel;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private int _rewardSuggestionCDInSec;
    private int _nextRewardSuggestionTime;
    [SerializeField] private int _coinsForReward;

    private int _timeOffset;

    private void Start()
    {
        _timeOffset = _rewardSuggestionCDInSec;

        _nextRewardSuggestionTime = DateManager.Instance.NextRewardSuggestionTimeForShop;
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GiveMoney;
        YandexGame.ErrorVideoEvent += CancelRewardSuggest;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GiveMoney;
        YandexGame.ErrorVideoEvent -= CancelRewardSuggest;
    }

    public void SuggestReward()
    {
        if ((int)(Time.time + _timeOffset) / _nextRewardSuggestionTime >= 1)
        {
            Time.timeScale = 0;
            _rewardWindow.SetActive(true);
            _shopPanel.SetActive(false);
        }
    }

    public void WatchRewardVid()
    {
        _nextRewardSuggestionTime += (int)Time.time + _rewardSuggestionCDInSec;
        DateManager.Instance.UpdateNextRewardSuggestionTimeForShop(_nextRewardSuggestionTime);
        Time.timeScale = 0;
        YandexGame.RewVideoShow(1);
    }

    private void GiveMoney(int index)
    {
        if (index == 1)
        {
            Time.timeScale = 1;

            DateManager.Instance.SaveCoins(DateManager.Instance.CoinsCount + _coinsForReward);
            _coinsText.SetText($"{DateManager.Instance.CoinsCount}");

            _rewardWindow.SetActive(false);
            _shopPanel.SetActive(true);

        }
    }

    public void CancelRewardSuggest()
    {
        Time.timeScale = 1;

        _rewardWindow.SetActive(false);
        _shopPanel.SetActive(true);
    }
}
