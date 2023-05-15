using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class layerControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Renderer m_ObjectRenderer;
    public AudioSource audio;
    bool isShown = false;
    int Win = 0;
    bool CoroutineFinished = false;
    IEnumerator co;
    // Start is called before the first frame update
    void Start()
    {
        
       m_ObjectRenderer = GetComponent<Renderer>();
       m_ObjectRenderer.sortingLayerName = "Default";
    }

    public void ShowWin(int win)
    {
        Win = win;
        audio.Play();
        co = increasePrize(win);
        StartCoroutine(co);
    }
    IEnumerator WaitUntilSetBool()
    {
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<logic>().SetBool();
    }

    IEnumerator increasePrize(int win)
    {
        
        yield return new WaitForSeconds(0.2f);
        m_ObjectRenderer.sortingLayerName = "New Layer 4";
        isShown = true;
        for (int i = 1; i <= win; i++)
        {
            text.text = i.ToString() + "$";
            yield return new WaitForSeconds(0.08f);
        }
        CoroutineFinished = true;
        FindObjectOfType<spin3>().setSpinnings();
        StartCoroutine(WaitUntilSetBool());
    }
    IEnumerator WaitUntilSetSpinnings()
    {
        yield return new WaitForSeconds(.1f);
        FindObjectOfType<spin3>().setSpinnings();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CoroutineFinished)
        {
            isShown = false;
            CoroutineFinished = false;
            m_ObjectRenderer.sortingLayerName = "Default";
            text.text = "";
            
        }
        else if (Input.GetMouseButtonDown(0) && isShown)
        {
            StopCoroutine(co);
            CoroutineFinished = true;
            text.text = Win.ToString() + "$";
            isShown = false;
            FindObjectOfType<spin3>().setSpinnings();
            StartCoroutine(WaitUntilSetBool());
        }
    }
}
