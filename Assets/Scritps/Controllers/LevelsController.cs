using Assets.Scritps.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class LevelsController : MonoBehaviour , IController
{
    public GameObject[] Levels;
    private GameObject _cam;
    private ColorCorrectionCurves _colorCorrectionCurves;
    private int cur = 0;

    private bool _finish;
    public bool Finish
    {
        get { return Finish; }
        set
        {
            if (value)            
                _finish = true;            
        }
    }

    public void Start()
    {
        _cam = GameObject.FindObjectOfType<Camera>().gameObject;
        _colorCorrectionCurves = _cam.GetComponent<ColorCorrectionCurves>();
    }

    void Update()
    {
        if (_finish)
        {
            var step = 0.012f;
            if (_colorCorrectionCurves.saturation<=step)
                Restart();

            _colorCorrectionCurves.saturation -= step;
        }
    }

    

    private void Restart()
    {
        cur++;
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        Instantiate(Levels[cur]);

        
        var gM = gameObject.GetComponent<GetMatrix>();
        gM.LvlNumber+=2;
        gM.Start();
        gameObject.GetComponent<LevelCreator>().Start();
        gameObject.GetComponent<MatrixController>().Start();
        gameObject.GetComponent<HeroController>().Start();

    }
}
