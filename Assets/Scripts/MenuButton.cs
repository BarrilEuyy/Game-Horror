using UnityEngine;
using UnityEngine.UI; // Jangan lupa tambahkan ini untuk komponen UI

public class MenuButton : MonoBehaviour
{
    private Button button; // Referensi ke komponen Button

    // Start is called before the first frame update
    void Start()
    {
        // Dapatkan komponen Button
        button = GetComponent<Button>();
        
        // Tambahkan listener untuk onClick
        button.onClick.AddListener(() => {
            Debug.Log("klik");
             UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}