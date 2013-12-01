﻿using UnityEngine;
using System.Collections;

public class RoadMaker : MonoBehaviour
{
    #region Public properties

    public GameObject panelPrefab;
    public int columnCount = 15;
    public float twistAmount = 90.0f;
    public float twistFreq = 0.3f;

    #endregion

    #region Private variables

    float scroll;

    #endregion

    #region Private method

    void CreateRow (float offset)
    {
        var origin = transform.position - transform.forward * offset;
        var dx = transform.right;

        for (var column = 0; column < columnCount; column++)
        {
            var position = origin + dx * (column - 0.5f * (columnCount - 1));
            Instantiate (panelPrefab, position, transform.rotation);
        }
    }

    #endregion

    #region Monobehaviour functions

    void Update ()
    {
        var twist = Perlin.Noise (Scroller.instance.position * twistFreq) * twistAmount;
        transform.localRotation = Quaternion.AngleAxis (twist, Vector3.forward);

        scroll += Scroller.instance.delta;
        while (scroll > 1.0f)
        {
            CreateRow (scroll);
            scroll -= 1.0f;
        }
    }

    #endregion
}
