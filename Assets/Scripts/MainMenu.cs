using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicSource; // AudioSource for background music
    [SerializeField] private AudioSource clickSoundSource;      // AudioSource for the click sound
    [SerializeField] private float delay = 1f;                 // Delay before loading the next scene

    private void Start()
    {
        // Start playing the background music in a loop
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlayGame()
    {
        // Play the click sound and load the game after a delay
        if (clickSoundSource != null)
        {
            clickSoundSource.Play();
        }

        Invoke("LoadGame", delay);
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
