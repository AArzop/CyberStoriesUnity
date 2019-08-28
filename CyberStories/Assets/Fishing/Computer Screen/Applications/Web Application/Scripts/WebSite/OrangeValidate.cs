using UnityEngine;
using UnityEngine.UI;

public class OrangeValidate : BaseWebSite
{
    public override void ResetWebSite()
    {

    }

    private void Awake()
    {
        GameObject go = GameObject.Find("Text");
        if (go != null)
            go.GetComponent<Text>().text = GlobalManager.GetLocalization("Loca_OrangeValidate");
    }
}
