using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private GameObject _scoreGameObject;
    private TextMeshProUGUI _scoreText;

    private float _score = 0f;
    private int _maxScore = int.MinValue;

    private void Start()
    {
        _scoreText = _scoreGameObject.GetComponent<TextMeshProUGUI>();
        _scoreText.SetText("0");
    }

    private void Update()
    {
        _score = transform.position.y * 100;
        if (_score > _maxScore)
        {
            _maxScore = Mathf.RoundToInt(transform.position.y * 100);
            _scoreText.SetText($"{_maxScore}");

            if (_maxScore > DateManager.Instance.BestScore)
                DateManager.Instance.SaveBestScore(_maxScore);

        }
    }
}
