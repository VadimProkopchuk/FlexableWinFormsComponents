using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Components.Contracts;
using WindowsFormsApp1.Configs;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Components
{
    public partial class UserInfoComponent : Component, ICustomComponent
    {
        private NameComponent _firstNameComponent;
        private NameComponent _middleNameComponent;
        private NameComponent _lastNameComponent;
        private NameComponent _facultyNameComponent;
        private EmailComponent _emailComponent;
        private GroupNumberComponent _groupNumberComponent;
        private DateOfBirthComponent _dateOfBirthComponent;
        private PhoneComponent _phoneComponent;

        private List<ICustomComponent> _components;
        private Panel _panel;

        public UserInfoComponent(UserInfo userInfo, int width)
        {
            InitializeComponent();
            InitComponents(userInfo, width);
            ReLocateComponents();
        }

        public Size Size => _panel.Size;

        public Point Location
        {
            get => _panel.Location;
            set => _panel.Location = value;
        }

        public IEnumerable<Control> Controls => new [] {_panel};
        public bool IsValid => _components.All(x => x.IsValid);
        public void Resize(int width)
        {
            _panel.Size = new Size(width, _panel.Size.Height);
            ReLocateComponents();
        }

        public UserInfo Data => new UserInfo()
        {
            DobDay = _dateOfBirthComponent.Data.Day,
            DobMonth = _dateOfBirthComponent.Data.Month,
            DobYear = _dateOfBirthComponent.Data.Year,
            Email = _emailComponent.Data,
            LastName = _lastNameComponent.Data,
            GroupNumber = _groupNumberComponent.Data,
            FacultyName = _facultyNameComponent.Data,
            Phone = _phoneComponent.Data,
            FirstName = _firstNameComponent.Data,
            MiddleName = _middleNameComponent.Data
        };

        private void InitComponents(UserInfo userInfo, int width)
        {
            var dateOfBirth = userInfo == null || 
                !DateTime.TryParse($"{userInfo.DobDay}.{userInfo.DobMonth}.{userInfo.DobYear}", out var date)
                    ? DateTime.Today : date;

            _components = new List<ICustomComponent>()
            {
                (_firstNameComponent = new NameComponent(userInfo?.FirstName, "First Name:")),
                (_middleNameComponent = new NameComponent(userInfo?.MiddleName, "Middle Name:")),
                (_lastNameComponent = new NameComponent(userInfo?.LastName, "Last Name:")),
                (_facultyNameComponent = new NameComponent(userInfo?.FacultyName, "Faculty Name:")),
                (_emailComponent = new EmailComponent(userInfo?.Email)),
                (_groupNumberComponent = new GroupNumberComponent(userInfo?.GroupNumber)),
                (_dateOfBirthComponent = new DateOfBirthComponent(dateOfBirth)),
                (_phoneComponent = new PhoneComponent(userInfo?.Phone))
            };

            _panel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(width, 0),
                BackColor = Color.DarkKhaki
            };
            _panel.Controls.AddRange(_components.SelectMany(x => x.Controls).ToArray());
        }

        private void ReLocateComponents()
        {
            var currentHeight = 0;

            for (var i = 0; i < _components.Count; i++)
            {
                if (i == 0)
                {
                    _components[i].Location = new Point(0, 0);
                }
                else
                {
                    var prevComponent = _components[i - 1];
                    var left = prevComponent.Location.X + prevComponent.Size.Width + GeneralConfiguration.MarginRightForEditors;

                    if (left + _components[i].Size.Width > Size.Width)
                    {
                        currentHeight += GeneralConfiguration.MarginTopForEditors + _components[i].Size.Height;
                        _components[i].Location = new Point(0, currentHeight);
                    }
                    else
                    {
                        _components[i].Location = new Point(left, currentHeight);
                    }
                }
            }

            _panel.Size = new Size(Size.Width, currentHeight + _components.Last().Size.Height);
        }
    }
}
