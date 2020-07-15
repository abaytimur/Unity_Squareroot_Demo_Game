using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CerceveManager : MonoBehaviour
{
    private Image _cerceveResmi;

    private int _randomDeger;

    private Color _color;

    // Start is called before the first frame update
    void Start()
    {
        _cerceveResmi = GetComponent<Image>();
        RengiDegistir();
    }

    private void RengiDegistir()
    {
        _randomDeger = Random.Range(0, 50);
        if (_randomDeger <= 10)
        {
            _color = Color.magenta;
        }
        else if (_randomDeger <= 20)
        {
            _color = Color.red;
        }
        else if (_randomDeger <= 30)
        {
            _color = Color.green;
        }
        else if (_randomDeger <= 40)
        {
            _color = Color.grey;
        }
        else if (_randomDeger <= 50)
        {
            _color = Color.blue;
        }

        if (_cerceveResmi != null)
        {
            _cerceveResmi.color = _color;
        }
    }
}