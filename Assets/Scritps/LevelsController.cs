using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class LevelsController : MonoBehaviour
{
    private GameObject _cam;
    private ColorCorrectionCurves _colorCorrectionCurves;

    private bool _finish;
    public bool Finish
    {
        get { return Finish; }
        set
        {
            if (value)
            {
                _finish = true;
            }
        }
    }

    void Start()
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
        Application.LoadLevel(Application.loadedLevelName);
    }
}
