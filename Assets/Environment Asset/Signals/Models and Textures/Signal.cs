using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public Light spotlightG1;
    public Light spotlightG2;
    public Light spotlightR1;
    public Light spotlightR2;

    // Start is called before the first frame update
    void Start()
    {
        spotlightG1.intensity = 10f;
        spotlightG2.intensity = 10f;
        spotlightR1.intensity = 0f;
        spotlightR2.intensity = 0f;
        StartCoroutine(InitiateSignal());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator InitiateSignal()
    {
        while (spotlightG1.intensity == 10f && spotlightG2.intensity == 10f)
        {
            yield return new WaitForSeconds(5);
            spotlightG1.intensity = 0f;
            spotlightG2.intensity = 0f;
            spotlightR1.intensity = 10f;
            spotlightR2.intensity = 10f;
            yield return new WaitForSeconds(5);
            spotlightG1.intensity = 10f;
            spotlightG2.intensity = 10f;
            spotlightR1.intensity = 0f;
            spotlightR2.intensity = 0f;
        }
    }

}
