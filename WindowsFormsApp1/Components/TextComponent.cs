using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Components.Contracts;
using WindowsFormsApp1.Configs;

namespace WindowsFormsApp1.Components
{
    public partial class TextComponent : Component, ICustomComponent
    {
        private Label _label;
        private TextBox _textBox;
        private readonly string _placeholder;

        public TextComponent(string data, string label, string placeholder = null)
        {
            _placeholder = placeholder;

            InitializeComponent();
            InitComponent(data, label, placeholder);

            ValidateData();
        }

        public Size Size => new Size(Math.Max(_textBox.Size.Width, _label.Size.Width), _textBox.Height + _label.Height);

        public Point Location
        {
            get => _label.Location;
            set
            {
                _label.Location = value;
                _textBox.Location = new Point(_label.Location.X, _label.Location.Y + _label.Height);
            }
        }

        public IEnumerable<Control> Controls
        {
            get
            {
                yield return _label;
                yield return _textBox;
            }
        }

        public string Data => _textBox.Text;

        public bool IsValid => !IsEqualToPlaceholder  && (DataValidator == null || DataValidator(Data));

        public Predicate<string> DataValidator { get; set; }

        private bool IsEqualToPlaceholder => _placeholder != null && Data == _placeholder;

        private void InitComponent(string data, string label, string placeholder)
        {
            _label = new Label()
            {
                Text = label,
                Size = GeneralConfiguration.LabelSize,
            };

            _textBox = new TextBox()
            {
                Size = GeneralConfiguration.TextBoxSize,
                Text = data ?? placeholder,
                BackColor = Color.Azure,

            };
            _textBox.KeyUp += (sender, args) => ValidateData();

            if (!String.IsNullOrEmpty(_placeholder))
            {
                _textBox.GotFocus += GotFocus;
                _textBox.LostFocus += LostFocus;
            }
        }

        private void ValidateData()
        {
            _textBox.BackColor = GetColor();

            Color GetColor()
            {
                if (String.IsNullOrWhiteSpace(Data) || IsEqualToPlaceholder) return Color.Azure;
                if (IsValid) return Color.LightGreen;

                return Color.LightPink;
            }
        }

        private void GotFocus(Object sender, EventArgs args)
        {
            if (_textBox.Text == _placeholder)
            {
                _textBox.Text = String.Empty;
            }
        }

        private void LostFocus(Object sender, EventArgs args)
        {
            if (String.IsNullOrWhiteSpace(_textBox.Text))
            {
                _textBox.Text = _placeholder;
            }
        }
    }
}
