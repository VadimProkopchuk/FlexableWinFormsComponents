using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WindowsFormsApp1.Configs;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    class UserInfoWriteService
    {
        public void WriteUsersInfo(List<UserInfo> usersInfo)
        {
            if (usersInfo == null) { throw new ArgumentNullException(nameof(usersInfo));}

            try
            {
                var content = String.Join(GeneralConfiguration.RowSeparator.ToString(), usersInfo.Select(ToSaveFormat));

                File.WriteAllText(GeneralConfiguration.UsersDataFilePath, content, Encoding.UTF8);
            }
            catch (Exception e)
            {
                // TODO: log
            }
        }

        private string ToSaveFormat(UserInfo userInfo)
        {
            return String.Join(GeneralConfiguration.PropertySeparator.ToString(), new[]
            {
                userInfo.Email,
                userInfo.FacultyName,
                userInfo.FirstName,
                userInfo.GroupNumber,
                userInfo.LastName,
                userInfo.MiddleName,
                userInfo.Phone,
                userInfo.DobDay.ToString(),
                userInfo.DobMonth.ToString(),
                userInfo.DobYear.ToString(),
                userInfo.Number.ToString(),
                userInfo.Id.ToString(),
            });
        }

    }
}
