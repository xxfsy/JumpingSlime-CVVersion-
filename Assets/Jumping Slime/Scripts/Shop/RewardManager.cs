using TMPro;
using UnityEngine;
using YG;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewardWindow, _shopPanel;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private int _rewardSuggestionCDInSec = 300;
    private int _nextRewardSuggestionTime;
    [SerializeField] private int _coinsForReward;

    private int _timeOffset;

    private void Start()
    {
        _timeOffset = _rewardSuggestionCDInSec;

        _nextRewardSuggestionTime = _rewardSuggestionCDInSec;
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
            _rewardWindow.SetActive(true);
            _shopPanel.SetActive(false);
        }
    }

    public void WatchRewardVid()
    {
        _nextRewardSuggestionTime += (int)Time.time + _rewardSuggestionCDInSec;
        YandexGame.RewVideoShow(1); // выключил чтобы не было рекламы в версии для резюме
        //GiveMoney(1); // for resume version
    }

    private void GiveMoney(int index)
    {
        if (index == 1)
        {
            DateManager.Instance.SaveCoins(DateManager.Instance.CoinsCount + _coinsForReward);
            _coinsText.SetText($"{DateManager.Instance.CoinsCount}");

            _rewardWindow.SetActive(false);
            _shopPanel.SetActive(true);

        }
    }

    public void CancelRewardSuggest()
    {
        _rewardWindow.SetActive(false);
        _shopPanel.SetActive(true);
    }
}
