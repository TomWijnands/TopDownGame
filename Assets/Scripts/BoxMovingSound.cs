using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Vector2 lastPosition;  // Store the box's position in the previous frame
    private float moveTimer = 0f;  // Timer to track how long the box has been moving
    private const float moveThreshold = 0.1f;  // Threshold time to start playing sound

    void Update()
    {
        // Check if the position has changed along the X or Y axis
        if (transform.position != (Vector3)lastPosition)
        {
            // Increase the moveTimer only when the box is moving
            moveTimer += Time.deltaTime;

            // If the box has moved for longer than the threshold time, play the sound
            if (moveTimer >= moveThreshold && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play(); // Play sound when the box moves for longer than 0.1s
            }
        }

        // Update last position to the current position
        lastPosition = transform.position;
    }
}
