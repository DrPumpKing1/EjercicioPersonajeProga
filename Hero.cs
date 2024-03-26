using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personajes
{
    internal class Hero : Character
    {
        public enum HeroArchetype
        {
            knight,    // x1.5 damage multiplier when under 1 third of max health
            barbarian, //becomes inmortal when dying 1 turn
            monk,      //can attack two times per turn with half the damage in the second attack
            gunslinger,//every third attack hits critical
            samurai    //dodges and counterattacks
        }

        public HeroArchetype _archetype;

        //Knight
        private bool knight;

        //Barbarian
        private bool barbarian;
        private const int INMORTAL_ATTACKS = 3;
        private bool inmortality;
        private bool inmortalityTrigger;

        //Monk
        private bool monk;

        public bool isMonk { get { return monk; } }

        private bool secondAttack;
        
        public bool SecondStrike { get { return secondAttack; } }

        //Gunslinger
        private bool gunslinger;
        private const int CRITICAL_ATTACK = 3;
        private int attackCount;

        //Samurai
        private bool samurai;
        private const int DODGE_PROBABILITY = 35;
        private const int COUNTER_ATTACK_PROBABILITY = 15;

        public Hero(string _name, float _health, float _attack, HeroArchetype _archetype) : base(_name, _health, _attack)
        {
            this._archetype = _archetype;
        
            switch (_archetype)
            {
                case HeroArchetype.knight:
                    knight = true;
                    break;
                
                case HeroArchetype.barbarian:
                    barbarian = true;
                    inmortality = true;
                    inmortalityTrigger = false;
                    break;

                case HeroArchetype.monk:
                    monk = true;
                    secondAttack = false;
                    break;

                case HeroArchetype.gunslinger:
                    gunslinger = true;
                    attackCount = 0;
                    break;

                case HeroArchetype.samurai: 
                    samurai = true;
                    break;
            }

            _type = "Hero";
        }

        public override bool TakeDamageIsDead(float damage, Character attacker)
        {
            if (samurai)
            {
                Random rand = new Random();

                float counter = rand.Next(0, 100);
                float dodge = rand.Next(0, 100);

                if(counter < COUNTER_ATTACK_PROBABILITY)
                {
                    Console.WriteLine("COUNTER ATTACK!");
                    attacker.TakeDamageIsDead(_attack, this);
                    return false;
                }

                if(dodge < DODGE_PROBABILITY)
                {
                    Console.WriteLine("DODGE");
                    return false;
                }
            }

            return base.TakeDamageIsDead(damage, attacker);
        }

        public override bool CheckDeath()
        {
            bool death = _health <= 0f;

            if (!death) return false;

            else if(barbarian && inmortality)
            {
                inmortalityTrigger = true;
                Console.WriteLine($"{_name} barbarian's will enrages hit in front of death");
                return false;
            } 
            
            else return true;
        }

        public override float DealDamage()
        {
            if(barbarian && inmortalityTrigger && inmortality)
            {
                _health = 1f;
                inmortality = false;
                Console.WriteLine($"Barbarian berserk expired, {_name} is left with 1 health point");
            } 

            if (knight)
            {
                Console.WriteLine($"With his last breath {_name} lifts his sword to the battle");
                float multiplier = _health <= _maxHealth * .33f ? 1.5f : 1f;

                return _attack * multiplier;
            }

            else if (monk)
            {
                float multiplier = secondAttack ? .5f : 1f;

                secondAttack = !secondAttack;

                return _attack * multiplier;
            }
            
            else if (gunslinger)
            {
                attackCount++;

                float multiplier = 1f;

                if(attackCount == CRITICAL_ATTACK)
                {
                    Console.WriteLine("Critical Shot!");
                    multiplier = 2f;
                    attackCount = 0;
                }

                return _attack * multiplier;
            }

            else return _attack;
        }
    }
}
