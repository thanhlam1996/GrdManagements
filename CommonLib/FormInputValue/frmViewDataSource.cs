using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CommonLib.FormInputValue
{
    public partial class frmViewDataSource : CommonLib.dxfrmExtend
    {
        public object DataSource = null;

        public frmViewDataSource()
        {
            InitializeComponent();
        }

        private void frmViewDataSource_Load(object sender, EventArgs e)
        {
            if (DataSource != null)
            {                
                if (DataSource is DataSet)
                {
                    DataTable dtTable = new DataTable();
                    dtTable.Columns.Add("TableName", typeof(string));
                    foreach (DataTable dt in ((DataSet)DataSource).Tables)
                    {
                        dtTable.Rows.Add(dt.TableName);
                    }
                    cboTable.DataSource = dtTable;
                    cboTable.DisplayMember = "TableName";
                    cboTable.ValueMember = "TableName";
                    cboTable.SelectedIndexChanged += new EventHandler(cboTable_SelectedIndexChanged);
                }
                else if (DataSource is DataView)
                {
                    dtgData.DataSource = DataSource;
                    InitImage();
                }
                else if (DataSource is DataTable)
                {
                    dtgData.DataSource = DataSource;
                    InitImage();
                }
                
            }
        }

        void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)DataSource).Tables[cboTable.SelectedValue.ToString()];
                dtgData.DataSource = dt.DefaultView;
                InitImage();
            }
            catch { }
        }

        #region Init Image
        void InitImage()
        {
            foreach (DataGridViewColumn col in dtgData.Columns)
            {
                if (col.ValueType == typeof(byte[]))
                {
                    try
                    {
                        ((DataGridViewImageColumn)col).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }
                    catch { }
                }
                else if (col.ValueType == typeof(Image))
                {
                    try
                    {
                        ((DataGridViewImageColumn)col).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region Menu
        private void mnuReplaceCell_Click(object sender, EventArgs e)
        {
            try
            {
                frmReplace frm = new frmReplace();
                frm.lblFind.Visible = false;
                frm.txtFind.Visible = false;
                frm.txtReplace.Text = dtgData.CurrentCell.Value.ToString();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    dtgData.CurrentCell.Value = frm.txtReplace.Text;
                }
            }
            catch { }
        }

        private void mnuReplaceColumn_Click(object sender, EventArgs e)
        {
            try
            {
                frmReplace frm = new frmReplace();
                frm.lblFind.Visible = false;
                frm.txtFind.Visible = false;
                frm.txtReplace.Text = dtgData.CurrentCell.Value.ToString();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    string value = frm.txtReplace.Text;
                    foreach (DataGridViewRow row in dtgData.Rows)
                    {
                        row.Cells[dtgData.CurrentCell.ColumnIndex].Value = value;
                    }
                    dtgData.UpdateCellValue(dtgData.CurrentCell.ColumnIndex, dtgData.CurrentCell.RowIndex);
                }
            }
            catch { }
        }

        private void mnuReplaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                frmReplace frm = new frmReplace();
                frm.txtFind.Text = dtgData.CurrentCell.Value.ToString();
                frm.txtReplace.Text = dtgData.CurrentCell.Value.ToString();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    string value = frm.txtReplace.Text;
                    string find = frm.txtFind.Text.ToLower();
                    foreach (DataGridViewRow row in dtgData.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            try
                            {
                                if (cell.Value.ToString().ToLower() == find)
                                {
                                    cell.Value = value;
                                }
                            }
                            catch { }
                        }
                    }
                    dtgData.UpdateCellValue(dtgData.CurrentCell.ColumnIndex, dtgData.CurrentCell.RowIndex);
                }
            }
            catch { }
        }
        #endregion
    }
}