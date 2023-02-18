using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayButtonController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    


    public void Play(int num)
    {
        if (!(bool)EventManager.OnPlayButtonPressed?.Invoke(num))
        {
            text.color = Color.red;

            text.text = "Fake Button :P";

            StartCoroutine(IEChangeText());
        }
    }

    IEnumerator IEChangeText()
    {
        yield return new WaitForSeconds(1f);
        
        text.color = Color.yellow;

        text.text = "PLAY";
    }
}
