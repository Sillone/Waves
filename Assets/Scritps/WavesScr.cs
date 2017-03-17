using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WavesScr : MonoBehaviour {
    public GameObject[] Waves;

    public int WavesAtTime;

    private int _current;
    private List<GameObject> _cunrrentWaves;
    public List<int> BlackList; 
    private Transform _trans;

    public float Speed;

    private float _curentSize;
    public float Size;
    private void Start()
    {
        BlackList = new List<int>();
        _curentSize = 0;
        _trans = gameObject.transform;
        _current = 0;
        _cunrrentWaves = new List<GameObject>();
        CreateNewWave(_current);
    }

    public void KillMe(int i)
    {
        Destroy(_cunrrentWaves[i]);
        print("destroed");
        BlackList.Add(i);
    }
    private void CreateNewWave(int cur)
    {
        for (var i = 0; i < WavesAtTime; i++)
        {
          //  if (!BlackList.Exists(e => e == i))
            {
                var go = (GameObject) Instantiate(Waves[cur], _trans.position, Quaternion.identity, _trans);
                go.transform.GetChild(0).GetComponent<WaveElement>().i = i;
                go.transform.RotateAround(go.transform.position, new Vector3(0, 1, 0), 90*i);
                _cunrrentWaves.Add(go);
            }
        }
    }

    private void DestrotyWaves()
    {
        foreach (var wave in _cunrrentWaves)
        {
            Destroy(wave);
       //     _cunrrentWaves.Remove(Wave);
        }
        _cunrrentWaves = new List<GameObject>();
    }

     

    private void Update()
    {
        var now = Speed * Time.deltaTime;   
        _curentSize += now;
        if (_curentSize < Size)
            _cunrrentWaves.ForEach(
                (cur) =>
                {
                    var t = cur.transform;
                    t.localScale = t.localScale + t.localScale*now;

                });
        else
        {
            _current++;
            DestrotyWaves();
            if (_current >= Waves.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                _curentSize = 0;
                CreateNewWave(_current);
            }
        }
    }
}
