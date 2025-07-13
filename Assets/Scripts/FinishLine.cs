using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float Delay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            Invoke("LoadNextScene", Delay);
        }
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)    
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes in the build settings!");
            SceneManager.LoadScene(0);
        }
    }
}
