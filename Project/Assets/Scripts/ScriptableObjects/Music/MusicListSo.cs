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
        
        public AudioClip GetCurrentAgeMusic()
        {
            if (currentAge >= 0 && currentAge < ageMusics.Length)
            {
                return ageMusics[currentAge];
            }
            return null;
        }

        public void UpgradeAge()
        {
            if (currentAge < ageMusics.Length - 1)
            {
                currentAge++;
            }
        }
    }
}