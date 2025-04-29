using UnityEngine;
using YG;
using System.Collections;

public class LanguageSwitcher : MonoBehaviour
{
    private string[] _languages = new string[] { "ru", "en", "tr" };

    private string _currentLaguage;
    private int _currentLanguageIndex;

    private IEnumerator Start()
    {
        while (!YandexGame.SDKEnabled)
        {
            yield return new WaitForSeconds(0.2f);
        }

        _currentLaguage = YandexGame.lang;

        for(int i = 0; i < _languages.Length; i++)
        {
            if (_languages[i] == _currentLaguage)
                _currentLanguageIndex = i;
        }

    }

    public void SwitchLanguage()
    {
        //Debug.Log("lng switched");

        if (_currentLanguageIndex == _languages.Length - 1)
            _currentLanguageIndex = 0;
        else
            _currentLanguageIndex++;

        YandexGame.SwitchLanguage(_languages[_currentLanguageIndex]);
        _currentLaguage = YandexGame.lang;
    }
}
