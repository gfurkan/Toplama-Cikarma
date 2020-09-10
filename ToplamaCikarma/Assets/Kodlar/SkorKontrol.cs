using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkorKontrol : MonoBehaviour
{
    int toplamSkor;
    int skorArtisi;
    int enYuksekSkor;

    [SerializeField]
    private Text skorText;
    [SerializeField]
    private Text toplamSkorText;
    [SerializeField]
    private Text enYuksekSkorText;


    void Start()
    {
        skorText.text = toplamSkor.ToString();
        enYuksekSkor = PlayerPrefs.GetInt("enyuksekskor");
        toplamSkor = 0;
    }
 
    public void SkorArtir(string zorluk)
    {
        switch (zorluk)
        {
            case "kolay":
                skorArtisi = 5;
                break;
            case "orta":
                skorArtisi = 10;
                break;
            case "zor":
                skorArtisi = 15;
                break;
        }
        toplamSkor += skorArtisi;
        skorText.text = toplamSkor.ToString();
        if (enYuksekSkor < toplamSkor)
        {
            enYuksekSkor = toplamSkor;
            PlayerPrefs.SetInt("enyuksekskor", enYuksekSkor);
            
        }
       
    }
   public void OyunBitisSkor()
    {
        PlayerPrefs.SetInt("toplamskor", toplamSkor);
        toplamSkorText.text = "Skor " + PlayerPrefs.GetInt("toplamskor");
        enYuksekSkorText.text = "En Yuksek Skor " + PlayerPrefs.GetInt("enyuksekskor");
    }
}
