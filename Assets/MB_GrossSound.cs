using UnityEngine;

public class MB_GrossSound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip grossClip;
    [SerializeField] Template_UIManager uIManager;


    public async void PlayGrossSound()
    {
        source.PlayOneShot(grossClip);

        await Awaitable.WaitForSecondsAsync(3);
        uIManager.Interact(uIManager.vide[2]);
    }
}
