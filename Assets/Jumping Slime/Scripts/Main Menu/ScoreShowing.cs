using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreShowing : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private IEnumerator Start()
    {
        while (DateManager.Instance == null || !DateManager.Instance.IsDateLoad)
        {
            yield return new WaitForSeconds(0.2f);
        }

        _text = GetComponent<TextMeshProUGUI>();
        _text.SetText(_text.text + " " + $"<color=red>{DateManager.Instance.BestScore}</color>");
    }

    private void OnEnable()
    {
        if (DateManager.Instance.IsDateLoad)
            _text.SetText(_text.text + " " + $"<color=red>{DateManager.Instance.BestScore}</color>");
    }
}
