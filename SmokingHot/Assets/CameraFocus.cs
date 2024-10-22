using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public float speed = 10.0f;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.MovePosition(transform.position + movement * Time.deltaTime * speed);
    }
}
