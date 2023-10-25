using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerInfinite : MonoBehaviour
{
    int maquina;
    int jugador01 = 2;
    int jugador02 = 2;
    bool results = false;
    public float timerActive = 20f;

    //Gameplay:
    [SerializeField]
    GameObject loose, luzB, luzR, luzY, luzBlack, butonB01, butonB02, butonR01, butonR02, butonY01, butonY02, buttonBlack, canvasBasic;    
    [SerializeField]
    Image redMI;
    [SerializeField]
    Slider timerSlider;
    [SerializeField]
    Material normal, blue, red, yellow, black;
    [SerializeField]
    TextMeshProUGUI máxRecord, record;
    float recordTiempo;

    //Animation:
    [SerializeField]
    GameObject cup01, cup02, copyCup02, camara;
    bool animationON = false;

    //Sonido:
    bool alreadyWin = false;
    [SerializeField]
    AudioSource reproductorSonido;
    [SerializeField]
    AudioClip victoriaClip, bebidaClip;

    //Tutorial:
    bool inputLocker;
    [SerializeField]
    GameObject canvasTuto;

    void Start()
    {
        Time.timeScale = 1f;

        //Sonido:
        reproductorSonido.volume = PlayerPrefs.GetFloat("sonido");
        camara.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("sonido");

        if (PlayerPrefs.GetInt("tutorialinfinite") != 1)
        {
            inputLocker = true;
            canvasBasic.SetActive(false);
            canvasTuto.SetActive(true);
            Time.timeScale = 0f;
        }

        maquina = Random.Range(2, 9);

        luzB.SetActive(false); luzR.SetActive(false); luzY.SetActive(false); luzBlack.SetActive(false);
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
                PlayerPrefs.SetInt("tutorialinfinite", 1);
            }
        }

        //Sistema de temporizador de cada ronda:
        timerSlider.value = timerActive;
        if (animationON == false)
        {
            timerActive -= Time.deltaTime;
        }

        if (timerActive <= 0)
        {
            End();
        }
        void End()
        {
            if (alreadyWin != true)
            {
                WinSound();
            }

            Time.timeScale = 0f;
            recordTiempo = Time.time - 20;
            if (PlayerPrefs.GetFloat("record") > 0)
            {
                if (recordTiempo > PlayerPrefs.GetFloat("record"))
                {
                    PlayerPrefs.SetFloat("record", recordTiempo);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("record", recordTiempo);
            }

            máxRecord.text = ("MáxRecord: " + PlayerPrefs.GetFloat("record").ToString("00.00"));
            record.text = ("Actual record: " + (Time.time -20 ).ToString("00.00"));
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
                CorrectAnswer(-1);
                animationON = true;
            }
            else if (maquina != 2 && maquina != 3 && maquina != 4 && maquina == (jugador01 + jugador02) && jugador01 != jugador02)
            {
                CorrectAnswer(+1);
                animationON = true;
            }
            else if (jugador01 == jugador02 && jugador01 == maquina)
            {
                CorrectAnswer(+1);
                animationON = true;
            }
            else if (jugador01 == jugador02 && jugador01 != maquina)
            {
                CorrectAnswer(-1);
                animationON = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) & results == false && inputLocker != true)
        {
            StartCoroutine("BlackButton");

            if (maquina == 8)
            {
                CorrectAnswer(+1);
                animationON = true;
            }
            else if (maquina != 8)
            {
                CorrectAnswer(-1);
                animationON = true;
            }
        }

        //Estétic:
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
    void CorrectAnswer(float valor)
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
            timerActive = timerActive + 0.5f * valor;
            timerSlider.maxValue = timerActive;
            animationON = false;
            inputLocker = false;
        });
    }
    IEnumerator BlackButton()
    {
        buttonBlack.GetComponent<MeshRenderer>().material = black;
        luzBlack.SetActive(true);

        yield return new WaitForSeconds(2);

        buttonBlack.GetComponent<MeshRenderer>().material = normal;
        luzBlack.SetActive(false);
    }
    public void WinSound()
    {
        reproductorSonido.PlayOneShot(victoriaClip);
        alreadyWin = true;
    }

    //Loose:
    public void Repeat()
    {
        SceneManager.LoadScene("Infinite");
    }
    public void Exit()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
