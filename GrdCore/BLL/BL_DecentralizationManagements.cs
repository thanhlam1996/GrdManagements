using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using GrdCore.DAL;
using GrdCore.Entities;

namespace GrdCore.BLL
{
    public class BL_DecentralizationManagements
    {
        public static DataTable GetGroupUserByStaffID(string staffID)
        {
            try
            {
                return DA_DecentralizationManagements.GetGroupUserByStaffID(staffID);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public static Staffs GetStaffs(string userID)
        {
            Staffs staff = new Staffs();
            try
            {
                DataTable dt = DA_DecentralizationManagements.GetStaffs(userID);
                if (dt.Rows.Count != 0)
                {
                    staff.StaffID = dt.Rows[0]["StaffID"].ToString();
                    staff.FirstName = dt.Rows[0]["FirstName"].ToString().Trim();
                    staff.MiddleName = dt.Rows[0]["MiddleName"].ToString().Trim();
                    staff.PassWord = dt.Rows[0]["Password"].ToString();
                    staff.LastName = dt.Rows[0]["LastName"].ToString().Trim();
                    try
                    {
                        staff.Department = dt.Rows[0]["DepartmentName"].ToString().Trim();
                    }
                    catch { }
                }
                return staff;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public static DataTable GetDecentralizationByGroupIDandFormID(string groupID, string formID)
        {
            try
            {
                return DA_DecentralizationManagements.GetDecentralizationByGroupIDandFormID(groupID, formID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ChangePassword(string staffID, string newPassword, string oldPassword)
        {
            try
            {
                return DA_DecentralizationManagements.ChangePassword(staffID, newPassword, oldPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
