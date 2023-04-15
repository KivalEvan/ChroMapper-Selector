using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.String;

namespace Selector.UserInterface
{
    public class UI
    {
        private readonly ExtensionButton _extensionBtn = new ExtensionButton();
        private readonly Selector _selector;
        private GameObject _selectorMenu;

        public UI(Selector selector)
        {
            _selector = selector;

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Selector.Icon.png");
            var data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);

            var texture2D = new Texture2D(256, 256);
            texture2D.LoadImage(data);

            _extensionBtn.Icon = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0, 0), 100.0f);
            _extensionBtn.Tooltip = "Selector";
            ExtensionButtons.AddButton(_extensionBtn);
        }

        public void AddMenu(MapEditorUI mapEditorUI)
        {
            var parent = mapEditorUI.MainUIGroup[5];
            _selectorMenu = new GameObject("Select Menu");
            _selectorMenu.transform.parent = parent.transform;

            AttachTransform(_selectorMenu, 360, 175, 1, 1, 0, 0, 1, 1);

            var image = _selectorMenu.AddComponent<Image>();
            image.sprite = PersistentUI.Instance.Sprites.Background;
            image.type = Image.Type.Sliced;
            image.color = new Color(0.24f, 0.24f, 0.24f);

            AddCheckbox("Select Note", "Select Note", new Vector2(-105, -5),
                Options.SelectNote, check => { Options.SelectNote = check; });
            AddCheckbox("Select Bomb", "Select Bomb", new Vector2(-105, -20),
                Options.SelectBomb, check => { Options.SelectBomb = check; });
            AddCheckbox("Select Event", "Select Event", new Vector2(-105, -35),
                Options.SelectEvent, check => { Options.SelectEvent = check; });
            AddCheckbox("Select Arc", "Select Arc", new Vector2(-5, -5), Options.SelectArc,
                check => { Options.SelectArc = check; });
            AddCheckbox("Select Chain", "Select Chain", new Vector2(-5, -20),
                Options.SelectChain, check => { Options.SelectChain = check; });
            AddCheckbox("Select Obstacle", "Select Obstacle", new Vector2(-5, -35),
                Options.SelectObstacle, check => { Options.SelectObstacle = check; });
            
            AddTextInput("Input Event Float Value Tolerance", "±",
                new Vector2(-5, -55),
                Options.TimeTolerance.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res))
                        Options.TimeTolerance = res;
                });
            AddTextInput("Select Time End", "to", new Vector2(-52.5f, -55),
                Options.TimeEnd.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res)) Options.TimeEnd = res;
                });
            AddTextInput("Select Time Start", "", new Vector2(-105, -55),
                Options.TimeStart.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res)) Options.TimeStart = res;
                });
            AddCheckbox("Select Time", "Time (Beat)", new Vector2(-145, -55),
                Options.TimeSelect, check => { Options.TimeSelect = check; });
            AddCheckbox("Select BPM Change", "Use BPM Change", new Vector2(-215, -55),
                Options.TimeBpmChange, check => { Options.TimeBpmChange = check; });
            
            AddLabel("Grid Label", "Grid", new Vector2(-55, -75));
            AddDropdown("Dropdown Grid Color", "", new Vector2(-5, -90),
                Items.NoteColor, value =>
                {
                    Options.GridColor = Items.NoteColor[value];
                });
            AddDropdown("Dropdown Grid Direction", "", new Vector2(-5, -110),
                Items.NoteDirection, value =>
                {
                    Options.GridDirection = Items.NoteDirection[value];
                });
            AddTextInput("Input Grid X", "", new Vector2(-62.5f, -130),
                Options.GridX.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.GridX = res;
                });
            AddTextInput("Input Grid Y", "", new Vector2(-62.5f, -150),
                Options.GridY.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.GridY = res;
                });
            AddCheckbox("Select Grid Color", "Color", new Vector2(-100, -90),
                Options.GridColorSelect, check => { Options.GridColorSelect = check; });
            AddCheckbox("Select Grid Direction", "Direction", new Vector2(-100, -110),
                Options.GridDirectionSelect, check => { Options.GridDirectionSelect = check; });
            AddCheckbox("Select Grid X", "X", new Vector2(-100, -130),
                Options.GridXSelect, check => { Options.GridXSelect = check; });
            AddCheckbox("Select Grid Y", "Y", new Vector2(-100, -150),
                Options.GridYSelect, check => { Options.GridYSelect = check; });
            
            AddLabel("Event Label", "Event", new Vector2(-255, -75));
            AddDropdown("Dropdown Event Value Color", "", new Vector2(-195, -90),
                Items.EventType, value =>
                {
                    Options.EventType = Items.EventType[value];
                });
            AddTextInput("Input Event Type", "", new Vector2(-157.5f, -90),
                Options.EventTypeCustom.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.EventTypeCustom = res;
                });
            AddDropdown("Dropdown Event Value Color", "", new Vector2(-195, -110),
                Items.EventValueColor, value =>
                {
                    Options.EventValueColor = Items.EventValueColor[value];
                });
            AddTextInput("Input Event Value", "", new Vector2(-157.5f, -110),
                Options.EventValueCustom.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.EventValueCustom = res;
                });
            AddDropdown("Dropdown Event Value Type", "", new Vector2(-195, -130),
                Items.EventValueType, value =>
                {
                    Options.EventValueType = Items.EventValueType[value];
                });
            AddTextInput("Input Event Float Value Tolerance", "±",
                new Vector2(-205, -150),
                Options.EventFloatValueTolerance.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res))
                        Options.EventFloatValueTolerance = res;
                });
            AddTextInput("Input Event Float Value", "", new Vector2(-252.5f, -150),
                Options.EventFloatValue.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res))
                        Options.EventFloatValue = res;
                });
            AddCheckbox("Select Event Type", "Type", new Vector2(-290, -90),
                Options.EventTypeSelect, check => { Options.EventTypeSelect = check; });
            AddCheckbox("Select Event Value Color", "Value", new Vector2(-290, -110),
                Options.EventValueColorSelect, check => { Options.EventValueColorSelect = check; });
            AddCheckbox("Select Event Value Type", "", new Vector2(-290, -130),
                Options.EventValueTypeSelect, check => { Options.EventValueTypeSelect = check; });
            AddCheckbox("Select Event Float Value", "Float Value", new Vector2(-290, -150),
                Options.EventFloatValueSelect, check => { Options.EventFloatValueSelect = check; });

            AddButton("Perform Select", "Select", new Vector2(-195, -5),
                () => { _selector.Select(); });
            AddButton("Perform Deselect", "Deselect", new Vector2(-195, -30),
                () => { _selector.Deselect(); });
            AddButton("Perform Select All", "Select All", new Vector2(-275, -5),
                () => { _selector.SelectAll(); });
            AddButton("Perform Deselect All", "Deselect All", new Vector2(-275, -30),
                () => { _selector.DeselectAll(); });

            _selectorMenu.SetActive(false);
            _extensionBtn.Click = () => { _selectorMenu.SetActive(!_selectorMenu.activeSelf); };
        }

        // i ended up copying Top_Cat's CM-JS UI helper, too useful to make my own tho
        // after askin TC if it's one of the only way, he let me use this
        private void AddButton(string title, string text, Vector2 pos, UnityAction onClick)
        {
            var button = Object.Instantiate(PersistentUI.Instance.ButtonPrefab, _selectorMenu.transform);
            MoveTransform(button.transform, 80, 25, 1, 1, pos.x, pos.y);

            button.name = title;
            button.SetText(text);
            button.Text.enableAutoSizing = false;
            button.Text.fontSize = 14;
            button.Button.onClick.AddListener(onClick);
        }

        private void AddLabel(string title, string text, Vector2 pos, Vector2? size = null)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = (RectTransform)entryLabel.transform;
            rectTransform.SetParent(_selectorMenu.transform);

            MoveTransform(rectTransform, 100, 20, 1, 1, pos.x, pos.y);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Center;
            textComponent.fontSize = 14;
            textComponent.text = text;
        }

        private void AddTextInput(string title, string text, Vector2 pos, string value,
            UnityAction<string> onChange)
        {
            if (!IsNullOrEmpty(text))
            {
                var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
                var rectTransform = (RectTransform)entryLabel.transform;
                rectTransform.SetParent(_selectorMenu.transform);

                MoveTransform(rectTransform, 60, 16, 1, 1, pos.x - 40, pos.y - 2);
                var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

                textComponent.name = title;
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

        private void AddCheckbox(string title, string text, Vector2 pos, bool value,
            UnityAction<bool> onClick)
        {
            if (!IsNullOrEmpty(text))
            {
                var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
                var rectTransform = (RectTransform)entryLabel.transform;
                rectTransform.SetParent(_selectorMenu.transform);
                MoveTransform(rectTransform, 80, 16, 1, 1, pos.x - 18, pos.y - 2);
                var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

                textComponent.name = title;
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

        private void AddDropdown(string title, string text, Vector2 pos, List<string> listStr,
            UnityAction<int> onChange)
        {
            if (!IsNullOrEmpty(text))
            {
                var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
                var rectTransform = (RectTransform)entryLabel.transform;
                rectTransform.SetParent(_selectorMenu.transform);
                MoveTransform(rectTransform, 60, 16, 1, 1, pos.x - 100, pos.y - 2);
                var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

                textComponent.name = title;
                textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
                textComponent.alignment = TextAlignmentOptions.Left;
                textComponent.fontSize = 12;
                textComponent.text = text;
            }

            var dropdown = Object.Instantiate(PersistentUI.Instance.DropdownPrefab, _selectorMenu.transform.transform);
            MoveTransform(dropdown.transform, 95, 20, 1, 1, pos.x, pos.y);
            dropdown.SetOptions(listStr);
            dropdown.Dropdown.onValueChanged.AddListener(onChange);
        }

        private static void AttachTransform(GameObject obj, float sizeX, float sizeY, float anchorX, float anchorY,
            float anchorPosX, float anchorPosY, float pivotX, float pivotY = 0.5f)
        {
            var rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector2(anchorPosX, anchorPosY);
        }

        private static void MoveTransform(Transform transform, float sizeX, float sizeY, float anchorX, float anchorY,
            float anchorPosX, float anchorPosY)
        {
            if (!(transform is RectTransform rectTransform)) return;

            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector2(anchorPosX, anchorPosY);
        }
    }
}