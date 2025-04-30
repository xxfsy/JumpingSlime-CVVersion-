using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSizeScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _defaultLocalScale, _scaledScale;
    private float _scaleCoef = 1.1f;

    private void Start()
    {
        _defaultLocalScale = transform.localScale;
        _scaledScale = _defaultLocalScale * _scaleCoef;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _scaledScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _defaultLocalScale;
    }
}
