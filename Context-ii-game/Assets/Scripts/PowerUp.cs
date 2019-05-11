using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int powerUpID;
    public int destroyTimer;

    ParticleSystem ps;
    ParticleSystem.MainModule main;

    // Start is called before the first frame update
    void Start()
    {
        powerUpID = Random.Range(1, 4);

        ps = GetComponent<ParticleSystem>();
        main = ps.main;

        //Fetch the Renderer from the GameObject
        Renderer rend = GetComponent<Renderer>();

        StartCoroutine(DestroyPowerup(rend));

        if (powerUpID == 1)
        {
            main.startColor = new Color(1, 0, 0, 1);
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        else if(powerUpID == 2)
        {
            main.startColor = new Color(1, 1, 0, 1);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
        }
        else if (powerUpID == 3)
        {
            main.startColor = new Color(0, 1, 1, 1);
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
        }
        
    }

    IEnumerator DestroyPowerup(Renderer rend)
    {
        yield return new WaitForSeconds(destroyTimer / 2);
        StartCoroutine(MeshFlikker(rend));
        yield return new WaitForSeconds(destroyTimer/2);
        Destroy(gameObject);
    }

    IEnumerator MeshFlikker(Renderer rend)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            // Find out whether current second is odd or even
            bool oddeven = Mathf.FloorToInt(Time.time) % 2 == 0;
            // Enable renderer accordingly
            rend.enabled = oddeven;
        }
    }

}
