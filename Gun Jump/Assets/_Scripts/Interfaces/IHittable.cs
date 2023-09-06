namespace _Scripts.Interfaces
{
    public interface IHittable
    {
        public void OnHit();

        public void OnHit(Projectile.Projectile projectile);
    }
}
