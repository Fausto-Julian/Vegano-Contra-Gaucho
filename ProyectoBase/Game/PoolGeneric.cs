using System;
using System.Collections.Generic;

namespace Game
{

    public class BulletPool
    {
        private List<Bullet> availables = new List<Bullet>();
        private List<Bullet> inUse = new List<Bullet>();

        public Bullet GetBullet()
        {
            Bullet returnBullet;

            if (availables.Count > 0)
            {
                returnBullet = availables[0];
                availables.RemoveAt(0);
            }
            else
            {
                returnBullet = new Bullet();
                returnBullet.OnDesactivate += Desactivate;
            }

            inUse.Add(returnBullet);

            return returnBullet;
        }

        public void Desactivate(Bullet bullet)
        {
            inUse.Remove(bullet);
            if (availables.Count > 10)
            {
                bullet.Destroy();
            }
            else
            {
                bullet.IsActive = false;
                availables.Add(bullet);
            }
            
        }
    }



    // Todo: crear un pool que sea para cualquier tipo de clase.

    //public class PoolGeneric<T>
    //{
    //    private T typeData;
    //    private List<T> availables = new List<T>();
    //    private List<T> inUse = new List<T>();

    //    public PoolGeneric(T typeData, int count)
    //    {
    //        this.typeData = typeData;
    //    }

    //    public T GetPool(T pool)
    //    {
    //        if (availables.Count > 0)
    //        {
    //            pool = availables[0];
    //            availables.RemoveAt(0);
    //        }
    //        else
    //        {
                
    //        }

    //        inUse.Add(pool);
    //        return inUse[0];
    //    }
    //}
}
