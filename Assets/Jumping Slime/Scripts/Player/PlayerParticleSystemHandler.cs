using UnityEngine;

public class PlayerParicleSystemHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        ParticleSystem.MainModule mainModule = _particleSystem.main;
        ParticleSystem.ShapeModule shapeModule = _particleSystem.shape;
        mainModule.startColor = DataManager.Instance.CurrentPlayerSkin.SkinColor;
        shapeModule.texture = DataManager.Instance.CurrentPlayerSkin.SkinSprite.texture;
    }
}
