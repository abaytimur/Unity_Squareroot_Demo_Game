using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] kokIciResimler;
    [SerializeField] private Sprite[] kokDisiResimler;
    [SerializeField] private Image morKokDisiResim, maviKokDisiResim, griKokDisiResim;
    [SerializeField] private Image sariKokDisiResim, kirmiziKokDisiResim, yesilKokDisiResim;
    [SerializeField] private Image ustKokIciResim, altKokIciResim;
    [SerializeField] private Transform sorularDairesi;
    [SerializeField] private Transform solBar, sagBar;
    [SerializeField] private GameObject dogruIcon, yanlisIcon, dogruYanlisObje;
    [SerializeField] private Text dogruAdetText, yanlisAdetText, puanText;
    [SerializeField] private GameObject bonusObje;
    [SerializeField] private AudioClip baslangicSesi;
    [SerializeField] private AudioClip daireSesi, barKapanisSesi;
    
    private string _butondakiResim;
    private int _hangiSoru;
    public int _dogruAdet, _yanlisAdet;
    private bool _daireUstteMi;
    private bool _daireDonsunMu;
    public int _toplamPuan, _puanArtisi;
    private int _bonusAdet;
    
    Vector3 _solBarBirinciYer = new Vector3(-175f, 61f, 0);
    Vector3 _sagBarBirinciYer = new Vector3(180f, 61f, 0);
    Vector3 _solBarIkinciYer = new Vector3(-105f, 61f, 0);
    Vector3 _sagBarIkinciYer = new Vector3(110f, 61f, 0);
    Vector3 _solBarUcuncuYer = new Vector3(-70f, 61f, 0);
    Vector3 _sagBarUcuncuYer = new Vector3(75f, 61f, 0);

    private int _kacinciYanlis;
    private bool _butonaBasilsinMi;
    
    void Start()
    {
        _butonaBasilsinMi = true;
        _bonusAdet = 0;
        _dogruAdet = 0;
        _yanlisAdet = 0;
        _kacinciYanlis = 0;
        _toplamPuan = 0;
        _puanArtisi = 0;
        _daireDonsunMu = true;
        _daireUstteMi = true;
        
        SesiCikar(baslangicSesi);
        
        ResimleriYerlestir();
    }

    void ResimleriYerlestir()
    {
        _hangiSoru = Random.Range(0, kokDisiResimler.Length - 3);
        int rastgeleDeger = Random.Range(0, 100);

        if (_daireUstteMi)
        {
            if (rastgeleDeger <= 33)
            {
                morKokDisiResim.sprite = kokDisiResimler[_hangiSoru];
                maviKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 1];
                griKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 2];
            }
            else if (rastgeleDeger <= 66)
            {
                morKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 1];
                maviKokDisiResim.sprite = kokDisiResimler[_hangiSoru];
                griKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 2];
            }
            else
            {
                morKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 2];
                maviKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 1];
                griKokDisiResim.sprite = kokDisiResimler[_hangiSoru];
            }
        }
        else
        {
            if (rastgeleDeger <= 33)
            {
                sariKokDisiResim.sprite = kokDisiResimler[_hangiSoru];
                kirmiziKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 1];
                yesilKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 2];
            }
            else if (rastgeleDeger <= 66)
            {
                sariKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 1];
                kirmiziKokDisiResim.sprite = kokDisiResimler[_hangiSoru];
                yesilKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 2];
            }
            else
            {
                sariKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 2];
                kirmiziKokDisiResim.sprite = kokDisiResimler[_hangiSoru + 1];
                yesilKokDisiResim.sprite = kokDisiResimler[_hangiSoru];
            }
        }

        if (_daireUstteMi)
        {
            ustKokIciResim.sprite = kokIciResimler[_hangiSoru];
        }
        else
        {
            altKokIciResim.sprite = kokIciResimler[_hangiSoru];
        }

        _daireUstteMi = !_daireUstteMi;
    }

    public void ButonaBasildi()
    {
        // Event sistemi kullanilarak tiklanilan objenin resminin ismini butondakiResim degiskenine atadik
        _butondakiResim = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0)
            .GetComponent<Image>().sprite.name;

        if (_butonaBasilsinMi)
        {
            _butonaBasilsinMi = false;
            SonucuKontrolEt();
        }
        
    }

    void SonucuKontrolEt()
    {
        if (_butondakiResim == kokDisiResimler[_hangiSoru].name)
        {
            _dogruAdet++;
            _bonusAdet++;
            dogruAdetText.text = _dogruAdet.ToString();
            DogruYanlisIconGoster(true);
            DaireyiCevir();

            if (_bonusAdet>=5 && _bonusAdet<=9)
            {
                _puanArtisi += 30;
                BonusScaleOn();
            }
            else
            {
                _puanArtisi += 20;
            }

            if (_bonusAdet>9)
            {
                BonusScaleOff();
                _bonusAdet = 0;
            }

        }
        else
        {
            BonusScaleOff();
            _yanlisAdet++;
            _bonusAdet = 0;
            yanlisAdetText.text = _yanlisAdet.ToString();
            DogruYanlisIconGoster(false);
            _kacinciYanlis++;
            BarlariKapat(_kacinciYanlis);
            _puanArtisi -= 5;
        }

        _toplamPuan += _puanArtisi;
        if (_toplamPuan<=0)
        {
            _toplamPuan = 0;
        }
        _puanArtisi = 0;
        puanText.text = _toplamPuan.ToString();
    }

    void DaireyiCevir()
    {
        _daireDonsunMu = false;
        _kacinciYanlis = 0;

        solBar.DOLocalMove(_solBarBirinciYer, 0.2f);
        sagBar.DOLocalMove(_sagBarBirinciYer, 0.2f);

        SesiCikar(daireSesi);
        
        ResimleriYerlestir();

        sorularDairesi.DORotate(sorularDairesi.rotation.eulerAngles + new Vector3(0, 0, 180f), 0.5f)
            .OnComplete(DaireDonsunMuTrueYap);
    }

    void DaireDonsunMuTrueYap()
    {
        _butonaBasilsinMi = true;
        _daireDonsunMu = true;
    }

    void BarlariKapat(int kacinciYanlis)
    {
        SesiCikar(barKapanisSesi);
        if (kacinciYanlis == 1)
        {
            _butonaBasilsinMi = true;
            solBar.DOLocalMove(_solBarIkinciYer, 0.2f);
            sagBar.DOLocalMove(_sagBarIkinciYer, 0.2f);
        }
        else if (kacinciYanlis == 2)
        {
            solBar.DOLocalMove(_solBarUcuncuYer, 0.2f);
            sagBar.DOLocalMove(_sagBarUcuncuYer, 0.2f).OnComplete(BarKapanisiniBekle);
        }
    }

    void BarKapanisiniBekle()
    {
        _daireDonsunMu = true;
        Invoke(nameof(DaireyiCevir), 1f);
    }

    void DogruYanlisIconGoster(bool dogruMu)
    {
        dogruYanlisObje.GetComponent<CanvasGroup>().alpha = 0;
        if (dogruMu)
        {
            dogruIcon.SetActive(true);
            yanlisIcon.SetActive(false);
        }
        else
        {
            yanlisIcon.SetActive(true);
            dogruIcon.SetActive(false);
        }

        dogruYanlisObje.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(TrueFalseIconuAlfasiniKapat);
    }

    void TrueFalseIconuAlfasiniKapat()
    {
        dogruYanlisObje.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
    }

    void BonusScaleOn()
    {
        bonusObje.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutElastic);
    }

    void BonusScaleOff()
    {
        bonusObje.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InElastic);
    }

    public void GeriDon()
    {
        SceneManager.LoadScene("EgitimLevel");
    }
    
    // Farkli bir yontemle ses klibini caldik
    void SesiCikar(AudioClip clip)
    {
        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f );  
        }
    }
}