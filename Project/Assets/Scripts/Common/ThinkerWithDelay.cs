using Common;
using UnityEngine;

namespace Supinfo.Project.Common
{
    public class ThinkerWithDelay : MonoBehaviour
    {
        public bool IsThinking {get; set;}
        [SerializeField]
        private BrainWithDelay brain;

        public BrainWithDelay Brain
        {
            get => brain;
            set => brain = value;
        }

        private void FixedUpdate()
        {
            if (!IsThinking && Brain is not null)
            {
                StartCoroutine(Brain.ThinkWithDelay(this));
            }
        }
    }
}