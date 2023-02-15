using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileDataSO")]

public class ProjectileDataSO : ScriptableObject
{
    public float speed;
 
    public float damage;
}
