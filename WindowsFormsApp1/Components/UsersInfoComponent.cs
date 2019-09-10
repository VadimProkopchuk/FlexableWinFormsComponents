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
    public partial class UsersInfoComponent : Component, ICustomComponent
    {
        private readonly List<UserInfoComponent> _components = new List<UserInfoComponent>();
        private readonly Action<List<UserInfo>> _saveUsersToFile;
        private readonly List<UserInfo> _users;
        private readonly PageModel _pageModel = new PageModel();

        private readonly Timer _timer = new Timer { Interval = 500, Enabled = false };
        private Panel _panel;
        private Button _create;
        private Button _left;
        private Button _rigth;
        private Button _save;
        
        public UsersInfoComponent(Func<List<UserInfo>> readUsersFromFile, Action<List<UserInfo>> saveUsersToFile)
        {
            _users = readUsersFromFile();
            _saveUsersToFile = saveUsersToFile;
            InitializeComponent();
            InitComponents();
        }

        public IEnumerable<UserInfo> Data => _components.Select(x => x.Data);

        public void Resize(int width, int height)
        {
            _panel.Size = new Size(width, height);
            _components.ForEach(x => x.Resize(Size.Width));
            _pageModel.CurrentPage = 1;
            Relocate();
        }

        public Size Size => _panel.Size;
        public Point Location { get; set; }
        public IEnumerable<Control> Controls => new[] { _panel };
        public bool IsValid => _components.All(x => x.IsValid);

        private UserInfoComponent CreateUserInfoComponent(UserInfo userInfo)
        {
            var userInfoComponent = new UserInfoComponent(userInfo, Size.Width);
            _panel.Controls.AddRange(userInfoComponent.Controls.ToArray());
            return userInfoComponent;
        }

        private void InitComponents()
        {
            _panel = new Panel { BackColor = Color.Aquamarine };
            _create = CreateButton("Create", 50);
            _left = CreateButton("<", 30);
            _rigth = CreateButton(">", 30);
            _save = CreateButton("Save", 50);
            _panel.Controls.AddRange(new Control[] { _create, _left, _rigth, _save });
            _components.AddRange(_users.Select(CreateUserInfoComponent));

            SubscribeToEvents();

            _timer.Enabled = true;
        }

        private Button CreateButton(string text, int width) => new Button()
        {
            Text = text,
            Size = new Size(width, GeneralConfiguration.ButtonHeight),
            BackColor = Color.White
        };

        private void SubscribeToEvents()
        {
            _timer.Tick += (sender, args) => _save.Enabled = _components.All(x => x.IsValid);

            _create.Click += (sender, args) =>
            {
                var userInfo = new UserInfo();

                _users.Add(userInfo);
                _components.Add(CreateUserInfoComponent(userInfo));
                Relocate();
            };

            _rigth.Click += (sender, args) =>
            {
                _pageModel.CurrentPage++;
                Relocate();
            };

            _left.Click += (sender, args) =>
            {
                _pageModel.CurrentPage--;
                Relocate();
            };

            _save.Click += (sender, args) => SaveUsersToFile();
        }

        private void SaveUsersToFile()
        {
            try
            {
                _saveUsersToFile(Data.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private int GetCountOfComponentsOnPage()
        {
            if (_components.Count == 0)
            {
                return 1;
            }

            var componentHeight = _components[0].Size.Height + GeneralConfiguration.UserInfoMarginBottom;
            var availableHeight = Size.Height - GeneralConfiguration.ButtonHeight;
            var componentsOnPage = availableHeight / componentHeight;

            return componentsOnPage < 1 ? 1 : componentsOnPage;
        }

        private void Relocate()
        {
            RelocateComponents();
            RelocateBtns();
        }

        private void RelocateComponents()
        {
            _pageModel.CountOfComponentOnPage = GetCountOfComponentsOnPage();
            _pageModel.CountOfPages = (int)Math.Ceiling(_components.Count * 1d / _pageModel.CountOfComponentOnPage * 1d);
            _left.Visible = _pageModel.CurrentPage > 1;
            _rigth.Visible = _pageModel.CurrentPage < _pageModel.CountOfPages;
            
            var renderComponents = _components
                .Skip((_pageModel.CurrentPage - 1) * _pageModel.CountOfComponentOnPage)
                .Take(_pageModel.CountOfComponentOnPage)
                .ToArray();

            _panel.Controls.Clear();
            _panel.Controls.AddRange(new Control[] { _create, _left, _rigth, _save });

            for (var i = 0; i < renderComponents.Length; i++)
            {
                var height = i == 0 ? 0
                    : renderComponents[i - 1].Location.Y + renderComponents[i - 1].Size.Height +
                      GeneralConfiguration.UserInfoMarginBottom;

                renderComponents[i].Location = new Point(0, height);
                _panel.Controls.AddRange(renderComponents[i].Controls.ToArray());
            }
        }

        private void RelocateBtns()
        {
            var visibleBtns = new[] {_create, _left, _rigth, _save}.Where(x => x.Visible).ToArray();

            for (var i = 0; i < visibleBtns.Length; i++)
            {
                if (i == 0)
                {
                    visibleBtns[i].Location = new Point(0, Size.Height - GeneralConfiguration.ButtonHeight);
                }
                else
                {
                    var prevVisibleBtn = visibleBtns[i - 1];
                    visibleBtns[i].Location =
                        new Point(prevVisibleBtn.Location.X + prevVisibleBtn.Size.Width +
                            GeneralConfiguration.ButtonMarginRight, Size.Height - GeneralConfiguration.ButtonHeight);
                }
            }

        }
    }
}
