using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeButtonsActiver : MonoBehaviour
{

    bool isSlided = false;

    public void Slide(GameObject objToSlide)
    {
        if (!objToSlide.activeInHierarchy)
        {
            objToSlide.SetActive(true);

            return;
        }

        objToSlide.SetActive(false);
    }
}
