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
    public partial class otpravka : Form
    {
        public otpravka()
        {
            InitializeComponent();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString);
            //имя сервера
            string ServerName = csBuilder.DataSource;
            //имя базы данных
            string DBName = csBuilder.InitialCatalog;
            //строка подключения
            ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Integrated Security=True";
            conn(ConnectionString,otpravkapo, dgv1);
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
        const string otpravkapo = "select zakaz.Nzak as [номер счета-фактуры],zakaz.Dopl as [Дата],TOVAR.NamePO as [ Наименование ПО],TIPLISE.Tiplis as [Тип лицензии], zakaz.Namepok as [Наименование покупателя],zakaz.AdresDost as  АдресДоставки ,zakaz.sposopl as [Способ оплаты],(zakaz.kookop* tovar.Stomost) as  сумма from zakaz,TIPLISE,TIPPO,TOVAR,DOGOVOR where  zakaz.Ntov=TOVAR.Ntov AND TOVAR.Ntiplis=TIPLISE.Ntiplis AND TOVAR.NtipPO=TIPPO.NtipPO AND DOGOVOR.Ndog=TOVAR.Ndog AND zakaz.Dopl is not null and zakaz.Ddost is null AND Stat = 1";

    private void Form5_Load(object sender, EventArgs e)
        {
            
        }
    }
}
