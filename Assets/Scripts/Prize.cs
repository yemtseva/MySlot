using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prize : MonoBehaviour
{
    public int Balance;
    [SerializeField] TextMeshProUGUI text;
    Dictionary<string, int> prizes = new Dictionary<string, int>()
    {
        { "Blink", 25 },
        { "Sb", 50 },
        { "Cheese", 50 },
        { "Branch", 3 },
        { "Rapier", 1000  },
        { "Daedalus", 100 }
    };


    public void SetBalance()
    {
        Balance -= 1;
        UpdateBalance();
    }
    
    public void UpdateBalance()
    {
        text.text = "Balance: " + Balance;
    }

    IEnumerator WaitUntilSetBool()
    {
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<logic>().SetBool();
    } 

    public void checkWinnings()
    {
        string item1 = FindObjectOfType<spin1>().GetItem();
        string item2 = FindObjectOfType<spin2>().GetItem();
        string item3 = FindObjectOfType<spin3>().GetItem();
        if (item1.Equals(item2) && item1.Equals(item3))
        {
            Balance += prizes[item1];
            UpdateBalance();
            FindObjectOfType<layerControl>().ShowWin(prizes[item1]);
        }
        else
        {
            FindObjectOfType<spin3>().setSpinnings();
            FindObjectOfType<logic>().SetBool();
        }
     
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Balance: "+ Balance;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
