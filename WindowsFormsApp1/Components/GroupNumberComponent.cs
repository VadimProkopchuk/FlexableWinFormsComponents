using System.Text.RegularExpressions;

namespace WindowsFormsApp1.Components
{
    public partial class GroupNumberComponent : TextComponent
    {
        private readonly Regex _groupValidationRegex = new Regex(@"^[0-9]{1,10}$", RegexOptions.IgnoreCase);

        public GroupNumberComponent(string groupNumber = null) : base(groupNumber, "Group Number:")
        {
            InitializeComponent();
            DataValidator = _groupValidationRegex.IsMatch;
        }
    }
}
