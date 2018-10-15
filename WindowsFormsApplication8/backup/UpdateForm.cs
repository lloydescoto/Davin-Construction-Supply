using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication8
{
    public partial class UpdateForm : Form
    {
        MySqlConnection conn;
        MySqlConnection conn2;
        string connectionString = "server=localhost;userid=root;password=;database=hardwaredatabase";
        DateTime dn = DateTime.Now;
        string updateCategory = "";
        public UpdateForm()
        {
            InitializeComponent();
            SetForm();
        }

        public void SetForm()
        {
            dataGridView1.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView3.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView4.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView5.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView5.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView6.Font = new System.Drawing.Font("Arial", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView6.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            dataGridView4.Hide();
            dataGridView6.Hide();
        }


       
 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //ITEMS UPDATE
            if (updateCategory == "UPDATE ITEMS")
            {
                conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(connectionString);
                cmd.Connection = conn;
                MySqlDataReader reader;
                conn.Open();
                string caption = "Invalid Input Detected";

                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    string message = "Item Code must be provided. Try Again!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Clear();
                    }
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM items WHERE ItemCode = @ic";
                    MySqlParameter ic = new MySqlParameter("@ic", MySqlDbType.Int32);
                    ic.Value = int.Parse(textBox1.Text.ToString());
                    cmd.Parameters.Add(ic);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["ItemCode"] == textBox1.Text)
                            {
                                MessageBox.Show("Inputted Item Code does not match any existing item.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cmd.Parameters.Clear();
                                //reader.Close();
                            }

                            else
                            {
                                reader.Close();
                                cmd.CommandText = "UPDATE Items SET ItemSellPrice = @upsellprice , ItemPrice = @upitemprice WHERE ItemCode = @upitemcode";
                                MySqlParameter upItCode = new MySqlParameter("@upitemcode", MySqlDbType.Int32);
                                MySqlParameter upSellP = new MySqlParameter("@upsellprice", MySqlDbType.Decimal);
                                MySqlParameter upItPrice = new MySqlParameter("@upitemprice", MySqlDbType.Decimal);
                                upItCode.Value = int.Parse(textBox1.Text.ToString());
                                upSellP.Value = double.Parse(textBox2.Text.ToString());
                                upItPrice.Value = double.Parse(textBox3.Text.ToString());
                                cmd.Parameters.Add(upItCode);
                                cmd.Parameters.Add(upSellP);
                                cmd.Parameters.Add(upItPrice);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();

                                dataGridView1.Rows.Clear();
                                cmd.CommandText = "SELECT * FROM items WHERE ItemCode = @itemcode";
                                MySqlParameter itemCode = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                                itemCode.Value = int.Parse(textBox1.Text.ToString());
                                cmd.Parameters.Add(itemCode);
                                using (reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {

                                        dataGridView1.Rows.Add(reader["ItemCode"], reader["ItemName"], reader["ItemSellPrice"], reader["ItemPrice"]);
                                    }
                                    reader.Close();

                                }
                            }

                            MessageBox.Show("DATA UPDATED");
                        }
                        reader.Close();
                    }
                }
            }

            //SALES UPDATE
            else if (updateCategory == "UPDATE SALES")
            {
                conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(connectionString);
                cmd.Connection = conn;
                MySqlDataReader reader;
                conn.Open();

                conn2 = new MySqlConnection(connectionString);
                MySqlCommand cmd2 = new MySqlCommand(connectionString);

                cmd2.Connection = conn2;
                conn2.Open();

                cmd.CommandText = "SELECT * FROM sales WHERE TransId = @transid AND ItemCode = @salICode";
                MySqlParameter transId = new MySqlParameter("@transid", MySqlDbType.Int32);
                MySqlParameter salItemCode = new MySqlParameter("@salICode", MySqlDbType.Int32);
                transId.Value = int.Parse(textBox1.Text.ToString());
                salItemCode.Value = int.Parse(textBox2.Text.ToString());
                cmd.Parameters.Add(transId);
                cmd.Parameters.Add(salItemCode);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Globals.quantity = int.Parse(reader["Quantity"].ToString());
                        Globals.iSPrice = double.Parse(reader["ItemSellPrice"].ToString());
                        Globals.salesId = int.Parse(reader["SalesId"].ToString());

                    }
                    reader.Close();
                }


                cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @sitemcode";
                MySqlParameter STICode = new MySqlParameter("@sitemcode", MySqlDbType.Int32);
                STICode.Value = int.Parse(textBox2.Text.ToString());
                cmd.Parameters.Add(STICode);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        Globals.stQuantity = int.Parse(reader["Quantity"].ToString());

                    }
                    reader.Close();
                }

                cmd.CommandText = "INSERT INTO salesreturns(ReturnQuantity, ReturnDate, SalesId) VALUES(@requantity, (SELECT NOW()), @sID)";
                MySqlParameter rquantity = new MySqlParameter("@requantity", MySqlDbType.Int32);
                MySqlParameter rDate = new MySqlParameter("@date", MySqlDbType.DateTime);
                MySqlParameter sID = new MySqlParameter("@sID", MySqlDbType.Int32);
                rquantity.Value = int.Parse(textBox3.Text.ToString());
                rDate.Value = dn.ToString("yyyy-MM-dd HH:mm:ss");
                sID.Value = Globals.salesId;
                cmd.Parameters.Add(rquantity);
                cmd.Parameters.Add(rDate);
                cmd.Parameters.Add(sID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();



                cmd2.CommandText = "UPDATE stocks SET Quantity = @upQuantity WHERE ItemCode = @upItemCode";
                MySqlParameter upQuan = new MySqlParameter("@upQuantity", MySqlDbType.Int32);
                MySqlParameter upItemCode = new MySqlParameter("@upItemCode", MySqlDbType.Int32);
                upQuan.Value = Globals.stQuantity + int.Parse(textBox3.Text.ToString());
                upItemCode.Value = int.Parse(textBox2.Text.ToString());
                cmd2.Parameters.Add(upQuan);
                cmd2.Parameters.Add(upItemCode);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();


                cmd2.CommandText = "UPDATE sales SET Quantity = @salQuantity WHERE ItemCode = @itemCode";
                MySqlParameter salQuantity = new MySqlParameter("@salQuantity", MySqlDbType.Int32);
                MySqlParameter itemCode = new MySqlParameter("@itemCode", MySqlDbType.Int32);
                salQuantity.Value = Globals.quantity - int.Parse(textBox3.Text.ToString());
                itemCode.Value = int.Parse(textBox2.Text.ToString());
                cmd2.Parameters.Add(salQuantity);
                cmd2.Parameters.Add(itemCode);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();



                cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @transacid";
                MySqlParameter transacID = new MySqlParameter("@transacid", MySqlDbType.Decimal);
                transacID.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(transacID);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        Globals.totalAmnt = double.Parse(reader["TotalAmount"].ToString());

                    }
                    reader.Close();
                }

                double newTotAmnt = Globals.iSPrice * int.Parse(textBox3.Text.ToString());
                cmd2.CommandText = "UPDATE transactions SET TotalAmount = @totAmount WHERE TransId = @transId";
                MySqlParameter upTotAmnt = new MySqlParameter("@totAmount", MySqlDbType.Decimal);
                MySqlParameter transID = new MySqlParameter("@transId", MySqlDbType.Int32);
                upTotAmnt.Value = Globals.totalAmnt - newTotAmnt;
                transID.Value = int.Parse(textBox1.Text.ToString());
                cmd2.Parameters.Add(upTotAmnt);
                cmd2.Parameters.Add(transID);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();

                cmd2.CommandText = "UPDATE sales SET Status = @status WHERE TransId = @transacid AND ItemCode = @salICode";
                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Text);
                MySqlParameter transacid = new MySqlParameter("@transacid", MySqlDbType.Int32);
                MySqlParameter salICode = new MySqlParameter("@salICode", MySqlDbType.Int32);
                status.Value = textBox4.Text;
                transacid.Value = int.Parse(textBox1.Text.ToString());
                salICode.Value = int.Parse(textBox2.Text.ToString());
                cmd2.Parameters.Add(status);
                cmd2.Parameters.Add(transacid);
                cmd2.Parameters.Add(salICode);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();

                cmd.CommandText = "SELECT * FROM sales WHERE TransId = @tId AND ItemCode = @itemcode";
                MySqlParameter itemcode = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                MySqlParameter tID = new MySqlParameter("@tId", MySqlDbType.Int32);
                itemcode.Value = int.Parse(textBox2.Text.ToString());
                tID.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(itemcode);
                cmd.Parameters.Add(tID);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dataGridView2.Rows.Clear();
                        dataGridView2.Rows.Add(reader["SalesId"], reader["TransId"], reader["ItemCode"], reader["ItemSellPrice"], reader["Quantity"], reader["Status"]);
                    }
                    reader.Close();

                }
                MessageBox.Show("DATA UPDATED");
            }

            //PURCHASES UPDATE
            else if(updateCategory == "UPDATE PURCHASES")
            {
                conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(connectionString);
                cmd.Connection = conn;
                MySqlDataReader reader;
                conn.Open();

                conn2 = new MySqlConnection(connectionString);
                MySqlCommand cmd2 = new MySqlCommand(connectionString);

                cmd2.Connection = conn2;
                conn2.Open();

                cmd.CommandText = "SELECT * FROM purchases WHERE TransId = @transacid AND ItemCode = @purICode";
                MySqlParameter transacId = new MySqlParameter("@transacid", MySqlDbType.Int32);
                MySqlParameter purchItemCode = new MySqlParameter("@purICode", MySqlDbType.Int32);
                transacId.Value = int.Parse(textBox1.Text.ToString());
                purchItemCode.Value = int.Parse(textBox2.Text.ToString());
                cmd.Parameters.Add(transacId);
                cmd.Parameters.Add(purchItemCode);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Globals.quantity = int.Parse(reader["Quantity"].ToString());
                        Globals.iPrice = double.Parse(reader["ItemPrice"].ToString());
                        Globals.purchID = int.Parse(reader["PurchId"].ToString());

                    }
                    reader.Close();
                }


                cmd.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @sitemcode";
                MySqlParameter STICode = new MySqlParameter("@sitemcode", MySqlDbType.Int32);
                STICode.Value = int.Parse(textBox2.Text.ToString());
                cmd.Parameters.Add(STICode);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        Globals.stQuantity = int.Parse(reader["Quantity"].ToString());

                    }
                    reader.Close();
                }

                cmd.CommandText = "INSERT INTO purchasesreturns(ReturnQuantity, ReturnDate, PurchId) VALUES(@purchquantity, (SELECT NOW()), @pID)";
                MySqlParameter rquantity = new MySqlParameter("@purchquantity", MySqlDbType.Int32);
                MySqlParameter date = new MySqlParameter("@date", MySqlDbType.DateTime);
                MySqlParameter pID = new MySqlParameter("@pID", MySqlDbType.Int32);
                rquantity.Value = int.Parse(textBox3.Text.ToString());
                date.Value = dn.ToString("yyyy-MM-dd HH:mm:ss");
                pID.Value = Globals.purchID;
                cmd.Parameters.Add(rquantity);
                cmd.Parameters.Add(date);
                cmd.Parameters.Add(pID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();



                cmd2.CommandText = "UPDATE stocks SET Quantity = @stquantity WHERE ItemCode = @stItemCode";
                MySqlParameter stquantity = new MySqlParameter("@stquantity", MySqlDbType.Int32);
                MySqlParameter stItemCode = new MySqlParameter("@stItemCode", MySqlDbType.Int32);
                stquantity.Value = Globals.stQuantity + int.Parse(textBox3.Text.ToString());
                stItemCode.Value = int.Parse(textBox2.Text.ToString());
                cmd2.Parameters.Add(stquantity);
                cmd2.Parameters.Add(stItemCode);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();


                cmd2.CommandText = "UPDATE purchases SET Quantity = @purchQuantity WHERE ItemCode = @purchICode";
                MySqlParameter purchQuantity = new MySqlParameter("@purchQuantity", MySqlDbType.Int32);
                MySqlParameter purchICode = new MySqlParameter("@purchICode", MySqlDbType.Int32);
                purchQuantity.Value = Globals.quantity - int.Parse(textBox3.Text.ToString());
                purchICode.Value = int.Parse(textBox2.Text.ToString());
                cmd2.Parameters.Add(purchQuantity);
                cmd2.Parameters.Add(purchICode);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();



                cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @traID";
                MySqlParameter traID = new MySqlParameter("@traID", MySqlDbType.Decimal);
                traID.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(traID);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        Globals.totalAmnt = double.Parse(reader["TotalAmount"].ToString());

                    }
                    reader.Close();
                }

                double newTotAmnt = Globals.iPrice * int.Parse(textBox3.Text.ToString());
                cmd2.CommandText = "UPDATE transactions SET TotalAmount = @totAmount WHERE TransId = @transId";
                MySqlParameter upTotAmnt = new MySqlParameter("@totAmount", MySqlDbType.Decimal);
                MySqlParameter transID = new MySqlParameter("@transId", MySqlDbType.Int32);
                upTotAmnt.Value = Globals.totalAmnt - newTotAmnt;
                transID.Value = int.Parse(textBox1.Text.ToString());
                cmd2.Parameters.Add(upTotAmnt);
                cmd2.Parameters.Add(transID);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();

                cmd2.CommandText = "UPDATE purchases SET Status = @status WHERE TransId = @transacid AND ItemCode = @pICode";
                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Text);
                MySqlParameter transacid = new MySqlParameter("@transacid", MySqlDbType.Int32);
                MySqlParameter pICode = new MySqlParameter("@pICode", MySqlDbType.Int32);
                status.Value = textBox4.Text;
                transacid.Value = int.Parse(textBox1.Text.ToString());
                pICode.Value = int.Parse(textBox2.Text.ToString());
                cmd2.Parameters.Add(status);
                cmd2.Parameters.Add(transacid);
                cmd2.Parameters.Add(pICode);
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                reader.Close();

                cmd.CommandText = "SELECT * FROM purchases WHERE TransId = @tId AND ItemCode = @itemcode";
                MySqlParameter itemcode = new MySqlParameter("@itemcode", MySqlDbType.Int32);
                MySqlParameter tID = new MySqlParameter("@tId", MySqlDbType.Int32);
                itemcode.Value = int.Parse(textBox2.Text.ToString());
                tID.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(itemcode);
                cmd.Parameters.Add(tID);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dataGridView3.Rows.Clear();
                        dataGridView3.Rows.Add(reader["PurchId"], reader["ItemCode"], reader["TransId"], reader["ItemPrice"], reader["Quantity"], reader["Status"]);
                    }
                    reader.Close();

                }
                MessageBox.Show("DATA UPDATED");
            }

            //PAYABLES UPDATE
            else if(updateCategory == "UPDATE PAYABLE")
            {
                conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(connectionString);
                cmd.Connection = conn;
                MySqlDataReader reader;
                conn.Open();


                cmd.CommandText = "UPDATE payables SET Status = @status WHERE PayableId = @payableId";
                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Text);
                MySqlParameter payableid = new MySqlParameter("@payableId", MySqlDbType.Int32);
                status.Value = textBox2.Text;
                payableid.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(status);
                cmd.Parameters.Add(payableid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT * FROM payables WHERE PayableId = @payableID ";
                MySqlParameter payableID = new MySqlParameter("@transid", MySqlDbType.Int32);
                payableID.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(payableID);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dataGridView4.Rows.Clear();
                        dataGridView4.Rows.Add(reader["PayableId"], reader["PurchaseId"], reader["Status"]);
                    }
                    reader.Close();

                }

                MessageBox.Show("DATA UPDATED");
            }

            //RECEIVABLES UPDATE
            else if(updateCategory == "UPDATE RECEIVABLES")
            {
                conn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(connectionString);
                cmd.Connection = conn;
                MySqlDataReader reader;
                conn.Open();


                cmd.CommandText = "UPDATE receivables SET Status = @status WHERE ReceivableId = @receiveId";
                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Text);
                MySqlParameter receiveId = new MySqlParameter("@receiveId", MySqlDbType.Int32);
                status.Value = textBox2.Text;
                receiveId.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(status);
                cmd.Parameters.Add(receiveId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT * FROM receivables WHERE ReceivableId = @recId";
                MySqlParameter recId = new MySqlParameter("@recId", MySqlDbType.Int32);
                recId.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(recId);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dataGridView5.Rows.Clear();
                        dataGridView5.Rows.Add(reader["ReceivableId"], reader["SalesId"], reader["Status"]);
                    }
                    reader.Close();

                }
                MessageBox.Show("DATA UPDATED");
            }
       
            //RETURN ALL ITEMS
            else if(updateCategory == "RETURN ALL ITEMS")
            {
                conn = new MySqlConnection(connectionString);
                conn2 = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand(connectionString);
                MySqlCommand cmd2 = new MySqlCommand(connectionString);
                cmd.Connection = conn;
                cmd2.Connection = conn2;
                MySqlDataReader reader;
                MySqlDataReader reader2;
                conn.Open();
                conn2.Open();

                cmd.CommandText = "SELECT * FROM transactions WHERE TransId = @transactionID";
                MySqlParameter transactionID = new MySqlParameter("@transactionID", MySqlDbType.Int32);
                transactionID.Value = int.Parse(textBox1.Text.ToString());
                cmd.Parameters.Add(transactionID);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Globals.transType = reader["TransType"].ToString();

                        if (Globals.transType == "Sales")
                        {
                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT * FROM Sales WHERE TransId = @transID";
                            MySqlParameter transID = new MySqlParameter("@transID", MySqlDbType.Int32);
                            transID.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(transID);

                            using (reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Globals.transQuantity = int.Parse(reader["Quantity"].ToString());
                                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                    Globals.salesRID = int.Parse(reader["SalesId"].ToString());


                                    cmd2.Parameters.Clear();
                                    cmd2.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @sItemc";
                                    MySqlParameter sItemc = new MySqlParameter("@sItemc", MySqlDbType.Int32);
                                    sItemc.Value = Globals.itemcode;
                                    cmd2.Parameters.Add(sItemc);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            Globals.stQuantity = int.Parse(reader2["Quantity"].ToString());
                                            Globals.stItemc = int.Parse(reader2["ItemCode"].ToString());
                                            reader2.Close();

                                            cmd2.CommandText = "UPDATE stocks SET Quantity = @stocksQuantity WHERE ItemCode = @stocksItemC";
                                            MySqlParameter stocksItemC = new MySqlParameter("@stocksItemC", MySqlDbType.Int32);
                                            MySqlParameter stocksQuantity = new MySqlParameter("@stocksQuantity", MySqlDbType.Decimal);
                                            stocksItemC.Value = Globals.stItemc;
                                            stocksQuantity.Value = Globals.stQuantity + Globals.transQuantity;
                                            cmd2.Parameters.Add(stocksItemC);
                                            cmd2.Parameters.Add(stocksQuantity);
                                            cmd2.Prepare();
                                            cmd2.ExecuteNonQuery();

                                        }
                                    }

                                }
                                reader.Close();
                            }

                            double newTotAmnt = 0;
                            cmd2.CommandText = "UPDATE transactions SET TotalAmount = @totAmount WHERE TransId = @transId";
                            MySqlParameter upTotAmnt = new MySqlParameter("@totAmount", MySqlDbType.Decimal);
                            MySqlParameter TID = new MySqlParameter("@transId", MySqlDbType.Int32);
                            upTotAmnt.Value = newTotAmnt;
                            TID.Value = int.Parse(textBox1.Text.ToString());
                            cmd2.Parameters.Add(upTotAmnt);
                            cmd2.Parameters.Add(TID);
                            cmd2.Prepare();
                            cmd2.ExecuteNonQuery();
                            reader.Close();

                            cmd.CommandText = "UPDATE sales SET Status = @status WHERE TransId = @transacID";
                            MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Text);
                            MySqlParameter transacID = new MySqlParameter("@transacID", MySqlDbType.Int32);
                            status.Value = textBox2.Text;
                            transacID.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(status);
                            cmd.Parameters.Add(transacID);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE sales SET Quantity = @quantity WHERE TransId = @transactionID";
                            MySqlParameter quantityParam = new MySqlParameter("@quantity", MySqlDbType.Text);
                            MySqlParameter transacIDParam = new MySqlParameter("@transactionID", MySqlDbType.Int32);
                            quantityParam.Value = Globals.transQuantity - Globals.transQuantity;
                            transacIDParam.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(quantityParam);
                            cmd.Parameters.Add(transacIDParam);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO salesreturns(ReturnQuantity, ReturnDate, SalesId) VALUES(@sarequantity, (SELECT NOW()), @srID)";
                            MySqlParameter sarequantity = new MySqlParameter("@sarequantity", MySqlDbType.Int32);
                            MySqlParameter srDate = new MySqlParameter("@date", MySqlDbType.DateTime);
                            MySqlParameter srID = new MySqlParameter("@srID", MySqlDbType.Int32);
                            sarequantity.Value = Globals.transQuantity;
                            srDate.Value = dn.ToString("yyyy-MM-dd HH:mm:ss");
                            srID.Value = Globals.salesRID;
                            cmd.Parameters.Add(sarequantity);
                            cmd.Parameters.Add(srDate);
                            cmd.Parameters.Add(srID);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();

                            dataGridView6.Rows.Clear();
                            cmd.CommandText = "SELECT * FROM sales WHERE TransId = @salesTransID";
                            MySqlParameter salesTransID = new MySqlParameter("@salesTransID", MySqlDbType.Int32);
                            salesTransID.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(salesTransID);
                            using (reader2 = cmd2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    Globals.stItemc = int.Parse(reader2["ItemCode"].ToString());
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "SELECT * FROM stocks WHERE ItemCode = @SIC";
                                    MySqlParameter SIC = new MySqlParameter("@SIC", MySqlDbType.Decimal);
                                    SIC.Value = Globals.stItemc;
                                    cmd.Parameters.Add(SIC);
                                    using (reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            Globals.stocksQuantity = int.Parse(reader["Quantity"].ToString());

                                        }
                                        reader.Close();
                                    }

                                    dataGridView6.Rows.Add(reader2["SalesId"], reader2["ItemCode"], reader2["TransId"], Globals.stocksQuantity, reader2["Status"]);
                                }
                                reader2.Close();

                            }

                            //MessageBox.Show("DATA UPDATED");
                        }

                        else
                        {
                            reader.Close();
                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT * FROM purchases WHERE TransId = @transID";
                            MySqlParameter transID = new MySqlParameter("@transID", MySqlDbType.Int32);
                            transID.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(transID);

                            using (reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Globals.transQuantity = int.Parse(reader["Quantity"].ToString());
                                    Globals.itemcode = int.Parse(reader["ItemCode"].ToString());
                                    Globals.purchRID = int.Parse(reader["PurchId"].ToString());

                                    cmd2.Parameters.Clear();
                                    cmd2.CommandText = "SELECT * FROM Stocks WHERE ItemCode = @sItemc";
                                    MySqlParameter sItemc = new MySqlParameter("@sItemc", MySqlDbType.Int32);
                                    sItemc.Value = Globals.itemcode;
                                    cmd2.Parameters.Add(sItemc);

                                    using (reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            Globals.stQuantity = int.Parse(reader2["Quantity"].ToString());
                                            Globals.stItemc = int.Parse(reader2["ItemCode"].ToString());
                                            reader2.Close();

                                            cmd2.CommandText = "UPDATE stocks SET Quantity = @stocksQuantity WHERE ItemCode = @stocksItemC";
                                            MySqlParameter stocksItemC = new MySqlParameter("@stocksItemC", MySqlDbType.Int32);
                                            MySqlParameter stocksQuantity = new MySqlParameter("@stocksQuantity", MySqlDbType.Decimal);
                                            stocksItemC.Value = Globals.stItemc;
                                            stocksQuantity.Value = Globals.stQuantity - Globals.transQuantity;
                                            cmd2.Parameters.Add(stocksItemC);
                                            cmd2.Parameters.Add(stocksQuantity);
                                            cmd2.Prepare();
                                            cmd2.ExecuteNonQuery();

                                        }
                                    }

                                }
                                reader.Close();
                            }

                            double newTotAmnt = 0;
                            cmd2.CommandText = "UPDATE transactions SET TotalAmount = @totAmount WHERE TransId = @transId";
                            MySqlParameter upTotAmnt = new MySqlParameter("@totAmount", MySqlDbType.Decimal);
                            MySqlParameter TID = new MySqlParameter("@transId", MySqlDbType.Int32);
                            upTotAmnt.Value = newTotAmnt;
                            TID.Value = int.Parse(textBox1.Text.ToString());
                            cmd2.Parameters.Add(upTotAmnt);
                            cmd2.Parameters.Add(TID);
                            cmd2.Prepare();
                            cmd2.ExecuteNonQuery();
                            reader.Close();

                            cmd.CommandText = "UPDATE purchases SET Status = @status WHERE TransId = @transacID";
                            MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Text);
                            MySqlParameter transacID = new MySqlParameter("@transacID", MySqlDbType.Int32);
                            status.Value = textBox2.Text;
                            transacID.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(status);
                            cmd.Parameters.Add(transacID);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE purchases SET Quantity = @quantity WHERE TransId = @transactionID";
                            MySqlParameter quantityParam = new MySqlParameter("@quantity", MySqlDbType.Text);
                            MySqlParameter transacIDParam = new MySqlParameter("@transactionID", MySqlDbType.Int32);
                            quantityParam.Value = Globals.transQuantity - Globals.transQuantity;
                            transacIDParam.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(quantityParam);
                            cmd.Parameters.Add(transacIDParam);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO purchasesreturns(ReturnQuantity, ReturnDate, PurchId) VALUES(@purchquantity, (SELECT NOW()), @pID)";
                            MySqlParameter rquantity = new MySqlParameter("@purchquantity", MySqlDbType.Int32);
                            MySqlParameter date = new MySqlParameter("@date", MySqlDbType.DateTime);
                            MySqlParameter pID = new MySqlParameter("@pID", MySqlDbType.Int32);
                            rquantity.Value = Globals.transQuantity;
                            date.Value = dn.ToString("yyyy-MM-dd HH:mm:ss");
                            pID.Value = Globals.purchRID;
                            cmd.Parameters.Add(rquantity);
                            cmd.Parameters.Add(date);
                            cmd.Parameters.Add(pID);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();

                            dataGridView6.Rows.Clear();
                            cmd.CommandText = "SELECT * FROM purchases WHERE TransId = @salesTransID";
                            MySqlParameter salesTransID = new MySqlParameter("@salesTransID", MySqlDbType.Int32);
                            salesTransID.Value = int.Parse(textBox1.Text.ToString());
                            cmd.Parameters.Add(salesTransID);

                            using (reader2 = cmd2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    Globals.stItemc = int.Parse(reader2["ItemCode"].ToString());
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = "SELECT * FROM stocks WHERE ItemCode = @SIC";
                                    MySqlParameter SIC = new MySqlParameter("@SIC", MySqlDbType.Decimal);
                                    SIC.Value = Globals.stItemc;
                                    cmd.Parameters.Add(SIC);
                                    using (reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            Globals.stocksQuantity = int.Parse(reader["Quantity"].ToString());

                                        }
                                        reader.Close();
                                    }

                                    dataGridView6.Rows.Add(reader2["PurchId"], reader2["ItemCode"], reader2["TransId"], Globals.stocksQuantity, reader2["Status"]);
                                }
                                reader2.Close();

                            }

                        }
                    }
                }
                MessageBox.Show("DATA UPDATED");
            }

        }

        private void btnUpItems_Click(object sender, EventArgs e)
        {
            updateCategory = "UPDATE ITEMS";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            label1.Text = "Item Code : ";
            label2.Text = "Item Sell Price : ";
            label3.Text = "Item Price : ";
            textBox3.Show();
            label3.Show();
            label4.Hide();
            textBox4.Hide();
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView1);
            dataGridView1.Rows.Clear();
            dataGridView1.Show();

        }

        private void btnUpSales_Click(object sender, EventArgs e)
        {
            updateCategory = "UPDATE SALES";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            label1.Text = "Transaction ID : ";
            label2.Text = "Item Code : ";
            label3.Text = "Quantity : ";
            label4.Text = "Status : ";
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            label3.Show();
            label4.Show();
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView2);
            dataGridView2.Rows.Clear();
            dataGridView2.Show();
        }

        private void btnUpPurch_Click(object sender, EventArgs e)
        {
            updateCategory = "UPDATE PURCHASES";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            label1.Text = "Transaction ID : ";
            label2.Text = "Item Code : ";
            label3.Text = "Quantity : ";
            label4.Text = "Status : ";
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            label3.Show();
            label4.Show();
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView3);
            dataGridView3.Rows.Clear();
            dataGridView3.Show();
        }

        private void btnUpPay_Click(object sender, EventArgs e)
        {
            updateCategory = "UPDATE PAYABLES";
            textBox1.Clear();
            textBox2.Clear();
            label1.Text = "Payable ID : ";
            label2.Text = "Status : ";
            label3.Hide();
            label4.Hide();
            textBox3.Hide();
            textBox4.Hide();
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView4);
            dataGridView4.Rows.Clear();
            dataGridView4.Show();
        }

        private void btnUpRec_Click(object sender, EventArgs e)
        {
            updateCategory = "UPDATE RECEIVABLES";
            textBox1.Clear();
            textBox2.Clear();
            label1.Text = "Receivable ID : ";
            label2.Text = "Status : ";
            label3.Hide();
            label4.Hide();
            textBox3.Hide();
            textBox4.Hide();
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView5);
            dataGridView5.Rows.Clear();
            dataGridView5.Show();
        }

        private void btnReItems_Click(object sender, EventArgs e)
        {
            updateCategory = "RETURN ALL ITEMS";
            textBox1.Clear();
            textBox2.Clear();
            label1.Text = "Transaction ID : ";
            label2.Text = "Status : ";
            label3.Hide();
            label4.Hide();
            textBox3.Hide();
            textBox4.Hide();
            panel1.Controls.Clear();
            panel1.Controls.Add(dataGridView6);
            dataGridView6.Rows.Clear();
            dataGridView6.Show();
        }


  
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpItems_MouseEnter_1(object sender, EventArgs e)
        {
            btnUpItems.BackColor = Color.SeaGreen;
            btnUpItems.FlatAppearance.BorderColor = Color.MediumAquamarine;
            btnUpItems.ForeColor = Color.White;
        }

        private void btnUpItems_MouseLeave_1(object sender, EventArgs e)
        {
            btnUpItems.BackColor = Color.FromArgb(229, 245, 242);
            btnUpItems.FlatAppearance.BorderColor = Color.SeaGreen;
            btnUpItems.ForeColor = Color.Black;
        }

        private void btnUpSales_MouseEnter(object sender, EventArgs e)
        {
            btnUpSales.BackColor = Color.FromArgb(229, 245, 242);
            btnUpSales.FlatAppearance.BorderColor = Color.SeaGreen;
            btnUpSales.ForeColor = Color.Black;
        }

        private void btnUpSales_MouseLeave(object sender, EventArgs e)
        {
            btnUpSales.BackColor = Color.FromArgb(229, 245, 242);
            btnUpSales.FlatAppearance.BorderColor = Color.SeaGreen;
            btnUpSales.ForeColor = Color.Black;
        }

        private void btnUpPurch_MouseEnter(object sender, EventArgs e)
        {
            btnUpPurch.BackColor = Color.FromArgb(229, 245, 242);
            btnUpPurch.FlatAppearance.BorderColor = Color.SeaGreen;
            btnUpPurch.ForeColor = Color.Black;
        }
    }
}
