using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILevelMessage : MonoBehaviour {
    private EnemyGenerator _enemygenerator;
    private Text _Text;
    // Use this for initialization
    void Start () {
        _enemygenerator = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        _Text = GameObject.Find("LevelMessage").GetComponent<Text>();
        _Text.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(_enemygenerator.PassChapter)
            _Text.enabled = true;
    }
}
