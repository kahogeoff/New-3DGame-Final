using UnityEngine;
using System.Collections;

public class SensorScript : MonoBehaviour {
	public string TriggerFunctionName = "";

	void OnTriggerEnter(Collider c)
	{
		Debug.Log ("Enter the sensor");
		transform.parent.gameObject.SendMessage (TriggerFunctionName);
	}

	void OnTriggerExit(Collider c)
	{
		Debug.Log ("Exit the sensor");
		transform.parent.gameObject.SendMessage (TriggerFunctionName);
	}
}
