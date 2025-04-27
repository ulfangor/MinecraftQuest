using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider1;
    public Slider slider2;

    // Start is called before the first frame update
    void Start()
    {
        slider1.value = audioSource.volume;
        slider2.value = audioSource.volume;
    }

    private void Update()
    {
        slider1.value = audioSource.volume;
        slider2.value = audioSource.volume;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("MinecraftQuest");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OnChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MinecraftQuest");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
