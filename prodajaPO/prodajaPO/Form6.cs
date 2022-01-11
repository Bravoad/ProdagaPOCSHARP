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
    public partial class otch : Form
    {
        public otch()
        {
            InitializeComponent();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            //имя сервера
            string ServerName = csBuilder.DataSource;
            //имя базы данных
            string DBName = csBuilder.InitialCatalog;
            //строка подключения
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
        }
        public string ConnectionString = "";
        public void conn(string CS, string cmdT, DataGridView dgv)
        {
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "Table");
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }
     

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn1.Open();
            //создаем новый экземпляр SQLCommand
            SqlCommand cmd = conn1.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Knigaprod]";
            cmd.Parameters.Add("@Data1", SqlDbType.Date, 4);
            cmd.Parameters["@Data1"].Value = dateTimePicker1.Value;
            cmd.Parameters.Add("@Data2", SqlDbType.Date, 4);
            cmd.Parameters["@Data2"].Value = dateTimePicker2.Value;
            //создание экземпляра адаптера
            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;
            //создание объекта DataSet (набор данных)
            DataSet dsReport = new DataSet();
            //Заполнение таблицы Instance абора данных DataSet данными из БД
            ReportAdapter.Fill(dsReport, "Knigaprod");
            //Связываем источник данных компонента dataGridView на форме, с таблицей Report объекта dsInstance:
            dgv1.DataSource = dsReport.Tables["Knigaprod"].DefaultView;
        
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn1.Open();
            //создаем новый экземпляр SQLCommand
            SqlCommand cmd = conn1.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Raitprod]";
            cmd.Parameters.Add("@Data1", SqlDbType.Date, 4);
            cmd.Parameters["@Data1"].Value = dateTimePicker1.Value;
            cmd.Parameters.Add("@Data2", SqlDbType.Date, 4);
            cmd.Parameters["@Data2"].Value = dateTimePicker2.Value;
            //создание экземпляра адаптера
            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;
            //создание объекта DataSet (набор данных)
            DataSet dsReport = new DataSet();
            //Заполнение таблицы Instance абора данных DataSet данными из БД
            ReportAdapter.Fill(dsReport, "Raitprod");
            //Связываем источник данных компонента dataGridView на форме, с таблицей Report объекта dsInstance:
            dgv1.DataSource = dsReport.Tables["Raitprod"].DefaultView;

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызывая метод Open объекта
            conn1.Open();
            //создаем новый экземпляр SQLCommand
            SqlCommand cmd = conn1.CreateCommand();
            //определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[RAITKATEPROD]";
            cmd.Parameters.Add("@Data1", SqlDbType.Date, 4);
            cmd.Parameters["@Data1"].Value = dateTimePicker1.Value;
            cmd.Parameters.Add("@Data2", SqlDbType.Date, 4);
            cmd.Parameters["@Data2"].Value = dateTimePicker2.Value;
            //создание экземпляра адаптера
            SqlDataAdapter ReportAdapter = new SqlDataAdapter();
            ReportAdapter.SelectCommand = cmd;
            //создание объекта DataSet (набор данных)
            DataSet dsReport = new DataSet();
            //Заполнение таблицы Instance абора данных DataSet данными из БД
            ReportAdapter.Fill(dsReport, "RAITKATEPROD");
            //Связываем источник данных компонента dataGridView на форме, с таблицей Report объекта dsInstance:
            dgv1.DataSource = dsReport.Tables["RAITKATEPROD"].DefaultView;
        }
    }
}
