using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class EgitimMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton, _goBackButton;
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private GameObject _kokIciPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private Sprite[] _kokIciResimler;
    [SerializeField] private Sprite[] _kokDisiResimler;
    [SerializeField] private Image _kokDisiImage;
    [SerializeField] private Text _aciklamateText;
    [SerializeField] private AudioClip alistirmaClip;
    
    void Start()
    {
        _aciklamateText.text = "";
        _fadePanel.SetActive(true);
        _fadePanel.GetComponent<CanvasGroup>().alpha = 1;
        if (_startButton != null)
        {
            _startButton.GetComponent<RectTransform>().localScale = Vector3.zero;
            
        }
        if (_goBackButton != null)
        {
            _goBackButton.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
        _fadePanel.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(ButonlariAc);
        
        KokIciResimleriOlustur();
    }

    private void ButonlariAc()
    {
        _fadePanel.SetActive(false);
        _aciklamateText.text =
            "Alttaki panelden resimleri sürükleyerek istediğiniz resme tıklayıp kök değerini öğrenebilirsiniz.";
        
        // Ses klibini caldik
        SesiCikar(alistirmaClip);
        
        _startButton.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
        _goBackButton.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBounce);
    }

    private void KokIciResimleriOlustur()
    {
        for (int i = 0; i < _kokIciResimler.Length; i++)
        {
            GameObject kokIciItem = Instantiate(_kokIciPrefab, _content);

            kokIciItem.GetComponent<KokIciButtonManager>().butonNo = i;
            kokIciItem.transform.GetChild(3).GetComponent<Image>().sprite = _kokIciResimler[i];

        }

        _kokDisiImage.sprite = _kokDisiResimler[0];
    }

    public void KokDisiResminiGoster(int butonNo)
    {
        _kokDisiImage.sprite = _kokDisiResimler[butonNo];
    }

    public void MenuLevelineDon()
    {
        SceneManager.LoadScene("MenuLevel");
    }
    
    public void OyunLevelineGit()
    {
        SceneManager.LoadScene("GameLevel");
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
