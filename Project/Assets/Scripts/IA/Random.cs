namespace IA.Event
{
    public class Random
    {
        private EventsFoundation eventInstance = new EventsFoundation();
        Random aleatoire = new Random();
        
        private int _indexSpawn;
        public int index;

        private int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }

        public void Spawn()
        {
            int minValue = 1;
            int maxValue = 10;
            index = getRand(minValue, maxValue);
            switch (index)
            {
                default:
                    // Your code here
                    break;
            }
        }
   
    }
}


// IA RANDOM 
// Fonction choisi un nombre entre p_input_1 et p_input_2

// Brain / Switch Fonctionement :

// Switch 4 Action possible (Appel fonction rand pour choisir) 
// Vérifier si action possible, si pas le cas re appel rand  (XP/ GOLD)
    // 1- Spawn Unit 
    // 2- Turret 
    // 3- Capacity
    // 4- Upgrade Age 
    // 5- Amelioration
    // Default spawn Unit 


// Switch Unit (Appel fonction rand pour choisir) 
// Vérifier si action possible, si pas le cas re appel rand (XP/ GOLD) 
    // 1- Melee
    // 2- Range
    // 3- Armor
    // 4- Anti-Armor
    // Default spawn Unit     

// Même Logique pour Capacity, Amelioration, Turret   