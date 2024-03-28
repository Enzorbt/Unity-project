using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Supinfo.Project.Scripts.Interfaces
{
    public interface IDetection
    {
        public Collider2D Detect(string detectTag, float range);
    }
}