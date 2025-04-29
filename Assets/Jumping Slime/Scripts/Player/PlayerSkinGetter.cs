using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinGetter : MonoBehaviour
{
    [SerializeField] private Image _background;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = DateManager.Instance.CurrentPlayerSkin.SkinSprite;
        _spriteRenderer.color = DateManager.Instance.CurrentPlayerSkin.SkinColor;

        _background.sprite = DateManager.Instance.CurrentBackgroundSkin.SkinSprite;
    }
}
