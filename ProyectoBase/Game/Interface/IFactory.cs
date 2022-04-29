using Game.Component;
using Game.Objects;
using Game.Objects.Character;

namespace Game.Interface
{
    public interface IFactory
    {
        Bullet CreateBullet(PoolGeneric<Bullet> bulletsPool, string ownerId, float speed, float damage, Animation animation);
        
        Bullet CreateBullet(PoolGeneric<Bullet> bulletsPool, string ownerId, float speed, float damage, Texture texture);

        Player CreatePlayer();

        EnemyBasic CreateEnemyBasic();

        Boss CreateEnemyBoss();
    }
}