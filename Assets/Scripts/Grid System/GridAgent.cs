using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAgent : MonoBehaviour
{
    public Vector2 GridPosition;
    [HideInInspector]
    public Vector3 Direction;
    public float Speed;

    //bool canMoveNextPosition;

    public void Start()
    {
        transform.position = new Vector3(
            GridPosition.x * Grid.instance.CellSize.x,
            GridPosition.y * Grid.instance.CellSize.y,
            transform.position.z);

        //var mid = Mathf.RoundToInt(Grid.instance.GridDimension.x / 2);
        //GridPosition = new Vector2(mid, 0);
        //transform.position = new Vector3(
        //    GridPosition.x * Grid.instance.CellSize.x,
        //    transform.position.y,
        //    transform.position.z);
    }

    public void Update()
    {
        float step = Speed * Time.deltaTime;
        Vector3 target = new Vector3(
            GridPosition.x * Grid.instance.CellSize.x,
            GridPosition.y * Grid.instance.CellSize.y,
            transform.position.z);

        Direction = target - transform.position;
        Direction.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        //canMoveNextPosition = Vector3.Distance(transform.position, target) < Grid.instance.CellSize / 2;
    }
    
    public void Move(Vector2 cells)
    {
        GridPosition = Grid.LimitPosition(GridPosition + cells);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 20), "Grid Position: " + GridPosition.ToString());
        GUI.Label(new Rect(0, 20, 200, 20), "Direction: " + Direction.ToString());
    }
}
