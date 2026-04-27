using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MB_PlayerInput : MonoBehaviour
{
    [SerializeField] Camera SceneCamera;
    [SerializeField] Transform mousePos;
    public MeshRenderer[] XRayMats;
    [System.NonSerialized] public Data_Rock currentRock;
    public static MB_PlayerInput input;
    [SerializeField] MB_GameManager Manager;
    [SerializeField] Image XrayTrickSlot;
    [SerializeField] Image XrayFlash;
    //controlling tool on click audio from this script
    [SerializeField] AudioClip HamSound;
    [SerializeField] AudioClip DrilSound;
    [SerializeField] AudioClip RaySound;
    [SerializeField] AudioSource AudioTool;
    //changing the cursor when tool is picked up
    [SerializeField] Texture2D[] cursor;
    //changing the cursor when mousing over the button

    public CursorMode cursorMode = CursorMode.Auto;

    private LayerMask defaultMask;
    private LayerMask rockMask;
    private Tool currentTool = Tool.none;

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
        mousePos.position = PositionFromMouse(SceneCamera);
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

    async void UseTool(InputAction.CallbackContext context)
    {
        switch(currentTool)
        {
            case Tool.hammer:
                UseHammer();
                AudioTool.PlayOneShot(HamSound);
                Cursor.SetCursor(cursor[1], new Vector2(0, 0), cursorMode);
                await Awaitable.WaitForSecondsAsync(.5f);
                Cursor.SetCursor(cursor[0], new Vector2(0, 0), cursorMode);
                break;

            case Tool.drill:
                AudioTool.PlayOneShot(DrilSound);
                UseDrill();
                Cursor.SetCursor(cursor[3], new Vector2(0, 0), cursorMode);
                await Awaitable.WaitForSecondsAsync(.5f);
                Cursor.SetCursor(cursor[2], new Vector2(0, 0), cursorMode);
                break;

            case Tool.none:
                break;
        }




    }

    void UseHammer(float power = 40f)
    {
        Collider[] hitRocks = Physics.OverlapSphere(mousePos.position, .5f, rockMask);

        if (hitRocks.Length <= 0)
        { return; }

        int layerStruck = hitRocks[0].GetComponent<MB_Rock>().layer;

        foreach (Collider rock in hitRocks)
        {
            if (!Manager.GameActive)
                break;
            float powerAfterFalloff = power - Vector3.Distance(mousePos.position, rock.transform.position);
            if (powerAfterFalloff <= 0)
            {
                powerAfterFalloff = 1f;
                Debug.Log("Power was less than or 0");
            }
                
            rock.GetComponent<MB_Rock>().TakeDamage(powerAfterFalloff, layerStruck);
        }
    }

    async void UseDrill(float power = 1f)
    {

        while(useToolAction.IsPressed())
        {

            Collider[] hitRocks = Physics.OverlapSphere(mousePos.position, .05f, rockMask);

            if (hitRocks.Length > 0)
            {
                int layerStruck = hitRocks[0].GetComponent<MB_Rock>().layer;

                foreach (Collider rock in hitRocks)
                {
                    if (!Manager.GameActive)
                        break;

                    rock.GetComponent<MB_Rock>().TakeDamage(power, layerStruck);

                }

                
                
            }
            await Task.Yield();

        }

        

    }

    public void TriggerXRay()
    {
        TriggerXRayImage(XrayFlash, .0125f);
        AudioTool.PlayOneShot(RaySound);

        if (currentRock.XRayTrick)
        {
            XrayTrickSlot.sprite = currentRock.XRayTrickImage;
            TriggerXRayImage(XrayTrickSlot);
            currentRock.XRayTrick = false;
            return;

        }


        foreach (MeshRenderer mat in XRayMats)
        {
            TriggerXRayInternal(mat.material);
        }
    }

    public async void TriggerXRayImage(Image slot, float fadePower = .1f)
    {
        float current = 0;
        float limit = 1;

        while (current < limit)
        {
            current += 0.1f;
            slot.color = new Color(1, 1, 1, current);
            await Task.Yield();
        }


        while (current > 0)
        {
            current -= fadePower;
            slot.color = new Color(1, 1, 1, current);
            await Task.Yield();
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

    public void SwitchTool(Data_Tool tool = null)
    {
        if (tool == null)
            currentTool = Tool.none;
        else
            currentTool = tool.tool;

        switch(currentTool)
        {
            case Tool.hammer:
                 Cursor.SetCursor(cursor[0], new Vector2(0, 0), cursorMode);
                break;

            case Tool.drill:
                 Cursor.SetCursor(cursor[2], new Vector2(0, 0), cursorMode);
                break;

            case Tool.none:
                break;
        }
       
        
    }
}
