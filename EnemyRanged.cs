using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personajes
{
    internal class EnemyRanged : Character, IDamageDealer
    {
        private const int MAX_AMMO = 3;

        private int _ammo;

        public EnemyRanged(string _name, float _health, float _attack) : base(_name, _health, _attack)
        {
            _ammo = MAX_AMMO;

            _type = "Ranged";
        }

        public float DealDamage()
        {
            if (_ammo > 0) _ammo--;
            else
            {
                _ammo = MAX_AMMO;
                return 0f;
            }

            return _attack;
        }
    }
}
