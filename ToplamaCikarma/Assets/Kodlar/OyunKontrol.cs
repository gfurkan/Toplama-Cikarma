using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OyunKontrol : MonoBehaviour
{
    [SerializeField]
    private GameObject kareprefab;
    [SerializeField]
    private Transform karepaneli;
    [SerializeField]
    private Transform sorupaneli;
    [SerializeField]
    private Text sorutext;
    [SerializeField]
    private Sprite[] oyuncakSprite;
    [SerializeField]
    private GameObject sonucpaneli;

    AudioSource []audioSource;


    GameObject gecerliKare;
    bool butonKullanimi = false;

    string soruZorlugu;

    int soruTipiSecici;
    int kacinciSoru;
    int kalanHak;
    int sonuc;
    int butonDegeri;
    int birinciSayi, ikinciSayi;
    List<int> bolumDegerleri = new List<int>();
    private GameObject[] karedizisi = new GameObject[25];
  
    CanKontrol canKontrol;
    SkorKontrol skorKontrol;

    private void Awake()
    {
        
        audioSource=GetComponents<AudioSource>();
       
        sonucpaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kalanHak = 3;
        canKontrol = Object.FindObjectOfType<CanKontrol>();
        skorKontrol = Object.FindObjectOfType<SkorKontrol>();
        canKontrol.KalanHaklarKontrol(kalanHak);
    }
    void Start()
    {
        audioSource[1].Play();
        KareOlustur();
        sorupaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
   
    void KareOlustur()
    {
        for (int i = 0; i < 25; i++) {

            GameObject kare= Instantiate(kareprefab, karepaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = oyuncakSprite[Random.Range(0, oyuncakSprite.Length)];
            kare.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karedizisi[i] = kare;
        }
        KareDegerleri();
        StartCoroutine(DoFade());
        Invoke("SoruPaneliniAc", 2.5f);    
    }
    void ButonaBasildi()
    {
        if (butonKullanimi)
        {
            audioSource[0].Play();
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            SonucKontrol();
        }
        }
    IEnumerator DoFade()
    {
        foreach (var kare in karedizisi) {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
          
            yield return new WaitForSeconds(0.1f);
        }
       
    }
    void KareDegerleri()
    {
        foreach(var kare in karedizisi)
        {
            int karedeger= Random.Range(13, 50);
            bolumDegerleri.Add(karedeger);
            kare.transform.GetChild(0).GetComponent<Text>().text = karedeger.ToString();
            
        }
    }

    void SoruPaneliniAc()
    {
        SoruOlustur();
        sorupaneli.GetComponent<RectTransform>().DOScale(0.75f, 0.3f).SetEase(Ease.OutBack);
        butonKullanimi = true;
    }
    void SoruOlustur()
    {
        soruTipiSecici = Random.Range(0, 2);
        if (soruTipiSecici == 0)
        {
            ikinciSayi = Random.Range(2, 8);
        kacinciSoru =Random.Range(0, bolumDegerleri.Count);
        sonuc = bolumDegerleri[kacinciSoru];
        birinciSayi =sonuc-ikinciSayi;
        sorutext.text = birinciSayi.ToString() + " + " + ikinciSayi.ToString();
        if (birinciSayi <= 40)
        {
            soruZorlugu = "kolay";
        }
        if (birinciSayi <= 60 && birinciSayi >= 40)
        {
            soruZorlugu = "orta";
        }
        if (birinciSayi <= 80 && birinciSayi >= 60)
        {
            soruZorlugu = "zor";
        }
        }
        if (soruTipiSecici == 1)
        {
            ikinciSayi = Random.Range(2, 20);
            kacinciSoru = Random.Range(0, bolumDegerleri.Count);
            sonuc = bolumDegerleri[kacinciSoru];
            birinciSayi = ikinciSayi + sonuc;
            sorutext.text = birinciSayi.ToString() + " - " + ikinciSayi.ToString();
            if (birinciSayi <= 20)
            {
                soruZorlugu = "kolay";
            }
            if (birinciSayi <= 40 && birinciSayi >= 20)
            {
                soruZorlugu = "orta";
            }
            if (birinciSayi <= 60 && birinciSayi >= 40)
            {
                soruZorlugu = "zor";
            }
        }
    }
    void SonucKontrol()
    {
        if (butonDegeri == sonuc)
        {
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<Text>().enabled = false;
            gecerliKare.transform.GetComponent<Button>().interactable = false;
            skorKontrol.SkorArtir(soruZorlugu);
            bolumDegerleri.RemoveAt(kacinciSoru);
            if (bolumDegerleri.Count > 0)
            {
                SoruPaneliniAc();
            }
            else
            {
                skorKontrol.OyunBitisSkor();
                OyunBitti();
            }
        }
        else
        {
            kalanHak--;
            canKontrol.KalanHaklarKontrol(kalanHak);
        }
        if (kalanHak <= 0)
        {
            skorKontrol.OyunBitisSkor();
            OyunBitti();
        }
    }
  
    void OyunBitti()
    {
        sonucpaneli.GetComponent<RectTransform>().DOScaleX(1, 0.5f).SetEase(Ease.OutBack);
        sonucpaneli.GetComponent<RectTransform>().DOScaleY(2, 0.5f).SetEase(Ease.OutBack);
    }
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("anamenu");
    }
    public void YenidenBasla()
    {
        SceneManager.LoadScene("level1");
    }
}
