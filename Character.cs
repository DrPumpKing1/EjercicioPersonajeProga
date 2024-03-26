using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personajes
{
    internal class Character
    {
        protected string _name;

        protected string _type;

        public string Name { get { return _name; } }

        protected float _health;

        public float Health { get { return _health; } }

        protected float _maxHealth;

        public float MaxHealth { get { return _maxHealth; } }

        protected float _attack;

        public float Attack { get { return _attack; } }

        public Character(string name, float health, float attack)
        {
            this._name = name;
            this._maxHealth = health;
            this._health = health;
            this._attack = attack;
        }

        public virtual bool TakeDamageIsDead(float damage, Character attacker)
        {
            _health -= Math.Clamp(damage, 0f, _maxHealth);

            Console.WriteLine($"{attacker.Name} dealt {damage} damage to {_name}!");

            if (!CheckDeath()) return false;

            else
            {
                Console.WriteLine($"{_name} was killed by {attacker.Name}!");
                return true;
            }
        }

        public virtual float DealDamage()
        {
            return _attack;
        }

        public virtual bool CheckDeath()
        {
            return _health <= 0f;
        }

        public string GetCharacterType()
        {
            return _type;
        }
    }
}
