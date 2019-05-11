using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicNarrativeLibraryScript : MonoBehaviour
{
    public AudioClip Polhasbeensend;
    public AudioClip hismission;
    public AudioClip forcenturies;
    public AudioClip themicehavebeen;
    public AudioClip whatpoldidntknow;
    public AudioClip thebossratOscary;
    public AudioClip weveheard;
    public AudioClip canyouhelp;
    public AudioClip nowitsupto;
    public AudioClip Goodluck;
    public AudioClip thisplanet;

    public AudioSource audioSource;


    void Oneshot_Polhasbeensend()
    {
        audioSource.PlayOneShot(Polhasbeensend, 1F);
    }
    void Oneshot_hismission()
    {
        audioSource.PlayOneShot(hismission, 1F);
    }
    void Oneshot_forcenturies()
    {
        audioSource.PlayOneShot(forcenturies, 1F);
    }
    void Oneshot_themicehavebeen()
    {
        audioSource.PlayOneShot(themicehavebeen, 1F);
    }
    void Oneshot_whatpoldidntknow()
    {
        audioSource.PlayOneShot(whatpoldidntknow, 1F);
    }
    void Oneshot_thebossratOscary()
    {
        audioSource.PlayOneShot(thebossratOscary, 1F);
    }
    void Oneshot_WeveHeard()
    {
        audioSource.PlayOneShot(weveheard, 1F);
    }
    void Oneshot_CanYouhelp()
    {
        audioSource.PlayOneShot(canyouhelp, 1F);
    }
    void Oneshot_Goodluck()
    {
        audioSource.PlayOneShot(Goodluck, 1F);
    }
    void Oneshot_thisplanet()
    {
        audioSource.PlayOneShot(thisplanet, 1F);
    }
    void Oneshot_nowitsupto()
    {
        audioSource.PlayOneShot(nowitsupto, 1F);
    }
}
