
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    int maquina;
    int player = 1;
    bool results = false;
    public float timer = 5f;
    private float timerActive;

    [SerializeField]
    GameObject win, loose, luzB, luzR, luzY, butonB, butonR, butonY, canvasBasic, canvasTuto;

    //Animation:
    [SerializeField]
    GameObject cup01, cup02, copyCup02;
    public float speed;
    bool animationON = false;

    bool inputLocker;
    [SerializeField]
    UnityEngine.UI.Image redMI;
    [SerializeField]
    UnityEngine.UI.Slider timerSlider;
    [SerializeField]
    Material normal, blue, red, yellow;

    void Start()
    {
        inputLocker = true;
        canvasBasic.SetActive(false);
        Time.timeScale = 0f; 

        luzB.SetActive(false); luzR.SetActive(false); luzY.SetActive(false);

        timerActive = timer;
        maquina = Random.Range(0, 3);

        win.SetActive(false);
        loose.SetActive(false);

        timerSlider.minValue = 0;
        timerSlider.maxValue = timerActive;
    }

    // Update is called once per frame
    void Update()
    {
        //Tutorial:
        if (canvasTuto.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputLocker = false;
                canvasBasic.SetActive(true);
                canvasTuto.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        //Sistema de temporizador de cada ronda:
        timerSlider.value = timerActive;
        if (animationON == false)
        {
            timerActive -= Time.deltaTime;
        }

        if (timerActive < 0)
        {
            Loose();
        }
        else if (timer <= 0.75f)
        {
            canvasBasic.SetActive(false);
            win.SetActive(true);
            results = true;
            Time.timeScale = 0f;
        }
        void Loose()
        {
            loose.SetActive(true);
            results = true;
            Time.timeScale = 0f;
        }


        //Sistema de Gameplay:
        if (Input.GetKeyDown(KeyCode.Q) & results == false && inputLocker != true)
        {
            if (player != 2)
            {
                player++;
            }
            else if (player == 2)
            {
                player = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) & results == false && inputLocker != true)
        {
            if (player != 0)
            {
                player--;
            }
            else if (player == 0)
            {
                player = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) & results == false && inputLocker != true)
        {
            if (maquina != player)
            {
                Loose();
            }
            else if (maquina == player)
            {
                CupAnimation();
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

        if (player == 0)
        {
            butonB.GetComponent<MeshRenderer>().material = normal; butonR.GetComponent<MeshRenderer>().material = red; butonY.GetComponent<MeshRenderer>().material = normal;
            luzB.SetActive(false); luzR.SetActive(true); luzY.SetActive(false);
        }
        else if (player == 1)
        {
            butonB.GetComponent<MeshRenderer>().material = normal; butonR.GetComponent<MeshRenderer>().material = normal; butonY.GetComponent<MeshRenderer>().material = yellow;
            luzB.SetActive(false); luzR.SetActive(false); luzY.SetActive(true);
        }
        else if (player == 2)
        {
            butonB.GetComponent<MeshRenderer>().material = blue; butonR.GetComponent<MeshRenderer>().material = normal; butonY.GetComponent<MeshRenderer>().material = normal;
            luzB.SetActive(true); luzR.SetActive(false); luzY.SetActive(false);
        }
    }
    void CupAnimation()
    {
        inputLocker = true;
        LeanTween.move(cup01, new Vector3(-2.936f, 0.026f, 8.97f), 1.5f).setOnComplete(() => {
            Destroy(cup01);
        });
        Instantiate(copyCup02, new Vector3(1.192f, 0.026f, 8.97f), Quaternion.identity);
        cup02 = GameObject.Find("BasoGranizado(Clone)");
        cup02.name = "cup02";
        LeanTween.move(cup02, new Vector3(-0.81f, 0.026f, 8.97f), 1.5f).setOnComplete(() => {
            cup01 = cup02;
            maquina = Random.Range(0, 3);
            timer = timer - 0.25f;
            timerActive = timer;
            timerSlider.maxValue = timerActive;
            animationON = false;
            inputLocker = false;
        });
    }
}
