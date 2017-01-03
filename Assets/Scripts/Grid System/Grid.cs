using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 CellSize;
    public Vector2 GridDimension;

    private static Grid _grid;
    public static Grid instance
    {
        get
        {
            if (!_grid)
            {
                _grid = FindObjectOfType(typeof(Grid)) as Grid;

                if (!_grid)
                {
                    Debug.LogError("There needs to be one active Grid script on a GameObject in your scene.");
                    return null;
                }
            }

            return _grid;
        }
    }

    //public static Vector3 Position
    //{
    //    get
    //    {
    //        return instance.transform.position;
    //    }
    //}

    public static Vector3 Snap(Vector3 position)
    {
        var cell = instance.CellSize;

        var x = Mathf.Pow(cell.x, Mathf.RoundToInt(Mathf.Log(position.x, cell.x)));
        var y = Mathf.Pow(cell.y, Mathf.RoundToInt(Mathf.Log(position.y, cell.y)));
        return new Vector3(x, y, position.z);
    }

    /// <summary>
    /// Limit the position in the Grid space (ignores transform)
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector2 LimitPosition(Vector2 position)
    {
        var limit = instance.GridDimension;
        var x = Mathf.Clamp(position.x, 0, limit.x);
        var y = Mathf.Clamp(position.y, 0, limit.y);
        return new Vector2(x, y);
    }

    public Vector2 GetCellSize()
    {
        return instance.CellSize;
    }
}
