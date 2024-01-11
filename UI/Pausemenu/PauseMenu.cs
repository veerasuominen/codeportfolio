using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexKeyGames
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private bool isPaused = false;
        private CursorLockMode unpausedLockState = CursorLockMode.Locked;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
            {
                PauseGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
            {
                Resume();
            }
        }

        public void Resume()
        {
            isPaused = false;
            Cursor.lockState = unpausedLockState;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Player.Instance.Controls.Enable();
        }

        public void PauseGame()
        {
            isPaused = true;
            unpausedLockState = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Player.Instance.Controls.Disable();
        }
    }
}