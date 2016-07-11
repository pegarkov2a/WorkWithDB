namespace WorkWithDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User : IDataErrorInfo
    {
        public int Id { get; set; }
        private readonly int MAX = 100;

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string Error
        {
            get
            {
                string error = "";
                if (UserName == null || UserName == "")
                    error = "Не заполнено поле UserName";
                else if (UserName.Length > MAX)
                    error = "Поле UserName превышает максимальную длинну.";
                else if (Login == null || Login == "")
                    error = "Не заполнено поле Login";
                else if (Login.Length > MAX)
                    error = "Поле Login превышает максимальную длинну.";
                else if (Password == null || Password == "")
                    error = "Не заполнено поле Password";
                else if (Password.Length > MAX)
                    error = "Поле Password превышает максимальную длинну.";
                else if (CompanyId == 0)
                    error = "Не выбрана обслуживающая компания.";
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = "";
                switch (columnName)
                {
                    case "UserName":
                        if (UserName == null || UserName == "")
                            error = "Не заполнено поле UserName";
                        else if (UserName.Length > MAX)
                            error = "Поле UserName превышает максимальную длинну.";
                        break;
                    case "Login":
                        if (Login == null || Login == "")
                            error = "Не заполнено поле Login";
                        else if (Login.Length > MAX)
                            error = "Поле Login превышает максимальную длинну.";
                        break;
                    case "Password":
                        if (Password == null || Password == "")
                            error = "Не заполнено поле Password";
                        else if (Password.Length > MAX)
                            error = "Поле Password превышает максимальную длинну.";
                        break;
                    case "CompanyId":
                        if (CompanyId == 0)
                            error = "Не выбрана обслуживающая компания.";
                        break;
                }
                return error;
            }
        }
    }
}
