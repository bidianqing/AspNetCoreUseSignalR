﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreUseSignalR.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
