using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  
/// </summary>
[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/Units/UnitData", order = 1)]
public class UnitData : ScriptableObject
{
    //--------- General properties ---------
    [Header("General properties")]
    [SerializeField]
    public string unitName;
    [SerializeField]
    public string age;
    
    // sprite data can be stored in the prefab or here
    [SerializeField]
    public GameObject prefab;
    
    
    //--------- Characteristics ---------
    [Header("Characteristics")]
    [SerializeField]
    public string type;
    [SerializeField]
    public int price;
    [SerializeField]
    public int damage;
    [SerializeField]
    public int hitSpeed;
    [SerializeField]
    public int buildTime;
    [SerializeField]
    public int walkSpeed;
    [SerializeField]
    public int range;
    [SerializeField]
    public int hitPoints;
    
    //--------- Rewards upon killing ---------
    [Header("Rewards upon killing")]
    [SerializeField]
    public int goldGiven;
    [SerializeField]
    public int experienceGiven;
}
