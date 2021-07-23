using UnityEngine;

public class HomeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Home controller started");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("Home button clicked");
            //GameObject.Find("Environment").SetActive(false);
            //GameObject.Find("Home").SetActive(true);
            //canvasController.gameObject.SetActive(!canvasController.gameObject.activeSelf);
        }
    }
}
