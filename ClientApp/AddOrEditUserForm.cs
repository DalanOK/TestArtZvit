using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class AddOrEditUserForm : Form
    {
        private bool editUserForm = false;
        private User user = new User();
        public AddOrEditUserForm()
        {
            InitializeComponent();
        }
        public AddOrEditUserForm(User userToEditId) : this()
        {
            editUserForm = true;
            user = userToEditId;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            using (var client = new ServiceReference1.UserServiceClient())
            {
                ClientApp.User clientUser = new ClientApp.User();
                ServiceReference1.User serviceUser = Mapper.MapUserToServiceReference1User(clientUser);

                InitializeUser(serviceUser);

                if (editUserForm)
                {
                    serviceUser.Id = user.Id;
                    client.UpdateUser(serviceUser);
                }
                else
                {
                    serviceUser.CreationDate = DateTime.Now;
                    client.AddUser(serviceUser);
                }
            }
            DialogResult = DialogResult.OK;
        }
        private void InitializeUser(ServiceReference1.User user)
        {
            user.FirstName = FirstName.Text;
            user.LastName = LastName.Text;
            user.Patronymic = Patronymic.Text;
            user.IdentificationNumber = IndefNum.Text;
            user.Email = Email.Text;
            user.ContactPhone = Phone.Text;
            user.LastModifiedDate = DateTime.Now;
        }

        private void AddOrEditUserForm_Load(object sender, EventArgs e)
        {
            if (editUserForm)
            {
                LoadUserData(user);
            }
        }
        private void LoadUserData(User userToLoad)
        {
             FirstName.Text = userToLoad.FirstName;
             LastName.Text = userToLoad.LastName;
             Patronymic.Text = userToLoad.Patronymic;
             IndefNum.Text = userToLoad.IdentificationNumber;
             Email.Text = userToLoad.Email;
             Phone.Text = userToLoad.ContactPhone;
        }
        private bool ValidateInput()
        {
            if (!Validation.IsValidName(FirstName.Text))
            {
                MessageBox.Show("Please enter a valid First Name.");
                return false;
            }
            if (!Validation.IsValidName(LastName.Text))
            {
                MessageBox.Show("Please enter a valid Last Name.");
                return false;
            }
            if (!Validation.IsValidName(Patronymic.Text))
            {
                MessageBox.Show("Please enter a valid Patronymic.");
                return false;
            }
            if (!Validation.IsValidIndefNum(IndefNum.Text))
            {
                MessageBox.Show("Please enter a valid Identification Number (10 digits).");
                return false;
            }
            if (!Validation.IsValidEmail(Email.Text))
            {
                MessageBox.Show("Please enter a valid Email.");
                return false;
            }
            if (!Validation.IsValidPhoneNumber(Phone.Text))
            {
                MessageBox.Show("Please enter a valid Contact Phone.");
                return false;
            }

            return true;

        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
