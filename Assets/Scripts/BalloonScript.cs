using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BalloonScript : MonoBehaviour/*,IUpdateSelectedHandler*/,IPointerDownHandler,IPointerUpHandler
{
    // Balloon game object
    public GameObject balloonObject;

    // Balloon button
    public Button balloonButton;
    public bool isHoldingButton = false;

    // Floating variables
    public float amplitude = 4;
    public float speed = 2.5f;

    // Growing variables
    public float growRate = 0.4f;
    public float shrinkRate = 0.4f;
    public float popSize = 12;
    public float normalSize = 2;

    // Prize text
    private TMP_Text prizeText;


    void Start()
    {

        // Prize text definition
        prizeText = UIManager.Instance._prizeText;

    }

    // Update is called once per frame
    void Update()
    {

            Vector3 pos = transform.position;
            pos.y = amplitude * Mathf.Cos(Time.time * speed);
            transform.position = pos;

        

        // Expand and shrink
        if (isHoldingButton == true && GameManager.Instance.poppedBalloon == false && GameManager.Instance.introFinished == true)
        {
            ExpandBalloon();
        }
        else
            ShrinkBalloon();

    }

    public void OnPointerDown(PointerEventData data)
    {
        isHoldingButton = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isHoldingButton = false;
    }
    private void ExpandBalloon()
    {
        // Expand balloon
        balloonObject.transform.localScale += balloonObject.transform.localScale * growRate * Time.deltaTime;

        // Balloon pop range
        if(balloonObject.transform.localScale.x > popSize)
        {
            BalloonPOP();
        }
    }
    private void ShrinkBalloon()
    {
        if(balloonObject.transform.localScale.x > normalSize && balloonObject.transform.localScale.y > normalSize)
        {
            // Shrink balloon if bigger than popRange
            balloonObject.transform.localScale -= balloonObject.transform.localScale * (shrinkRate) * Time.deltaTime;
        }
        
    }

    void BalloonPOP()
    {
        GameManager.Instance.poppedBalloon = true;
        Destroy(this.gameObject);
        GameManager.Instance.UpdateGameState(GameState.Prize);
    }

    public async void roll()
    {
        SetColor();
        //GameManager.Instance.hasClicked = true;
        // Unrender Button
        //balloonButton.image.enabled = false;

        // Get price
        PriceInfo price = await SupabaseClient.GetPriceInfo();

        // Update text
        prizeText.text = price.name + " - " + price.message;
        // bottom text = price.message
    }

    void SetColor()
    {
        if (gameObject.CompareTag("BlueBalloon"))
        {
            // Set text color
            // Convert the hex color code to a Color object
            Color desiredColorBlue = HexToColor("#004cd3");
            prizeText.color = desiredColorBlue;
        }
        else if (gameObject.CompareTag("RedBalloon"))
        {
            Color desiredColorRed = HexToColor("#ff0310");
            prizeText.color = desiredColorRed;
        }
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
