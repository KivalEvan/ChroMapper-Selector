using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.String;

namespace Selector.UserInterface;

internal class UI
{
    private readonly ExtensionButton _extensionBtn = new();
    private readonly Main _main;
    private GameObject _selectorMenu;

    internal UI(Main main)
    {
        _main = main;

        _extensionBtn.Icon = Utils.LoadSpriteFromResources("Selector.Icon.png");
        ;
        _extensionBtn.Tooltip = "Selector";
        ExtensionButtons.AddButton(_extensionBtn);
    }

    public void AddMenu(MapEditorUI mapEditorUI)
    {
        var parent = mapEditorUI.MainUIGroup[5];
        _selectorMenu = new("Select Menu");
        _selectorMenu.transform.parent = parent.transform;

        AttachTransform(_selectorMenu, 360, 175, 1, 1, 0, 0, 1, 1);

        var image = _selectorMenu.AddComponent<Image>();
        image.sprite = PersistentUI.Instance.Sprites.Background;
        image.type = Image.Type.Sliced;
        image.color = new(0.24f, 0.24f, 0.24f);

        AddCheckbox("Select Note", new(-105, -5),
            Options.SelectNote, check => { Options.SelectNote = check; });
        AddCheckbox("Select Bomb", new(-105, -20),
            Options.SelectBomb, check => { Options.SelectBomb = check; });
        AddCheckbox("Select Event", new(-105, -35),
            Options.SelectEvent, check => { Options.SelectEvent = check; });
        AddCheckbox("Select Arc", new(-5, -5), Options.SelectArc,
            check => { Options.SelectArc = check; });
        AddCheckbox("Select Chain", new(-5, -20),
            Options.SelectChain, check => { Options.SelectChain = check; });
        AddCheckbox("Select Obstacle", new(-5, -35),
            Options.SelectObstacle, check => { Options.SelectObstacle = check; });

        AddTextInput("±",
            new(-5, -55),
            Options.TimeTolerance.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res))
                    Options.TimeTolerance = res;
            });
        AddTextInput("to", new(-52.5f, -55),
            Options.TimeOperand2.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res)) Options.TimeOperand2 = res;
            });
        AddTextInput("", new(-105, -55),
            Options.TimeOperand1.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res)) Options.TimeOperand1 = res;
            });
        AddCheckbox("Time (Beat)", new(-145, -55),
            Options.TimeSelect, check => { Options.TimeSelect = check; });

        AddLabel("Grid", new(-55, -75));
        AddDropdown("", new(-5, -90),
            Items.NoteColor.Select(x => x.name), value => { Options.GridColor = Items.NoteColor[value]; });
        AddDropdown("", new(-5, -110),
            Items.NoteDirection.Select(x => x.name), value => { Options.GridDirection = Items.NoteDirection[value]; });
        AddTextInput("", new(-62.5f, -130),
            Options.GridX.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.GridX = res;
            });
        AddTextInput("", new(-62.5f, -150),
            Options.GridY.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.GridY = res;
            });
        AddCheckbox("Color", new(-100, -90),
            Options.GridColorSelect, check => { Options.GridColorSelect = check; });
        AddCheckbox("Direction", new(-100, -110),
            Options.GridDirectionSelect, check => { Options.GridDirectionSelect = check; });
        AddCheckbox("X", new(-100, -130),
            Options.GridXSelect, check => { Options.GridXSelect = check; });
        AddCheckbox("Y", new(-100, -150),
            Options.GridYSelect, check => { Options.GridYSelect = check; });

        AddLabel("Event", new(-255, -75));
        AddDropdown("", new(-195, -90),
            Items.EventType.Select(x => x.name), value => { Options.EventType = Items.EventType[value]; });
        AddTextInput("", new(-157.5f, -90),
            Options.EventTypeCustom.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.EventTypeCustom = res;
            });
        AddDropdown("", new(-195, -110),
            Items.EventValueColor.Select(x => x.name),
            value => { Options.EventValueColor = Items.EventValueColor[value]; });
        AddTextInput("", new(-157.5f, -110),
            Options.EventValueCustom.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.EventValueCustom = res;
            });
        AddDropdown("", new(-195, -130),
            Items.EventValueType.Select(x => x.name),
            value => { Options.EventValueType = Items.EventValueType[value]; });
        AddTextInput("±",
            new(-205, -150),
            Options.EventFloatValueTolerance.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res))
                    Options.EventFloatValueTolerance = res;
            });
        AddTextInput("", new(-252.5f, -150),
            Options.EventFloatValue.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res))
                    Options.EventFloatValue = res;
            });
        AddCheckbox("Type", new(-290, -90),
            Options.EventTypeSelect, check => { Options.EventTypeSelect = check; });
        AddCheckbox("Value", new(-290, -110),
            Options.EventValueColorSelect, check => { Options.EventValueColorSelect = check; });
        AddCheckbox("", new(-290, -130),
            Options.EventValueTypeSelect, check => { Options.EventValueTypeSelect = check; });
        AddCheckbox("Float Value", new(-290, -150),
            Options.EventFloatValueSelect, check => { Options.EventFloatValueSelect = check; });

        AddButton("Select", new(-195, -5),
            () => { _main.Select(); });
        AddButton("Deselect", new(-195, -30),
            () => { _main.Deselect(); });
        AddButton("Select All", new(-275, -5),
            () => { _main.SelectAll(); });
        AddButton("Deselect All", new(-275, -30),
            () => { _main.DeselectAll(); });

        _selectorMenu.SetActive(false);
        _extensionBtn.Click = () => { _selectorMenu.SetActive(!_selectorMenu.activeSelf); };
    }

    private void AddButton(string text, Vector2 pos, UnityAction onClick)
    {
        var button = Object.Instantiate(PersistentUI.Instance.ButtonPrefab, _selectorMenu.transform);
        MoveTransform(button.transform, 80, 25, 1, 1, pos.x, pos.y);

        button.name = text;
        button.SetText(text);
        button.Text.enableAutoSizing = false;
        button.Text.fontSize = 14;
        button.Button.onClick.AddListener(onClick);
    }

    private void AddLabel(string text, Vector2 pos, Vector2? size = null)
    {
        var entryLabel = new GameObject(text + " Label", typeof(TextMeshProUGUI));
        var rectTransform = (RectTransform)entryLabel.transform;
        rectTransform.SetParent(_selectorMenu.transform);

        MoveTransform(rectTransform, 100, 20, 1, 1, pos.x, pos.y);
        var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

        textComponent.name = text;
        textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.fontSize = 14;
        textComponent.text = text;
    }

    private void AddTextInput(string text, Vector2 pos, string value,
        UnityAction<string> onChange)
    {
        if (!IsNullOrEmpty(text))
        {
            var entryLabel = new GameObject(text + " Label", typeof(TextMeshProUGUI));
            var rectTransform = (RectTransform)entryLabel.transform;
            rectTransform.SetParent(_selectorMenu.transform);

            MoveTransform(rectTransform, 60, 16, 1, 1, pos.x - 40, pos.y - 2);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = text;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Right;
            textComponent.fontSize = 12;
            textComponent.text = text;
        }

        var textInput = Object.Instantiate(PersistentUI.Instance.TextInputPrefab, _selectorMenu.transform);
        MoveTransform(textInput.transform, 36, 20, 1, 1, pos.x, pos.y);
        textInput.GetComponent<Image>().pixelsPerUnitMultiplier = 3;
        textInput.InputField.text = value;
        textInput.InputField.onFocusSelectAll = false;
        textInput.InputField.textComponent.alignment = TextAlignmentOptions.Left;
        textInput.InputField.textComponent.fontSize = 10;
        textInput.InputField.onValueChanged.AddListener(onChange);
    }

    private void AddCheckbox(string text, Vector2 pos, bool value,
        UnityAction<bool> onClick)
    {
        if (!IsNullOrEmpty(text))
        {
            var entryLabel = new GameObject(text + " Label", typeof(TextMeshProUGUI));
            var rectTransform = (RectTransform)entryLabel.transform;
            rectTransform.SetParent(_selectorMenu.transform);
            MoveTransform(rectTransform, 80, 16, 1, 1, pos.x - 18, pos.y - 2);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = text;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Right;
            textComponent.fontSize = 12;
            textComponent.text = text;
        }

        var original = GameObject.Find("Strobe Generator").GetComponentInChildren<Toggle>(true);
        var toggleObject = Object.Instantiate(original, _selectorMenu.transform.transform);
        MoveTransform(toggleObject.transform, 16, 16, 1, 1, pos.x, pos.y - 2);

        var toggleComponent = toggleObject.GetComponent<Toggle>();
        var colorBlock = toggleComponent.colors;
        colorBlock.normalColor = Color.white;
        toggleComponent.colors = colorBlock;
        toggleComponent.isOn = value;
        toggleComponent.onValueChanged.AddListener(onClick);
    }

    private void AddDropdown(string text, Vector2 pos, IEnumerable<string> listStr,
        UnityAction<int> onChange)
    {
        if (!IsNullOrEmpty(text))
        {
            var entryLabel = new GameObject(text + " Label", typeof(TextMeshProUGUI));
            var rectTransform = (RectTransform)entryLabel.transform;
            rectTransform.SetParent(_selectorMenu.transform);
            MoveTransform(rectTransform, 60, 16, 1, 1, pos.x - 100, pos.y - 2);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = text;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Left;
            textComponent.fontSize = 12;
            textComponent.text = text;
        }

        var dropdown = Object.Instantiate(PersistentUI.Instance.DropdownPrefab, _selectorMenu.transform.transform);
        MoveTransform(dropdown.transform, 95, 20, 1, 1, pos.x, pos.y);
        dropdown.SetOptions(listStr.ToList());
        dropdown.Dropdown.onValueChanged.AddListener(onChange);
    }

    private static void AttachTransform(GameObject obj, float sizeX, float sizeY, float anchorX, float anchorY,
        float anchorPosX, float anchorPosY, float pivotX, float pivotY = 0.5f)
    {
        var rectTransform = obj.AddComponent<RectTransform>();
        rectTransform.localScale = new(1, 1, 1);
        rectTransform.sizeDelta = new(sizeX, sizeY);
        rectTransform.pivot = new(pivotX, pivotY);
        rectTransform.anchorMin = rectTransform.anchorMax = new(anchorX, anchorY);
        rectTransform.anchoredPosition = new(anchorPosX, anchorPosY);
    }

    private static void MoveTransform(Transform transform, float sizeX, float sizeY, float anchorX, float anchorY,
        float anchorPosX, float anchorPosY)
    {
        if (!(transform is RectTransform rectTransform)) return;

        rectTransform.localScale = new(1, 1, 1);
        rectTransform.sizeDelta = new(sizeX, sizeY);
        rectTransform.pivot = new(1, 1);
        rectTransform.anchorMin = rectTransform.anchorMax = new(anchorX, anchorY);
        rectTransform.anchoredPosition = new(anchorPosX, anchorPosY);
    }
}