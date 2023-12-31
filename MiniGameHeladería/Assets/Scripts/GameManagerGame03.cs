using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerGame03 : MonoBehaviour
{
    int maquina;
    int jugador01 = 2;
    int jugador02 = 2;
    bool results = false;
    public float timer = 5f;
    private float timerActive;

    //Gameplay:
    [SerializeField]
    GameObject win, loose, luzB, luzR, luzY, luzBlack, butonB01, butonB02, butonR01, butonR02, butonY01, butonY02, buttonBlack, canvasBasic;
    [SerializeField]
    Image redMI;
    [SerializeField]
    Slider timerSlider;
    [SerializeField]
    Material normal, blue, red, yellow, black;

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
        if (PlayerPrefs.GetInt("tutorial03") != 1)
        {
            inputLocker = true;
            canvasBasic.SetActive(false);
            canvasTuto.SetActive(true);
            Time.timeScale = 0f;
        }

        luzB.SetActive(false); luzR.SetActive(false); luzY.SetActive(false); luzBlack.SetActive(false);

        timerActive = timer;
        maquina = Random.Range(2, 9);

        win.SetActive(false);
        loose.SetActive(false);

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
                PlayerPrefs.SetInt("tutorial03", 1);
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
        else if (timer <= 1.75f)
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
            if (jugador01 != 4)
            {
                jugador01++;
            }
            else if (jugador01 == 4)
            {
                jugador01 = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) & results == false && inputLocker != true && inputLocker != true)
        {
            if (jugador01 != 2)
            {
                jugador01--;
            }
            else if (jugador01 == 2)
            {
                jugador01 = 4;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) & results == false && inputLocker != true)
        {
            if (jugador02 != 4)
            {
                jugador02++;
            }
            else if (jugador02 == 4)
            {
                jugador02 = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) & results == false && inputLocker != true && inputLocker != true)
        {
            if (jugador02 != 2)
            {
                jugador02--;
            }
            else if (jugador02 == 2)
            {
                jugador02 = 4;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) & results == false && inputLocker != true)
        {
            if (maquina != (jugador01 + jugador02) && jugador01 != jugador02)
            {
                Loose();
            }
            else if (maquina != 2 && maquina != 3 && maquina != 4 && maquina == (jugador01 + jugador02) && jugador01 != jugador02)
            {
                CorrectAnswer();
                animationON = true;
            }
            else if (jugador01 == jugador02 && jugador01 == maquina)
            {
                CorrectAnswer();
                animationON = true;
            }
            else if (jugador01 == jugador02 && jugador01 != maquina)
            {
                Loose();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) & results == false && inputLocker != true)
        {
            StartCoroutine("BlackButton");

            if (maquina == 8)
            {
                CorrectAnswer();
                animationON = true;
            }
            else if (maquina != 8)
            {
                Loose();
            }
        }

        //Est�tic:
        if (maquina == 2)
        {
            redMI.color = Color.red;
        }
        else if (maquina == 3)
        {
            redMI.color = Color.yellow;
        }
        else if (maquina == 4)
        {
            redMI.color = Color.blue;
        }
        else if (maquina == 5)
        {
            redMI.color = new Color32(255, 96, 0, 255);
        }
        else if (maquina == 6)
        {
            redMI.color = new Color32(149, 0, 255, 255);
        }
        else if (maquina == 7)
        {
            redMI.color = Color.green;
        }
        else if (maquina == 8)
        {
            redMI.color = Color.black;
        }

       if (jugador01 == 2)
        {
            butonB01.GetComponent<MeshRenderer>().material = normal; butonR01.GetComponent<MeshRenderer>().material = red; butonY01.GetComponent<MeshRenderer>().material = normal;
            luzR.SetActive(true);
        }
        else if (jugador02 != 2)
        {
            luzR.SetActive(false);
        }
        if (jugador01 == 3)
        {
            butonB01.GetComponent<MeshRenderer>().material = normal; butonR01.GetComponent<MeshRenderer>().material = normal; butonY01.GetComponent<MeshRenderer>().material = yellow;
            luzY.SetActive(true);
        }
        else if (jugador02 != 3)
        {
            luzY.SetActive(false);
        }
        if (jugador01 == 4)
        {
            butonB01.GetComponent<MeshRenderer>().material = blue; butonR01.GetComponent<MeshRenderer>().material = normal; butonY01.GetComponent<MeshRenderer>().material = normal;
            luzB.SetActive(true);
        }
        else if (jugador02 != 4)
        {
            luzB.SetActive(false);
        }

        if (jugador02 == 2)
        {
            butonB02.GetComponent<MeshRenderer>().material = normal; butonR02.GetComponent<MeshRenderer>().material = red; butonY02.GetComponent<MeshRenderer>().material = normal;
            luzR.SetActive(true);
        }
        else if (jugador01 != 2)
        {
            luzR.SetActive(false);
        }
        if (jugador02 == 3)
        {
            butonB02.GetComponent<MeshRenderer>().material = normal; butonR02.GetComponent<MeshRenderer>().material = normal; butonY02.GetComponent<MeshRenderer>().material = yellow;
            luzY.SetActive(true);
        }
        else if (jugador01 != 3)
        {
            luzY.SetActive(false);
        }
        if (jugador02 == 4)
        {
            butonB02.GetComponent<MeshRenderer>().material = blue; butonR02.GetComponent<MeshRenderer>().material = normal; butonY02.GetComponent<MeshRenderer>().material = normal;
            luzB.SetActive(true);
        }
        else if (jugador01 != 4)
        {
            luzB.SetActive(false);
        }
    }
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
            maquina = Random.Range(2, 9);
            timer = timer - 0.75f;
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
        PlayerPrefs.SetInt("ganado03", 1);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MenuPrincipal");
    }
    public void WinSound()
    {
        reproductorSonido.PlayOneShot(victoriaClip);
        alreadyWin = true;
    }

    IEnumerator BlackButton()
    {
        buttonBlack.GetComponent<MeshRenderer>().material = black;
        luzBlack.SetActive(true);

        yield return new WaitForSeconds(2);

        buttonBlack.GetComponent<MeshRenderer>().material = normal;
        luzBlack.SetActive(false);
    }


    //Loose:
    public void Repeat()
    {
        SceneManager.LoadScene("Game03");
    }
    public void Exit()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
