using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerGame02 : MonoBehaviour
{
    int maquina;
    int player01 = 2;
    int player02 = 2;
    bool results = false;
    public float timer = 5f;
    private float timerActive;

    [SerializeField]
    GameObject win, loose;
    [SerializeField]
    Image redPI01, redPI02, redMI;
    [SerializeField]
    TextMeshProUGUI timerText;

    void Start()
    {
        timerActive = timer;
        maquina = Random.Range(2, 8);

        win.SetActive(false);
        loose.SetActive(false);
    }

    void Update()
    {
        //Sistema de temporizador de cada ronda:
        timerActive -= Time.deltaTime;
        if (timerActive < 0)
        {
            Loose();
        }
        else if (timer <= 1.75f)
        {
            win.SetActive(true);
            results = true;
            Time.timeScale = 0f;
        }


        //Sistema de Gameplay:
        if (Input.GetKeyDown(KeyCode.Q) & results == false)
        {
            if (player01 != 4)
            {
                player01++;
            }
            else if (player01 == 4)
            {
                player01 = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) & results == false)
        {
            if (player02 != 4)
            {
                player02++;
            }
            else if (player02 == 4)
            {
                player02 = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) & results == false)
        {
            if (maquina != (player01 + player02) && player01 != player02)
            {
                Loose();
            }
            else if (maquina != 2 && maquina != 3 && maquina != 4 && maquina == (player01 + player02) && player01 != player02)
            {
                maquina = Random.Range(2, 8);
                timer = timer - 0.25f;
                timerActive = timer;
            }
            else if (player01 == player02 && player01 == maquina)
            {
                maquina = Random.Range(2, 8);
                timer = timer - 0.25f;
                timerActive = timer;
            }
        }

        timerText.text = timerActive.ToString("00.00");
        Debug.Log(timerActive.ToString("00.00"));

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
            redMI.color = Color.cyan;
        }
        else if (maquina == 6)
        {
            redMI.color = Color.magenta;
        }
        else if (maquina == 7)
        {
            redMI.color = Color.green;
        }

        if (player01 == 2)
        {
            redPI01.color = Color.red;
        }
        else if (player01 == 3)
        {
            redPI01.color = Color.yellow;
        }
        else if (player01 == 4)
        {
            redPI01.color = Color.blue;
        }

        if (player02 == 2)
        {
            redPI02.color = Color.red;
        }
        else if (player02 == 3)
        {
            redPI02.color = Color.yellow;
        }
        else if (player02 == 4)
        {
            redPI02.color = Color.blue;
        }
    }

    void Loose()
    {
        loose.SetActive(true);
        results = true;
        Time.timeScale = 0f;
    }

}
