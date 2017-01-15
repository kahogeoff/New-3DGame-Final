using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuTogglePanel : MonoBehaviour {
    public GameObject _CraditPanel;
    public bool toggleCradit = false;
    // Use this for initialization
    void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerClick;
        entry3.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        trigger.triggers.Add(entry3);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerClick(PointerEventData data)
    {
        if (toggleCradit)
            toggleCradit = false;
        else
            toggleCradit = true;
        if (toggleCradit)
            _CraditPanel.SetActive(true);
        else
            _CraditPanel.SetActive(false);
    }
}
