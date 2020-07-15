using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _hakkimdaPanel;

    private bool _isHakkimdaPanelOpen;
    
    public void StartGame()
    {
        _isHakkimdaPanelOpen = false;
        
        SceneManager.LoadScene("GameLevel");
    }

    public void OpenHakkimdaPanel()
    {
        if (!_isHakkimdaPanelOpen)
        {
            _hakkimdaPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        }
        else
        {
            _hakkimdaPanel.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        }

        _isHakkimdaPanelOpen = !_isHakkimdaPanelOpen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
