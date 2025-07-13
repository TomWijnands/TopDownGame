using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player
    [SerializeField] private AudioClip moveSound;  // The sound to play when moving
    private AudioSource audioSource;  // Reference to the AudioSource component

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 moveInput; // Stores the input from the player

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D and AudioSource components attached to the player
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Make sure an AudioSource is attached to the player
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input from W, A, S, D or arrow keys
        float moveX = Input.GetAxisRaw("Horizontal"); // A (-1) and D (+1)
        float moveY = Input.GetAxisRaw("Vertical");   // W (+1) and S (-1)

        // Combine input into a Vector2
        moveInput = new Vector2(moveX, moveY).normalized; // Normalize to ensure consistent speed in all directions

        // Play sound when moving, and stop when idle
        HandleMovementAudio();
    }

    // FixedUpdate is called at a fixed interval for physics calculations
    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D using linearVelocity
        rb.linearVelocity = moveInput * moveSpeed; // Use 'velocity' instead of 'linearVelocity'
    }

    // Handle playing and stopping the audio based on player movement
    private void HandleMovementAudio()
    {
        if (moveInput.magnitude > 0 && !audioSource.isPlaying) // Only play if moving and no sound is playing
        {
            audioSource.PlayOneShot(moveSound); // Play the sound
        }
        else if (moveInput.magnitude == 0 && audioSource.isPlaying) // Stop if idle
        {
            audioSource.Stop(); // Stop the sound
        }
    }
}
