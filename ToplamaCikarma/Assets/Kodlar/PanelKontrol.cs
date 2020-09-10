using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelKontrol : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        Fade();
    }

    void Fade()
    {
        panel.GetComponent<CanvasGroup>().DOFade(0, 2f);
    }
}
