using System.Text.RegularExpressions;

namespace WindowsFormsApp1.Components
{
    public partial class PhoneComponent : TextComponent
    {
        private readonly Regex _phoneValidationRegex = new Regex(@"^\+375[\s]{0,1}\((17|25|29|33|44)\)[\s]{0,1}[0-9]{3}[-]{0,1}[0-9]{2}[-]{0,1}[0-9]{2}$", RegexOptions.IgnoreCase);

        public PhoneComponent(string phone = null) : base(phone, "Mobile Phone:", "+375 (XX) XXX-XX-XX")
        {
            InitializeComponent();
            DataValidator = _phoneValidationRegex.IsMatch;
        }
    }
}
