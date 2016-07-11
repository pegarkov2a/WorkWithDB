namespace WorkWithDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company : IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        public int ContractStatId { get; set; }

        public virtual ContractStat ContractStat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "CompanyName":
                        if (CompanyName == null || CompanyName == "")
                            error = "Не задано название компании.";
                        else if (CompanyName.Length > 100)
                            error = "Слишком длинное название компании";
                        break;
                    case "ContractStatId":
                        if (ContractStatId == 0)
                            error = "Не задан стату контракта";
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get
            {
                string error = String.Empty;

                if (CompanyName == null | CompanyName == "")
                    error = "Не задано название компании.";
                else if (CompanyName.Length > 100)
                    error = "Слишком длинное название компании";
                else if (ContractStatId == 0)
                    error = "Не задан статуc контракта";
                return error;
            }
        }
    }
}
