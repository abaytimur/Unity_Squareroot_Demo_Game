using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KokIciButtonManager : MonoBehaviour
{
    [SerializeField] private Image _kokIciImage;

    public int butonNo;

    private EgitimMenuManager _egitimMenuManager;


    void Awake()
    {
        _egitimMenuManager = Object.FindObjectOfType<EgitimMenuManager>();
    }

    public void ButonaTiklandi()
    {
        _egitimMenuManager.KokDisiResminiGoster(butonNo);
    }
}
