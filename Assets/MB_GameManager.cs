using UnityEngine;

public class MB_GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] FossilRocks;
    [SerializeField] MB_FossilBar fossilBar;
    private GameObject currentRock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNewRock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAwayRock()
    {

    }

    public void SpawnNewRock()
    {
        currentRock = Instantiate(FossilRocks[0]);
        MB_PlayerInput.input.currentRock = currentRock.GetComponent<MB_XRay>().data;
        fossilBar.rockData = currentRock.GetComponent<MB_XRay>().data;

    }
}
