using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingZone : MonoBehaviour
{
    [Tooltip("The Post Process Volume to switch")]
    public PostProcessVolume defaultPostProcessVolume;

    [Tooltip("The new Post Process Volume")]
    public PostProcessVolume newPostProcessVolume;

    [Tooltip("The transition duration in seconds when entering the zone")]
    public float enterTransitionDuration = 1f;

    [Tooltip("The transition duration in seconds when exiting the zone")]
    public float exitTransitionDuration = 1f;

    private bool isInZone = false;
    private float transitionProgress = 0f;
    private float currentTransitionDuration;

    private void Start()
    {
        newPostProcessVolume.weight = 0f;
        defaultPostProcessVolume.weight = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
            transitionProgress = 0f;
            currentTransitionDuration = enterTransitionDuration;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
            transitionProgress = 0f;
            currentTransitionDuration = exitTransitionDuration;
        }
    }

    private void Update()
    {
        if (isInZone)
        {
            transitionProgress += Time.deltaTime / currentTransitionDuration;
        }
        else
        {
            transitionProgress -= Time.deltaTime / currentTransitionDuration;
        }

        transitionProgress = Mathf.Clamp01(transitionProgress);

        newPostProcessVolume.weight = transitionProgress;
        defaultPostProcessVolume.weight = 1f - transitionProgress;
    }
}
