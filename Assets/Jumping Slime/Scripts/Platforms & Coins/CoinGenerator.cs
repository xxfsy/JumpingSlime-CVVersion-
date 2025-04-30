using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab, _coinParent;
    private Vector3 _coinSize;

    private Vector3 _lowerLeftCornerPosition;

    private void Start()
    {
        _coinSize = _coinPrefab.GetComponent<Renderer>().bounds.size;
        _lowerLeftCornerPosition = _lowerLeftCornerPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    public void GenerateCoin(Vector3 platformPoisition)
    {
        float xCord = Random.Range(_lowerLeftCornerPosition.x + _coinSize.x, -_lowerLeftCornerPosition.x - _coinSize.x);
        float yCord = platformPoisition.y + _coinSize.y * 1.2f;
        Vector3 coinPosition = new Vector3(xCord, yCord, 0);

        Instantiate(_coinPrefab, coinPosition, Quaternion.identity, _coinParent.transform);

    }
}
