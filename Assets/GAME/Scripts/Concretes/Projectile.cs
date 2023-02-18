using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float damage;

    Vector3 direction;

    Transform target;
    [SerializeField] string teamTag;

    [SerializeField] private ProjectileDataSO projectileDataSO;

    [SerializeField] Renderer _renderer;

    float timer = 0;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Awake()
    {
        speed = projectileDataSO.speed;
        damage = projectileDataSO.damage;
    }

    void Update()
    {
        Go(target);
    }

    public void Go(Transform target)
    {
        timer += Time.deltaTime;

        if (!target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);

            return;
        }

        this.target = target;

        direction = (target.position - transform.position + Vector3.up).normalized;

        transform.Translate(direction * speed * Time.deltaTime);


    }

    public void SetProjectile(string targetTag, Color color)
    {
        this.teamTag = targetTag;
        _renderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable) && !collision.gameObject.CompareTag(teamTag))
        {
            damagable.TakeDamage(damage);

            gameObject.SetActive(false);
        }
    }
}
 