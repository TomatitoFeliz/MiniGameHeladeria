
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    int maquina;
    int jugador = 1;
    bool results = false;
    public float timer = 5f;
    private float timerActive;

    //Gameplay:
    [SerializeField]
    GameObject win, loose, luzB, luzR, luzY, butonB, butonR, butonY, canvasBasic;    
    [SerializeField]
    UnityEngine.UI.Image redMI;
    [SerializeField]
    UnityEngine.UI.Slider timerSlider;
    [SerializeField]
    Material normal, blue, red, yellow;

    //Animation:
    [SerializeField]
    GameObject cup01, cup02, copyCup02, camara;
    bool animationON = false;

    //Sonido:
    bool alreadyLoose = false;
    bool alreadyWin = false;
    [SerializeField]
    AudioSource reproductorSonido;
    [SerializeField]
    AudioClip victoriaClip, derrotaClip, bebidaClip;

    //Tutorial:
    bool inputLocker;
    [SerializeField]
    GameObject canvasTuto;


    void Start()
    {
        //Sonido:
        reproductorSonido.volume = PlayerPrefs.GetFloat("sonido");
        camara.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("sonido");

        //Tutorial:
        if (PlayerPrefs.GetInt("tutorial01") != 1)
        {
            canvasTuto.SetActive(true);
            inputLocker = true;
            canvasBasic.SetActive(false);
            Time.timeScale = 0f;
        }

        //Gameplay:
        maquina = Random.Range(0, 3);

        luzB.SetActive(false); luzR.SetActive(false); luzY.SetActive(false);
        win.SetActive(false);
        loose.SetActive(false);

        timerActive = timer;
        timerSlider.minValue = 0;
        timerSlider.maxValue = timerActive;
    }

    void Update()
    {
        //ExitLvl:
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

        //Tutorial:
        if (canvasTuto.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputLocker = false;
                canvasBasic.SetActive(true);
                canvasTuto.SetActive(false);
                Time.timeScale = 1f;
                PlayerPrefs.SetInt("tutorial01", 1);
            }
        }

        //Sistema de temporizador de cada ronda:
        timerSlider.value = timerActive;
        if (animationON == false)
        {
            timerActive -= Time.deltaTime;
        }

        if (timerActive < 0 && alreadyLoose != true)
        {
            Loose();
        }
        else if (timer <= 0.75f)
        {
            StartCoroutine("Win");
        }
        void Loose()
        {
            alreadyLoose = true;
            reproductorSonido.PlayOneShot(derrotaClip);
            inputLocker = true;
            canvasBasic.SetActive(false);
            loose.SetActive(true);
            results = true;
        }


        //Sistema de Gameplay:
        if (Input.GetKeyDown(KeyCode.Q) & results == false && inputLocker != true)
        {
            if (jugador != 2)
            {
                jugador++;
            }
            else if (jugador == 2)
            {
                jugador = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) & results == false && inputLocker != true)
        {
            if (jugador != 0)
            {
                jugador--;
            }
            else if (jugador == 0)
            {
                jugador = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) & results == false && inputLocker != true)
        {
            if (maquina != jugador)
            {
                Loose();
            }
            else if (maquina == jugador)
            {
                CorrectAnswer();
                animationON = true;
            }
        }

        //Estétic:
        if (maquina == 0)
        {
            redMI.color = Color.red;
        }
        else if (maquina == 1)
        {
            redMI.color = Color.yellow;
        }
        else if (maquina == 2)
        {
            redMI.color = Color.blue;
        }

        if (jugador == 0)
        {
            butonB.GetComponent<MeshRenderer>().material = normal; butonR.GetComponent<MeshRenderer>().material = red; butonY.GetComponent<MeshRenderer>().material = normal;
            luzB.SetActive(false); luzR.SetActive(true); luzY.SetActive(false);
        }
        else if (jugador == 1)
        {
            butonB.GetComponent<MeshRenderer>().material = normal; butonR.GetComponent<MeshRenderer>().material = normal; butonY.GetComponent<MeshRenderer>().material = yellow;
            luzB.SetActive(false); luzR.SetActive(false); luzY.SetActive(true);
        }
        else if (jugador == 2)
        {
            butonB.GetComponent<MeshRenderer>().material = blue; butonR.GetComponent<MeshRenderer>().material = normal; butonY.GetComponent<MeshRenderer>().material = normal;
            luzB.SetActive(true); luzR.SetActive(false); luzY.SetActive(false);
        }
    }

    //Correct:
    void CorrectAnswer()
    {
        reproductorSonido.PlayOneShot(bebidaClip);
        inputLocker = true;

        LeanTween.move(cup01, new Vector3(-2.936f, 0.026f, 8.97f), 1.5f).setOnComplete(() => 
        {
            Destroy(cup01);
        });
        Instantiate(copyCup02, new Vector3(1.192f, 0.026f, 8.97f), Quaternion.identity);
        cup02 = GameObject.Find("BasoGranizado(Clone)");
        cup02.name = "cup02";

        LeanTween.move(cup02, new Vector3(-0.81f, 0.026f, 8.97f), 1.5f).setOnComplete(() => 
        {
            cup01 = cup02;
            maquina = Random.Range(0, 3);
            timer = timer - 0.25f;
            timerActive = timer;
            timerSlider.maxValue = timerActive;
            animationON = false;
            inputLocker = false;
        });
    }
    IEnumerator Win()
    {
        if (alreadyWin != true)
        {
            WinSound();
        }
        timerActive = 40;
        canvasBasic.SetActive(false);
        win.SetActive(true);
        results = true;
        PlayerPrefs.SetInt("ganado01", 1);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Game02");
    }
    public void WinSound()
    {
        reproductorSonido.PlayOneShot(victoriaClip);
        alreadyWin = true;
    }

    //Loose:
    public void Repeat()
    {
        SceneManager.LoadScene("Game01");
    }
    public void Exit()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
