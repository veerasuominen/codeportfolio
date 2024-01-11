using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

namespace HexKeyGames
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject confirmExit;
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject loadingScreenPanel;
        [SerializeField] private bool confirmExitActive = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneByName("MainMenu").isLoaded && confirmExitActive == false)
            {
                confirmExit.SetActive(true);

                confirmExitActive = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneByName("MainMenu").isLoaded && confirmExitActive == true)
            {
                CloseConfirmExit();
                confirmExitActive = false;
            }
        }

        public void CreditsButton()
        {
            if (creditsPanel.activeSelf == false)
            {
                creditsPanel.SetActive(true);
                settingsPanel.SetActive(false);
            }
            else if (creditsPanel.activeSelf == true)
            {
                creditsPanel.SetActive(false);
            }
        }

        public void OptionsButton()
        {
            if (settingsPanel.activeSelf == false)
            {
                settingsPanel.SetActive(true);
                creditsPanel.SetActive(false);
            }
            else if (settingsPanel.activeSelf == true)
            {
                settingsPanel.SetActive(false);
            }
        }

        //public void CloseCredits()
        //{
        //    mainMenu.SetActive(true);
        //    creditsPanel.SetActive(false);
        //}

        public void OpenConfirmExit()
        {
            confirmExit.SetActive(true);
            confirmExitActive = true;
        }

        public void CloseConfirmExit()
        {
            confirmExit.SetActive(false);
            confirmExitActive = false;
        }

        public void StartButton()
        {
            loadingScreenPanel.SetActive(true);
            StartCoroutine(LoadSceneAfterUpdate(1));
        }

        private IEnumerator LoadSceneAfterUpdate(int sceneBuildIndex)
        {
            yield return null; // Wait one frame
            SceneManager.LoadScene(sceneBuildIndex);
        }

        public void OpenSettingsPanel()
        {
            mainMenu.SetActive(false);
            settingsPanel.SetActive(true);
        }

        //public void CloseSettingsMenu()
        //{
        //    settingsPanel.SetActive(false);
        //    mainMenu.SetActive(true);
        //}

        public void BackToMainMenu()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}