using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreShowing : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _isDataLoaded = false;

    private IEnumerator Start()
    {
        while (DataManager.Instance == null || !DataManager.Instance.IsDateLoad)
        {
            yield return new WaitForSeconds(0.2f);
        }
        _isDataLoaded = true;

        _text = GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    private void OnEnable()
    {
        if (_isDataLoaded)
            UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _text.SetText(_text.text + " " + $"<color=red>{DataManager.Instance.BestScore}</color>");
    }
}
