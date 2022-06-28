using System;
using Game.Components;
using Game.Interface;
using Game.Objects;
using Game.Objects.Character;

namespace Game
{
    public class Factory : IFactory
    {
        private static Factory _instance;
        
        public static Factory Instance => _instance ?? (_instance = new Factory());

        private PoolGeneric<Bullet> _bulletsPool = new PoolGeneric<Bullet>();
        private PoolGeneric<EnemyBasic> _enemies = new PoolGeneric<EnemyBasic>();

        public void ClearList()
        {
            _bulletsPool = new PoolGeneric<Bullet>();
            _enemies = new PoolGeneric<EnemyBasic>();
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

        public EnemyBasic CreateEnemyBasic(bool modeVegan)
        {
            var enemy = _enemies.GetOrCreate("EnemyBasic");

            if (enemy.Value == null)
            {
                var number = new Random();
                var randomActivate = number.Next(0, 100);
                Animation leftAnimation;
                Animation rightAnimation;
                
                Texture textureBullet;

                if (GameManager.Instance.ModeVegan)
                {
                    rightAnimation = Animation.CreateAnimation("Right", "Texture/Enemies/Pizza/Right/PizzaAnimRight_", 11, true, 0.2f);
                    leftAnimation = Animation.CreateAnimation("Left", "Texture/Enemies/Pizza/Left/PizzaAnimLeft_", 11, true, 0.2f);
                    textureBullet = new Texture("Texture/Enemies/Pizza/Bullet.png");
                }
                else
                {
                    rightAnimation = randomActivate <= 50 ? Animation.CreateAnimation("Right", "Texture/Enemies/VeganMan/Right/VeganManAnimRigth_", 16, true, 0.2f) 
                        : Animation.CreateAnimation("Right", "Texture/Enemies/VeganWoman/Right/VeganWomanAnimRigth_", 15, true, 0.2f);
                    leftAnimation = randomActivate <= 50 ? Animation.CreateAnimation("Left", "Texture/Enemies/VeganMan/Left/VeganManAnimLeft_", 15, true, 0.2f) 
                        : Animation.CreateAnimation("Left", "Texture/Enemies/VeganWoman/Left/VeganWomanAnimLeft_", 16, true, 0.2f);
                    textureBullet = new Texture("Texture/Enemies/molly.png");
                }

                enemy.Value = new EnemyBasic("Enemy", rightAnimation, leftAnimation, textureBullet, 100, 2f);

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
                var rightAnimation = Animation.CreateAnimation("Right", "Texture/Enemies/GauchoBoss/Right/GauchoBossAnimRight_", 9, true, 0.5f);
                var leftAnimation = Animation.CreateAnimation("Left", "Texture/Enemies/GauchoBoss/Left/GauchoBossAnimLeft_", 9, true, 0.5f);
                var bulletTexture = new Texture("Texture/Enemies/GauchoBoss/Bullet.png");
                return new Boss("Boss", 500, 350, 1.5f, rightAnimation, leftAnimation, bulletTexture, new Vector2(600, 150));
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