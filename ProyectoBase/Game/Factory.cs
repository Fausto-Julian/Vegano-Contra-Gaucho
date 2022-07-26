using System;
using Game.Components;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;
using Game.PhysicsEngine;

namespace Game
{
    public class Factory : IFactory
    {
        private static Factory _instance;
        
        public static Factory Instance => _instance ?? (_instance = new Factory());

        private readonly PoolGeneric<Bullet> _bulletsPool = new PoolGeneric<Bullet>();
        private readonly PoolGeneric<EnemyBasic> _enemies = new PoolGeneric<EnemyBasic>();
        
        private readonly Random _number = new Random();

        public void ClearList()
        {
            _bulletsPool.Clear();
            _enemies.Clear();
        }

        public Bullet CreateBullet(string ownerId, float speed, float damage, Animation animation)
        {
            var bullet = _bulletsPool.GetOrCreate($"Bullet{ownerId}");

            if (bullet.Value == null)
            {
                bullet.Value = new Bullet(ownerId, speed, damage, animation);

                bullet.Value.OnDeactivate += () =>
                {
                    bullet.Value.SetActive(false);
                    _bulletsPool.InUseToAvailable(bullet);
                };
            }
            bullet.Value.SetActive(true);
            return bullet.Value;
        }

        public Bullet CreateBullet(string ownerId, float speed, float damage, Texture texture)
        {
            var bullet = _bulletsPool.GetOrCreate($"Bullet{ownerId}");

            if (bullet.Value == null)
            {
                bullet.Value = new Bullet(ownerId, speed, damage, texture);

                bullet.Value.OnDeactivate += () =>
                {
                    _bulletsPool.InUseToAvailable(bullet);
                    bullet.Value.SetActive(false);
                };
            }
            bullet.Value.SetActive(true);
            return bullet.Value;
        }

        public Player CreatePlayer()
        {
            return new Player("Player", 100f, 250, new Vector2(200, 860), Vector2.One);
        }
        bool aux = false;
        public EnemyBasic CreateEnemyBasic(bool modeVegan)
        {
            var enemy = _enemies.GetOrCreate("EnemyBasic");

            if (enemy.Value == null)
            {
                // Generate animation
                if (GameManager.Instance.ModeVegan)
                {
                    enemy.Value = new EnemyBasic("Enemy", Animation.CreateAnimationRightAndLeft("Texture/Enemies/GauchoEnemie2/Right/ChaduchoAnimIdleRight2_", "Texture/Enemies/GauchoEnemie2/Left/2ChaduchoAnimIdleLeft_",
                16, true, 0.05f), new Texture("Texture/Enemies/molly.png"), 160, 3f);
                    aux = true;
                }
                else
                {
                    if (aux)
                    {
                        enemy.Value = new EnemyBasic("Enemy", Animation.CreateAnimationRightAndLeft("Texture/Enemies/VeganMan/Right/VeganAnimIdleRight_", "Texture/Enemies/VeganMan/Left/VeganAnimIdleLeft_",
                            32, true, 0.05f), new Texture("Texture/Enemies/molly.png"), 160, 2f);
                        aux = false;
                    }
                    else
                    {
                        enemy.Value = new EnemyBasic("Enemy", Animation.CreateAnimationRightAndLeft("Texture/Enemies/VeganWoman/Right/VeganFemAnimIdleRight_", "Texture/Enemies/VeganWoman/Left/VeganFemAnimIdleLeft_",
                            32, true, 0.05f), new Texture("Texture/Enemies/molly.png"), 160, 2f);
                        aux = true;
                    }
                }

                enemy.Value.OnDeactivate += () =>
                {
                    enemy.Value.SetActive(false);
                    _enemies.InUseToAvailable(enemy);
                };
            }
            enemy.Value.SetActive(true);
            return enemy.Value;
        }

        public Boss CreateEnemyBoss()
        {
            if (GameManager.Instance.ModeVegan)
            {
                var animation = Animation.CreateAnimation("Idle", "Texture/Enemies/GauchoBoss/Idle/OvniAnimIdle_", 32, true, 0.05f);
                var bulletTexture = new Texture("Texture/Enemies/GauchoBoss/Bullet.png");
                return new Boss("Boss", 500, 350, 1.5f, animation, bulletTexture, new Vector2(600, 150));
            }
            else
            {
                var rightAnimation = Animation.CreateAnimation("Right", "Texture/Enemies/CowBoss/Right/CowAnimRight_", 11, true, 0.5f);
                var leftAnimation = Animation.CreateAnimation("Left", "Texture/Enemies/CowBoss/Left/CowAnimLeft_", 11, true, 0.5f);
                var bulletTexture = new Texture("Texture/Enemies/Cow/Bullet.png");
                return new Boss("Boss", 500, 350, 1.5f, rightAnimation, leftAnimation, bulletTexture, new Vector2(600, 150));
            }
        }
    }
}