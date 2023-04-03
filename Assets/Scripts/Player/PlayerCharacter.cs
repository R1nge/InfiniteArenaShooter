using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health;
        
        public void TakeDamage(int amount)
        {
            if (health <= 0)
            {
                //Show popup (try again, return home)
            }
        }
    }
}