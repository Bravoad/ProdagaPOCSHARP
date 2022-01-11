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
    public partial class Fstat : Form
    {
        public Fstat()
        {
            InitializeComponent();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            //имя сервера
            string ServerName = csBuilder.DataSource;
            //имя базы данных
            string DBName = csBuilder.InitialCatalog;
            //строка подключения
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            conn(ConnectionString, select_zak, dgv1);

        }
        public static Fstat SelfRef
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
        public string select_zak = "SELECT Nzak as [Номер заказа],Ntov as [Номер товара],dzak as [Дата заказа],Namepok as [Наименование покупателя],Prof as [Профессия],Tel as [Телефон],Kookop as [Количество копий],AdresDost as [Адрес доставки],Dopl as [Дата оплаты],Ddost as [Дата доставки],Stat as [Статус],sposopl as [Способ оплаты] FROM  zakaz";
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
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[opkach]";
            //создаем параметр
            cmd.Parameters.Add("@Date1", SqlDbType.Date, 150);
            //задаем значение параметра
            cmd.Parameters["@Date1"].Value = dateTimePicker1.Value;
            cmd.Parameters.Add("@zak", SqlDbType.Int, 4);
            //определяем ID книговыдачи
            string zak = dgv1[0, dgv1.CurrentRow.Index].Value.ToString();
            //задаем значение параметра
            cmd.Parameters["@zak"].Value = zak;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Изменения внесены.", "Добавление записей");
            conn(ConnectionString, select_zak, dgv1);
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
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[dostav]";
            //создаем параметр
            cmd.Parameters.Add("@Date", SqlDbType.Date, 150);
            //задаем значение параметра
            cmd.Parameters["@Date"].Value = dateTimePicker1.Value;
            cmd.Parameters.Add("@zak", SqlDbType.Int, 4);
            //определяем ID книговыдачи
            string zak = dgv1[0, dgv1.CurrentRow.Index].Value.ToString();
            //задаем значение параметра
            cmd.Parameters["@zak"].Value = zak;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Изменения внесены.", "Добавление записей");
            conn(ConnectionString, select_zak, dgv1);
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.ExecuteNonQuery();
        }
        private void dgv1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grid = (sender as DataGridView);
            if ((e.RowIndex > -1) && (e.RowIndex < grid.RowCount - 1))
            {
                // раскрасить в зависимости от значения определенного поля
                if (dgv1.Rows[e.RowIndex].Cells[10].Value.ToString() == "0")
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                if (dgv1.Rows[e.RowIndex].Cells[10].Value.ToString() == "1")
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                if (dgv1.Rows[e.RowIndex].Cells[10].Value.ToString() == "2")
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }
    }
}

