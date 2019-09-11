using System;
using System.Drawing;
using System.IO;

namespace WindowsFormsApp1.Configs
{
    class GeneralConfiguration
    {
        private const string FilePath = "users.txt";
        public static string UsersDataFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);
        public static char RowSeparator => '\\';
        public static char PropertySeparator => '/';

        public const int ButtonHeight = 25;
        public const int ButtonMarginRight = 5;
        public const int UserInfoMarginBottom = 5;
        public const int LabelWidth = 150;
        public const int LabelHeight = 16;
        public const int TextBoxWidth = 150;
        public const int TextBoxHeight = 22;
        public const int MarginTopForEditors = 5;
        public const int MarginRightForEditors = 5;
        public static Size LabelSize => new Size(LabelWidth, LabelHeight);
        public static Size TextBoxSize => new Size(TextBoxWidth, TextBoxHeight);
    }
}
