using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Music
{
    [CreateAssetMenu(fileName = "MusicList", menuName = "ScriptableObjects/MusicList", order = 1)]
    public class MusicListSo : ScriptableObject
    {
        [SerializeField]
        private AudioClip[] ageMusics;
        public AudioClip AgeMusics => ageMusics[currentAge];
        
        [SerializeField]
        private AudioClip menuMusic;
        public AudioClip MenuMusic => menuMusic;
        

        private int currentAge;
        
        public void UpgradeAge(Component sender, object data)
        {
            currentAge++;
        }
    }
}