using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        private PlayerCharacter _player;
        
        [Inject]
        private void Construct(PlayerCharacter player)
        {
            _player = player;
        }
        
        //Move to player
    }
}