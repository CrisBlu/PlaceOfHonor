using UnityEngine;
using UnityEngine.InputSystem;

public class MB_GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] FossilRocks;
    [SerializeField] MB_FossilBar fossilBar;
    private GameObject currentRock;
    private int i;


    private InputAction nextFossilRock;
    private InputAction removeFossilRock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = 0;
        nextFossilRock = InputSystem.actions.FindAction("NextFossil");
        removeFossilRock = InputSystem.actions.FindAction("RemoveFossil");

        nextFossilRock.performed += SpawnNewRock;
        removeFossilRock.performed += TakeAwayRock;

       
    }

    private void OnDisable()
    {
        nextFossilRock.performed -= SpawnNewRock;
        removeFossilRock.performed -= TakeAwayRock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAwayRock(InputAction.CallbackContext context)
    {
        if(currentRock)
        {
            Destroy(currentRock);
            MB_PlayerInput.input.currentRock = null;
            fossilBar.ResetBar();
            fossilBar.rockData = null;
            currentRock = null;
            
        }
        
    }

    public void SpawnNewRock(InputAction.CallbackContext context)
    {
        if (currentRock)
        {
            Destroy(currentRock);
            MB_PlayerInput.input.currentRock = null;
            fossilBar.ResetBar();
            fossilBar.rockData = null;
            currentRock = null;
            
        }

        currentRock = Instantiate(FossilRocks[i]);
        MB_PlayerInput.input.currentRock = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.rockData = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.RearmBar();

        i++;
        i %= FossilRocks.Length;

    }
}
