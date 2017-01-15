using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveCounter : MonoBehaviour
{
    public Text CounterText;
    public Text TimeText;
    private EnemyGenerator _enemygenerator;

    // Use this for initialization
    void Start()
    {
        //_enemygenerator.WaveCounter
        _enemygenerator = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        CounterText.text = string.Format("Wave:{0}\nEnemy Num:{1}", _enemygenerator.WaveCounter, _enemygenerator.EnemyNumNow);

    }

    // Update is called once per frame
    void Update()
    {
        if (!_enemygenerator.PassChapter)
        {
            if (_enemygenerator.EnemyNumNow > 0)
            {
                TimeText.enabled = false;
                CounterText.enabled = true;
                CounterText.text = string.Format("Wave:{0}\nEnemy Num:{1}", _enemygenerator.WaveCounter, _enemygenerator.EnemyNumNow);

            }
            else
            {
                TimeText.enabled = true;
                CounterText.enabled = false;
                TimeText.text = string.Format("Next Wave Remain:{0}", (int)_enemygenerator.TimeCounter);

            }
        }
        else
        {
            TimeText.enabled = true;
            CounterText.enabled = false;
            TimeText.text = string.Format("Chapter Clear");
        }

    }
}