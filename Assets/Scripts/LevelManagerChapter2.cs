using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerChapter2 : MonoBehaviour {
    private EnemyGenerator _enemygenerator;
    // Use this for initialization
    void Start () {
        _enemygenerator = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemygenerator.PassChapter)
        {
            if (Input.GetKey(KeyCode.Return))
                SceneManager.LoadScene("demo_menu");
        }
    }
}
