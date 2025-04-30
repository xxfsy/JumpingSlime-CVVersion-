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

            GameObject dateManager = Instantiate(_dateManager);
            DateManager dateManagerComponent = dateManager.GetComponent<DateManager>();
            dateManagerComponent.Initialize();

            GameObject sceneSwitcher = Instantiate(_sceneSwitcher);
            SceneSwitcher sceneSwitcherComponent = sceneSwitcher.GetComponent<SceneSwitcher>();
            sceneSwitcherComponent.Initialize();

            _isFirstRun = false;
        }
    }
}
