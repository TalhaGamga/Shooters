using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseData", menuName = "ScriptableObjects/HouseDataSO")]
public class HouseDataSO : ScriptableObject
{
    public float spawnInterval;

    public float health;

    public Color color;
}
 