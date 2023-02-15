
using UnityEngine;

interface IDamagable
{
    void TakeDamage(float damage);

    void Die();
}

interface IDirectable
{
    void OnSelected();

    void OnDeselect();
    void MoveTo();
}

interface IMovable
{
    void Move();
}

