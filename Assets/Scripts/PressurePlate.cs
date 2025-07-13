using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
    public GameObject wall; // Assign the wall in the Inspector
    public float lowerSpeed = 2f; // Speed of lowering the wall
    public float lowerDepth = 1f; // How far the wall should lower
    private Vector3 originalPosition;
    private Coroutine movementCoroutine; // Store the active coroutine
    private int objectsOnPlate = 0; // Track how many objects are on the plate
    public GameObject specificBox; // The specific box that can activate the plate (assign in the Inspector)

    private void Start()
    {
        originalPosition = wall.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player or the specific box steps on the plate
        if (other.CompareTag("Player") || other.gameObject == specificBox)
        {
            objectsOnPlate++; // Increment the object count
            if (objectsOnPlate == 1) // Only start lowering when the first object steps on
            {
                GetComponent<AudioSource>().Play();
                StartMovementCoroutine(originalPosition.y - lowerDepth);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player or the specific box exits the plate
        if (other.CompareTag("Player") || other.gameObject == specificBox)
        {
            objectsOnPlate--; // Decrement the object count
            if (objectsOnPlate == 0) // Only start raising when no objects are left
            {
                StartMovementCoroutine(originalPosition.y);
            }
        }
    }

    private void StartMovementCoroutine(float targetY)
    {
        // Check if the GameObject is active before starting a coroutine
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        // Stop any existing movement coroutine to ensure only one is active
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }

        // Start a new movement coroutine
        movementCoroutine = StartCoroutine(MoveWall(targetY));
    }

    private IEnumerator MoveWall(float targetY)
    {
        // Move the wall to the target Y position smoothly
        while (Mathf.Abs(wall.transform.position.y - targetY) > 0.01f)
        {
            float newY = Mathf.MoveTowards(wall.transform.position.y, targetY, lowerSpeed * Time.deltaTime);
            wall.transform.position = new Vector3(wall.transform.position.x, newY, wall.transform.position.z);
            yield return null;
        }

        // Ensure the wall reaches the exact target position
        wall.transform.position = new Vector3(wall.transform.position.x, targetY, wall.transform.position.z);
        movementCoroutine = null; // Clear the active coroutine reference
    }
}
