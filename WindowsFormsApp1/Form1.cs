using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Components;
using WindowsFormsApp1.Configs;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly UserInfoReadService _userInfoReadService;
        private readonly UserInfoWriteService _userInfoWriteService;

        private UsersInfoComponent _usersInfoComponent;

        public Form1()
        {
            _userInfoWriteService = new UserInfoWriteService();
            _userInfoReadService = new UserInfoReadService();

            InitializeComponent();
        }

        private int FormWidth => this.Width - 25;
        private int FormHeight => this.Height - 50;

        private void InitComponents()
        {
            this.SuspendLayout();

            _usersInfoComponent = new UsersInfoComponent(
                readUsersFromFile: _userInfoReadService.ReadUsersInfo,
                saveUsersToFile: _userInfoWriteService.WriteUsersInfo);
            _usersInfoComponent.Resize(FormWidth, FormHeight);

            this.Controls.AddRange(_usersInfoComponent.Controls.ToArray());
            this.ResumeLayout(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitComponents();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _usersInfoComponent?.Resize(FormWidth, FormHeight);
        }
    }
}
