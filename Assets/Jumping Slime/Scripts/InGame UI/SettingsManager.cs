using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _joystick; 

    [SerializeField] private Toggle _soundToggle, _musicToggle;
    [SerializeField] private Image _soundCheckmark, _musicCheckmark;

    private void Start() 
    {
        if (SoundManager.Instance.BackgroundMusicAudioSource.mute)
        {
            _musicCheckmark.color = Color.black;
            _musicToggle.isOn = true;
        }
        else
        {
            _musicCheckmark.color = Color.white;
            _musicToggle.isOn = false;
        }

        if (SoundManager.Instance.GeneralSoundsAudioSources[0].mute)
        {
            _soundCheckmark.color = Color.black;
            _soundToggle.isOn = true;
        }
        else
        {
            _soundCheckmark.color = Color.white;
            _soundToggle.isOn = false;
        }
    }

    public void OpenSettings()
    {
        Time.timeScale = 0;
        _settingsPanel.SetActive(true);
        if (YandexGame.EnvironmentData.isMobile) 
            _joystick.SetActive(false);
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
        if (YandexGame.EnvironmentData.isMobile) 
            _joystick.SetActive(true);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        CloseSettings();
        SceneManager.LoadScene(0);
    }

    public void SoundMuteToggleChanged(bool toogleValue)
    {
        if (toogleValue)
            _soundCheckmark.color = Color.black;
        else
            _soundCheckmark.color = Color.white;

        foreach (AudioSource audioSource in SoundManager.Instance.GeneralSoundsAudioSources)
        {
            audioSource.mute = toogleValue;
        }
    }

    public void MusicMuteToggleChanged(bool toogleValue)
    {
        if (toogleValue)
            _musicCheckmark.color = Color.black;
        else
            _musicCheckmark.color = Color.white;

        SoundManager.Instance.BackgroundMusicAudioSource.mute = toogleValue;
    }
}
