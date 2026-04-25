using System.Threading.Tasks;
using UnityEngine;

public class MB_XRay : MonoBehaviour
{
    public Data_Rock data;
    public MeshRenderer[] XRayMats;

    public GameObject[] baseObj;

    private void Awake()
    {
        foreach(MeshRenderer mat in XRayMats)
        {
            mat.material.renderQueue = 3100;
        }

        for (int i = 0; i < baseObj.Length; i++)
        {
            baseObj[i].GetComponent<MeshRenderer>().material.renderQueue = 3000;
        }

        MB_PlayerInput.input.XRayMats = XRayMats;

    }




    void Update()
    {
        
    }

}
