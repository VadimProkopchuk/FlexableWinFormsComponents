using System.Text.RegularExpressions;

namespace WindowsFormsApp1.Components
{
    public partial class EmailComponent : TextComponent
    {
        private readonly Regex _emailValidationRegex =
            new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);

        public EmailComponent(string email = null) : base(email, "Email:")
        {
            InitializeComponent();
            DataValidator = _emailValidationRegex.IsMatch;
        }
    }
}
