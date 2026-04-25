using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;

public class UIManagerRecord : MonoBehaviour
{
    public GameObject container_NPC;
    public GameObject container_PLAYER;
    public Text[] text_Choices;
    public Text text_NPC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        
        {

            if (!VD.isActive)
            {
                Begin();
            }
        }
        
    }
    void Begin()
    {
        Debug.Log("gygy");
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(text_NPC.GetComponent<VIDE_Assign>());
    }
 
    void UpdateUI(VD.NodeData data)
    {
        Debug.Log(data.comments[0]);
    }
 
    void End(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }
 
    void OnDisable()
    {
        if (container_NPC != null)
            End(null);
    }
}
