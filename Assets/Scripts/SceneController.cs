using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                if (Input.anyKeyDown) {SceneManager.LoadScene(1);}
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.R)) {SceneManager.LoadScene(1);}
                break;
            
        }
        //go from start to
        
        //restart level
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
