using Common;
using UnityEngine;

namespace Supinfo.Project.Common
{
    public class ThinkerWithDelay : MonoBehaviour
    {
        [SerializeField]
        private BrainWithDelay brain;

        private void FixedUpdate()
        {
            StartCoroutine(brain.ThinkWithDelay(this));
        }
    }
}