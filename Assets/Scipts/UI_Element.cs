using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/**
 * The goal is to define in a very generic way a UI Element (Input Field, Slider, Text, Button etc)
 * Implement a sender that will transmit values to a container that will communicate with the core
 * 
 */

public class UI_Element : MonoBehaviour
{

    private string[] ui_Value;
    private string prev_Value;

    public string text;
    public COMMAND command;

    // Start is called before the first frame update
    void Start()
    {
        ui_Value = new string[2];
        command = COMMAND.IGNORE;
        text = "";
        SetToDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (prev_Value != ui_Value[0])
        {
            SendInputToParent();
            prev_Value = ui_Value[0];
        }
        ui_Value[1] = text;
    }

    public void OnValueChange_String(string value)
    {
        ui_Value[0] = value;
    }

    public void OnValueChange_Float(float value)
    {
        ui_Value[0] = value.ToString();
    }

    public void ReceiveAndDisplay(string value, string receiver)
    {
        if (receiver == name)
        {
            GetComponent<Text>().text = value;
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
}
