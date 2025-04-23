using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.String;

namespace Selector.UI;

internal class UI
{
    private readonly ExtensionButton _extensionBtn = new();
    private readonly Main _main;
    private GameObject _selectorMenu;

    internal UI(Main main)
    {
        _main = main;

        _extensionBtn.Icon = Utils.LoadSpriteFromResources("Selector.Icon.png");
        _extensionBtn.Tooltip = "Selector";
        ExtensionButtons.AddButton(_extensionBtn);
    }

    public void AddMenu(MapEditorUI mapEditorUI)
    {
        var parent = mapEditorUI.MainUIGroup[5];
        _selectorMenu = new("Select Menu");
        _selectorMenu.transform.parent = parent.transform;

        AttachTransform(_selectorMenu, 300, 175, 1, 1, 0, 0, 1, 1);

        var image = _selectorMenu.AddComponent<Image>();
        image.sprite = PersistentUI.Instance.Sprites.Background;
        image.type = Image.Type.Sliced;
        image.color = new(0.24f, 0.24f, 0.24f);

        AddCheckbox("Bomb", new(-5, -5), Options.SelectBomb,
            check => { Options.SelectBomb = check; });
        AddCheckbox("Obstacle", new(-5, -20),
            Options.SelectObstacle, check => { Options.SelectObstacle = check; });
        AddCheckbox("Event", new(-5, -35),
            Options.SelectEvent, check => { Options.SelectEvent = check; });
        AddCheckbox("Note", new(-65, -5),
            Options.SelectNote, check => { Options.SelectNote = check; });
        AddCheckbox("Arc", new(-65, -20),
            Options.SelectArc, check => { Options.SelectArc = check; });
        AddCheckbox("Chain", new(-65, -35),
            Options.SelectChain, check => { Options.SelectChain = check; });

        AddTextInput("±",
            new(-5, -55),
            Options.Time.Tolerance.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res))
                    Options.Time.Tolerance = res;
            });
        AddTextInput("op", new(-52.5f, -55),
            Options.Time.Operand2.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res)) Options.Time.Operand2 = res;
            });
        AddTextInput("", new(-105, -55),
            Options.Time.Operand1.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res)) Options.Time.Operand1 = res;
            });
        AddDropdown("", new(-145, -55),
            Items.Operators.Select(x => x.name), value => { Options.Time.Operation = Items.Operators[value].op; });
        AddCheckbox("Time (Beat)", new(-220, -55),
            Options.Time.Enabled, check => { Options.Time.Enabled = check; });

        AddLabel("Grid", new(-55, -75));
        AddDropdown("", new(-5, -90),
            Items.NoteColors.Select(x => x.name), value => { Options.GridColorDropdown = Items.NoteColors[value]; });
        AddCheckbox("Color", new(-80, -90),
            Options.GridColor.Enabled, check => { Options.GridColor.Enabled = check; });
        AddDropdown("", new(-5, -110),
            Items.NoteDirections.Select(x => x.name),
            value => { Options.GridDirectionDropdown = Items.NoteDirections[value]; });
        AddCheckbox("Direction", new(-80, -110),
            Options.GridDirection.Enabled, check => { Options.GridDirection.Enabled = check; });
        AddTextInput("", new(-42.5f, -130),
            Options.GridX.Operand1.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.GridX.Operand1 = res;
            });
        AddCheckbox("X", new(-80, -130),
            Options.GridX.Enabled, check => { Options.GridX.Enabled = check; });
        AddTextInput("", new(-42.5f, -150),
            Options.GridY.Operand1.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.GridY.Operand1 = res;
            });
        AddCheckbox("Y", new(-80, -150),
            Options.GridY.Enabled, check => { Options.GridY.Enabled = check; });

        AddLabel("Event", new(-230, -75));
        AddDropdown("", new(-175, -90),
            Items.EventTypes.Select(x => x.name), value => { Options.EventTypeDropdown = Items.EventTypes[value]; });
        AddTextInput("", new(-137.5f, -90),
            Options.EventType.Operand1.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.EventType.Operand1 = res;
            });
        AddCheckbox("Type", new(-250, -90),
            Options.EventType.Enabled, check => { Options.EventType.Enabled = check; });
        AddDropdown("", new(-175, -110),
            Items.EventValueColors.Select(x => x.name),
            value => { Options.EventValueColorDropdown = Items.EventValueColors[value]; });
        AddTextInput("", new(-137.5f, -110),
            Options.EventValue.Operand1.ToString(), value =>
            {
                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                    Options.EventValue.Operand1 = res;
            });
        AddDropdown("", new(-175, -130),
            Items.EventValueTypes.Select(x => x.name),
            value => { Options.EventValueTypeDropdown = Items.EventValueTypes[value]; });
        AddCheckbox("Value", new(-250, -110),
            Options.EventValue.Enabled, check => { Options.EventValue.Enabled = check; });
        AddTextInput("±",
            new(-165, -150),
            Options.EventFloatValue.Tolerance.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res))
                    Options.EventFloatValue.Tolerance = res;
            });
        AddTextInput("", new(-212.5f, -150),
            Options.EventFloatValue.Operand1.ToString(CultureInfo.InvariantCulture), value =>
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                        out var res))
                    Options.EventFloatValue.Operand1 = res;
            });
        AddCheckbox("Float", new(-250, -150),
            Options.EventFloatValue.Enabled, check => { Options.EventFloatValue.Enabled = check; });

        AddButton("Select", new(-135, -5),
            () => { _main.Select(); });
        AddButton("Deselect", new(-135, -30),
            () => { _main.Deselect(); });
        AddButton("Select All", new(-215, -5),
            () => { _main.SelectAll(); });
        AddButton("Deselect All", new(-215, -30),
            () => { Main.DeselectAll(); });

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
        MoveTransform(dropdown.transform, 75, 20, 1, 1, pos.x, pos.y);
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