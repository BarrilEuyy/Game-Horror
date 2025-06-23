using UnityEngine;
using UnityEngine.UI;

public class EmailDetailPanel : MonoBehaviour
{
    public Text senderEmail;
    public Text senderName;
    // public Text bodyText;
    // public Image missionImage;
    public Button startButton;


    private MissionData currentMission;

    public void ShowDetail(MissionData mission)
    {
        currentMission = mission;
        Debug.Log(mission.senderEmail);
        senderEmail.text = mission.senderEmail;
        senderName.text = mission.senderName;
        // bodyText.text = mission.body;
        // missionImage.sprite = mission.image;
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => {
            StartMission(mission.sceneName);
        });

        gameObject.SetActive(true); // pastikan panel tampil
    }

    void StartMission(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
