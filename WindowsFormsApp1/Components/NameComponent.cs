using System.Text.RegularExpressions;

namespace WindowsFormsApp1.Components
{
    public partial class NameComponent : TextComponent
    {
        private readonly Regex _nameValidationRegex = new Regex(@"^[A-Z\s]*$", RegexOptions.IgnoreCase);

        public NameComponent(string data = null, string label = null) : base(data, label)
        {
            InitializeComponent();
            DataValidator = _nameValidationRegex.IsMatch;
        }
    }
}
