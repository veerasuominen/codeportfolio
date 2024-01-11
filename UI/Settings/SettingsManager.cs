using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace HexKeyGames
{
    public class SettingsManager : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider announcementsSlider;
        [SerializeField] private Slider SFXSlider;
        public AudioMixer audioMixer;

        [Header("Graphic Settings")]
        public TMPro.TMP_Dropdown resolutionDropdown;
        private Resolution[] resolutions;
        [SerializeField] private TMPro.TMP_Dropdown qualityDropDown;

        private void Start()
        {
            LoadPlayerAudioPreferences();

            //checks for previously set qualitylevel
            if (PlayerPrefs.HasKey("Quality"))
            {
                int Quality = PlayerPrefs.GetInt("Quality", 0);
                qualityDropDown.value = Quality;
            }

            resolutions = Screen.resolutions;

            //reverses the resolutions so they're listed from biggest to smallest
            Array.Reverse(resolutions);

            resolutionDropdown.ClearOptions();

            //creates a list of all the possible resolutions and refresh rates available on that PC
            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height + "@" + resolutions[i].refreshRateRatio.value.ToString("F0") + "hz";
                options.Add(option);

                if (resolutions[i].width == Screen.width &&
                    resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            //adds those resolutions and refresh rates onto a dropdown menu
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void LoadPlayerAudioPreferences()
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                LoadMasterVolume();
            }
            else
            {
                SetMasterVolume();
            }

            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                LoadMusicVolume();
            }
            else
            {
                SetMusicVolume();
            }

            if (PlayerPrefs.HasKey("AnnouncementsVolume"))
            {
                LoadAnnouncementsVolume();
            }
            else
            {
                SetAnnouncementsVolume();
            }

            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                LoadSFXVolume();
            }
            else
            {
                SetSFXVolume();
            }
        }

        //Graphic quality settings using unitys built-in system
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            PlayerPrefs.SetInt("Quality", qualityIndex);
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        //fullscreen toggle
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void MuteOverAllVolume()
        {
            audioMixer.SetFloat("MasterVolume", 0);
        }

        public void SetMasterVolume()
        {
            float volume = masterSlider.value;

            audioMixer.SetFloat("MasterVolume", volume);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void SetMusicVolume()
        {
            float musicVolume = musicSlider.value;
            audioMixer.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }

        public void SetAnnouncementsVolume()
        {
            float announcementsVolume = announcementsSlider.value;
            audioMixer.SetFloat("AnnouncementsVolume", announcementsVolume);
            PlayerPrefs.SetFloat("AnnouncementsVolume", announcementsVolume);
        }

        public void SetSFXVolume()
        {
            float SFXVolume = SFXSlider.value;
            audioMixer.SetFloat("SFXVolume", SFXVolume);
            PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        }

        private void LoadMasterVolume()
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            SetMasterVolume();
        }

        private void LoadMusicVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume();
        }

        private void LoadAnnouncementsVolume()
        {
            announcementsSlider.value = PlayerPrefs.GetFloat("AnnouncementsVolume");
            SetAnnouncementsVolume();
        }

        private void LoadSFXVolume()
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume();
        }
    }
}