using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Button reload = null;

    void Start()
    {
        if (reload != null)
        {
            reload.onClick.AddListener(Reload);
        }
    }

    void Update()
    {
        // Android registers Escape as the back button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
