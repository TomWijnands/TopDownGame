using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance; // Singleton instance

    [Header("UI Elements")]
    public GameObject settingsPanel; // The panel for the settings menu
    public Slider volumeSlider; // Slider for volume control

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    private void Start()
    {
        // Initialize settings menu
        if (settingsPanel != null)
            settingsPanel.SetActive(false); // Start with menu hidden

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume; // Sync slider with current volume
            volumeSlider.onValueChanged.AddListener(AdjustVolume);
        }
    }

    private void Update()
    {
        // Toggle settings menu with Escape key or button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf); // Show/Hide menu
    }

    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume; // Adjust global audio volume
    }
}
