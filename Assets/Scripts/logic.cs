using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logic : MonoBehaviour
{
    public Animator anim;
    public AudioSource audio;
    public AudioSource audioS;
    bool isPressed = false;

    IEnumerator playSoundAfter()
    {
        yield return new WaitForSeconds(0.1f);
        audio.Play();
    }

    public void SetBool()
    {
        isPressed = false;
    }

    public void ExtendedSpin()
    {
        audio.Stop();
        audioS.Play();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPressed)
        {
            isPressed = true;
            anim.SetTrigger("pull");
            StartCoroutine(playSoundAfter());
            FindObjectOfType<Prize>().SetBalance();

        }
        
    }
}
