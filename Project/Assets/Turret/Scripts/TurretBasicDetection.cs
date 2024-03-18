using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretBasicDetection : MonoBehaviour, IDetection
    {
        public Collider2D Detect(string detectTag, float range)
        {
            var targets = Physics2D.OverlapCircleAll(transform.position, range);
            var minTarget = targets[0];
            var minDistance = Vector3.Distance(transform.position, minTarget.transform.position);
            foreach (var target in targets)
            {
                if (!target.CompareTag(detectTag)) break; 
                var distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance > minDistance) break;
                minDistance = distance;
                minTarget = target;

            }
            return minTarget;
        }
    }
}