using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : Singleton<ProjectilePool>
{
    [SerializeField] private Projectile _projectilePrefab;

    private Queue<Projectile> _projectilePool = new();

    protected override void Awake()
    {
        base.Awake();
    }

    public Projectile GetPooledObject()
    {
        if (_projectilePool.Count == 0)
            AddProjectile(1);

        Projectile projectile = _projectilePool.Dequeue();
        projectile.gameObject.SetActive(true);

        return projectile;
    }

    public void ReturnToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _projectilePool.Enqueue(projectile);
    }

    private void AddProjectile(int count)
    {
        if (count <= 0)
            throw new ArgumentException("The count value cannot be less than or equal to zero.");

        for (int i = 0; i < count; i++)
        {
            Projectile projectile = Instantiate(_projectilePrefab);
            projectile.gameObject.SetActive(false);
            _projectilePool.Enqueue(projectile);
        }
    }
}
