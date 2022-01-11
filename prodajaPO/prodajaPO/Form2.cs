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
    public partial class Fpost : Form
    {
        public Fpost()
        {
            InitializeComponent();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            //имя сервера
            string ServerName = csBuilder.DataSource;
            //имя базы данных
            string DBName = csBuilder.InitialCatalog;
            //строка подключения
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            conn(ConnectionString, select_dog, dgv1);
            conn(ConnectionString, select_tov, dgv2);
            conn2(ConnectionString, select_dog, comboBox1, "Наимменование поставщика", "Номер договора");
            conn2(ConnectionString, select_kat, comboBox2, "Название", "Номер категорий");
           conn2(ConnectionString, select_tp, comboBox3, "Название", "Номер типа ПО");
           conn2(ConnectionString, select_tl, comboBox4, "Название", "Номер типа лицензии");
            SelfRef = this;
        }
        public static Fpost SelfRef
        {
            get;
            set;
        }
        public string ConnectionString = "";
        public void conn(string CS, string cmdT, DataGridView dgv)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }
        public void conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
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
        public string select_dog = "SELECT Ndog as [Номер договора], NamePos as [Наимменование поставщика], vendor as Вендор,BankRekv as [Банковские реквизиты поставщика], Dpodpis as [Дата подписания] FROM Dogovor";
        public string select_tov = "SELECT Ntov as [Номер товара],Ndog as [Номер договора],NamePO as [Наименование ПО],NtipPO as [Номер типа ПО], Nkat as [Номер категорий],"
            + "Plat as [Платформа],Ntiplis as [Номер типа лицензии],Yaz as [Язык],Razrab as [Разработчик],"
            + "Kolvokop as [Количество копий],Stomost [Стоимость 1-ой копии] FROM TOVAR";
        const string select_kat = "SELECT nkat as [Номер категорий], Kat as Название from Kate";
        const string select_tp = "SELECT  ntippo as [Номер типа ПО], tippo as Название from Tippo";
        const string select_tl = "SELECT ntiplis as [Номер типа лицензии], tiplis as Название from Tiplise";


        private void button1_Click(object sender, EventArgs e)
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
            cmd.CommandText = "[oformdog]";
            //создаем параметр
            cmd.Parameters.Add("@NAMEpos", SqlDbType.VarChar, 50);
            cmd.Parameters["@NAMEpos"].Value = nampost.Text;
            cmd.Parameters.Add("@Dpodpis", SqlDbType.Date, 70);
            cmd.Parameters["@Dpodpis"].Value = dtp1.Value;
            cmd.Parameters.Add("@vendor", SqlDbType.VarChar, 50);
            cmd.Parameters["@vendor"].Value = vend.Text;
            cmd.Parameters.Add("@bankrekv", SqlDbType.VarChar, 50);
            cmd.Parameters["@bankrekv"].Value = bank.Text;
            cmd.ExecuteScalar();
            conn2(ConnectionString, select_dog, comboBox1, "Наимменование поставщика", "Номер договора");
            conn(ConnectionString, select_dog, dgv1);
           tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nampost.Text=string.Empty;
            vend.Text = string.Empty;
            bank.Text = string.Empty;
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.SelectedValue = dgv1[0, dgv1.CurrentRow.Index].Value.ToString();
            tabControl1.SelectedTab = tabPage2;
        }

        private void bank_KeyPress(object sender, KeyPressEventArgs e)
        {
            ponam(e);
        }

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
            cmd.CommandText = "[vvodtov]";
            //создаем параметр
            cmd.Parameters.Add("@Ndog", SqlDbType.Int, 4);
            cmd.Parameters["@Ndog"].Value = comboBox1.SelectedValue;
            cmd.Parameters.Add("@nkat", SqlDbType.Int, 4);
            cmd.Parameters["@nkat"].Value = comboBox2.SelectedValue;
            cmd.Parameters.Add("@ntippo", SqlDbType.Int, 4);
            cmd.Parameters["@ntippo"].Value = comboBox3.SelectedValue;
            cmd.Parameters.Add("@ntiplis", SqlDbType.Int, 4);
            cmd.Parameters["@ntiplis"].Value = comboBox4.SelectedValue;
            cmd.Parameters.Add("@namepo", SqlDbType.VarChar, 70);
            cmd.Parameters["@namepo"].Value = nampo.Text;
            cmd.Parameters.Add("@plat", SqlDbType.VarChar, 10);
            cmd.Parameters["@plat"].Value = plat.Text;
            cmd.Parameters.Add("@yaz", SqlDbType.VarChar, 50);
            cmd.Parameters["@yaz"].Value = lang.Text;
            cmd.Parameters.Add("@razrab", SqlDbType.VarChar, 70);
            cmd.Parameters["@razrab"].Value = razrab.Text;
            cmd.Parameters.Add("@kolvokop", SqlDbType.Int,10);
            cmd.Parameters["@kolvokop"].Value = kol.Text;
            cmd.Parameters.Add("@stomost", SqlDbType.Money);
            cmd.Parameters["@stomost"].Value = stom.Text;
            cmd.ExecuteScalar();
            conn(ConnectionString, select_tov, dgv2);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            nampo.Text = string.Empty;
            lang.Text = string.Empty;
            plat.Text = string.Empty;
            razrab.Text = string.Empty;
            stom.Text = string.Empty;
            kol.Text = string.Empty;
        }

        private void kol_KeyPress(object sender, KeyPressEventArgs e)
        {
            ponam(e);
        }

        private void stom_KeyPress(object sender, KeyPressEventArgs e)
        {
            ponam(e);
        }

        private void lang_KeyPress(object sender, KeyPressEventArgs e)
        {
            pocha(e);
        }
    }
}
