using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public float CalculateIncreasingValue(int _level, float _increaseFactor)
    {
        float _sum = 0;

        for (int i = 1; i <= _level; i++)
        {
            _sum += (i * _increaseFactor);
        }

        return _sum;
    }
}