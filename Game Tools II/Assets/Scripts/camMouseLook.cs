using UnityEngine;
 
public class camMouseLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public Transform target;


    [SerializeField]
    [HideInInspector]
    private Vector3 initialOffset;

    private Vector3 currentOffset;

    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentOffset = initialOffset;
    }

    void LateUpdate()
    {

        transform.position = target.position + currentOffset;

        float movement = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        if (!Mathf.Approximately(movement, 0f))
        {
            transform.RotateAround(target.position, Vector3.up, movement);
            currentOffset = transform.position - target.position;
        }





        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }



    [ContextMenu("Set Current Offset")]
    private void SetCurrentOffset()
    {
        if (target == null)
        {
            return;
        }

        initialOffset = transform.position - target.position;
    }
}
