using System;
using System.Collections.Generic;
using System.Data.Odbc;
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

namespace LogOn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void RunQuery(ODBC odbc, string _sqlQ)
        {
            string ConectStreng;
            ConectStreng = "DSN=BZDSNT;UID=Z6SGJ;Pwd=xxxxxx";

            using (var conn = new OdbcConnection(ConectStreng))
            {
                try
                {
                    OdbcCommand cmd = new OdbcCommand(_sqlQ, conn);
                    cmd.CommandTimeout = 1000 * 60; // 5 minutter
                    conn.Open();
                    var schema = conn.GetSchema();
                    var datasource = conn.DataSource;
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //SqlResult res = new SqlResult();
                            //result.Add(res);
                            //Kolloner  col = new Kolloner();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string value;
                                value = reader.GetValue(i).ToString();

                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    if (ex.Message.Contains("USERNAME AND"))
                    {
                        MessageBox.Show("Formentlig password fejl. Der indhentes nyt ved næste SQL opslag." + "\n" + ex.Message);
                    }
                    else
                        MessageBox.Show(ex.Message);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    conn.Close();
                }

            }

        }

        private void brtStart_Click_1(object sender, RoutedEventArgs e)
        {
            string sqlQ = "SELECT ID_ORG_ADM_ENHED"
                        + " FROM UDV1.SS61000T"
                        + " FETCH FIRST 10 ROWS ONLY";
            var _Odbc = new ODBC() { OdbcNavn = "BZDSNT", };
            RunQuery(_Odbc, sqlQ);
        }
    }
}
