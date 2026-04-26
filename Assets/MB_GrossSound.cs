using UnityEngine;

public class MB_GrossSound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip grossClip;


    public void PlayGrossSound()
    {
        source.PlayOneShot(grossClip);
    }
}
