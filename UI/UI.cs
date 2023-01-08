using System.Globalization;
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

            AttachTransform(_selectorMenu, 275, 175, 1, 1, 0, 0, 1, 1);

            var image = _selectorMenu.AddComponent<Image>();
            image.sprite = PersistentUI.Instance.Sprites.Background;
            image.type = Image.Type.Sliced;
            image.color = new Color(0.24f, 0.24f, 0.24f);

            AddCheckbox(_selectorMenu.transform, "Select Note", "Select Note", new Vector2(0, -15),
                Options.SelectNote, check => { Options.SelectNote = check; });
            AddCheckbox(_selectorMenu.transform, "Select Bomb", "Select Bomb", new Vector2(0, -30),
                Options.SelectBomb, check => { Options.SelectBomb = check; });
            AddCheckbox(_selectorMenu.transform, "Select Event", "Select Event", new Vector2(0, -45),
                Options.SelectEvent, check => { Options.SelectEvent = check; });
            AddCheckbox(_selectorMenu.transform, "Select Arc", "Select Arc", new Vector2(85, -15), Options.SelectEvent,
                check => { Options.SelectArc = check; });
            AddCheckbox(_selectorMenu.transform, "Select Chain", "Select Chain", new Vector2(85, -30),
                Options.SelectObstacle, check => { Options.SelectChain = check; });
            AddCheckbox(_selectorMenu.transform, "Select Obstacle", "Select Obstacle", new Vector2(85, -45),
                Options.SelectObstacle, check => { Options.SelectObstacle = check; });

            AddTextInput(_selectorMenu.transform, "Select Time Start", "Time", new Vector2(-25, -60),
                Options.TimeStart.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res)) Options.TimeStart = res;
                });
            AddTextInput(_selectorMenu.transform, "Select Time End", "", new Vector2(35, -60),
                Options.TimeEnd.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res)) Options.TimeEnd = res;
                });

            AddLabel(_selectorMenu.transform, "Grid Label", "Grid", new Vector2(55, -85));
            AddTextInput(_selectorMenu.transform, "Input Grid Color", "Color", new Vector2(65, -100),
                Options.GridColor.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.GridColor = res;
                });
            AddTextInput(_selectorMenu.transform, "Input Grid Direction", "Direction", new Vector2(65, -120),
                Options.GridDirection.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.GridDirection = res;
                });
            AddTextInput(_selectorMenu.transform, "Input Grid X", "X", new Vector2(65, -140),
                Options.GridX.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.GridX = res;
                });
            AddTextInput(_selectorMenu.transform, "Input Grid Y", "Y", new Vector2(65, -160),
                Options.GridY.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.GridY = res;
                });
            AddCheckbox(_selectorMenu.transform, "Select Grid Color", "", new Vector2(170, -105),
                Options.GridColorSelect, check => { Options.GridColorSelect = check; });
            AddCheckbox(_selectorMenu.transform, "Select Grid Direction", "", new Vector2(170, -125),
                Options.GridDirectionSelect, check => { Options.GridDirectionSelect = check; });
            AddCheckbox(_selectorMenu.transform, "Select Grid X", "", new Vector2(170, -145),
                Options.GridXSelect, check => { Options.GridXSelect = check; });
            AddCheckbox(_selectorMenu.transform, "Select Grid Y", "", new Vector2(170, -165),
                Options.GridYSelect, check => { Options.GridYSelect = check; });

            AddLabel(_selectorMenu.transform, "Event Label", "Event", new Vector2(-55, -85));
            AddTextInput(_selectorMenu.transform, "Input Event Type", "Type", new Vector2(-45, -100),
                Options.EventType.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.EventType = res;
                });
            AddTextInput(_selectorMenu.transform, "Input Event Value", "Value", new Vector2(-45, -120),
                Options.EventValue.ToString(), value =>
                {
                    if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out var res))
                        Options.EventValue = res;
                });
            AddTextInput(_selectorMenu.transform, "Input Event Float Value", "Float Value", new Vector2(-45, -140),
                Options.EventFloatValue.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res))
                        Options.EventFloatValue = res;
                });
            AddTextInput(_selectorMenu.transform, "Input Event Float Value Tolerance", "Float Value Tolerance",
                new Vector2(-45, -160),
                Options.EventFloatValueTolerance.ToString(CultureInfo.InvariantCulture), value =>
                {
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat,
                            out var res))
                        Options.EventFloatValueTolerance = res;
                });
            AddCheckbox(_selectorMenu.transform, "Select Event Type", "", new Vector2(60, -105),
                Options.EventTypeSelect, check => { Options.EventTypeSelect = check; });
            AddCheckbox(_selectorMenu.transform, "Select Event Value", "", new Vector2(60, -125),
                Options.EventValueSelect, check => { Options.EventValueSelect = check; });
            AddCheckbox(_selectorMenu.transform, "Select Event Float Value", "", new Vector2(60, -145),
                Options.EventFloatValueSelect, check => { Options.EventFloatValueSelect = check; });

            AddButton(_selectorMenu.transform, "Perform Select", "Select", new Vector2(-105, -15),
                () => { _selector.Select(); });
            AddButton(_selectorMenu.transform, "Perform Deselect", "Deselect", new Vector2(-105, -40),
                () => { _selector.Deselect(); });
            AddButton(_selectorMenu.transform, "Perform Select All", "Select All", new Vector2(-105, -65),
                () => { _selector.SelectAll(); });
            AddButton(_selectorMenu.transform, "Perform Deselect All", "Deselect All", new Vector2(-105, -90),
                () => { _selector.DeselectAll(); });

            _selectorMenu.SetActive(false);
            _extensionBtn.Click = () => { _selectorMenu.SetActive(!_selectorMenu.activeSelf); };
        }

        // i ended up copying Top_Cat's CM-JS UI helper, too useful to make my own tho
        // after askin TC if it's one of the only way, he let me use this
        private void AddButton(Transform parent, string title, string text, Vector2 pos, UnityAction onClick)
        {
            var button = Object.Instantiate(PersistentUI.Instance.ButtonPrefab, parent);
            MoveTransform(button.transform, 60, 25, 0.5f, 1, pos.x, pos.y);

            button.name = title;
            button.Button.onClick.AddListener(onClick);

            button.SetText(text);
            button.Text.enableAutoSizing = false;
            button.Text.fontSize = 12;
        }

        private void AddLabel(Transform parent, string title, string text, Vector2 pos, Vector2? size = null)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = (RectTransform)entryLabel.transform;
            rectTransform.SetParent(parent);

            MoveTransform(rectTransform, 110, 24, 0.5f, 1, pos.x, pos.y);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Center;
            textComponent.fontSize = 16;
            textComponent.text = text;
        }

        private void AddTextInput(Transform parent, string title, string text, Vector2 pos, string value,
            UnityAction<string> onChange)
        {
            if (!IsNullOrEmpty(text))
            {
                var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
                var rectTransform = (RectTransform)entryLabel.transform;
                rectTransform.SetParent(parent);

                MoveTransform(rectTransform, 50, 16, 0.5f, 1, pos.x - 27.5f, pos.y);
                var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

                textComponent.name = title;
                textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
                textComponent.alignment = TextAlignmentOptions.Right;
                textComponent.fontSize = 12;
                textComponent.text = text;
            }

            var textInput = Object.Instantiate(PersistentUI.Instance.TextInputPrefab, parent);
            MoveTransform(textInput.transform, 55, 20, 0.5f, 1, pos.x + 27.5f, pos.y);
            textInput.GetComponent<Image>().pixelsPerUnitMultiplier = 3;
            textInput.InputField.text = value;
            textInput.InputField.onFocusSelectAll = false;
            textInput.InputField.textComponent.alignment = TextAlignmentOptions.Left;
            textInput.InputField.textComponent.fontSize = 10;

            textInput.InputField.onValueChanged.AddListener(onChange);
        }

        private void AddCheckbox(Transform parent, string title, string text, Vector2 pos, bool value,
            UnityAction<bool> onClick)
        {
            if (!IsNullOrEmpty(text))
            {
                var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
                var rectTransform = (RectTransform)entryLabel.transform;
                rectTransform.SetParent(parent);
                MoveTransform(rectTransform, 80, 16, 0.5f, 1, pos.x + 10, pos.y + 5);
                var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

                textComponent.name = title;
                textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
                textComponent.alignment = TextAlignmentOptions.Left;
                textComponent.fontSize = 12;
                textComponent.text = text;
            }

            var original = GameObject.Find("Strobe Generator").GetComponentInChildren<Toggle>(true);
            var toggleObject = Object.Instantiate(original, parent.transform);
            MoveTransform(toggleObject.transform, 100, 25, 0.5f, 1, pos.x, pos.y);

            var toggleComponent = toggleObject.GetComponent<Toggle>();
            var colorBlock = toggleComponent.colors;
            colorBlock.normalColor = Color.white;
            toggleComponent.colors = colorBlock;
            toggleComponent.isOn = value;

            toggleComponent.onValueChanged.AddListener(onClick);
        }

        private RectTransform AttachTransform(GameObject obj, float sizeX, float sizeY, float anchorX, float anchorY,
            float anchorPosX, float anchorPosY, float pivotX = 0.5f, float pivotY = 0.5f)
        {
            var rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector3(anchorPosX, anchorPosY, 0);

            return rectTransform;
        }

        private void MoveTransform(Transform transform, float sizeX, float sizeY, float anchorX, float anchorY,
            float anchorPosX, float anchorPosY, float pivotX = 0.5f, float pivotY = 0.5f)
        {
            if (!(transform is RectTransform rectTransform)) return;

            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector3(anchorPosX, anchorPosY, 0);
        }
    }
}