using UnityEngine;
using UnityEngine.Events;

public class LevelEditorCell : MonoBehaviour
{
    public Material Normal;
    public Material Hightlighted;

    [HideInInspector]
    public bool Selected;

    UnityAction RemoveAction;
    MeshRenderer mRenderer;

    void Start()
    {
        mRenderer = GetComponent<MeshRenderer>();
        RemoveAction = new UnityAction(Remove);
        LevelEditor.OnGridChange.AddListener(RemoveAction);
        LevelEditor.OnSelectedCellCheck.AddListener(new UnityAction(() => { Selected = false; }));
    }

    void Update ()
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

    void Remove()
    {
        Destroy(gameObject);
        LevelEditor.OnGridChange.RemoveListener(RemoveAction);
    }
}
