using UnityEngine;

public class LevelEditorNavigation : MonoBehaviour
{
    public float Speed = 0.7f;
    float horizontal;
    float vertical;

	void Start () {
		
	}
	
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal") * Speed;
        vertical = Input.GetAxis("Vertical") * Speed;
        transform.Translate(horizontal, vertical, 0);

        //Middle mouse
        if (Input.GetMouseButton(2))
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
        }
    }
}
