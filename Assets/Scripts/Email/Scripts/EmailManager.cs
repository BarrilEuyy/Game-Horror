using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    public MissionData[] allMissions;
    public GameObject userButtonPrefab;
    public Transform userListParent;
    public EmailDetailPanel detailPanel;

    void Start()
    {
        if (userButtonPrefab == null || userListParent == null || detailPanel == null)
        {
            Debug.LogError("Required references are not set in EmailManager!");
            return;
        }

        foreach (var mission in allMissions)
        {
            // if (mission ==    null) continue;
            // if (!mission.unlocked) continue; // Uncomment jika ingin filter mission terkunci

            GameObject btn = Instantiate(userButtonPrefab, userListParent);
            if (btn == null) continue;

            Text btnText = btn.GetComponentInChildren<Text>();
            if (btnText != null)
            {
                btnText.text = mission.senderName;
            }

            Button button = btn.GetComponent<Button>();
            if (button != null)
            {
                var currentMission = mission; // Variabel lokal untuk closure
                button.onClick.AddListener(() => {
                    detailPanel.ShowDetail(currentMission);
                });
            }
        }

        // Tampilkan detail mission pertama (opsional)
        if (allMissions.Length > 0 && allMissions[0] != null)
        {
            detailPanel.ShowDetail(allMissions[0]);
        }
    }
}