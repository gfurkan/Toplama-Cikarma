using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CanKontrol : MonoBehaviour
{
    [SerializeField]
    private GameObject can1, can2, can3;

    public void KalanHaklarKontrol(int kalanHaklar)
    {
        switch (kalanHaklar)
        {
            case 3:
                can3.SetActive(true);
                can2.SetActive(true);
                can1.SetActive(true);
                break;
            case 2:
                can3.SetActive(true);
                can2.SetActive(true);
                can1.SetActive(false);
                break;
            case 1:
                can3.SetActive(true);
                can2.SetActive(false);
                can1.SetActive(false);  
                break;
            case 0:
                can3.SetActive(false);
                can2.SetActive(false);
                can1.SetActive(false);
                break;
        }
    }
}
