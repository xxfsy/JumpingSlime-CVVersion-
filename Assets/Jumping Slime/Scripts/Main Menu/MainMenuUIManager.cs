using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel, _settingsPanel;
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
        _mainPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
        _mainPanel.SetActive(true);
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
