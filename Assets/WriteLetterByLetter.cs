using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteLetterByLetter : MonoBehaviour
{
    public string textToWrite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WriteText());
    }
    private IEnumerator WriteText()
    {
        for(int i = 0; i < textToWrite.Length; i++)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text += textToWrite[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < textToWrite.Length; i++)
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<TMPro.TextMeshProUGUI>().text.Remove(GetComponent<TMPro.TextMeshProUGUI>().text.Length - 1);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
