using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesScr : MonoBehaviour {
    public GameObject[] Waves;

    private int _current;
    private GameObject _cunrrentWave;
    private Transform _trans;

    public float speed;

    private float curentSize;
    public float size;
    private void Start()
    {
        curentSize = 0;
        _trans = gameObject.transform;
        _current = 0;
        _cunrrentWave = (GameObject)Instantiate(Waves[_current], _trans.position, Quaternion.identity, _trans);
    }

    private void Update()
    {
        var now = speed * Time.deltaTime;   
        curentSize += now;
        if (curentSize < size)
            _cunrrentWave.transform.localScale = _cunrrentWave.transform.localScale + _cunrrentWave.transform.localScale * now;
        else
        {
            _current++;
            Destroy(_cunrrentWave);
            if (_current >=Waves.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                curentSize = 0;
                _cunrrentWave = (GameObject)Instantiate(Waves[_current], _trans.position, Quaternion.identity, _trans);
            }
        }
    }
}
