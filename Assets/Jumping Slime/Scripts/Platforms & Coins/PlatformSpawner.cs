using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platforms Settings")]
    [SerializeField] private GameObject _simplePlatformForSpawner;
    [SerializeField] private GameObject[] _brownPlatformsForSpawner;
    [SerializeField] private GameObject _bluePlatformForSpawner;

    [Header("Objects Settings")]
    [SerializeField] private GameObject[] _objectsForSpawner;

    [Header("Platform Spawner Settings")]
    private int _initialPlatformsCount;

    private bool _isStartGeneration;
    private float _startYCord;
    private float _currentYOfPlatform;
    private Vector3 _lowerLeftCornerPosition;
    private Vector3 _platformSize;
    private Vector3 _platformLocalScale;

    [SerializeField] private float _spaceBetweenSizeCheck = 100, _minXSizeForPlatform = 0.6f;
    private float _yCordForSizeCheck;

    [SerializeField] private int _spaceInPlatformsBetweenCoinGeneration = 20;
    private CoinGenerator _coinGenerator;
    private int _generalCountOfPlatforms = 0;

    private PlatformSkinChanger _platformSkinChanger;

    [SerializeField] private int _yCordValueToStartSpawningBluePlatforms = 100;

    private void Start()
    {
        _lowerLeftCornerPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _platformSize = _simplePlatformForSpawner.GetComponent<Renderer>().bounds.size;
        _platformLocalScale = _simplePlatformForSpawner.transform.localScale;
        _initialPlatformsCount = (int)((Mathf.Abs(_lowerLeftCornerPosition.y) * 2 / _platformSize.y) * 1.5f);
        _startYCord = _lowerLeftCornerPosition.y + (_platformSize.y * 1.5f);
        _currentYOfPlatform = _startYCord;

        _yCordForSizeCheck = _spaceBetweenSizeCheck;

        _coinGenerator = GetComponent<CoinGenerator>();

        _platformSkinChanger = GetComponent<PlatformSkinChanger>();

        _isStartGeneration = true;
        for (int i = 0; i < _initialPlatformsCount; i++)
        {
            GeneratePlatform();
        }
        _isStartGeneration = false;

    }

    public void GeneratePlatform()
    {
        int countOfGeneratedGreenPlatforms = 0;

        PlatformSizeCheck();
        _platformSkinChanger.PlatformSkinChangeCkeck(_currentYOfPlatform);

        float platformXCord = Random.Range(_lowerLeftCornerPosition.x + _platformSize.x, -_lowerLeftCornerPosition.x - _platformSize.x);
        float spaceBetweenPlatforms = _platformSize.y * Random.Range(2, 5);

        int randomChanceOfBrownPlatform = Random.Range(1, 6); // шанс 1/5

        if (randomChanceOfBrownPlatform == 1)
        {

            float brownPlatformXCord = Random.Range(_lowerLeftCornerPosition.x + _platformSize.x, -_lowerLeftCornerPosition.x - _platformSize.x);
            float brownPlatformYCord = _currentYOfPlatform + (_platformSize.y * 1.5f);
            spaceBetweenPlatforms += _platformSize.y / 2;

            Vector3 brownPlatformPosition = new Vector3(brownPlatformXCord, brownPlatformYCord, 0);
            GameObject newBrownPlatform = Instantiate(_brownPlatformsForSpawner[Random.Range(0, _brownPlatformsForSpawner.Length)], brownPlatformPosition, Quaternion.identity, this.gameObject.transform);
            newBrownPlatform.transform.localScale = _platformLocalScale;
        }

        Vector3 platformPosition = new Vector3(platformXCord, _currentYOfPlatform, 0);

        int randomChanceOfBluePlatform = Random.Range(1, 6);

        GameObject newPlatform;

        if (_currentYOfPlatform >= _yCordValueToStartSpawningBluePlatforms && randomChanceOfBluePlatform == 1) // голубая платформа
        {
            newPlatform = Instantiate(_bluePlatformForSpawner, platformPosition, Quaternion.identity, this.gameObject.transform);
        }
        else // обычная
        {
            newPlatform = Instantiate(_simplePlatformForSpawner, platformPosition, Quaternion.identity, this.gameObject.transform);
            if (_platformSkinChanger.CurrentSkin != null)
            {
                newPlatform.GetComponent<SpriteRenderer>().sprite = _platformSkinChanger.CurrentSkin;
            }
        }

        newPlatform.transform.localScale = _platformLocalScale;
        _currentYOfPlatform += spaceBetweenPlatforms + (_platformSize.y * 1.5f);
        countOfGeneratedGreenPlatforms++;
        _generalCountOfPlatforms++;

        if (_generalCountOfPlatforms % _spaceInPlatformsBetweenCoinGeneration == 0)
        {
            _coinGenerator.GenerateCoin(newPlatform.transform.position);
        }

        if (randomChanceOfBrownPlatform != 1)
        {
            TryToGenerateObjectOnPlatform(newPlatform);
        }

        if (_isStartGeneration)
            _initialPlatformsCount -= (int)(spaceBetweenPlatforms / _platformSize.y + countOfGeneratedGreenPlatforms);
    }

    private void TryToGenerateObjectOnPlatform(GameObject newPlatform)
    {
        int randomChanceOfSimpleSpringOnPlatform = Random.Range(1, 21);
        int randomChanceOfMediumSpringOnPlatform = Random.Range(1, 36);
        int randomChanceOfBigSpringOnPlatform = Random.Range(1, 61);

        _platformSize = newPlatform.GetComponent<Renderer>().bounds.size;

        float offset = 0f;
        float springXCord = Random.Range(newPlatform.transform.position.x - _platformSize.x / 2 + offset, newPlatform.transform.position.x + _platformSize.x / 2 - offset);
        float springYCord = newPlatform.transform.position.y + offset;

        Vector3 springPosition = new Vector3(springXCord, springYCord, 0);

        if (randomChanceOfSimpleSpringOnPlatform == 2)
        {
            GameObject simpleSpring = Instantiate(_objectsForSpawner[0], springPosition, Quaternion.identity);
            simpleSpring.transform.parent = newPlatform.transform;
        }
        else if (randomChanceOfMediumSpringOnPlatform == 7)
        {
            GameObject mediumSpring = Instantiate(_objectsForSpawner[1], springPosition, Quaternion.identity);
            mediumSpring.transform.parent = newPlatform.transform;
        }
        else if (randomChanceOfBigSpringOnPlatform == 19)
        {
            GameObject legendSpring = Instantiate(_objectsForSpawner[2], springPosition, Quaternion.identity);
            legendSpring.transform.parent = newPlatform.transform;
        }
    }

    private void PlatformSizeCheck()
    {
        if ((int)(_currentYOfPlatform / _yCordForSizeCheck) == 1 && _platformLocalScale.x > _minXSizeForPlatform)
        {
            _platformLocalScale = new Vector3(_platformLocalScale.x - 0.1f, _platformLocalScale.y, _platformLocalScale.z);
            _yCordForSizeCheck += _spaceBetweenSizeCheck;
        }
    }
}
