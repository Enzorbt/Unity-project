using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObject/Turrets/TurretData", order = 1)]
public class TurretData : ScriptableObject
{
    //--------- General properties ---------
    [Header("General properties")]
    [SerializeField]
    public string turretName;
    [SerializeField]
    public string age;
    
    // sprite data can be stored in the prefab or here
    [SerializeField]
    public GameObject prefab;
    
    //--------- Characteristics ---------
    [Header("Characteristics")]
    [SerializeField]
    public int price;
    [SerializeField]
    public int damage;
    [SerializeField]
    public int hitSpeed;
    [SerializeField]
    public int range;
}
