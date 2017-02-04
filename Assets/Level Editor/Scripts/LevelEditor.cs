﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    #region Grid Properties

    [HideInInspector]
    public Vector2 CellSize;
    [HideInInspector]
    public Vector2 GridDimension;

    #endregion

    Level TempLevel;
    public LevelEditorCell Cell;
    [HideInInspector]
    public static GameObject SelectedShip;

    #region UI Fields
    [Space]

    [Header("UI Fields")]
    public InputField CellHorizontal;
    public InputField CellVertical;
    public InputField GridLines;
    public InputField GridColumns;

    #endregion

    [Space]
    [Header("Enemies")]
    EnemyPlacement[] Enemies = new EnemyPlacement[100];

    #region Events

    public static UnityEvent OnGridChange = new UnityEvent();
    public static UnityEvent OnSelectedCellCheck = new UnityEvent();

    #endregion

    #region Unity Methods

    void Start()
    {
        CellSize = Vector2.one;
        GridDimension = Vector2.one;

        OnGridChange.AddListener(new UnityAction(CreateCubes));
        OnGridChange.AddListener(new UnityAction(UpdateTemp));

        InitializeTempFile();

        OnGridChange.Invoke();
        CellHorizontal.text = CellSize.x.ToString();
        CellVertical.text = CellSize.y.ToString();
        GridLines.text = GridDimension.x.ToString();
        GridColumns.text = GridDimension.y.ToString();
    }

    #endregion

    #region Cell and Grid

    public void SetCellSize_X(string input)
    {
        int x = 0;
        if (int.TryParse(input, out x))
        {
            CellSize.Set(x, CellSize.y);
            OnGridChange.Invoke();
        }
    }

    public void SetCellSize_Y(string input)
    {
        int y = 0;
        if (int.TryParse(input, out y))
        {
            CellSize.Set(CellSize.x, y);
            OnGridChange.Invoke();
        }
    }

    public void SetLines(string input)
    {
        int lines = 0;
        if (int.TryParse(input, out lines))
        {
            GridDimension.Set(lines, GridDimension.y);
            OnGridChange.Invoke();
        }
    }

    public void SetColumns(string input)
    {
        int columns = 0;
        if (int.TryParse(input, out columns))
        {
            GridDimension.Set(GridDimension.x, columns);
            OnGridChange.Invoke();
        }
    }

    public void CreateCubes()
    {
        for (int x = 0; x < GridDimension.x; x++)
        {
            for (int y = 0; y < GridDimension.y; y++)
            {
                Instantiate(Cell, new Vector3(x * CellSize.x, y * CellSize.y, 0), Cell.transform.rotation);
            }
        }
    }

    #endregion

    #region Ship Placing

    public void SelectShip(GameObject ship)
    {
        SelectedShip = ship;
    }

    #endregion

    #region Temp 

    void InitializeTempFile()
    {
        TempLevel = AssetDatabase.LoadAssetAtPath<Level>("Assets/Level Editor/~temp.asset");
        if (TempLevel == null)
        {
            TempLevel = ScriptableObject.CreateInstance<Level>();
            AssetDatabase.CreateAsset(TempLevel, "Assets/Level Editor/~temp.asset");
        }
        CellSize = TempLevel.CellSize;
        GridDimension = TempLevel.GridDimension;
    }

    void UpdateTemp()
    {
        TempLevel.CellSize = CellSize;
        TempLevel.GridDimension = GridDimension;
        AssetDatabase.SaveAssets();
    }

    #endregion
}

[Serializable]
public class InputEvent : UnityEvent<string> { }