using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MB_PlayerInput : MonoBehaviour
{
    [SerializeField] Camera SceneCamera;
    [SerializeField] Transform Cursor;

    private float power = 12f;
    private LayerMask defaultMask;
    private LayerMask rockMask;

    private InputAction useToolAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        useToolAction = InputSystem.actions.FindAction("Use");
        useToolAction.performed += UseTool;


        defaultMask = LayerMask.GetMask("Default");
        rockMask = LayerMask.GetMask("Rock");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.position = PositionFromMouse(SceneCamera);
    }


    public Vector3 PositionFromMouse(Camera sceneCamera)
    {
        Vector3 mousePos = Input.mousePosition;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            mousePos.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, defaultMask))
            {
                return hit.point;
            }
        }

        return new Vector3(999, 999, 999);
    }

    void UseTool(InputAction.CallbackContext context)
    {
        Collider[] hitRocks = Physics.OverlapSphere(Cursor.position, 1f, rockMask);

        foreach (Collider rock in hitRocks)
        {
            rock.GetComponent<MB_Rock>().TakeDamage(power);
        }




    }
}
