using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] TargetDetector targetDetector;

    [SerializeField] string teamTag;

    [SerializeField] Color projectileColor;

    private void OnEnable()
    {
        targetDetector.OnEnableFire += Fire;
    }

    private void OnDisable()
    {
        targetDetector.OnEnableFire -= Fire;
    }

    void Fire(Vector3 initPos,Transform target)
    {
        Projectile projectile = ProjectilePool.OnFiringProjectile?.Invoke();
        projectile.transform.position = initPos;
        projectile.SetProjectile(teamTag, projectileColor);

        projectile.Go(target);
    }
}
