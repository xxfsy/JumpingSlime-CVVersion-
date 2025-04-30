using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinGetter : MonoBehaviour
{
    [SerializeField] private Image _background;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = DataManager.Instance.CurrentPlayerSkin.SkinSprite;
        _spriteRenderer.color = DataManager.Instance.CurrentPlayerSkin.SkinColor;

        _background.sprite = DataManager.Instance.CurrentBackgroundSkin.SkinSprite;
    }
}
