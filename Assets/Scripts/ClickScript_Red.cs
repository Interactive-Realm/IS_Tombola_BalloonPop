using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickScript_Red : MonoBehaviour
{
    private Button Button;
    private TextMeshProUGUI text;
    private List<string> textMessages;
    private string message1;
    private string message2;
    private string message3;
    private string message4;

    // Start is called before the first frame update
    void Start()
    {

        // Object definitions
        textMessages = new List<string>();
        Button = this.gameObject.GetComponent<Button>();
        Button.onClick.AddListener(Clicked);
        text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "";

        // Set text color
        // Convert the hex color code to a Color object
        Color desiredColor = HexToColor("#ff0310");

        // Assign the color to the TextMeshPro text component
        text.color = desiredColor;

        message1 = "1 dollar for you sir.";
        message2 = "no dollar for you sir.";
        message3 = "You won free shoes sir. Good for you!";
        message4 = "Splendid! First prize sir!";


        textMessages.Add(message1);
        textMessages.Add(message2);
        textMessages.Add(message3);
        textMessages.Add(message4);

        Debug.Log(textMessages[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Clicked()
    {
        // Unrender Button
        Button.image.enabled = false;
        string nowText;
        // Random message based on textMessages list.
        nowText = textMessages[Random.Range(0, 4)];
        text.text = nowText;

    }


    // HexToColor function converts a hexadecimal color code into a Unity Color object.
    // The ColorUtility.TryParseHtmlString method is used to perform this conversion.
    // If the conversion is successful, the color is set as the text color for the TextMeshPro component.
    Color HexToColor(string hex)
    {
        Color color = Color.black; // Default color if parsing fails

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }

        return color;
    }
}


