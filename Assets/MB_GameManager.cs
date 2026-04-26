using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MB_GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] FossilRocks;
    [SerializeField] MB_FossilBar fossilBar;
    [SerializeField] MB_Timer Timer;
    
    [SerializeField] Button[] Tools;
    private GameObject currentRock;
    private int i;
    private bool GameActive;


    private InputAction nextFossilRock;
    private InputAction removeFossilRock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = 0;
        GameActive = false;
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



    public void TakeAwayRock(InputAction.CallbackContext context)
    {
        if(currentRock)
        {
            GameActive = false;
            Destroy(currentRock);
            MB_PlayerInput.input.currentRock = null;
            fossilBar.ResetBar();
            fossilBar.rockData = null;
            currentRock = null;
            Timer.StopAllCoroutines();
            MB_PlayerInput.input.SwitchTool();

            foreach(Button tool in Tools)
                tool.interactable = false;
            
            
        }
        
    }

    public void SpawnNewRock(InputAction.CallbackContext context)
    {
        if (currentRock)
        {
            GameActive = false;
            Destroy(currentRock);
            MB_PlayerInput.input.currentRock = null;
            fossilBar.ResetBar();
            fossilBar.rockData = null;
            currentRock = null;
            Timer.StopAllCoroutines();
            MB_PlayerInput.input.SwitchTool();

            foreach (Button tool in Tools)
                tool.interactable = false;

        }

        GameActive = true;
        foreach (Button tool in Tools)
            tool.interactable = true;

        currentRock = Instantiate(FossilRocks[i]);
        MB_PlayerInput.input.currentRock = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.rockData = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.RearmBar();
        Timer.StartTimer();

        i++;
        i %= FossilRocks.Length;

    }
    //TODO state where game is over but rock is not away
}
