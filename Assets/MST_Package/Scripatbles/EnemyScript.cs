using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyScript : ScriptableObject
{
    public string enemyName;
    public int level;
    public int health;
    public int damage;
}
