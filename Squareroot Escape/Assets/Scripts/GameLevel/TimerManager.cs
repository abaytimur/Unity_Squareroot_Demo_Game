using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject sonucPanel;
    [SerializeField] private AudioClip bitisSesi;
    
    private int _kalanSure;
    private bool _sureSaysinMi;

    private GameManager _gameManager;
    private SonucManager _sonucManager;

    private void Awake()
    {
        _gameManager = Object.FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _kalanSure = 30;
        _sureSaysinMi = true;
        StartCoroutine(SureTimerRoutine());
    }

    IEnumerator SureTimerRoutine()
    {
        while (_sureSaysinMi)
        {
            yield return new WaitForSeconds(1f);

            if (_kalanSure < 10)
            {
                timerText.text = "0" + _kalanSure.ToString();
                timerText.color = Color.red;
            }
            else
            {
                timerText.text = _kalanSure.ToString();
            }

            if (_kalanSure <= 0)
            {
                _sureSaysinMi = false;
                timerText.text = "";
                sonucPanel.SetActive(true);

                SesiCikar(bitisSesi);
                
                if (sonucPanel != null)
                {
                    _sonucManager = Object.FindObjectOfType<SonucManager>();
                    _sonucManager.SonuclariYazdir(_gameManager._dogruAdet, _gameManager._yanlisAdet,
                        _gameManager._toplamPuan);
                }
            }

            _kalanSure--;
        }
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