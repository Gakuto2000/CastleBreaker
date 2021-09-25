using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyManager
{
    private Player player;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();

    public EnemyManager(Player p)
    {
        player = p;
    }

    public void AddEnemy(Enemy e)
    {
        enemies.Add(e);
    }

    public void DeleteEnemy(Enemy e)
    {
        enemies.Remove(e);
    }

    public Enemy getNearEnemy()
    {
        Enemy nearEnemy = null;
        float minDistance = 100f;
        foreach (var e in enemies)//エネミーをすべて格納して、一番近いやつを探す。
        {
            float distance = getDistance(player.transform.position, e.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                nearEnemy = e;
            }
        }
        return nearEnemy;
    }

    private float getDistance(Vector3 a, Vector3 b)
    {
        Vector3 dv = a - b;
        return Mathf.Pow((dv.x * dv.x + dv.y * dv.y + dv.z * dv.z), 0.5f);
    }
}
