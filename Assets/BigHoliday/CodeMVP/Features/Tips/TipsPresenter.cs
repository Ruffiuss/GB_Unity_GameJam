using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TipsPresenter : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _tipShowTime;
    private Image _image;
    private Text _tipText;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _tipText = GetComponentInChildren<Text>();
        _image = GetComponent<Image>();
        _image.enabled = false;
        _tipText.enabled = false;
        _image.ObserveEveryValueChanged(i => i.enabled == true).Delay(TimeSpan.FromSeconds(2)).Subscribe(_ => { _image.enabled = false; _tipText.enabled = false; });
    }

    #endregion

    #region Methods

    public void SubscribeFlow(ReactiveProperty<string> flow)
    {
        flow.Where(t => t != null).Subscribe(_ => 
        {
            _tipText.text = flow.Value;
            _tipText.enabled = true;
            _image.enabled = true;
        });
    }

    #endregion
}
