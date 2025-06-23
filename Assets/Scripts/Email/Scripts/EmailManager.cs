using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    public MissionData[] allMissions;
    public GameObject userButtonPrefab;

    public GameObject leftPanel;
    public Transform userListParent;
    public EmailDetailPanel detailPanel;

    void Start()
    {
        if (userButtonPrefab == null || userListParent == null)
        {
            Debug.LogError("Required references are not set in EmailManager!");
            
            return;
        }

        // detailPanel.gameObject.SetActive(false);

        foreach (var mission in allMissions)
        {
            // if (mission ==    null) continue;x   
            // if (!mission.unlocked) continue; // Uncomment jika ingin filter mission terkunci

            GameObject btn = Instantiate(userButtonPrefab, userListParent);
            ScrollRect listUserPanel = userListParent.GetComponentInParent<ScrollRect>();
            if (btn == null) continue;

            Text btnText = btn.GetComponentInChildren<Text>();
            if (btnText != null)
            {
                btnText.text = mission.senderEmail;
            }

            Button button = btn.GetComponent<Button>();
            if (button != null)
            {
                var currentMission = mission; // Variabel lokal untuk closure
                button.onClick.AddListener(() =>
                {
                    listUserPanel.gameObject.SetActive(false);
                    leftPanel.SetActive(false);
                    detailPanel.gameObject.SetActive(true);
                    detailPanel.ShowDetail(currentMission);
                });
            }
        }

        // Tampilkan detail mission pertama (opsional)
        // if (allMissions.Length > 0 && allMissions[0] != null)
        // {
        //     detailPanel.ShowDetail(allMissions[0]);
        // }
    }
}