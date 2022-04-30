using System;
using Game.Component;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game
{
    public class Factory : IFactory
    {
        private static Factory _instance;
        
        public static Factory Instance => _instance ?? (_instance = new Factory());

        private readonly PoolGeneric<Bullet> bulletsPool = new PoolGeneric<Bullet>();
        private readonly PoolGeneric<EnemyBasic> enemies = new PoolGeneric<EnemyBasic>();

        public Bullet CreateBullet(string ownerId, float speed, float damage, Animation animation)
        {
            var bullet = bulletsPool.GetorCreate($"Bullet{ownerId}");

            if (bullet.Value == null)
            {
                bullet.Value = new Bullet(ownerId, speed, damage, animation);

                bullet.Value.OnDeactivate += () =>
                {
                    bullet.Value.SetActive(false);
                    bulletsPool.InUseToAvailable(bullet);
                };
            }
            bullet.Value.SetActive(true);
            return bullet.Value;
        }

        public Bullet CreateBullet(string ownerId, float speed, float damage, Texture texture)
        {
            var bullet = bulletsPool.GetorCreate($"Bullet{ownerId}");

            if (bullet.Value == null)
            {
                bullet.Value = new Bullet(ownerId, speed, damage, texture);

                bullet.Value.OnDeactivate += () =>
                {
                    bullet.Value.SetActive(false);
                    bulletsPool.InUseToAvailable(bullet);
                };
            }
            bullet.Value.SetActive(true);
            return bullet.Value;
        }

        public Player CreatePlayer()
        {
            return new Player("Player", 100f, 250, new Vector2(200, 860), Vector2.One);
        }

        public EnemyBasic CreateEnemyBasic()
        {
            var enemy = enemies.GetorCreate("EnemyBasic");

            if (enemy.Value == null)
            {
                var number = new Random();
                var randomActivate = number.Next(0, 100);
                
                var texture = randomActivate <= 50 ? new Texture("Texture/Enemies/Vegan1.png") : new Texture("Texture/Enemies/Vegan2.png");
                enemy.Value = new EnemyBasic("enemy", texture, 100, 2f);
                
                enemy.Value.OnDeactivate += () =>
                {
                    enemy.Value.SetActive(false);
                    enemies.InUseToAvailable(enemy);
                };
            }
            enemy.Value.SetActive(true);
            return enemy.Value;
        }

        public Boss CreateEnemyBoss()
        {
            return new Boss("Boss", 325, 350, 1.5f, new Texture($"Texture/Enemies/Boss.png"), new Vector2(600, 150));
        }
    }
}