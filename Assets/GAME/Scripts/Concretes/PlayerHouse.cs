using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHouse : HouseBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnUpgradeSpawnSpeed += UpgradeSpawn;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnUpgradeSpawnSpeed -= UpgradeSpawn;
    }

    public override void Start()
    {
        base.Start();
    }

    public override IEnumerator IESpawnCharacter()
    {
        yield return new WaitForSeconds(.5f);

        Player player = PlayerPool.OnGettingFromPool?.Invoke();
        player.transform.position = spawnPoint.position;

        player.GetComponent<TargetDetector>().InitTargetDetector(targetHouse);

        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            player = PlayerPool.OnGettingFromPool?.Invoke();
            player.transform.position = spawnPoint.position;

            player.GetComponent<TargetDetector>().InitTargetDetector(targetHouse);
        }
    }

    public override void UpgradeSpawn()
    {
        if (spawnLevel<10)
        {
            base.UpgradeSpawn();
        }
    }

    public override void InitHouse()
    {
        IESpawnCharacterCor = StartCoroutine(IESpawnCharacter());
    }
}