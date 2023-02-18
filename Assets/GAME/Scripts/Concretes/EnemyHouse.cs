using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHouse : HouseBase
{
    float timer = 0;

    [SerializeField] float updateTime = 5;

    public override void Start()
    {
        base.Start();
    }

    public override IEnumerator IESpawnCharacter()
    {
        yield return new WaitForSeconds(.5f);

        while (true)
        {
            Enemy enemy = EnemyPool.OnGettingFromPool?.Invoke();
            enemy.transform.position = spawnPoint.position;

            enemy.GetComponent<TargetDetector>().InitTargetDetector(targetHouse);

            yield return new WaitForSeconds(spawnInterval);
        }
    }


    IEnumerator IEUpgradeSpawn()
    {
        while (spawnLevel < 10)
        {
            timer += Time.deltaTime;

            if (timer > updateTime)
            {
                updateTime *= 1.2f;
                updateTime += 10;

                base.UpgradeSpawn();

                timer = 0;
            }

            yield return null;
        }
    }

    public override void InitHouse()
    {
        IESpawnCharacterCor = StartCoroutine(IESpawnCharacter());

        StartCoroutine(IEUpgradeSpawn());
    }
}
