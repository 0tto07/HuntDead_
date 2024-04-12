using UnityEngine;

public class LockpickGame : MonoBehaviour
{
    [System.Serializable]
    public class SweetSpot
    {
        public float startDegree;
        public float endDegree;
    }

    public GameObject lockpickPrefab; // Assign a prefab of the lockpick in the editor
    private GameObject lockpickInstance; // Current instance of the lockpick
    public AudioSource audioSource; // Assign an AudioSource in the editor
    public AudioClip gettingCloserClip; // Assign this AudioClip in the editor
    public AudioClip breakClip; // Assign this AudioClip in the editor
    public AudioClip successClip; // Assign this AudioClip for successful lockpick
    public SweetSpot[] sweetSpots; // Assign sweet spots in the editor

    private float currentAngle = 0f;
    private int currentSweetSpotIndex = 0;
    private bool isBroken = false;
    private int respawnCount = 0; // Counter for respawns

    void Start()
    {
        SpawnLockpick(); // Initial spawning of the lockpick
    }

    void Update()
    {
        if (isBroken || audioSource == null || lockpickInstance == null) return;

        float rotationStep = 20f * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLockpick(rotationStep);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateLockpick(-rotationStep);
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            CheckForSweetSpotClick();
        }
    }

    void SpawnLockpick()
    {
        if (lockpickInstance != null) Destroy(lockpickInstance);
        lockpickInstance = Instantiate(lockpickPrefab, transform.position, Quaternion.identity);
        lockpickInstance.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        isBroken = false;
    }

    void RotateLockpick(float rotationStep)
    {
        currentAngle += rotationStep;
        currentAngle = NormalizeAngle(currentAngle);

        if (lockpickInstance != null)
        {
            lockpickInstance.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }

        bool isInSweetSpot = IsInCurrentSweetSpot();
        ManageSound(isInSweetSpot);
    }

    bool IsInCurrentSweetSpot()
    {
        SweetSpot currentSweetSpot = sweetSpots[currentSweetSpotIndex];
        return currentAngle >= currentSweetSpot.startDegree && currentAngle <= currentSweetSpot.endDegree;
    }

    void ManageSound(bool isInSweetSpot)
    {
        if (!audioSource.isPlaying)
        {
            if (isInSweetSpot)
            {
                audioSource.clip = gettingCloserClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else if (!isInSweetSpot && audioSource.clip == gettingCloserClip)
        {
            audioSource.Stop();
        }
    }

    void CheckForSweetSpotClick()
    {
        if (IsInCurrentSweetSpot())
        {
            audioSource.PlayOneShot(successClip); // Play success audio
            currentSweetSpotIndex++;
            if (currentSweetSpotIndex >= sweetSpots.Length)
            {
                Debug.Log("All sweet spots cleared!");
                currentSweetSpotIndex = 0; // Reset or advance to next stage
            }
        }
        else
        {
            BreakLockpick();
        }
    }

    float NormalizeAngle(float angle)
    {
        while (angle < 0f) angle += 360f;
        while (angle >= 360f) angle -= 360f;
        return angle;
    }

    void BreakLockpick()
    {
        if (!isBroken)
        {
            isBroken = true;
            audioSource.PlayOneShot(breakClip);
            Destroy(lockpickInstance);
            Debug.Log("Lockpick Broken!");
            if (respawnCount < 2) // Only respawn up to two times
            {
                Invoke("SpawnLockpick", 2f); // Respawn after 2 seconds
                respawnCount++; // Increment the respawn counter
            }
        }
    }
}
