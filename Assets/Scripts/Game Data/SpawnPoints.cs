using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPoints", menuName = "Game Data/Spawn Points", order = 1)]
public class SpawnPoints : ScriptableObject 
{
    public GameObject spawnObject;
    public VectorXZ[] spawnPoints;
}