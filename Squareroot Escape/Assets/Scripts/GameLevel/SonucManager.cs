using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SonucManager : MonoBehaviour
{
    [SerializeField] private GameObject sonucPanelImage;
    [SerializeField] private Text dogruText, yanlisText, puanText;
    [SerializeField] private GameObject [] yokEdilecekler;
    private void OnEnable()
    {
        sonucPanelImage.transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.OutBack);
        sonucPanelImage.GetComponent<CanvasGroup>().DOFade(1f, .5f);

        EkraniTemizle();
    }

    public void SonuclariYazdir(int dogruAdet, int yanlisAdet, int toplamPuan)
    {
        dogruText.text = dogruAdet.ToString() + " DOĞRU";
        yanlisText.text = yanlisAdet.ToString() + " YANLIŞ";
        puanText.text = toplamPuan.ToString() + " PUAN";
    }

    void EkraniTemizle()
    {
        foreach (var yoklar in yokEdilecekler)
        {
            yoklar.SetActive(false);
        }
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
