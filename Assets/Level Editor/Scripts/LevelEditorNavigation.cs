using UnityEngine;

public class LevelEditorNavigation : MonoBehaviour
{
    float Speed = 0.1f;
    float horizontal;
    float vertical;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * Speed;
        vertical = Input.GetAxis("Vertical") * Speed;
        transform.Translate(horizontal, vertical, 0);

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            var cell = hit.transform.GetComponent<LevelEditorCell>();
            if (cell != null)
            {
                cell.Selected = true;
                if (Input.GetMouseButtonDown(0))
                {
                    cell.SetShip();
                }
            }
        }
        else
        {
            LevelEditor.OnSelectedCellCheck.Invoke();
        }


        //Left mouse button
        

        //Middle mouse
        if (Input.GetMouseButton(2))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            var target = Camera.main.ScreenToWorldPoint(mousePosition);
            target.z = Camera.main.transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
        }
    }
}
