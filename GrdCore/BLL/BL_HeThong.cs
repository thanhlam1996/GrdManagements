using GrdCore.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrdCore.BLL
{
    public class BL_HeThong
    {
        public static DataSet GetDataSetDataDictionary(string staffID, string groupD)
        {
            try
            {
                return DA_HeThong.GetDataSetDataDictionary(staffID, groupD);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdateCurrentValues(string userID, string currentTerm, string currentYearStudy, string currentGraduateLevelID, string currentStudyTypeID)
        {
            try
            {
                return DA_HeThong.UpdateCurrentValues(userID, currentTerm, currentYearStudy, currentGraduateLevelID, currentStudyTypeID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
