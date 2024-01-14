using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObject/Turrets/TurretData", order = 1)]
    public class TurretData : ScriptableObject
    {
        //--------- General properties ---------
        [Header("General properties")] [SerializeField]
        private string _turretName;

        [SerializeField] private string _age;

        // sprite data can be stored in the prefab or here
        [SerializeField] private GameObject _prefab;

        //--------- Characteristics ---------
        [Header("Characteristics")] [SerializeField]
        private int _price;

        [SerializeField] private int _damage;
        [SerializeField] private int _hitSpeed;
        [SerializeField] private int _range;
    }
}
