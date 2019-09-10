using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Components.Contracts;
using WindowsFormsApp1.Configs;

namespace WindowsFormsApp1.Components
{
    public partial class DateOfBirthComponent : Component, ICustomComponent
    {
        private Label _label;
        private DateTimePicker _dateTimePicker;

        public DateOfBirthComponent(DateTime data)
        {
            InitializeComponent();
            InitComponent(data, "Date of Birth:");
        }

        public Size Size => new Size(Math.Max(_dateTimePicker.Size.Width, _label.Size.Width), _dateTimePicker.Height + _label.Height);

        public Point Location
        {
            get => _label.Location;
            set
            {
                _label.Location = value;
                _dateTimePicker.Location = new Point(_label.Location.X, _label.Location.Y + _label.Height);
            }
        }

        public IEnumerable<Control> Controls
        {
            get
            {
                yield return _label;
                yield return _dateTimePicker;
            }
        }

        public DateTime Data => DateTime.Parse(_dateTimePicker.Text);

        public bool IsValid => true;

        private void InitComponent(DateTime data, string label)
        {
            _label = new Label()
            {
                Text = label,
                Size = GeneralConfiguration.LabelSize,
            };

            _dateTimePicker = new DateTimePicker()
            {
                Size = GeneralConfiguration.TextBoxSize,
                Text = data.ToShortDateString(),
                BackColor = Color.Azure,
                MaxDate = DateTime.Today
            };
        }
    }
}
