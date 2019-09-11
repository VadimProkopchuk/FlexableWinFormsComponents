using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using WindowsFormsApp1.Configs;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    class UserInfoReadService
    {
        public List<UserInfo> ReadUsersInfo()
        {
            var usersInfo = default(List<UserInfo>);

            if (File.Exists(GeneralConfiguration.UsersDataFilePath))
            {
                var content = File.ReadAllText(GeneralConfiguration.UsersDataFilePath, Encoding.UTF8);
                var usersString = content.Split(new[] { GeneralConfiguration.RowSeparator },
                    StringSplitOptions.RemoveEmptyEntries);

                if (usersString?.Any() == true)
                {
                    usersInfo = usersString.Select(Parse).ToList();
                }
            }

            return usersInfo ?? Enumerable.Empty<UserInfo>().ToList();
        }

        private UserInfo Parse(string str)
        {
            var props = str?.Split(new[] {GeneralConfiguration.PropertySeparator},
                StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            var id = Guid.TryParse(GetElementOrDefault(props, 10), out var userId) ? userId : Guid.NewGuid();
            var day = int.TryParse(GetElementOrDefault(props, 7), out var dobDay) ? dobDay : 0;
            var month = int.TryParse(GetElementOrDefault(props, 8), out var dobMonth) ? dobMonth : 0;
            var year = int.TryParse(GetElementOrDefault(props, 9), out var dobYear) ? dobYear : 0;
            var user = new UserInfo(id)
            {
                Email = GetElementOrDefault(props, 0),
                FacultyName = GetElementOrDefault(props, 1),
                FirstName = GetElementOrDefault(props, 2),
                GroupNumber = GetElementOrDefault(props, 3),
                LastName = GetElementOrDefault(props, 4),
                MiddleName = GetElementOrDefault(props, 5),
                Phone = GetElementOrDefault(props, 6),
                DobDay = day,
                DobMonth = month,
                DobYear = year,
            };

            return user;
        }

        private string GetElementOrDefault(string[] props, int index)
        {
            if (props != null && props.Length > index)
            {
                return props[index];
            }

            return String.Empty;
        }

    }
}
