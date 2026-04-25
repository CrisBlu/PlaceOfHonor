using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MB_PlayerInput : MonoBehaviour
{
    [SerializeField] Camera SceneCamera;
    [SerializeField] Transform Cursor;
    public MeshRenderer[] XRayMats;
    [System.NonSerialized] public Data_Rock currentRock;
    public static MB_PlayerInput input;




    
    private LayerMask defaultMask;
    private LayerMask rockMask;
    private Tool currentTool = Tool.hammer;

    private InputAction useToolAction;

    private void Awake()
    {
        input = this;
    }
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
        switch(currentTool)
        {
            case Tool.hammer:
                UseHammer();
                break;

            case Tool.drill:
                UseDrill();
                break;

            case Tool.none:
                break;
        }




    }

    void UseHammer(float power = 40f)
    {
        Collider[] hitRocks = Physics.OverlapSphere(Cursor.position, .5f, rockMask);

        if (hitRocks.Length <= 0)
        { return; }

        int layerStruck = hitRocks[0].GetComponent<MB_Rock>().layer;

        foreach (Collider rock in hitRocks)
        {
            //float powerAfterFalloff = 
            rock.GetComponent<MB_Rock>().TakeDamage(power, layerStruck);
        }
    }

    async void UseDrill(float power = .5f)
    {

        while(useToolAction.IsPressed())
        {

            Collider[] hitRocks = Physics.OverlapSphere(Cursor.position, .05f, rockMask);

            if (hitRocks.Length > 0)
            {
                int layerStruck = hitRocks[0].GetComponent<MB_Rock>().layer;

                foreach (Collider rock in hitRocks)
                {
                    //float powerAfterFalloff = 
                    rock.GetComponent<MB_Rock>().TakeDamage(power, layerStruck);
                }
            }
            await Task.Yield();

        }

        

    }

    public void TriggerXRay()
    {
        foreach (MeshRenderer mat in XRayMats)
        {
            TriggerXRayInternal(mat.material);
        }
    }


    public async void TriggerXRayInternal(Material mat)
    {
        float current = 0;
        float limit = 8;

        while (current < limit)
        {
            current += 0.1f;
            mat.SetColor("_EmissionColor", Color.white * current);
            await Task.Yield();
        }

        while (current > limit / 4)
        {
            current -= 0.05f;
            mat.SetColor("_EmissionColor", Color.white * current);
            await Task.Yield();
        }

        while (current > 0)
        {
            current -= 0.01f;
            mat.SetColor("_EmissionColor", Color.white * current);
            await Task.Yield();
        }
    }

    public void SwitchTool(Data_Tool tool)
    {
        currentTool = tool.tool;
    }
}
