using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;

namespace prodajaPO
{
    public partial class Fzak : Form
    {
        public Fzak()
        {
            InitializeComponent();
             SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            //имя сервера
            string ServerName = csBuilder.DataSource;
            //имя базы данных
            string DBName = csBuilder.InitialCatalog;
            //строка подключения
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            conn2(ConnectionString, select_tov, comboBox1, "Наименование", "Номер");
        }
        public string ConnectionString = "";
        private void conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            //создание объекта DataSet (набор данных)
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            // привязка ComboBox к таблице БД
            CB.DataSource = ds.Tables["Table"];
            CB.DisplayMember = field1; //установка отображаемого в списке поля
            CB.ValueMember = field2; //установка ключевого поля
        }

        const string select_tov = "SELECT Ntov as Номер, NamePO as Наименование FROM TOVAR";
        private static void ponam(KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b') return;
            else
            {
                MessageBox.Show("Введите число",
                       "Сообщение");
                e.Handled = true;
            }
        }
        private static void pocha(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
            {
                MessageBox.Show("Введите букву",
                       "Сообщение");
                e.Handled = true;
            }

        }

            private void button2_Click(object sender, EventArgs e)
    {
            // Создадим новое подключение, в качестве параметра укажем строку подключения //ConnectionString.
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn1.Open();
            //создаем новый экземпляр SQLCommand
            SqlCommand cmd = conn1.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[oformzak]";
            //создаем параметр
            cmd.Parameters.Add("@NAMEpok", SqlDbType.VarChar, 50);
            cmd.Parameters["@NAMEpok"].Value = nampok.Text;
            cmd.Parameters.Add("@Dzak", SqlDbType.Date, 70);
            cmd.Parameters["@Dzak"].Value = dtp1.Value;
            cmd.Parameters.Add("@prof", SqlDbType.VarChar, 50);
            cmd.Parameters["@prof"].Value = prof.Text;
            cmd.Parameters.Add("@tel", SqlDbType.VarChar, 50);
            cmd.Parameters["@tel"].Value = tel.Text;
            cmd.Parameters.Add("@adresdost", SqlDbType.VarChar, 50);
            cmd.Parameters["@adresdost"].Value = adrd.Text;
            cmd.Parameters.Add("@sposopl", SqlDbType.VarChar, 50);
            cmd.Parameters["@sposopl"].Value = sposopl.Text;
            cmd.Parameters.Add("@kookop", SqlDbType.Int, 10);
            cmd.Parameters["@kookop"].Value = kolkop.Text;
            cmd.Parameters.Add("@Ntov", SqlDbType.Int, 4);
            cmd.Parameters["@Ntov"].Value = comboBox1.SelectedValue;
            cmd.ExecuteScalar();
            MessageBox.Show("Заказ оформлен, пожалуйста подтвердите оплату","ПродажаПО");
            Fstat frm = new Fstat();
            frm.ShowDialog();

        }

        private void nampok_KeyPress(object sender, KeyPressEventArgs e)
        {
            pocha(e);
        }

        private void prof_KeyPress(object sender, KeyPressEventArgs e)
        {
            pocha(e);
        }

        private void sposopl_KeyPress(object sender, KeyPressEventArgs e)
        {
            pocha(e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Fstat frm = new Fstat();
            frm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nampok.Text = string.Empty;
            prof.Text = string.Empty;
            tel.Text = string.Empty;
            adrd.Text = string.Empty;
            sposopl.Text = string.Empty;
            kolkop.Text = string.Empty;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            otpravka  frm = new otpravka();
            frm.ShowDialog();
        }
    }
}

