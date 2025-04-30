using UnityEngine;

[System.Serializable]
public class SkinInfo
{
    [SerializeField] internal Sprite _skinSprite;
    [SerializeField] internal Color _skinColor = Color.white;
    public Sprite SkinSprite => _skinSprite;
    public Color SkinColor => _skinColor;
    public bool IsBought, IsChoosen;
    public int Cost;
    public int IndexInArray;
}
