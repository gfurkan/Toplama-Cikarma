using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour
{
    [SerializeField]
    public GameObject girisButton;
    public GameObject cikisButton;

    AudioSource audiosource;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
        FadeOut();
    }

    void FadeOut()
    {
        girisButton.GetComponent<CanvasGroup>().DOFade(1, 0.6f);
        cikisButton.GetComponent<CanvasGroup>().DOFade(1, 0.6f).SetDelay(0.5f);

    }

    public void Cikis()
    {
        Application.Quit();
    }
    public void Basla()
    {
        SceneManager.LoadScene("level1");
    }
}
