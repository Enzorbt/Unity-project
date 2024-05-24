// Attack : Stratégie COUNTER + TANK (Armor + RANGE) 







// Base : Acheté Deux tourelles
// Amélioration : Acheté amélioration RANGE ET ARMOR EN PRIORITÉ, PUIS LE GOLD AMELIORATION puis les autres 
// Quand +7 UNITÉ Adversaise USE CAPACITY IF POSSIBLE 
// UPGRADE AGE + TOT POSSIBLE 
// ALWAY KEEP MONEY FOR BUYING MELEE TO DEFENCE 

// UNLOCK 

// Spawn Unit : 

    // SI JOUEUR PLACE ANTI-ARMOR   
        // SPAWN JOUEUR PLACE RANGE

    // SI JOUEUR PLACE RANGE  
        // SPAWN JOUEUR PLACE MELEE

    // SI JOUEUR PLACE MELEE  
        // SPAWN JOUEUR PLACE ARMOR

    // SI JOUEUR PLACE ARMOR 
        // SPAWN JOUEUR PLACE ANTI-ARMOR 
        
    // SI JOUEUR NE PLACE RIEN  
        // SPAWN ARMOR 
        // SPAWN RANGE         
        
using UnityEngine;

namespace IA.Event
{
    public class Difficult : MonoBehaviour
    {

    }
}        