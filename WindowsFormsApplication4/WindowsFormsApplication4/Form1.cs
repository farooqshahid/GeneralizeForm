using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using DevExpress.XtraEditors;
namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=LENOVOPC\SQLEXPRESS;Initial Catalog=TempDatabase;User ID=sa;Password=ghafoor*aslam786");
        public Form1()
        {
            InitializeComponent();
        }         
        private void Form1_Load(object sender, EventArgs e)
        {
            // AddFormSettings();
            CreateDesign(this.Name);
        }
        private void AddFormSettings()
        {
            foreach (var item in this.Controls)
            {
                if (item is GroupControl)
                {
                    GroupControl objgrpctrl = item as GroupControl;

                    var controls = objgrpctrl.Controls;

                    foreach (var ControlType in controls)
                    {
                        if (ControlType is ComboBoxEdit)
                        {
                            ComboBoxEdit objComboBoxEdit = ControlType as ComboBoxEdit;
                            SaveControlDetail(this.Name, objComboBoxEdit.Name, objComboBoxEdit.Height, objComboBoxEdit.Width, objComboBoxEdit.Location.X, objComboBoxEdit.Location.Y, "ComboBox");
                        }
                        else if (ControlType is TextEdit)
                        {
                            TextEdit objTextEdit = ControlType as TextEdit;
                            SaveControlDetail(this.Name, objTextEdit.Name, objTextEdit.Height, objTextEdit.Width, objTextEdit.Location.X, objTextEdit.Location.Y, "TextBox");
                        }
                        else if (ControlType is SimpleButton)
                        {
                            SimpleButton objSimpleButton = ControlType as SimpleButton;
                            SaveControlDetail(this.Name, objSimpleButton.Name, objSimpleButton.Height, objSimpleButton.Width, objSimpleButton.Location.X, objSimpleButton.Location.Y, "SimpleButton");
                        }
                    }
                }
            }
        }

        private void SaveControlDetail(string FormName, string ControlName, int height, int width, int X, int Y, string Type)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string SqlQuery = "INSERT INTO [dbo].[FormDetail]([FormName],[ControlName],[height],[width],[X],[Y],[Type]) ";
                SqlQuery = SqlQuery + "VALUES('" + FormName + "','" + ControlName + "'," + height + "," + width + "," + X + "," + Y + ",'" + Type + "')";
                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        DataTable dt = new System.Data.DataTable();

        private DataTable LoadDesign(string FormName)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string SqlQuery = "SELECT * FROM  [dbo].[FormDetail] WHERE FormName = '" + FormName + "'";
                SqlDataAdapter ad = new SqlDataAdapter(SqlQuery, con);
                ad.SelectCommand.CommandType = System.Data.CommandType.Text;
                ad.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        private void CreateDesign(string FormName)
        {
            DataTable dtdesign = new System.Data.DataTable();
            dtdesign = LoadDesign(FormName);
            if (dtdesign.Rows.Count > 0)
            {
                for (int i = 0; i < dtdesign.Rows.Count; i++)
                {
                    if (dtdesign.Rows[i]["Type"].ToString() == "SimpleButton")
                    {
                        SimpleButton objSimpleButton = new SimpleButton();
                        objSimpleButton.Name = dtdesign.Rows[i]["ControlName"].ToString();
                        objSimpleButton.Text = dtdesign.Rows[i]["ControlText"].ToString();
                        objSimpleButton.Width = Convert.ToInt16(dtdesign.Rows[i]["width"]);
                        objSimpleButton.Height = Convert.ToInt16(dtdesign.Rows[i]["height"]);
                        //objSimpleButton.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), dtdesign.Rows[i]["AnchorBottom"].ToString());
                        //objSimpleButton.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), dtdesign.Rows[i]["AnchorRight"].ToString());
                        objSimpleButton.Location = new Point(Convert.ToInt16(dtdesign.Rows[i]["X"]), Convert.ToInt16(dtdesign.Rows[i]["Y"]));
                        objSimpleButton.Click += new EventHandler(objSimpleButton_Click);
                        this.Controls.Add(objSimpleButton);
                    }
                    else if (dtdesign.Rows[i]["Type"].ToString() == "ComboBox")
                    {
                        ComboBoxEdit objSimpleButton = new ComboBoxEdit();
                        objSimpleButton.Name = dtdesign.Rows[i]["ControlName"].ToString();
                        objSimpleButton.Text = dtdesign.Rows[i]["ControlText"].ToString();
                        objSimpleButton.Width = Convert.ToInt16(dtdesign.Rows[i]["width"]);
                        objSimpleButton.Height = Convert.ToInt16(dtdesign.Rows[i]["height"]);
                        //objSimpleButton.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), dtdesign.Rows[i]["AnchorBottom"].ToString());
                        //objSimpleButton.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), dtdesign.Rows[i]["AnchorRight"].ToString());
                        objSimpleButton.Location = new Point(Convert.ToInt16(dtdesign.Rows[i]["X"]), Convert.ToInt16(dtdesign.Rows[i]["Y"]));
                        this.Controls.Add(objSimpleButton);
                    }
                    else if (dtdesign.Rows[i]["Type"].ToString() == "TextBox")
                    {
                        TextEdit objSimpleButton = new TextEdit();
                        objSimpleButton.Name = dtdesign.Rows[i]["ControlName"].ToString();
                        objSimpleButton.Text = dtdesign.Rows[i]["ControlText"].ToString();
                        objSimpleButton.Width = Convert.ToInt16(dtdesign.Rows[i]["width"]);
                        objSimpleButton.Height = Convert.ToInt16(dtdesign.Rows[i]["height"]);
                        //objSimpleButton.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), dtdesign.Rows[i]["AnchorBottom"].ToString());
                        //objSimpleButton.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), dtdesign.Rows[i]["AnchorRight"].ToString());
                        objSimpleButton.Location = new Point(Convert.ToInt16(dtdesign.Rows[i]["X"]), Convert.ToInt16(dtdesign.Rows[i]["Y"]));
                        objSimpleButton.PreviewKeyDown += new PreviewKeyDownEventHandler (objSimpleButton_PreviewKeyDown);
                        this.Controls.Add(objSimpleButton);
                    }
                }
            }
        }

        private void objSimpleButton_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextEdit objbutton = (TextEdit)sender;
            if ( objbutton.Name == "txtName")
            {
                if(e.KeyCode == Keys.F10)
                {
                    DataTable dtWithData = LoadData(objbutton.Text, sender);
                    if (dtWithData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtWithData.Columns.Count; i++)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dtWithData.Columns[i].ColumnName.ToString() == dt.Rows[j]["ProcedureName"].ToString())
                                {
                                    Control[] controls = this.Controls.Find(dt.Rows[j]["ControlName"].ToString(), true);
                                    if (controls.Length == 1)
                                    {
                                        TextEdit t = controls[0] as TextEdit;
                                        t.Text = dtWithData.Rows[0][i].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("no record found");
                    }
                }
            }
        }

        private void objSimpleButton_Click(object sender, EventArgs e)
        
        {
            SimpleButton objbutton = (SimpleButton)sender;
            if (objbutton.Text == "&Add")
            {
                if (DataInsert(dt))
                {
                    MessageBox.Show("Record Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Record not saved successfully");
                }
            }
            else if (objbutton.Text == "&Cancel")
            {
                var result = MessageBox.Show("Are You Sure ?", "Cancel", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;
                Dispose();
            }
        }

        private bool DataInsert(DataTable dt)
        {
            bool isSaved = false;
            try
            {
                string procedureName = "";
                SqlCommand cmd = new SqlCommand(procedureName, con);
                foreach (var item in this.Controls)
                {
                    if (item is TextEdit)
                    {
                        TextEdit objTextEdit = item as TextEdit;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["ControlName"].ToString() == objTextEdit.Name)
                            {
                                cmd.Parameters.Add(dt.Rows[i]["ProcedureName"].ToString(), SqlDbType.VarChar, 50).Value = objTextEdit.Text;
                                if (procedureName == "")
                                    procedureName = dt.Rows[i]["ParameterName"].ToString();
                            }
                        }
                    }
                }
                cmd.Parameters.Add("Operation", SqlDbType.VarChar, 50).Value = "Save";
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                isSaved = true;
            }
            catch(Exception ex)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message);
                isSaved = false;
            }
            return isSaved;
        }

        private DataTable LoadData(string ID, object sender)
        {
            TextEdit objbutton = (TextEdit)sender;
            bool isFound = false;
            DataTable dtDataLoaded = new DataTable();
            try
            {
                string procedureName = "";
                SqlDataAdapter ad = new SqlDataAdapter(procedureName, con);
                foreach (var item in this.Controls)
                {
                    if (item is TextEdit)
                    {
                        TextEdit objTextEdit = item as TextEdit;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["ControlName"].ToString() == objbutton.Name)
                            {
                               ad.SelectCommand.Parameters.Add(dt.Rows[i]["ProcedureName"].ToString(), SqlDbType.VarChar, 50).Value = ID;
                               ad.SelectCommand.Parameters.Add("@Operation", SqlDbType.VarChar, 20).Value = "Select";
                                if (procedureName == "") procedureName = dt.Rows[i]["ParameterName"].ToString();
                                isFound = true;
                                break;

                            }
                        }
                    }
                    if (isFound) break;
                }
                ad.SelectCommand.CommandText = procedureName;
                ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                con.Open();
                ad.Fill(dtDataLoaded);
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message);
            }
            return dtDataLoaded;
        }
    }
}
