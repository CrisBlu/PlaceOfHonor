using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MB_GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] FossilRocks;
    [SerializeField] MB_FossilBar fossilBar;
    [SerializeField] Animator doorAnimator;
    [SerializeField] MB_Timer Timer;
    [SerializeField] Data_Scoring Scoring;
    [SerializeField] TextMeshProUGUI ScoreDisplay;
    [SerializeField] AudioClip DoorOpen;
    [SerializeField] AudioClip DoorClosed;
    [SerializeField] AudioSource AudioSourceOk;
    [SerializeField] Template_UIManager uiManager;
    
    [SerializeField] Button[] Tools;
    private GameObject currentRock;
    private int i;
    private int j;
    private int[] jValues = { 2, 3, 4 };
    [NonSerialized] public bool GameActive;


    private InputAction nextFossilRock;
    private InputAction removeFossilRock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = 0;
        j = 0;
        GameActive = false;
        nextFossilRock = InputSystem.actions.FindAction("NextFossil");
        removeFossilRock = InputSystem.actions.FindAction("RemoveFossil");

        nextFossilRock.performed += SpawnNewRockInput;
        removeFossilRock.performed += TakeAwayRock;

       
    }

    private void OnDisable()
    {
        nextFossilRock.performed -= SpawnNewRockInput;
        removeFossilRock.performed -= TakeAwayRock;
    }



    public void TakeAwayRock(InputAction.CallbackContext context)
    {
        StopGame();
        ClearRock();


    }

    public void SpawnNewRockInput(InputAction.CallbackContext context)
    {
        SpawnNewRock();

    }

    public async void SpawnNewRock()
    {



        ClearRock();

        await Awaitable.WaitForSecondsAsync(1);
        GameActive = true;
        foreach (Button tool in Tools)
            tool.interactable = true;

        doorAnimator.SetBool("GameActive", GameActive);
        AudioSourceOk.PlayOneShot(DoorOpen);
        currentRock = Instantiate(FossilRocks[i]);
        MB_PlayerInput.input.currentRock = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.rockData = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.RearmBar();
        Timer.StartTimer();

        i++;
        i %= FossilRocks.Length;

    }

    public void StopGame()
    {
        AudioSourceOk.PlayOneShot(DoorClosed);
        GameActive = false;

        doorAnimator.SetBool("GameActive", GameActive);
        Timer.StopAllCoroutines();
        foreach (Button tool in Tools)
            
            tool.interactable = false;

        MB_PlayerInput.input.SwitchTool();

        Scoring.scores.Add(fossilBar.score);

        ShowScore(fossilBar.score);
    }


    void ClearRock()
    {
        if (currentRock)
        {
            currentRock.GetComponent<MB_XRay>().data.totalRocksOverFossil = 0;
            Destroy(currentRock);
            MB_PlayerInput.input.currentRock = null;
            fossilBar.ResetBar();
            fossilBar.rockData = null;
            currentRock = null;
            


        }
    }
    

    async void ShowScore(float score)
    {
        await Awaitable.WaitForSecondsAsync(.5f);

        ScoreDisplay.text = ((int)(score * 100)).ToString() + '%';
        ScoreDisplay.enabled = true;
        

        await Awaitable.WaitForSecondsAsync(4);

        ScoreDisplay.enabled = false;

        await Awaitable.WaitForSecondsAsync(1);

        if(i < jValues[j])
            SpawnNewRock();
        else
        {
            j++;
            uiManager.Interact(uiManager.vide[1]);
        }
            
        
    }
}
