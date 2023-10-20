using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : MonoBehaviour
{
    [SerializeField] private GameObject popUpcontainer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Open()
    {
        popUpcontainer.SetActive(true);
    }
    public void Close()
    {
        popUpcontainer.SetActive(false);
    }
}
