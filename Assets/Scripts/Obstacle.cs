using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private RectInt _unWalkableCells;
    
    public IEnumerable<Vector2Int> GetUnWalkablePositions()
    {
        for (int x = 0; x < _unWalkableCells.width; x++)
        {
            for (int y = 0; y < _unWalkableCells.height; y++)
            {
                yield return new Vector2Int(_unWalkableCells.x + x, _unWalkableCells.y + y);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        var center = new Vector3(_unWalkableCells.center.x - 0.5f, 0.5f, _unWalkableCells.center.y - 0.5f);
        Gizmos.DrawWireCube(center, new Vector3(_unWalkableCells.size.x, 1, _unWalkableCells.size.y));
    }
}
