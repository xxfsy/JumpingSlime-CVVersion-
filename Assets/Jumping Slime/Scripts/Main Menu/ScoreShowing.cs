using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreShowing : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _isSDKReady = false;

    private IEnumerator Start()
    {
        while (DateManager.Instance == null || !DateManager.Instance.IsDateLoad)
        {
            yield return new WaitForSeconds(0.2f);
        }
        _isSDKReady = true;

        _text = GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    private void OnEnable()
    {
        if (_isSDKReady)
            UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _text.SetText(_text.text + " " + $"<color=red>{DateManager.Instance.BestScore}</color>");
    }
}
