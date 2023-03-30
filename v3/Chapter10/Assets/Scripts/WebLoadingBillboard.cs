using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour
{
    public void Operate()
    {
        Managers.Image.GetWebImage(OnWebImage);
    }

    private void OnWebImage(Texture2D texture)
    {
        GetComponent<Renderer>().material.mainTexture = texture;
    }
}