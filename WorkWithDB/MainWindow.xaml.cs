using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Reflection;

namespace WorkWithDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WorkDBContext db;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDB();
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            db.Dispose();
        }

        private void LoadDB()
        {
            if (db != null)
                db.Dispose();
            db = new WorkDBContext();
            db.Company.Load();
            foreach (var d in db.Company.Local)
            {
                d.CompanyName = d.CompanyName.TrimEnd();
                d.ContractStat.StatName = d.ContractStat.StatName.TrimEnd();
            }
            companysTable.ItemsSource = db.Company.Local;

            db.ContractStat.Load();
            foreach (var s in db.ContractStat.Local)
                s.StatName = s.StatName.TrimEnd();
            comboBoxContStat.ItemsSource = db.ContractStat.Local;

            db.User.Load();
            foreach (var u in db.User)
            {
                u.UserName = u.UserName.TrimEnd();
                u.Login = u.Login.TrimEnd();
                u.Password = u.Password.TrimEnd();
                u.Company.CompanyName = u.Company.CompanyName.TrimEnd();
            }
            usersTable.ItemsSource = db.User.Local;

            comboBoxCompanys.ItemsSource = db.Company.Local;
        }

        private void MenuItem_Click_Load(object sender, RoutedEventArgs e)
        {
            LoadDB();
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            IEnumerable<DbEntityValidationResult> validError = db.GetValidationErrors();
            if (validError.Count<DbEntityValidationResult>() != 0)
                MessageBox.Show("Присутвую некоректные данные.");
            else
                db.SaveChanges();
        }

        private void MenuItem_Click_DelUser(object sender, RoutedEventArgs e)
        {
            if (usersTable.SelectedItems.Count > 0)
            {
                List<User> userList = new List<User>();
                foreach (var item in usersTable.SelectedItems)
                    if (item is User)
                        userList.Add((User)item);
                foreach (var user in userList)
                    db.User.Local.Remove(user);
            }
        }

        private void MenuItem_Click_DelCompany(object sender, RoutedEventArgs e)
        {
            if (companysTable.SelectedItems.Count > 0)
            {
                List<Company> compList = new List<Company>();
                foreach (var item in companysTable.SelectedItems)
                    if (item is Company)
                        compList.Add((Company)item);
                foreach (var comp in compList)
                {
                    if (comp.User == null || comp.User.Count == 0)
                        db.Company.Local.Remove(comp);
                    else
                        MessageBox.Show("Нельзя удадить компанию с клиентами.");
                }

            }
        }

        private void MenuItem_Click_AddUser(object sender, RoutedEventArgs e)
        {
            db.User.Local.Add(new User());
        }

        private void MenuItem_Click_AddCompany(object sender, RoutedEventArgs e)
        {
            db.Company.Local.Add(new Company());
        }
    }

    public class ContractTableValidate : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Company comp = (value as BindingGroup).Items[0] as Company;
            if (comp.CompanyName == null || comp.CompanyName == "")
                return new ValidationResult(false, "Отсутвует имя компании");
            return ValidationResult.ValidResult;
        }
    }

    public partial class DataGrid : System.Windows.Controls.DataGrid
    {
        /// <summary>
        /// This method overrides the 
        /// if (canExecute && HasRowValidationError) condition of the base method to allow
        /// ----entering edit mode when there is a pending validation error
        /// ---editing of other rows
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCanExecuteBeginEdit(System.Windows.Input.CanExecuteRoutedEventArgs e)
        {

            bool hasCellValidationError = false;
            bool hasRowValidationError = false;
            BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Instance;
            //Current cell
            PropertyInfo cellErrorInfo = this.GetType().BaseType.GetProperty("HasCellValidationError", bindingFlags);
            //Grid level
            PropertyInfo rowErrorInfo = this.GetType().BaseType.GetProperty("HasRowValidationError", bindingFlags);

            if (cellErrorInfo != null) hasCellValidationError = (bool)cellErrorInfo.GetValue(this, null);
            if (rowErrorInfo != null) hasRowValidationError = (bool)rowErrorInfo.GetValue(this, null);

            base.OnCanExecuteBeginEdit(e);
            if (!e.CanExecute && !hasCellValidationError && hasRowValidationError)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }
    }
}
