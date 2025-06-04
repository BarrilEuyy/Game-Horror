<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;



public class MenuButton : MonoBehaviour
{
    private Button button; // Referensi ke komponen Button

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(() => {
            Debug.Log("klik");
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
=======
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button button; // Referensi ke komponen Button

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(() => {
            Debug.Log("klik");
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> origin/BarrilDev
}