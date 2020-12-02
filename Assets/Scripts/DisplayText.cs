using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayText : MonoBehaviour
{
    public string textToDisplay;
    TMP_Text text;
    Vector3 dir;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = textToDisplay;
    }
    private void LateUpdate()
    {
        dir = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(dir); ;
    }
}
