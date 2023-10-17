
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
    GameObject win, loose, luzB, luzR, luzY, butonB, butonR, butonY;

    //Animation:
    [SerializeField]
    GameObject cup01, cup02;
    public float speed;

    [SerializeField]
    UnityEngine.UI.Image redMI;
    [SerializeField]
    UnityEngine.UI.Slider timerSlider;
    [SerializeField]
    Material normal, blue, red, yellow;

    void Start()
    {
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
        //Sistema de temporizador de cada ronda:
        timerSlider.value = timerActive;
        timerActive -= Time.deltaTime;

        if (timerActive < 0)
        {
            Loose();
        }
        else if (timer <= 0.75f)
        {
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
        if (Input.GetKeyDown(KeyCode.Q) & results == false)
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
        if (Input.GetKeyDown(KeyCode.E) & results == false)
        {
            if (maquina != player)
            {
                Loose();
            }
            else if (maquina == player)
            {
                CupAnimation();
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
        void CupAnimation()
        {
            Time.timeScale = 0;
            LeanTween.moveLocal(cup01, Vector3.right * speed, 3f).setOnComplete(() => {
                cup01.IsDestroyed();
                cup02 = cup01;
            });
            Instantiate(cup02, Vector3.zero, Quaternion.identity);
            LeanTween.moveLocal(cup02, Vector3.left * speed, 3f).setOnComplete(() => {
                Time.timeScale = 1;
                maquina = Random.Range(0, 3);
                timer = timer - 0.25f;
                timerActive = timer;
                timerSlider.maxValue = timerActive;
            });
        }
    }

}
