using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{

    #region Grid Properties

    Vector2 CellSize;
    Vector2 GridDimension;

    #endregion

    Level CurrentLevel;
    Level TempLevel;

    public Text status;

    public LevelEditorCell Cell;

    [Space]

    [Header("UI Fields")]
    public InputField CellHorizontal;
    public InputField CellVertical;
    public InputField GridLines;
    public InputField GridColumns;

    public static UnityEvent OnGridChange = new UnityEvent();
    public static UnityEvent OnSelectedCellCheck = new UnityEvent();

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

    void Update()
    {
        status.text = string.Format("Cell size: ({0}, {1})", CellSize.x, CellSize.y);
        OnSelectedCellCheck.Invoke();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.GetComponent<LevelEditorCell>().Selected = true;
        }

        
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