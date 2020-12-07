using EpPathFinding.cs;
using UnityEngine;

public static class GridPosHelper
{
    public static Vector3 GetPosition(this GridPos gridPos) => new Vector3(gridPos.x, 0, gridPos.y);
}

