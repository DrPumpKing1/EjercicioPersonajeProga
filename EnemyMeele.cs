using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personajes
{
    internal class EnemyMeele : Character, IDamageDealer
    {
        private const int MAX_DURABILITY = 5;

        private int _durability;
  
        public EnemyMeele(string _name, float _health, float _attack) : base(_name, _health, _attack)
        {
            _durability = MAX_DURABILITY;

            _type = "Meele";
        }

        public float DealDamage()
        {
            if(_durability > 0) _durability--;

            float attack = _durability <= 0 ? Math.Max(_attack - 2f, 1f) : _attack;

            return attack;
        }
    }
}
