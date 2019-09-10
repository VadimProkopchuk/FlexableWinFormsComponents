using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using WindowsFormsApp1.Configs;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    class UserInfoReadService
    {
        private readonly IFormatter _formatter;

        public UserInfoReadService(IFormatter formatter)
        {
            _formatter = formatter;
        }

        public List<UserInfo> ReadUsersInfo()
        {
            var usersInfo = default(List<UserInfo>);

            if (File.Exists(GeneralConfiguration.UsersDataFilePath))
            {
                try
                {
                    using (var inputStream =
                        new FileStream(GeneralConfiguration.UsersDataFilePath, FileMode.Open, FileAccess.Read))
                    {
                        usersInfo = _formatter.Deserialize(inputStream) as List<UserInfo>;
                    }
                }
                catch (Exception ex)
                {
                    // todo: log ex
                    throw;
                }
            }

            return usersInfo ?? Enumerable.Empty<UserInfo>().ToList();
        }
    }
}
