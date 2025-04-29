using UnityEngine;

public class PlatformSkinChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _skinsForGreenPlatform; 
    [SerializeField] private int[] _heights;
    private int _index = 0;
    private Sprite _currentSkin;
    public Sprite CurrentSkin => _currentSkin;

    public void PlatformSkinChangeCkeck(float currentY)
    {
        if(_index != _heights.Length && currentY >= _heights[_index])
        {
            _currentSkin = _skinsForGreenPlatform[_index];
            _index++;
        }
    }
}
