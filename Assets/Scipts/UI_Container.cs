using System.Collections.Generic;
using UnityEngine;


public class UI_Container : MonoBehaviour
{
    private Dictionary<string, string> ui_Values;
    private bool updated;
    // Start is called before the first frame update
    void Start()
    {
        ui_Values = new Dictionary<string, string>();
        updated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(updated)
        {
            updated = false;
            //Debug.Log("["+name+"] - "+ ToString());
        }

    }

    public void ReceiveItemFromObject(string[] value, string name, COMMAND c)
    {
        if (value.Length <= 2)
            { 
                updated = true;
                if (ui_Values.ContainsKey(name))
                {
                    ui_Values[name] = value[0];
                }
                else
                    ui_Values.Add(name, value[0]);

                switch (c)
                {
                    case COMMAND.BROADCAST:
                        Debug.Log("[" + name + "] - BROADCAST IN PROGRESS");
                        BroadcastToChildren(value[0], value[1]);
                        break;
                    default:
                        break;
                }
            }
        else
        {
            Debug.Log(">>> ERROR: [" + name + "] - string[] value has length > 2.");
        }
    }

    public override string ToString()
    {
        string temp = "";
        foreach(string S in ui_Values.Keys)
        {
            temp += "( " + S + ", " + ui_Values[S] + ")";
        }
        return temp;
    }

    private void BroadcastToChildren(string message, string receiver)
    {
        UI_Element[] elements = GetComponentsInChildren<UI_Element>();
        foreach (UI_Element _Element in elements)
        {
            _Element.ReceiveAndDisplay(message, receiver);
        }
    }

    string GetItemFromKey(string key)
    {
        return ui_Values[key];
    }
}
