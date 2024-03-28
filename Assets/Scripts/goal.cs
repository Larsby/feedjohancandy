using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class goal : MonoBehaviour
{

    public GameObject[] tossers;
    public Text highscore;
    int score = 0;
    public GameObject gameOverLay;
    public GameObject advertisementPanel;
    public VideoPlayer vp;
    float posischSize = 2.0f;
    float timeMofifier = 4.0f;
    public ParticleSystem psExplode;
    public ParticleSystem psEat;
    public HSController hs;

    public Slider HighScoreSlider;
    Animator m_Animator;

    float goalDelayTime = 0;

    int currentTween = 1;


    float biggerOrSmaller = 0.33f;

    Toss tempTosser;

    public AudioSource audiosource;
    public AudioSource ExplodingOutTheCandy;

    float Scale = 1.0f;


    public GameObject ad;

    public void InitAds()
    {
        //	print("InitADDDS");

#if !UNITY_EDITOR
       
        AppLovin.SetSdkKey("cQiOh1amPJ0ywVME6E3S8a-vk89aDO0wNwrXM989beVQSsaiqq9EoKn2HJX6-VO3DZgpudHWb2J7RwmNMfTZoA");
	//	AppLovin.SetTestAdsEnabled("YES");
       
		AppLovin.InitializeSdk();
	//	AppLovin.SetTestAdsEnabled("YES");

           AppLovin.ShowAd(AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_TOP);
#endif

    }
    public void gameOver()
    {
        hs.setScoreAndUpload(score);
        hs.startGetScores();
        StartCoroutine(GoGoGameover(0.6f));
    }


    public void StartNewGame()
    {
        vp.Pause();
        iTween.Stop(gameObject);
        float width;


        width = Screen.width;
        width = width * -1;

        iTween.MoveTo(advertisementPanel, new Vector3(width, advertisementPanel.transform.position.y, 0), 0.5f);
        hs.setCurrentScore(score);
        hs.setScoreAndUpload(score);
        score = 0;
        StartCoroutine(CreateNewTosser(0.1f));
        //   StartCoroutine(ShootTosser(0.12f));
        highscore.text = "Score: " + score + " High:" + hs.GetHighScore();
        posischSize = 1.0f;
        timeMofifier = 1.0f;
        goalDelayTime = 0;
        NewGoal();
#if !UNITY_EDITOR
       
    

           AppLovin.ShowAd(AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_TOP);
#endif

    }

    void Start()
    {
        InitAds();
        hs.startGetScores();

        gameOverLay.SetActive(true);
        m_Animator = GetComponent<Animator>();
        m_Animator.SetInteger("Eating", 0);
        m_Animator.SetInteger("Exploding", 0);
        HighScoreSlider.value = hs.positionInTheWorld;



        hs.LoadScores();
        highscore.text = "Score: " + score + " High:" + hs.GetHighScore();




    }

    iTween.EaseType getTween()
    {
        switch (currentTween)
        {
            case 1:
                return iTween.EaseType.linear;
            case 2:
                return iTween.EaseType.easeInOutCubic;
            case 3:
                return iTween.EaseType.easeInExpo;
            case 4:
                return iTween.EaseType.easeInCirc;
            case 5:
                return iTween.EaseType.easeInOutExpo;
            case 6:
                return iTween.EaseType.easeOutExpo;
            case 7:
                return iTween.EaseType.easeInOutCirc;
            //  case 8:
            //      return iTween.EaseType.spring;
            case 9:
                return iTween.EaseType.easeOutCirc;
            //    case 10:
            //        return iTween.EaseType.easeInOutBounce;
            //    case 11:
            //        return iTween.EaseType.easeOutBounce;
            //    case 12:
            //        return iTween.EaseType.easeInBounce;
            case 13:
                return iTween.EaseType.easeInBack;
            case 14:
                return iTween.EaseType.easeOutBack;
            case 15:
                return iTween.EaseType.easeInOutBack;
            case 16:
                return iTween.EaseType.easeInOutSine;
            case 17:
                return iTween.EaseType.easeInOutQuint;
            case 18:
                return iTween.EaseType.easeOutQuint;
            case 19:
                return iTween.EaseType.easeInOutQuart;
            case 20:
                return iTween.EaseType.easeInQuart;
            case 21:
                return iTween.EaseType.easeOutQuart;
            case 22:
                return iTween.EaseType.easeInCubic;
            case 23:
                return iTween.EaseType.easeOutCubic;
            case 24:
                return iTween.EaseType.easeInQuad;
            case 25:
                return iTween.EaseType.easeOutQuad;
            default:
                return iTween.EaseType.linear;
        }

        return iTween.EaseType.linear;



    }

    private IEnumerator newAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        iTween.MoveTo(gameObject, iTween.Hash("x", posischSize, "time", timeMofifier, "loopType", "pingPong", "easeType", getTween(), "delay", goalDelayTime));
        if (score > 30)
        {
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 0, Random.Range(10.15f, -10.15f)), "easetype", getTween(), "time", 0.9f, "loopType", "pingPong"));
        }
        if (score > 90)
            iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0, 0, Random.Range(2.15f, -2.15f)), "easetype", getTween(), "time", 0.9f, "loopType", "pingPong"));

        m_Animator.SetInteger("Eating", 0);
        m_Animator.SetInteger("Exploding", 0);
    }

    private void NewGoal()
    {
        Scale = 1.0f;

        if (score == 0)
        {
            currentTween = 1;
        }
        else
        {
            currentTween = Random.Range(1, 75);
        }

        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(Scale, Scale, Scale), "easetype", getTween(), "time", 0.9f));


        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(-posischSize, Random.Range(2.15f, -1.0f), 0), "easetype", getTween(), "time", 0.9f));
        //  iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 0, 0), "easetype", getTween(), "time", 0.2f));


        //  if (score > 30)
        {
            //add rotation
            //    print("add rotation");

        }



        StartCoroutine(newAnimation(0.9f));

    }

    private void explodeGoal()
    {


    }


    public void explode()
    {
        iTween.Stop(gameObject);

        posischSize += Random.Range(-0.2f, 0.2f);
        timeMofifier *= Random.Range(0.95f, 0.98f);
        goalDelayTime += Random.Range(-0.2f, 0.4f);

        psExplode.Play();

        ExplodingOutTheCandy.pitch = Random.Range(0.90f, 1.10f);
        ExplodingOutTheCandy.Play();

        m_Animator.SetInteger("Exploding", 1);


        StartCoroutine(resetGoal(0.3f));

        posischSize = Mathf.Clamp(posischSize, -2.44f, 2.44f);
        goalDelayTime = Mathf.Clamp(goalDelayTime, 0.2f, 1f);


        if (timeMofifier < 0.1f)
            timeMofifier = 4.0f;

        /*
            print("posischSize: " + posischSize);
            print("timeMofifier: " + timeMofifier);
            print("goalDelayTime: " + timeMofifier);
        */





    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        audiosource.pitch = Random.Range(0.90f, 1.10f);
        audiosource.Play();
        score++;
        hs.setCurrentScore(score);
        Scale -= Random.Range(0.05f, 0.07f);

        Rigidbody2D rd2 = collision.collider.gameObject.GetComponent<Rigidbody2D>();

        SpriteRenderer sr = collision.collider.gameObject.GetComponent<SpriteRenderer>();


        m_Animator.SetInteger("Eating", 1);


        rd2.simulated = false;

        float animationTime = 0.1f;

        iTween.ScaleTo(collision.collider.gameObject.gameObject, new Vector3(0, 0, 0), animationTime);
        iTween.FadeTo(collision.collider.gameObject.gameObject, 0, animationTime);

        StartCoroutine(DestroyTosser(0.2f, collision.collider.gameObject.gameObject));


        iTween.ScaleTo(gameObject, iTween.Hash("x", Scale, "y", Scale));

        StartCoroutine(CreateNewTosser(.1f));
        highscore.text = "Score: " + score;
        highscore.text = "Score: " + score + " High:" + hs.GetHighScore();

        psEat.Play();
        if (Scale > 2)
        {
            explode();
        }

        if (Scale < 0.5)
        {
            explode();
        }

    }



    private IEnumerator resetGoal(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        NewGoal();

    }

    public void Update()
    {

    }

    private IEnumerator DestroyTosser(float waitTime, GameObject go)
    {
        yield return new WaitForSeconds(waitTime);
        m_Animator.SetInteger("Eating", 0);

        Destroy(go);

    }
    private IEnumerator CreateNewTosser(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tempTosser = Instantiate(tossers[Random.Range(0, tossers.Length)]).GetComponent<Toss>();
        iTween.Resume(gameObject);
    }


    private IEnumerator ShootTosser(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tempTosser.TossCandy();
    }

    private IEnumerator GoGoGameover(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        HighScoreSlider.value = hs.positionInTheWorld;

        float width = Screen.width;
        iTween.MoveTo(gameOverLay, new Vector3(0, 0, 0), 0.5f);

        width = (width / 2);
        iTween.MoveTo(advertisementPanel, new Vector3(width, advertisementPanel.transform.position.y, 0), 0.5f);
        ad.SetActive(true);

        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, 2.15f, 0), "easetype", getTween(), "time", 0.1f));
        vp.Play();
    }
}
