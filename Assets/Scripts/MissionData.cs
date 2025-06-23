using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission/Mission Data")]
public class MissionData : ScriptableObject
{
    public string subject;
    public string senderEmail;

    public string senderName;
    [TextArea(3, 10)]
    public string body;
    public Sprite missionImage;
    public string sceneName;


    public bool unlocked;
}
