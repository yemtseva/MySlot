using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spin1 : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 itemPosition;
    string Item;
    bool isTriggered = false;
    public int rowNum;
    float endpoint = -17.5f;
    float endTime;
    float speed;
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

        itemPosition = transform.position;

        initialPosition = transform.position;
        initialPosition.y = 16.8f;

        endTime = 1.4f;

    }

    void Spining()
    {
        if (isTriggered)
        {
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
        }
        else
        {
            isTriggered = true;
        }
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTriggered == false && !FindObjectOfType<spin3>().isSpining())
        {
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
