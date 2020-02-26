using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * The goal is to define in a very generic way a UI Element (Input Field, Slider, Text, Button etc)
 * Implement a sender that will transmit values to a container that will communicate with the core
 * 
 */

public class UI_Element : MonoBehaviour
{

    private string[] ui_Value;
    private string prev_Value;

    public string destination;
    public COMMAND command;

    // Start is called before the first frame update
    void Start()
    {
        ui_Value = new string[2];
        destination = "";
        SetToDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (prev_Value != ui_Value[0])
        {
            if(command == COMMAND.CHECK_DATA)
            {
                Debug.Log("Checking Data");
                CheckDataType();
            }
            else
            {
                SendInputToParent();
            }
            
            prev_Value = ui_Value[0];
        }
        ui_Value[1] = destination;
    }

    public void OnValueChange(string value)
    {
        ui_Value[0] = value;
    }

    public void OnValueChange(float value)
    {
        ui_Value[0] = value.ToString();
    }

    public void OnValueChange(int value)
    {
        ui_Value[0] = value.ToString();
    }

    public void ReceiveAndDisplay<T>(T value, string receiver) where T : IConvertible
    {
        if (receiver == name)
        {
            if(value.GetType() == typeof(string))
            {
                GetComponent<Text>().text = value.ToString();
            }
            else if (value.GetType() == typeof(string[]))
            {
                string[] tmp = (string[])Convert.ChangeType(value, typeof(string[]));
                if(tmp.Length<1)
                {
                    return;
                }
                GetComponent<Dropdown>().options.RemoveRange(0, GetComponent<Dropdown>().options.Count);
                Dropdown.OptionData option;
                for (int i = 0; i<tmp.Length; i++)
                {
                    option = new Dropdown.OptionData(tmp[i]);
                    GetComponent<Dropdown>().options.Add(option);
                }
                
            }
        }
        else
        {
            return; //DO SOMETHING
        }
    }

    private void SendInputToParent()
    {
        Debug.Log("[" + name + "] - Sending message to parent: "+ui_Value[0] +"-"+ ui_Value[1] +"-"+"-"+name+"-"+command);
        GetComponentInParent<UI_Container>().ReceiveItemFromObject(ui_Value, name, command);
        SetToDefault();
    }

    private void SetToDefault()
    {
        ui_Value[0] = "";
        ui_Value[1] = "";
        prev_Value = "";
    }

    private DATA_TYPES CheckDataType()
    {
        Debug.Log("Start data check");
        foreach(char Chr in ui_Value[0])
        {
            if(Chr == '@')
            {
                string[] Email = ui_Value[0].Split('@');
                Debug.Log("Data is Email, Prefix: " + Email[0] + ", Suffix: " + Email[1]); //CHECK IF BOTH STRINGS ARE NOT EMPTY
                return DATA_TYPES.EMAIL;
            }
        }
        Debug.Log("Data is undefined");
        return DATA_TYPES.UNDEF;
    }
}
