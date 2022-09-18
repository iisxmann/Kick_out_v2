using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Pause : MonoBehaviour
{
    public GameObject PausePanel;
   public void Pause_Control()
    {
        if(Time.timeScale == 1)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
    }
}
