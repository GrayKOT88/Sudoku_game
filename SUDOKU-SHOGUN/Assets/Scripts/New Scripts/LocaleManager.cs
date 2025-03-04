using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace NewScripts
{
    public class LocaleManager : MonoBehaviour
    {
        private bool active = false;
        void Start()
        {
            int ID = PlayerPrefs.GetInt("LocaleKey", 0);
            ChangeLocale(ID);
        }
        public void StartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        public void ChangeLocale(int localeID)
        {
            if (active == true)
            {
                return;
            }
            StartCoroutine(SetLocale(localeID));
        }
        IEnumerator SetLocale(int _localeID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            PlayerPrefs.SetInt("LocaleKey", _localeID);
            active = false;
        }
    }
}