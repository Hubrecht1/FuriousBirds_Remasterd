using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject defaultMenu;

    bool debugActive = false;
    UIMenu currentUIMenu;

    enum UIMenu
    {
        defaultMenu,
        settingsMenu,
        quitMenu

    }

    private void Start()
    {
        debugActive = false;
        debugMenu.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            debugActive = !debugActive;
            debugMenu.SetActive(debugActive);

            defaultMenu.SetActive(!debugActive);

        }




    }
}
