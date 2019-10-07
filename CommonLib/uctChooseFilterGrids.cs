using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.UserControls
{
    public partial class uctChooseFilterGrids : UserControl
    {
        #region Init
        public uctChooseFilterGrids()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Set or get selected a element by index
        /// </summary>
        public int SelectedIndex
        {
            get { return cboChoose.SelectedIndex; }
            set { cboChoose.SelectedIndex = value; }
        }
        /// <summary>
        /// Set or get selected a element by value
        /// </summary>
        public object SelectedValue
        {
            get
            {
                if (cboChoose.SelectedValue != null)
                    if (cboChoose.SelectedValue.GetType() == typeof(DataRowView))
                    {
                        DataRowView dr = (DataRowView)cboChoose.SelectedValue;
                        return dr.Row["ID"];
                    }
                    else
                        return cboChoose.SelectedValue;
                else
                    return null;
            }
            set { cboChoose.SelectedValue = value; }
        }
        #endregion

        #region function local
        private void InitData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Rows.Add("1", "Lọc cả 2 danh sách");
            dt.Rows.Add("2", "Lọc danh sách trên");
            dt.Rows.Add("3", "Lọc danh sách dưới");
            cboChoose.DataSource = new DataView(dt);
            cboChoose.DisplayMember = "Value";
            cboChoose.ValueMember = "ID";
        }
        #endregion

        #region Event control
        public event EventHandler SelectIndexChange;
        protected virtual void OnSelectIndexChange(EventArgs e)
        {
            if (SelectIndexChange != null)
                SelectIndexChange(this, e);
        }
        #endregion

        #region cboChooses_SelectedIndexChanged()
        private void cboChooses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectIndexChange != null)
            {
                SelectIndexChange(sender, e);
            }
        }
        #endregion
    }
}
