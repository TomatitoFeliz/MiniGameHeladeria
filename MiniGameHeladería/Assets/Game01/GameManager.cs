
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    int maquina;
    int player = 1;
    bool results = false;
    public float timer = 5f;
    private float timerActive;

    [SerializeField]
    GameObject win, loose;
    [SerializeField]
    Image redPI, redMI;
    [SerializeField] 
    TextMeshProUGUI timerText;

    void Start()
    {
        timerActive = timer;
        maquina = Random.Range(0, 3);

        win.SetActive(false);
        loose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        //Sistema de temporizador de cada ronda:
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
                maquina = Random.Range(0, 3);
                timer = timer - 0.25f;
                timerActive = timer;
            }
        }

        timerText.text = timerActive.ToString("00.00");
        Debug.Log(timerActive.ToString("00.00"));

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
            redPI.color = Color.red;
        }
        else if (player == 1)
        {
            redPI.color = Color.yellow;
        }
        else if (player == 2)
        {
            redPI.color = Color.blue;
        }
    }

    void Loose()
    {
        loose.SetActive(true);
        results = true;
        Time.timeScale = 0f;
    }
}
