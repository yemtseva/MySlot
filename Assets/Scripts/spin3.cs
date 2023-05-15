using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spin3 : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 itemPosition;
    bool isSpinings = false;
    string Item;
    bool isTriggered = false;
    public int rowNum;
    bool ExtendSpin = false;
    float endpoint = -17.5f;
    float endTime;
    float speed;
    Color defaultColor;
    public SpriteRenderer sr;
    Dictionary<float, string> items = new Dictionary<float, string>()
    {
        { -15.19f, "Blink" },
        { -12.8f, "Sb" },
        { -10.2f, "Cheese" },
        { -7.42f, "Sb" },
        { -4.49f, "Branch" },
        { -1.82f, "Cheese" },
        { 1f, "Rapier" },
        { 3.68f, "Branch" },
        { 6.5f, "Blink" },
        { 9.19f, "Branch" },
        { 11.75f, "Branch" },
        { 14.53f, "Daedalus" }
    };
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = sr.color;
        itemPosition = transform.position;

        initialPosition = transform.position;
        initialPosition.y = 16.8f;
        endTime = 1.82f;
    }

    void Spining()
    {
        if(ExtendSpin)
        {
            sr.color = new Color(234, 234, 234, 255);
            ExtendSpin = false;
            Invoke("Spining", 5.71f);
        }
        else if (isTriggered)
        {
            sr.color = defaultColor;
            isTriggered = false;
            if (!items.ContainsKey(transform.position.y))
            {
                float min = 99999f;
                foreach (var item in items)
                {
                    if (Math.Abs(transform.position.y - item.Key) < min)
                    {
                        Item = item.Value;
                        itemPosition.y = item.Key;
                    }
                    min = Math.Min(min, Math.Abs(transform.position.y - item.Key));
                }
                transform.position = itemPosition;
            }
            FindObjectOfType<Prize>().checkWinnings();
        }

        else
        {
            isTriggered = true;

        }
    }
    
    public void SetExtendSpin()
    {
        ExtendSpin = true;
    }
    static float NextFloat(float min, float max)
    {
        System.Random rand = new System.Random();
        double val = (rand.NextDouble() * (max - min) + min);
        return (float)val;
    }

    public string GetItem()
    {
        return Item;
    }

    public bool isSpining()
    {
        return isSpinings;
    }

    public void setSpinnings()
    {
        StartCoroutine(WaitUntilSetFalse());
    }

    IEnumerator WaitUntilSetFalse()
    {
        yield return new WaitForSeconds(.1f);
        isSpinings = false;
    }

    public void WaitUntilSetBool()
    {
        isSpinings = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTriggered && !isSpinings)
        {
            Invoke("WaitUntilSetBool", 0.1f);
            speed = NextFloat(-80f, -60f);
            Invoke("Spining", 0.3f);
            Invoke("Spining", endTime);


        }
        if(isTriggered)
        {

            if (transform.position.y <= endpoint)
            {
                transform.position = initialPosition;
            }
            Vector3 movement = new Vector3(0, speed, 0);

            transform.Translate(movement * Time.deltaTime);
        }
    
    }
}
