using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TextMesh OutputTimer;
    float outputMinutes = 0f;
    float outputSeconds = 0f;
    float startTimeStamp = 0f;
    float pauseTimeStamp = 0f;

    bool actionFlag = false;
    public Camera mainCamera;
    RaycastHit buttonWithHit;
    bool pressButtonFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        startTimeStamp = Time.time;
        OutputTimer.text = "00:00";
    }

    //Update is called once per frame
    void Update()
    {
        if (actionFlag) 
        {
            outputMinutes = Mathf.Floor((Time.time - startTimeStamp) / 60);
            outputSeconds = Mathf.Floor((Time.time - startTimeStamp) % 60);
            OutputTimer.text = outputMinutes.ToString("00") + ':' + outputSeconds.ToString("00");
             
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray rayToButton;
            
            rayToButton = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayToButton, out buttonWithHit))
            {
                buttonWithHit!.transform.position -= new Vector3(0.1f, 0, 0);
                pressButtonFlag = true;
            }

        }
        if (Input.GetMouseButtonUp(0) && pressButtonFlag)
        {
            buttonWithHit.transform.position += new Vector3(0.1f, 0, 0);
            pressButtonFlag = false;
            if (buttonWithHit.transform.gameObject.name == "bStart" && !actionFlag)
            {
                actionFlag = true;
                startTimeStamp = Time.time - pauseTimeStamp;
                pauseTimeStamp = 0f;
            }
            if (buttonWithHit.transform.gameObject.name == "bStop")
            {
                actionFlag = false;
                OutputTimer.text = "00:00";
                pauseTimeStamp = 0f;
            }
            if (buttonWithHit.transform.gameObject.name == "bPause" )
            {
                if (actionFlag)
                {
                    actionFlag = false;
                    pauseTimeStamp = Time.time - startTimeStamp;
                }
                else 
                if (pauseTimeStamp != 0f)
                {
                    actionFlag = true;
                    startTimeStamp = Time.time - pauseTimeStamp;
                    pauseTimeStamp = 0f;
                }
            }

        }

    }
}
