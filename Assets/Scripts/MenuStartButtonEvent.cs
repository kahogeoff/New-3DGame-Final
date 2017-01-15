using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuStartButtonEvent : MonoBehaviour {

    private Text _startText;
	// Use this for initialization
	void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger.triggers.Add(entry);
        _startText = GameObject.Find("StartText").GetComponent<Text>();
        

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        trigger.triggers.Add(entry2);

        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerClick;
        entry3.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        trigger.triggers.Add(entry3);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerEnter(PointerEventData data)
    {
        _startText.color = Color.green;
    }

    public void OnPointerExit(PointerEventData data)
    {
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString("#00D6C4FF", out myColor);
        _startText.color = myColor;
    }

    public void OnPointerClick(PointerEventData data)
    {
        SceneManager.LoadScene("demo");
    }
}
