using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personajes
{
    internal class Controller
    {
        string[] defaultEnemyNames = new string[10] { "Pedro", "Juan", "Alberto", "Jefferson", "Ignacio", "Jaime", "Enrique", "Gabriel", "Rodrigo", "Luis" };

        const int MAX_ENEMIES = 3;
        const int MAX_SKILL_POINTS = 100;
        const int ENEMIES_SKILL_POINTS = 60;

        public Hero SetHero()
        {
            Console.WriteLine("Welcome to adventure, dear...");

            string name = Console.ReadLine();

            int skillPoints = MAX_SKILL_POINTS;
            int attack, health;

            Console.WriteLine($"Now, please allocate your skill points. You've {MAX_SKILL_POINTS}");

            bool canContinue = false;

            do
            {
                Console.WriteLine("How many health points?");

                health = int.Parse(Console.ReadLine());

                canContinue = health < skillPoints && health > 0;

                if (!canContinue) Console.WriteLine("Please allocate a valid quantity");

            } while (!canContinue);

            skillPoints -= health;

            do
            {
                Console.WriteLine("How many attack points?");

                attack = int.Parse(Console.ReadLine());

                canContinue = attack <= skillPoints && attack > 0;

                if (!canContinue) Console.WriteLine($"Please allocate a valid quantity. You only have {skillPoints} points left");

            } while (!canContinue);

            skillPoints -= attack;

            if(skillPoints > 0)
            {
                Console.WriteLine($"You've {skillPoints} skill points more. Do you want to allocate it (Yes/No)");

                do
                {
                    string answer = Console.ReadLine();

                    if(answer == null)
                    {
                        canContinue = false;
                        continue;
                    }
                    
                    answer = answer.Trim().ToLower();

                    if(answer == "yes" || answer == "y")
                    {
                        Console.WriteLine("Do you want to allocate it in:");
                        Console.WriteLine("1. Health");
                        Console.WriteLine("2. Attack");
                        Console.WriteLine("Type the number of your choice");

                        canContinue = false;

                        do
                        {
                            string choice = Console.ReadLine();

                            if(choice == null)
                            {
                                canContinue = false;
                                continue;
                            }

                            choice = choice.Trim().ToLower();

                            if(choice == "1")
                            {
                                health += skillPoints;
                                canContinue = true;

                                Console.WriteLine($"Your new health points are {health}");
                            } else if(choice == "2")
                            {
                                attack += skillPoints;
                                canContinue = true;

                                Console.WriteLine($"Your new attack points are {attack}");
                            } else
                            {
                                canContinue = false;
                                Console.WriteLine("Please! Give a proper answer");
                            }
                        } while (!canContinue);

                        canContinue = true;
                    } else if (answer == "no" || answer == "n"){
                        Console.WriteLine("So it be!");

                        canContinue = true;
                    } else
                    {
                        Console.WriteLine("Please! Give a proper answer");
                        canContinue = false;
                    }
                } while (!canContinue);
            }

            Hero.HeroArchetype archetype = new Hero.HeroArchetype();

            Console.WriteLine("Now choose your Hero archetype");
            Console.WriteLine("1. Knight: Have a x1.5 attack multiplier when below one third of max health");
            Console.WriteLine("2. Barbarian: When facing dead activates inmortality until next attack");
            Console.WriteLine("3. Monk: Hits twice, second strike with 50% of the damage");
            Console.WriteLine("4. Gunslinger: Every third attack hits critical");
            Console.WriteLine("5. Samurai: Adds a slight probability for dodges and counter attacks");
            Console.WriteLine("Type the number of your choice");

            do
            {
                string choice = Console.ReadLine();

                if(choice == null)
                {
                    canContinue= false;
                    continue;
                }

                choice = choice.Trim().ToLower();

                switch(choice)
                {
                    case "1":
                        archetype = Hero.HeroArchetype.knight;
                        break;

                    case "2":
                        archetype = Hero.HeroArchetype.barbarian;
                        break;

                    case "3":
                        archetype = Hero.HeroArchetype.monk;
                        break;

                    case "4":
                        archetype = Hero.HeroArchetype.gunslinger;
                        break;

                    case "5":
                        archetype = Hero.HeroArchetype.samurai;
                        break;

                    default:
                        Console.WriteLine("Not a valid choice. Type again");
                        canContinue = false;
                        break;

                }

            } while (!canContinue);

            return new Hero(name, health, attack, archetype);
        }

        public Character[] SetEnemies()
        {
            Random rng = new Random();

            Character[] enemies = new Character[MAX_ENEMIES];

            for(int i = 0; i < MAX_ENEMIES; i++)
            {
                string name = defaultEnemyNames[rng.Next(defaultEnemyNames.Length)];

                int health = rng.Next(10, ENEMIES_SKILL_POINTS - 10);
                int attack = ENEMIES_SKILL_POINTS - health;

                float type = rng.Next(2);

                if(type == 0) enemies[i] = new EnemyMeele(name, health, attack);
                else enemies[i] = new EnemyRanged(name, health, attack);
            }

            return enemies;
        }
    }
}
