using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import this namespace for TextMeshPro support
using System.Collections;

public class ButtonActivator : MonoBehaviour
{
    public GameObject wall; // The wall to move
    public Button uiButton; // The UI button to show/hide
    public TextMeshProUGUI timerText; // The TextMeshPro text for the timer
    public float lowerSpeed = 2f; // Speed of wall movement
    public float lowerDepth = 1f; // How far the wall should move on the X-axis
    public float timerDuration = 5f; // Timer duration in seconds

    private Vector3 originalPosition; // Store the original position of the wall
    private Coroutine movementCoroutine; // Active coroutine for wall movement
    private bool isTimerRunning = false; // Is the timer active?
    private float timer; // Timer value

    private void Start()
    {
        if (wall == null)
        {
            return;
        }

        originalPosition = wall.transform.position; // Store wall's starting position
        uiButton.gameObject.SetActive(false); // Hide the UI button at the start
        timerText.gameObject.SetActive(false); // Hide the timer text at the start

        // Assign the UI button's click listener
        uiButton.onClick.AddListener(StartTimer);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && uiButton != null)
        {
            uiButton.gameObject.SetActive(true); // Show the UI button when player steps on
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && uiButton != null)
        {
            uiButton.gameObject.SetActive(false); // Hide the UI button when player steps off
        }
    }

    private void StartTimer()
    {
        if (isTimerRunning)
            return;

        // Start the timer
        GetComponent<AudioSource>().Play();
        timer = timerDuration;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true); // Show the timer UI

        // Start moving the wall to the right
        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveWall(originalPosition.x + lowerDepth)); // Move the wall along the X-axis
        StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        // Run the timer countdown
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString(); // Update the timer text
            yield return null;
        }

        // When timer ends, reset the wall
        isTimerRunning = false;
        timerText.gameObject.SetActive(false); // Hide the timer UI

        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveWall(originalPosition.x)); // Move the wall back to the original position
    }

    private IEnumerator MoveWall(float targetX)
    {
        // Move the wall smoothly along the X-axis
        while (Mathf.Abs(wall.transform.position.x - targetX) > 0.01f)
        {
            float newX = Mathf.MoveTowards(wall.transform.position.x, targetX, lowerSpeed * Time.deltaTime);
            wall.transform.position = new Vector3(newX, wall.transform.position.y, wall.transform.position.z); // Adjust only the X position
            yield return null;
        }

        wall.transform.position = new Vector3(targetX, wall.transform.position.y, wall.transform.position.z); // Ensure the wall reaches the exact target position
    }
}
