using Supinfo.Project.Common;
using UnityEngine;

/// <summary>
/// This class represents a thinker with a delay. It uses a BrainWithDelay object to perform thinking.
/// </summary>
public class ThinkerWithDelay : MonoBehaviour
{
    /// <summary>
    /// A flag that indicates whether the thinker is currently thinking.
    /// </summary>
    public bool IsThinking {get; set;}

    /// <summary>
    /// The brain that the thinker uses to perform thinking.
    /// </summary>
    [SerializeField]
    private BrainWithDelay brain;

    /// <summary>
    /// The brain that the thinker uses to perform thinking.
    /// </summary>
    public BrainWithDelay Brain
    {
        get => brain;
        set => brain = value;
    }

    /// <summary>
    /// This method is called by Unity every physics frame. It checks if the thinker is not currently thinking
    /// and if the brain is not null, then it starts a coroutine to perform thinking with a delay.
    /// </summary>
    private void FixedUpdate()
    {
        // Check if the thinker is not currently thinking
        if (!IsThinking)
        {
            // Check if the brain is not null
            if (brain != null)
            {
                // Start a coroutine to perform thinking with a delay
                StartCoroutine(brain.ThinkWithDelay(this));
            }
            else
            {
                // Log an error message if the brain is null
                Debug.LogError("Brain is null in ThinkerWithDelay");
            }
        }
    }
}