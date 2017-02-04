using UnityEngine;
using UnityEngine.Events;

public class LevelEditorCell : MonoBehaviour
{
    public Material Normal;
    public Material Hightlighted;
    public Material Transparent;

    [HideInInspector]
    public bool Selected;
    [HideInInspector]
    public GameObject Ship = null;

    UnityAction RemoveAction;
    MeshRenderer mRenderer;
    MeshFilter mfilter;
    LevelEditor editor;

    void Start()
    {
        editor = FindObjectOfType<LevelEditor>();
        mRenderer = GetComponent<MeshRenderer>();
        mfilter = GetComponent<MeshFilter>();
        RemoveAction = new UnityAction(Remove);
        LevelEditor.OnGridChange.AddListener(RemoveAction);
        LevelEditor.OnSelectedCellCheck.AddListener(new UnityAction(() => { Selected = false; }));
    }

    void Update()
    {
        if (Selected)
        {
            mRenderer.material = Hightlighted;
        }
        else
        {
            mRenderer.material = Normal;
        }
    }

    //Create a visual representation of the enemy ship on the position
    public void SetShip()
    {
        if (LevelEditor.SelectedShip != null && Ship == null)
        {
            Ship = LevelEditor.SelectedShip;
            Instantiate(Ship, transform, false);
            mRenderer.material = Transparent;
        }
    }

    // Destroy the cell
    void Remove()
    {
        Destroy(gameObject);
        LevelEditor.OnGridChange.RemoveListener(RemoveAction);
    }
}
