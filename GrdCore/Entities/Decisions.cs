using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GrdCore.BLL;

namespace GrdCore.Entities
{
    public class Decisions
    {
        #region Variables
        private string _decisionNumber = string.Empty;
        private string _decisionAlias = string.Empty;
        private string _signStaff = string.Empty;
        private string _signDate = string.Empty;
        private string _updateDate = string.Empty;
        private string _reason = string.Empty;
        private int _decisionTypeID = 0;
        private string _updateStaff = string.Empty;
        private string _note = string.Empty;
        private bool _isInUsed = false;
        #endregion

        #region Properties
        public string DecisionNumber
        {
            get { return _decisionNumber; }
            set { _decisionNumber = value; }
        }
        public string DecisionAlias
        {
            get { return _decisionAlias; }
            set { _decisionAlias = value; }
        }
        public string SignStaff
        {
            get { return _signStaff; }
            set { _signStaff = value; }
        }
        public string SignDate
        {
            get { return _signDate; }
            set { _signDate = value; }
        }
        public string UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value; }
        }
        public string UpdateStaff
        {
            get { return _updateStaff; }
            set { _updateStaff = value; }
        }
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        public int DecisionTypeID
        {
            get { return _decisionTypeID; }
            set { _decisionTypeID = value; }
        }
        public bool IsInUsed
        {
            get { return _isInUsed; }
            set { _isInUsed = value; }
        }

        public string Note
        {
            get { return _note ; }
            set { _note = value; }
        }
        #endregion

        #region Constructions
        public Decisions()
        {
            _decisionNumber = string.Empty;
            _decisionAlias = string.Empty;
            _signStaff = string.Empty;
            _signDate = string.Empty;
            _updateDate = string.Empty;
            _reason = string.Empty;
            _decisionTypeID = 0;
            _updateStaff = string.Empty;
            _isInUsed = false;
        }

        public Decisions(string decisionNumber)
        {
            try
            {
                DataTable dt = BL_Decision.GetDecisionByDecisionNumber(decisionNumber);

                DataRow dr = dt.Rows[0];
                _decisionNumber = dr["DecisionNumber"].ToString();
                _decisionAlias = dr["DecisionAlias"].ToString();
                _signStaff = dr["SignStaff"].ToString();
                _signDate = dr["SignDate"].ToString();
                _updateDate = dr["UpdateDate"].ToString();
                _updateStaff = dr["UpdateStaff"].ToString();
                _reason = dr["Reason"].ToString();
                _decisionTypeID = int.Parse(dr["DecisionTypeID"].ToString());
                _isInUsed = (dr["IsInUsed"].ToString().ToUpper() == "TRUE");
            }
            catch
            {
                _decisionNumber = string.Empty;
                _decisionAlias = string.Empty;
                _signStaff = string.Empty;
                _signDate = string.Empty;
                _updateDate = string.Empty;
                _reason = string.Empty;
                _decisionTypeID = 0;
                _updateStaff = string.Empty;
                _isInUsed = false;
            }
        }

        public Decisions(string decisionNumber, string decisionAlias, string signStaff, string signDate, string updateDate, string reason, int decisionTypeID)
        {
            _decisionNumber = decisionNumber;
            _decisionAlias = decisionAlias;
            _signStaff = signStaff;
            _signDate = signDate;
            _updateDate = updateDate;
            _reason = reason;
            _decisionTypeID = decisionTypeID;
        }
        #endregion

        #region Functions
        public int InsertDecisions()
        {
            return BL_Decision.InsertDecision(this);
        }

        public int UpdateDecisions(string oldDecisionNumber)
        {
            return BL_Decision.UpdateDecision(this, oldDecisionNumber);
        }

        //public int DeleteDecision(string decisionNumberHuy, string decisionNumber)
        //{
        //    return BL_Decision.DeleteDecision(decisionNumberHuy, decisionNumber);
        //}
        #endregion
    }
}
