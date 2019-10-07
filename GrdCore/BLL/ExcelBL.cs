using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace GrdCore.BLL
{
    public class ExcelBL
    {
        public static DataTable GetSchema(string filePath)
        {
            try
            {
                return CommonLib.ExcelBL.GetSchema(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetSheetContent(string filePath, string fullSheetName)
        {
            try
            {
                return CommonLib.ExcelBL.GetSheetContent(filePath, fullSheetName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
    }
}
