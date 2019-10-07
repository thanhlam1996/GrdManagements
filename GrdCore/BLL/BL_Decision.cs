using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GrdCore.DAL;
using GrdCore.Entities;

namespace GrdCore.BLL
{
    public class BL_Decision
    {
        public static DataTable GetDecisionByDecisionNumber(string decisionNumber)
        {
            try
            {
                return DA_Decision.GetDecisionByDecisionNumber(decisionNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int InsertDecision(Decisions decisionInfo)
        {
            try
            {
                return DA_Decision.InsertDecision(decisionInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteDecision(string decisionNumberHuy, int decisionTypeID, string StaffID)
        {
            try
            {
                return DA_Decision.DeleteDecision(decisionNumberHuy, decisionTypeID, StaffID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteDecision(string decisionNumber)
        {
            try
            {
                return DA_Decision.DeleteDecision(decisionNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdateDecision(Decisions decisionInfo, string oldDecisionNumber)
        {
            try
            {
                return DA_Decision.UpdateDecision(decisionInfo, oldDecisionNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetDecisionsByDate(string fromDate, string toDate, bool filterBySignDate, int decisionTypeID)
        {
            try
            {
                return DA_Decision.GetDecisionsByDate(fromDate, toDate, filterBySignDate, decisionTypeID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
