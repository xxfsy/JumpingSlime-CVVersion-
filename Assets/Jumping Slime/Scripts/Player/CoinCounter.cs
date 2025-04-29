using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private GameObject _coinsCountGameObject;
    private TextMeshProUGUI _coinsCountText;

    private int _coinCount = 0;

    private void Start()
    {
        _coinsCountText = _coinsCountGameObject.GetComponent<TextMeshProUGUI>();

        _coinCount = DateManager.Instance.CoinsCount;
        
        _coinsCountText.SetText($"{_coinCount}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            SoundManager.Instance.PlayCoinSound();

            _coinCount++;
            _coinsCountText.SetText($"{_coinCount}");

            DateManager.Instance.SaveCoins(_coinCount);

            Destroy(collision.gameObject);
        }
    }
}
