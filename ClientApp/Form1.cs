using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private DataTable dataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDataTable();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var client = new ServiceReference1.UserServiceClient();
            var users = client.GetAllUsers();
            UpdateDataTable(users);
        }

        private void UpdateDataTable(IEnumerable<ServiceReference1.User> users)
        {
            dataTable.Clear();

            foreach (var user in users)
            {
                dataTable.Rows.Add(user.Id, user.FirstName, user.LastName, user.Patronymic, user.IdentificationNumber, user.Email, user.ContactPhone, user.CreationDate, user.LastModifiedDate);
            }

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void InitDataTable()
        {
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("Patronymic", typeof(string));
            dataTable.Columns.Add("IdentificationNumber", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("ContactPhone", typeof(string));
            dataTable.Columns.Add("CreationDate", typeof(DateTime));
            dataTable.Columns.Add("LastModifiedDate", typeof(DateTime));
        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddOrEditUserForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUserInfo();
                }
            }
        }

        private void UpdateUser_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var user = new ClientApp.User
                {
                    Id = (int)selectedRow.Cells["Id"].Value,
                    FirstName = (string)selectedRow.Cells["FirstName"].Value,
                    LastName = (string)selectedRow.Cells["LastName"].Value,
                    Patronymic = (string)selectedRow.Cells["Patronymic"].Value,
                    IdentificationNumber = (string)selectedRow.Cells["IdentificationNumber"].Value,
                    Email = (string)selectedRow.Cells["Email"].Value,
                    ContactPhone = (string)selectedRow.Cells["ContactPhone"].Value,
                    CreationDate = (DateTime)selectedRow.Cells["CreationDate"].Value,
                    LastModifiedDate = (DateTime)selectedRow.Cells["LastModifiedDate"].Value
                };

                using (var editForm = new AddOrEditUserForm(user))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadUserInfo();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a row.");
            }
        }
    }
}
