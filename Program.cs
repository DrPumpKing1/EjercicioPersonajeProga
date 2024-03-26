using Personajes;

Controller controller = new Controller();

Hero player = controller.SetHero();

Character[] enemies = controller.SetEnemies();

bool end = false;

do
{
    bool canContinue = false;

    Console.WriteLine($"{player.Name}: {player.Health}/{player.MaxHealth} HP .. {player.Attack} ATK");

    Console.WriteLine("Write the number the enemy you want to attack");

    do
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CheckDeath())
            {
                Console.WriteLine($"{i + 1}. {enemies[i].Name}(Dead)");
            } else
            {
                Console.WriteLine($"{i + 1}. {enemies[i].Name}({enemies[i].GetCharacterType()}) .. {enemies[i].Health}/{enemies[i].MaxHealth} HP .. {enemies[i].Attack} ATK");
            }
        }

        int choice = int.Parse( Console.ReadLine() );
        choice--;

        if(choice > enemies.Length || choice < 0)
        {
            canContinue = false;
            Console.WriteLine("Write a valid option");
        } else if (enemies[choice].CheckDeath())
        {
            canContinue = false;
            Console.WriteLine("This enemy is already dead. Choose another one");
        } else
        {
            float damage = player.DealDamage();
            if(damage > 0) enemies[choice].TakeDamageIsDead(damage, player);
            canContinue = true;

            if(player.isMonk && player.SecondStrike)
            {
                Console.WriteLine($"{player.Name} can perform another attack");

                canContinue = false;

                bool allDeathInSecondStrike = true;

                foreach (Character enemy in enemies)
                {
                    if (!enemy.CheckDeath())
                    {
                        allDeathInSecondStrike = false;
                        break;
                    }
                }

                if (allDeathInSecondStrike)
                {
                    Console.WriteLine("...But All enemies are already dead");
                    canContinue = true;
                }
            }
        }
    } while (!canContinue);

    bool allDeath = true;

    foreach (Character enemy in enemies)
    {
        if (!enemy.CheckDeath())
        {
            allDeath = false;
            break;
        }
    }

    if(allDeath)
    {
        Console.WriteLine($"All enemies are dead. {player.Name} raises victorious");
        end = true;
        break;
    }

    foreach (Character enemy in enemies)
    {
        if (enemy.CheckDeath()) continue;

        float damage = enemy.DealDamage();
        player.TakeDamageIsDead(damage, enemy);

        if (player.CheckDeath())
        {
            end = true;
            Console.WriteLine($"{player.Name} is dead. We all are doomed");
            break;
        }
    }
} while (!end);

