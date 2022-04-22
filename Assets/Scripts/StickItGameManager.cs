using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickItGameManager : MonoBehaviour
{

    public static StickItGameManager instance;

    [SerializeField] private GameObject groundsWithStickyBall;
    [SerializeField] private GameObject firstTrampoline;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject[] lifeObjs;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject gameOverText;

    private int minScore = 14;

    private float _jumpTime = 0f;

    private Vector3 _groundPosition;
    public Vector3 GroundPosition
    {
        get { return _groundPosition; }
        set { _groundPosition = value; }
    }

    private GameObject _levelFirstTrampoline;

    public GameObject LevelFirstTrampoline
    {
        get { return _levelFirstTrampoline; }
        set { _levelFirstTrampoline = value; }
    }

    public float JumpTime
    {
        get { return _jumpTime; }
        set { _jumpTime = value; }
    }

    private GameObject _trampoline;

    public GameObject Trampoline
    {
        get { return _trampoline; }
        set { _trampoline = value; }
    }

    private float _stickTime = 0;

    public float StickTime
    {
        get { return _stickTime; }
        set { _stickTime = value; }
    }

    private int _score = 0;

    public int Score
    {
        get { return _score; }
        set { _score = value;}
    }

    private int _timeLeft = 30;

    public int TimeLeft
    {
        get { return _timeLeft; }
        set { _timeLeft = value; }
    }

    private int _level = 0;

    public int Level
    {
        get { return _level; }
        set {_level = value;}
    }

    private const int maxLives = 3;
    private int _lives = 3;

    public int Lives
    {
        get { return _lives; }
        set {
            _lives = value;
        }
    }

    private bool _fallOutsideTrampoline = false;

    public bool FallOutsideTrampoline
    {
        get { return _fallOutsideTrampoline; }
        set { _fallOutsideTrampoline = value; }
    }

    void Awake()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Trampoline = firstTrampoline;
        GroundPosition = new Vector3(groundsWithStickyBall.transform.position.x, groundsWithStickyBall.transform.position.y, groundsWithStickyBall.transform.position.z + 1f);
        LevelFirstTrampoline = firstTrampoline;
    }

    private void Start()
    {
        StartCoroutine("UpdateTimer");
    }


    private void Update()
    {
        GoToNextLevel();

        if (Lives == 0)
        {
            gameOverText.SetActive(true);
            groundsWithStickyBall.SetActive(false);
            slider.SetActive(false);
        }
    }

    public IEnumerator UpdateTimer()
    {
        int sliderTime = TimeLeft;
     
        while (sliderTime >= 0)
        {
            yield return new WaitForSeconds(1f);
            slider.GetComponent<Slider>().value = (float)sliderTime / TimeLeft;
            sliderTime -= 1;
        };
    }

    public Coroutine updateTimer;

    private void GoToNextLevel()
    {
        if (Score > minScore & Score % 5 == 0 & Score % 3 == 0)
        {
            minScore = Score;
            Level += 1;
            TimeLeft -= 2;
            restartTimer();

            GroundPosition = groundsWithStickyBall.transform.position;
            LevelFirstTrampoline = Trampoline;
            Debug.Log("Next Level");
        }
    }

    public void updateScoreText()
    {
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + _score.ToString();
    }

    public void deactivateLifeObj()
    {
        lifeObjs[_lives].SetActive(false);
    }

    public void restartTimer()
    {
        StopCoroutine("UpdateTimer");
        StartCoroutine("UpdateTimer");
    }
}
