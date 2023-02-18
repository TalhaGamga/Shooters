using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamController : MonoBehaviour
{
    Vector3 initPos;

    private void OnEnable()
    {
        EventManager.OnGameEnd += ShakeCamera;
        EventManager.OnEnemyKilled += EnemyKillCameraShake;
    }

    private void OnDisable()
    {
        EventManager.OnGameEnd -= ShakeCamera;
        EventManager.OnEnemyKilled -= EnemyKillCameraShake;
    }

    private void Start()
    {
        initPos = transform.position;
    }

    void ShakeCamera()
    {
        transform.DOShakePosition(1f, 3, 10).OnStepComplete(()=>transform.position = initPos);
    }

    void EnemyKillCameraShake()
    {
        transform.DOShakePosition(0.3f, 1, 10).OnStepComplete(() => transform.position = initPos);
    }
}
