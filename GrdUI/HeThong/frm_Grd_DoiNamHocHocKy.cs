using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace GrdUI.HeThong
{
    public partial class frm_Grd_DoiNamHocHocKy : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        public string _currentYearStudy;
        public string _currentTermID;
        public bool _isSubmitted = false;
        #endregion

        #region Inits
        public frm_Grd_DoiNamHocHocKy()
        {
            InitializeComponent();
        }

        private void frm_Grd_DoiNamHocHocKy_Load(object sender, EventArgs e)
        {
            try
            {
                GetYearStudy();
                lkuNamHoc.EditValue = User._CurrentYearStudy;
                lookUpEdit_YearStudy_EditValueChanged(null, null);
                _currentYearStudy = User._CurrentYearStudy;
                _currentTermID = User._CurrentTerm;
            }
            catch { }
        }
        #endregion

        #region Functions
        private void GetYearStudy()
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.Add("YearStudy", typeof(string));
                dtData.Columns.Add("YearStudyID", typeof(string));

                foreach (DataRow dr in User._dsDataDictionaries.Tables["Terms"].Rows)
                    if (dtData.Select("YearStudy = '" + dr["YearStudy"].ToString() + "'").Length == 0)
                        dtData.Rows.Add(new object[] { dr["YearStudy"].ToString(), dr["YearStudy"].ToString() });

                DataView myDataView = new DataView(dtData);
                myDataView.Sort = "YearStudy DESC";

                lkuNamHoc.Properties.DataSource = myDataView.ToTable();
                lkuNamHoc.Properties.DisplayMember = "YearStudy";
                lkuNamHoc.Properties.ValueMember = "YearStudyID";

                LookUpColumnInfoCollection coll = lkuNamHoc.Properties.Columns;
                coll.Add(new LookUpColumnInfo("YearStudy", 0, "Năm học"));

                lkuNamHoc.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuNamHoc.Properties.SearchMode = SearchMode.AutoComplete;
                lkuNamHoc.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }

        public void LoadData(string yearStudy, string termID)
        {
            try
            {
                _currentYearStudy = yearStudy;
                _currentTermID = termID;
            }
            catch { }
        }

        public void GetTerms(string yearStudy)
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData.Columns.Add("TermID", typeof(string));
                dtData.Columns.Add("TermName", typeof(string));

                DataRow[] drSelect = User._dsDataDictionaries.Tables["Terms"].Select("YearStudy = '" + yearStudy + "'");
                foreach (DataRow dr in drSelect)
                    dtData.Rows.Add(new object[] { dr["TermID"].ToString(), dr["TermName"].ToString() });

                DataView dv = new DataView(dtData);
                dv.Sort = "TermName";

                lkuHocKy.Properties.DataSource = dv.ToTable();
                lkuHocKy.Properties.DisplayMember = "TermName";
                lkuHocKy.Properties.ValueMember = "TermID";

                LookUpColumnInfoCollection coll = lkuHocKy.Properties.Columns;
                coll.Clear();
                coll.Add(new LookUpColumnInfo("TermName", 0, "Học kỳ"));

                lkuHocKy.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lkuHocKy.Properties.SearchMode = SearchMode.AutoComplete;
                lkuHocKy.Properties.AutoSearchColumnIndex = 0;
            }
            catch { }
        }
        #endregion

        #region Events
        private void lookUpEdit_YearStudy_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetTerms(lkuNamHoc.EditValue.ToString());
                lkuHocKy.EditValue = User._CurrentTerm;
                lookUpEdit_Terms_EditValueChanged(null, null);
            }
            catch { }
        }

        private void lookUpEdit_Terms_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _currentYearStudy = lkuNamHoc.EditValue.ToString();
                _currentTermID = lkuHocKy.EditValue.ToString();
            }
            catch { }
        }

        private void btn_DongY_Click(object sender, EventArgs e)
        {
            try
            {
                _currentYearStudy = lkuNamHoc.EditValue.ToString();
                _currentTermID = lkuHocKy.EditValue.ToString();
                _isSubmitted = true;
                this.Close();
            }
            catch { }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            _isSubmitted = false;
            this.Close();
        } 
        #endregion
    }
}
