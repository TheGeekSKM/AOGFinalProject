using System.Collections;
using System.Collections.Generic;
using SaiUtils.Singleton;
using TMPro;
using UnityEngine;

public enum ChatterType
{
    Pawn,
    Player,
    System
}
public class PawnChatManager : Singleton<PawnChatManager>
{
    [SerializeField] TextMeshProUGUI chatText;
    [SerializeField] RectTransform chatParent;

    public void AddChat(string chat, ChatterType chatterType)
    {
        switch (chatterType)
        {
            case ChatterType.Pawn:
                chat = "<color=green>" + "<b>Pawn:</b>" + chat + "</color>";
                break;
            case ChatterType.System:
                chat = "<color=red>"+ "<b>System:</b>" + chat + "</color>";
                break;
        }

        var chatTextInstance = Instantiate(chatText, chatParent);
        chatTextInstance.text = chat;
    }
}
