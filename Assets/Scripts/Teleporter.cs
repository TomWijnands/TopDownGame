using UnityEngine;
using System.Collections.Generic;

public class Teleporter : MonoBehaviour
{
    public Transform targetLocation; // The location where the object will be teleported
    public bool allowMultipleTeleports = true; // Allow the same object to teleport again
    private float cooldownTime = 0.5f; // Cooldown to prevent instant re-teleportation
    private Dictionary<GameObject, float> teleportCooldowns = new Dictionary<GameObject, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is allowed to teleport
        if (targetLocation != null && IsTeleportAllowed(other.gameObject))
        {
            GetComponent<AudioSource>().Play();
            // Teleport the object
            other.transform.position = targetLocation.position;

            // Update the cooldown for this object
            teleportCooldowns[other.gameObject] = Time.time + cooldownTime;
        }
    }

    private bool IsTeleportAllowed(GameObject obj)
    {
        // If teleporting the same object multiple times isn't allowed, ensure it's not in cooldown
        if (!allowMultipleTeleports && teleportCooldowns.ContainsKey(obj))
        {
            return Time.time >= teleportCooldowns[obj];
        }

        return true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove cooldown when the object exits the teleporter
        if (teleportCooldowns.ContainsKey(other.gameObject))
        {
            teleportCooldowns.Remove(other.gameObject);
        }
    }
}
