﻿using System;
using System.Collections;
using System.Collections.Generic;
using ARUnit;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ARImageTrackProgress : MonoBehaviour
{
    public string[] ignoreTargets;

    public GameObject child;
    public Image progress;

    bool isActive = false;
    protected Vector3 position;
    protected Quaternion rotation;

    void Start()
    {
        ARToEstimate.onImageTrackUpdate += OnImageTrackUpdate;
        ARToEstimate.onImageTrackStop += OnImageTrackStop;
    }

    private void OnImageTrackStop()
    {
        child.SetActive(false);
        isActive = false;
    }

    private void OnImageTrackUpdate(ARImage ARImage, float t)
    {
        if (ignoreTargets.Contains(ARImage.name))
        {
            OnImageTrackStop();
            return;
        }

        position = ARImage.position;
        rotation = ARImage.rotation;
        if (!isActive)
        {
            isActive = true;
            child.SetActive(true);
            progress.fillAmount = 0;
        }
        progress.fillAmount = t;

    }

    private void Update()
    {
        transform.localPosition = position;
        transform.localRotation = rotation;
    }
}
