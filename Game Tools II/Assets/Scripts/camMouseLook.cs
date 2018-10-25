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
        /*
        var md = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));
 
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
  
        //transform.rotation = Quaternion.AngleAxis(-mouseLook.y,  Vector3.right);
        //target.transform.rotation = Quaternion.AngleAxis(mouseLook.x, target.transform.up);


        */

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
