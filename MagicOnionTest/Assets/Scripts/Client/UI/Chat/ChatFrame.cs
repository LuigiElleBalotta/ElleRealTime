using System.Collections.Generic;
using Client;
using Common;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ChatFrame : MonoBehaviour
{
    //[SerializeField, UsedImplicitly] private InputReference input;
    [SerializeField, UsedImplicitly] private ScrollRect scrollRect;
    [SerializeField, UsedImplicitly] private ChatFrameMessage messagePrototype;
    [SerializeField, UsedImplicitly] private TMP_InputField inputField;
    [SerializeField, UsedImplicitly] private Transform messageContainer;
    //[SerializeField, UsedImplicitly] private HotkeyInputItem chatFocusHotkey;
    [SerializeField, UsedImplicitly] private int maxMessageCount = 100;

    private readonly List<ChatFrameMessage> chatMessages = new List<ChatFrameMessage>();
    private const float BottomSnapThreshold = 0.001f;

    private static ChatFrame _instance;
    public static ChatFrame Instance { get { return _instance; } }

    [UsedImplicitly]
    public bool SnapToBottom { get; set; } = true;

    [UsedImplicitly]
    private void Awake()
    {
        /*EventHandler.RegisterEvent<Unit, string>(EventHandler.GlobalDispatcher, GameEvents.UnitChat, OnUnitChat);
        EventHandler.RegisterEvent<HotkeyState>(chatFocusHotkey, GameEvents.HotkeyStateChanged, OnHotkeyStateChanged);*/
        _instance = this;
        inputField.onSubmit.AddListener(OnSubmit);
        inputField.onDeselect.AddListener(OnDeselect);
        inputField.onSelect.AddListener(OnSelect);
        /*
        GameObjectPool.PreInstantiate(messagePrototype, maxMessageCount);*/
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
        /*foreach (ChatFrameMessage message in chatMessages)
            GameObjectPool.Return(message, true);

        chatMessages.Clear();
        inputField.onSubmit.RemoveListener(OnSubmit);
        inputField.onDeselect.RemoveListener(OnDeselect);
        EventHandler.UnregisterEvent<Unit, string>(EventHandler.GlobalDispatcher, GameEvents.UnitChat, OnUnitChat);
        EventHandler.UnregisterEvent<HotkeyState>(chatFocusHotkey, GameEvents.HotkeyStateChanged, OnHotkeyStateChanged);*/
    }

    [UsedImplicitly]
    private void Update()
    {
        if (SnapToBottom && scrollRect.verticalNormalizedPosition > BottomSnapThreshold)
            scrollRect.normalizedPosition = Vector2.zero;
    }

    private void OnSelect(string text)
    {
        Debug.Log("[CHAT] On SELECT");
        BaseUnit.Instance.SetChatIsOpen(true);
    }

    private void OnDeselect(string text)
    {
        inputField.text = string.Empty;
        BaseUnit.Instance.SetChatIsOpen(true);
    }

    private void OnSubmit(string text)
    {
        Debug.Log($"Sending message: {text}");
        inputField.text = string.Empty;

        InitClient.Instance.SendChatMessageAsync(text);

        /*if (!inputField.wasCanceled && !string.IsNullOrEmpty(text))
            input.Say(text);*/

        if (EventSystem.current.currentSelectedGameObject == inputField.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
            BaseUnit.Instance.SetChatIsOpen(true);
        }
            
    }

    public void AddMessageToChat(string playerName, string text)
    {
        ChatFrameMessage chatFrameMessage;
        if (chatMessages.Count >= maxMessageCount)
        {
            chatFrameMessage = chatMessages[0];
            chatMessages.RemoveAt(0);
        }
        else
        {
            //Create an empty message
            chatFrameMessage = GameObjectPool.Take(messagePrototype);
            //Set the object inside the container.
            chatFrameMessage.RectTransform.SetParent(messageContainer, false);
        }
        chatMessages.Add(chatFrameMessage);
        chatFrameMessage.Modify(playerName, text);
        chatFrameMessage.MoveToBottom();
    }

    /*private void OnUnitChat(Unit unit, string text)
    {
        ChatFrameMessage chatFrameMessage;
        if (chatMessages.Count >= maxMessageCount)
        {
            chatFrameMessage = chatMessages[0];
            chatMessages.RemoveAt(0);
        }
        else
        {
            chatFrameMessage = GameObjectPool.Take(messagePrototype);
            chatFrameMessage.RectTransform.SetParent(messageContainer, false);
        }

        chatMessages.Add(chatFrameMessage);
        chatFrameMessage.Modify(unit, text);
        chatFrameMessage.MoveToBottom();

        if (scrollRect.verticalNormalizedPosition < BottomSnapThreshold)
            SnapToBottom = true;
    }*/
    
    private void OnHotkeyStateChanged(HotkeyState state)
    {
        if (enabled && state == HotkeyState.Pressed)
            if (EventSystem.current.currentSelectedGameObject != inputField.gameObject)
                EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }
}