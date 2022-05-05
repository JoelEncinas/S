using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    public UIManager uIManager;
    public TextMeshProUGUI text;
    public TextMeshProUGUI title;
    public float scrollSpeed = 3f;

    private TextMeshProUGUI cloneText;
    private RectTransform textRectTransform;

    // Use this for initialization
    void Awake()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        title = GameObject.Find("MissionTitle").GetComponent<TextMeshProUGUI>();

        // mission content
        if (uIManager.GetMissionInfo(SceneManager.GetActiveScene().name) != null)
        {
            title.text = uIManager.GetMissionInfo(SceneManager.GetActiveScene().name)["title"];
            text.text = uIManager.GetMissionInfo(SceneManager.GetActiveScene().name)["content"];
        }

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
