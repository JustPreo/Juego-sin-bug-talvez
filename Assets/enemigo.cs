using MoreMountains.TopDownEngine;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public GameObject[] doors; // Assign the doors in the area to this array
    public GameObject[] enemies; // Assign the enemies in the area to this array

    private bool shouldUpdate = false; // Flag to control whether to continue updating
    private bool hasBeenActivated = false; // Flag to track if the script has been activated

    void Start()
    {
        // Initialize doors and enemies arrays
        doors = GameObject.FindGameObjectsWithTag("Door");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Initially disable all doors
        DisableAllDoors();
    }

    void Update()
    {
        // Check if updating is allowed
        if (!shouldUpdate)
        {
            return;
        }

        // Check if all enemies are dead
        bool allEnemiesDead = AreAllEnemiesDead();

        // Debug log the state of enemies and doors
        Debug.Log("All Enemies Dead: " + allEnemiesDead);
        Debug.Log("Number of Doors: " + doors.Length);

        // Disable doors if all enemies are dead
        if (allEnemiesDead)
        {
            DisableAllDoors();
            Debug.Log("All doors disabled.");

            // Stop updating when conditions are met
            shouldUpdate = false;

            // Deactivate the script after it has been triggered once
            hasBeenActivated = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider is the player and the script has not been activated
        if (other.CompareTag("Player") && !hasBeenActivated)
        {
            // Enable all doors after 2 seconds when the player enters
            Invoke("EnableAllDoors", 0.5f);
            shouldUpdate = true;
            Debug.Log("Player entered the area. Start updating.");
        }
    }

    bool AreAllEnemiesDead()
    {
        foreach (GameObject enemy in enemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();

            if (enemyHealth != null && enemyHealth.CurrentHealth > 0)
            {
                return false;
            }
        }

        return true;
    }

    void DisableAllDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }

    void EnableAllDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }
}