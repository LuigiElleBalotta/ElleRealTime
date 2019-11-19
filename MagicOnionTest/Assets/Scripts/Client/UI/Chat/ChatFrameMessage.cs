using System;
using JetBrains.Annotations;
using MessagePack.Internal;
using UnityEngine;
using TMPro;

public class ChatFrameMessage : MonoBehaviour
{
    [SerializeField, UsedImplicitly] private TMP_Text messageLabel;
    [SerializeField, UsedImplicitly] private RectTransform rectTransform;
    //[SerializeField, UsedImplicitly] private LocalizedString chatGeneralString;

    public RectTransform RectTransform => rectTransform;

    public void Modify(string playerName, string message)
    {
        messageLabel.text = $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] [{playerName}]: {message}";
    }

    public void MoveToBottom()
    {
        rectTransform.SetAsLastSibling();
    }
}