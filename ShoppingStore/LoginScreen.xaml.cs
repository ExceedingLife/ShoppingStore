using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using ShoppingStore.DataAccess;
using ShoppingStore.DataBase;
using System.ComponentModel;

namespace ShoppingStore
{
    public partial class LoginScreen : Window
    {
        string username = "";//txtUsername.Text;
        string password = "";// txtPassword.Password;
        private User loginUser = new User();
        BackgroundWorker backgroundWorker = null;

        public LoginScreen()
        {
            InitializeComponent();
            txtUsername.Text = "Username";
            txtPassword.Password = "Password";

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);            
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void BtnLoginUser_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            username = txtUsername.Text;
            password = txtPassword.Password;

            if (string.IsNullOrEmpty(txtUsername.Text) || username.Equals("Username"))
            {   //verify username isn't empty or default
                MessageBox.Show("Enter your username.", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                txtUsername.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtPassword.Password) || password.Equals("Password"))
            {   //verify password isn't empty or default
                MessageBox.Show("Enter your password.", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPassword.Focus();
                return;
            }
            else
            {
                if (username != "" || username != "Username")
                {
                    valid = true;
                }
                else if (password != "" || password != "Password")
                {
                    valid = true;
                }
                else
                    lblInvalidText.Visibility = Visibility.Visible;

                if (valid)
                {
                    // Disable login and make create button a cancel.
                    BtnLoginUser.IsEnabled = false;
                    BtnCreateUser.Content = "Cancel";
                    // Start the background worker.
                    backgroundWorker.RunWorkerAsync();
                }
            }
        }


        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (BtnCreateUser.Content.Equals("Cancel"))
            {
                if (backgroundWorker.IsBusy)
                {
                    backgroundWorker.CancelAsync();
                }
            }
            else
            {
                Window showCustAdd = new CustomerAdd();
                showCustAdd.Show();
                this.Close();
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {   //Sometimes loading happens so fast so introduce 5 sec thread.sleep()
            for(int i = 1; i <= 10; i++)
            {
                System.Threading.Thread.Sleep(500);
                backgroundWorker.ReportProgress(i);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {   //Start the .Gif (Play and make visible)    free to interact with UI in progressChanged. 
            LoadSpinner.Play();
            LoadSpinner.Visibility = Visibility.Visible;
            try
            {
                loginUser = SQLuserAccess.UserLogin(username, password);
                if (loginUser != null)
                {
                    if (username == loginUser.Username && password == loginUser.Password)
                    { }
                    else
                        lblInvalidText.Visibility = Visibility.Visible;
                }
                else
                    lblInvalidText.Visibility = Visibility.Visible;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Stop the loading  RUNS when thread is COMPLETED can work on UI
            BtnCreateUser.Content = "Register";
            BtnLoginUser.IsEnabled = true;
            LoadSpinner.Visibility = Visibility.Hidden;
            LoadSpinner.Stop();
            LoadSpinner.Close();
            if (loginUser != null)
            {
                if (loginUser.IsAdmin)
                {
                    Window WindowAdminMenu = new AdminWindow(loginUser);
                    WindowAdminMenu.Show();
                    Close();
                }
                else if (loginUser.IsCustomer)
                {
                    Window WindowCustomerMenu = new CustomerScreen(loginUser);
                    WindowCustomerMenu.Show();
                    Close();
                }
                else
                    lblInvalidText.Content = "Invalid Account Information";
            }
            else
                lblInvalidText.Visibility = Visibility.Visible;
        }



        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text.Equals("Username"))
               txtUsername.Text = "";
        }
        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
                txtUsername.Text = "Username";
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = "";
        }
        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
                txtPassword.Password = "Password";
        }

        private void LoadSpinner_MediaEnded(object sender, RoutedEventArgs e)
        {
            LoadSpinner.Position = new TimeSpan(0, 0, 1);
            LoadSpinner.Play();
        }
    }
}
/***** COMMENTED CODE LOGINING BEFORE MULTI-THREADED BACKGROUNDWORKER 
 * 
 * 
        private void BtnLoginUser_Click(object sender, RoutedEventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Password;
            try
            {
                loginUser = SQLuserAccess.UserLogin(txtUsername.Text, txtPassword.Password.ToString());
                if (loginUser != null)
                {
                    if (txtUsername.Text == loginUser.Username && txtPassword.Password == loginUser.Password)
                    {
                        if (loginUser.IsAdmin == true)
                        {
                            Window showAdminSrc = new AdminWindow(loginUser);
                            showAdminSrc.Show();
                            Close();
                        }
                        else if (loginUser.IsAdmin == false)
                        {
                            Window nonAdmin = new CustomerScreen(loginUser);
                            nonAdmin.Show();
                            Close();
                        }
                        else
                            lblInvalidText.Content = "Admin is not True or False!";
                    }
                    else
                        lblInvalidText.Visibility = Visibility.Visible;
                }
                else
                    lblInvalidText.Visibility = Visibility.Visible;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
*/
