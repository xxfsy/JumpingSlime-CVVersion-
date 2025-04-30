using UnityEngine;

public class EntrypointMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _dateManager, _soundManager, _sceneSwitcher;

    private static bool _isFirstRun = true;

    private void Awake()
    {
        if (_isFirstRun)
        {
            Debug.Log("EntryPoint");

            GameObject soundManager = Instantiate(_soundManager);
            SoundManager soundManagerComponent = soundManager.GetComponent<SoundManager>();
            soundManagerComponent.Initialize();

            GameObject dataManager = Instantiate(_dateManager);
            DataManager dataManagerComponent = dataManager.GetComponent<DataManager>();
            dataManagerComponent.Initialize();

            GameObject sceneSwitcher = Instantiate(_sceneSwitcher);
            SceneSwitcher sceneSwitcherComponent = sceneSwitcher.GetComponent<SceneSwitcher>();
            sceneSwitcherComponent.Initialize();

            _isFirstRun = false;
        }
    }
}
