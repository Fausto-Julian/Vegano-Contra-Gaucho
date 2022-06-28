using System;
using Game;
using Game.Components;
using Game.Objects;
using Game.Objects.Character;
using Game.PhysicsEngine;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestFunctionRestLife()
        {
            var rightAnimation = Animation.CreateAnimation("Right", "Texture/Enemies/Pizza/Right/PizzaAnimRight_", 12, true, 0.2f);
            var leftAnimation = Animation.CreateAnimation("Left", "Texture/Enemies/Pizza/Left/PizzaAnimLeft_", 12, true, 0.2f);
            var textureBullet = new Texture("Texture/Enemies/Pizza/Bullet.png");
            
            var bullet = new Bullet("bulletTest", 1f, 20f, new Texture("Texture/Lettuce.png"));
            var enemy = new EnemyBasic("Enemy", rightAnimation, leftAnimation, textureBullet, 100, 2f);

            bullet.GetComponent<Body>().OnTrigger(enemy);

            Assert.AreEqual(80f, enemy.GetComponent<HealthController>().CurrentHealth);
        }
        
        [Test]
        public void TestCollisionAndEventTrigger()
        {
            // Inicializo al Enemigo
            var rightAnimation = Animation.CreateAnimation("Right", "Texture/Enemies/Pizza/Right/PizzaAnimRight_", 12, true, 0.2f);
            var leftAnimation = Animation.CreateAnimation("Left", "Texture/Enemies/Pizza/Left/PizzaAnimLeft_", 12, true, 0.2f);
            var textureBullet = new Texture("Texture/Enemies/Pizza/Bullet.png");
            
            var enemy = new EnemyBasic("Enemy", rightAnimation, leftAnimation, textureBullet, 100, 2f);

            enemy.Transform.Position = new Vector2(500f, 500f);
            
            // Inicializo al Player
            var player = new Player("Player", 100, 200, new Vector2(500f, 500f), Vector2.One);
            
            //Creo el mundo
            var world = new World();
            
            // Creo el body A, lo añado al mundo y lo subscribo al lamba
            var bodyA = Body.CreateBoxBody(enemy, enemy.Transform, enemy.RealSize, true, false, true);
            world.AddBody(bodyA);
            
            var bodyAOn = false;
            bodyA.OnTrigger += (GameObject gameObject) =>
            {
                if (gameObject.Id == player.Id)
                {
                    bodyAOn = true;
                }
            };
            
            // Creo el body B, lo añado al mundo y lo subscribo al lamba
            var bodyB = Body.CreateBoxBody(player, player.Transform, player.RealSize, true);
            world.AddBody(bodyB);

            var bodyBOn = false;
            bodyA.OnTrigger += (GameObject gameObject) =>
            {
                if (gameObject.Id == enemy.Id)
                {
                    bodyBOn = true;
                }
            };
            
            // Hago que el mundo chequee la collision
            world.Step(0f, 1);
            
            // Verifico si ambos eventos se activaron y collisionaron
            var finish = bodyAOn && bodyBOn;

            Assert.AreEqual(true, finish);
        }
    }
}