using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI title;
    public float scrollSpeed = 3f;

    private TextMeshProUGUI cloneText;
    private RectTransform textRectTransform;
    private List<string> message;

    // Use this for initialization
    void Awake()
    {
        // messages db
        // TODO get messages from dialogue db
        message = new List<string>
        {
            " Reconnaissance and Surveillance of Sector X23-F. ",
        };

        title = GameObject.Find("MissionTitle").GetComponent<TextMeshProUGUI>();
        title.text = "Mission 01";

        text.text = message[Random.Range(0, message.Count)];
        textRectTransform = text.GetComponent<RectTransform>();

        cloneText = Instantiate(text) as TextMeshProUGUI;
        RectTransform cloneRectTransform = cloneText.GetComponent<RectTransform>();
        cloneRectTransform.SetParent(textRectTransform);
        cloneRectTransform.localPosition = new Vector3(text.preferredWidth, 0, cloneRectTransform.position.z);
        cloneRectTransform.localScale = new Vector3(1, 1, 1);
        cloneText.text = text.text;

    }

    private IEnumerator Start()
    {
        float width = text.preferredWidth;
        Vector3 startPosition = textRectTransform.localPosition;

        float scrollPosition = 0;

        while (true)
        {
            textRectTransform.localPosition = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);
            scrollPosition += scrollSpeed * 20 * Time.deltaTime;
            yield return null;
        }
    }
}
