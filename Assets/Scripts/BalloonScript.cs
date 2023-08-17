using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BalloonScript : MonoBehaviour
{
    private float _totalWeight;
    private float _win;
    private List<float> _items = new List<float>();
    private TextMeshProUGUI _topText;

    // The balloon button
    private Button Button;

    // Floating variables
    public float amplitude = 4;
    public float speed = 2.5f;




    // Start is called before the first frame update
    void Start()
    {
        _items.Add(74f);
        _items.Add(18.52f);
        _items.Add(5.56f);
        _items.Add(1.85f);
        _items.Add(0.07f);
        _totalWeight = 0;
        foreach (float item in _items)
        {
            _totalWeight += item;
        }
        


        // Object definitions
        Button = this.gameObject.GetComponent<Button>();
        Button.onClick.AddListener(roll);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        p.y = amplitude * Mathf.Cos(Time.time * speed);
        transform.position = p;
    }

    public void roll()
    {
        SetColor();
        GameManager.Instance.hasClicked = true;
        // Unrender Button
        Button.image.enabled = false;

        // 0 - 100
        float _diceRoll = Random.Range(0f, _totalWeight);

        foreach (float i in _items)
        {
            if (i >= _diceRoll)
            {
                _win = i;
                switch (_win)
                {
                    case 74:
                        _topText.text = "you did not win";
                        break;
                    case 18.52f:
                        _topText.text = "you won 4.price";
                        break;
                    case 5.56f:
                        _topText.text = "you won 3.price";
                        break;
                    case 1.85f:
                        _topText.text = "you won 2.price";
                        break;
                    case 0.07f:
                        _topText.text = "you won 1.price";
                        break;
                    default:
                        _topText.text = "you did not win";
                        break;
                }
                return;
            }
            _diceRoll -= i;
        }
    }

    



void SetColor()
    {
        _topText = GameObject.Find("TopText").GetComponent<TextMeshProUGUI>();
        if (this.gameObject.CompareTag("BlueBalloon"))
        {
            // Set text color
            // Convert the hex color code to a Color object
            Color desiredColorBlue = HexToColor("#004cd3");
            _topText.color = desiredColorBlue;
        }
        else if (this.gameObject.CompareTag("RedBalloon"))
        {
            Color desiredColorRed = HexToColor("#ff0310");
            _topText.color = desiredColorRed;
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


