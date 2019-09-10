using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using WindowsFormsApp1.Configs;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    class UserInfoWriteService
    {
        private readonly IFormatter _formatter;

        public UserInfoWriteService(IFormatter formatter)
        {
            _formatter = formatter;
        }

        public void WriteUsersInfo(List<UserInfo> usersInfo)
        {
            if (usersInfo == null) { throw new ArgumentNullException(nameof(usersInfo));}

            using (var outputStream = new FileStream(GeneralConfiguration.UsersDataFilePath, 
                FileMode.OpenOrCreate, FileAccess.Write))
            {
                _formatter.Serialize(outputStream, usersInfo.ToList());
            }
        }
    }
}
