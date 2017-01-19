﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAgent : MonoBehaviour
{
    [HideInInspector]
    public Vector2 GridPosition;
    [HideInInspector]
    public Vector3 Direction;
    public float Speed;
    public bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    public void Start()
    {
        transform.localPosition = new Vector3(
            GridPosition.x * Grid.instance.CellSize.x,
            GridPosition.y * Grid.instance.CellSize.y,
            0);
    }

    public void Update()
    {
        if (!canMove)
            return;

        float step = Speed * Time.deltaTime;
        Vector3 target = new Vector3(
            GridPosition.x * Grid.instance.CellSize.x,
            GridPosition.y * Grid.instance.CellSize.y,
            0);

        Direction = target - transform.localPosition;
        Direction.Normalize();
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, step);

        //canMoveNextPosition = Vector3.Distance(transform.position, target) < Grid.instance.CellSize / 2;
    }

    public void Move(Vector2 cells)
    {
        GridPosition = Grid.LimitPosition(GridPosition + cells);
    }
}
