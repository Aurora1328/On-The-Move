using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  
    public Transform tr;

    private Vector3 movement;

    void Start()
    {
        movement = Vector3.forward;
    }

    void Update()
    {
        tr.Translate(movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tr.Rotate(0, -90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tr.Rotate(0, 90, 0);
        }
    }
}
