using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class InitializeApp : MonoBehaviour
{
    private const string GAME = "Game";
    private const string URL = "https://www.google.ru/";
    private const string PRIVACY_POLICY = "https://www.google.ru/"; // добавить политику конфиденциальности, сейчас ссылка на google.com

    [SerializeField] private Button _privacyPolicyButton;
    [SerializeField] private Button _switchButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _backGroundSwitch;

    private bool _isAgreeWithPrivacyPolicy = false;

    private void Awake()
    {
        _privacyPolicyButton.onClick.AddListener(delegate
        {
            Application.OpenURL(PRIVACY_POLICY);
        });
        _switchButton.onClick.AddListener(delegate
        {
            if (_rectTransform.localPosition.x == -2)
            {
                _isAgreeWithPrivacyPolicy = true;
                _rectTransform.localPosition = new Vector3(40, 0, 0);
                _backGroundSwitch.color = new Color(0,220,90);
            }
            else
            {
                _isAgreeWithPrivacyPolicy = false;
                _rectTransform.localPosition = new Vector3(-2, 0, 0);
                _backGroundSwitch.color = new Color(226, 226, 226, 255);
            }
        });
        _startButton.onClick.AddListener(delegate
        {
            if (!_isAgreeWithPrivacyPolicy)
                return;

            if (Application.internetReachability == NetworkReachability.NotReachable)
                 SceneManager.LoadScene(GAME);
            else
                  Application.OpenURL(URL);
        });
    }
}
