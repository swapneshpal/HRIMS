using System;
using System.Collections.Generic;
using System.Text;

using Dev.A4;
using Dev.A4.Enums;
using Dev.A4.Exceptions;
using Dev.A4.General;
using Dev.A4.Interfaces;
using Dev.A4.Internal;
using Dev.A4.Storages;
using Dev.A4.DataTypes;
using System.Data;
using System.Data.SqlClient;

namespace HRIMS
{
    
    /// <summary>
    /// Emp Leave
    /// 
    /// 5/27/2015 12:48:56 PM
    /// </summary>
    public class cEmpLeave : cEmpLeave_Base
    {

        //public override void _onValidateBeforeSaving()
        //{
        //    // This is invoked on calling Save() i.e. while creation and updation
        //    base._onValidateBeforeSaving();
        //    // If validations are not successful throw new cValidationException(sReason);
        //    if (m_bCreating)
        //    {
        //        // Validations while creating a new object
        //        // TODO: 
        //    }
        //    else
        //    {
        //        // Validations while updating a new object
        //        // TODO: 
        //    }
        //}

        //protected override bool onSecurityCheck(int i_iAction)
        //{
        //    base.onSecurityCheck(i_iAction);
        //    // Perform any additional security checks that may be required before allowing the action to proceed
        //    // If security checks are not successful then throw new cInsufficientRightsException(CLASS_ID + ":" + i_iAction + "<-" + i_oToken.ToString());
        //    switch ((enEmpLeave_Action)i_iAction)
        //    {
        //        case enEmpLeave_Action.Create:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Delete:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Find:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Update:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Get:
        //            // TODO: 
        //            break;
        //        default:
        //            throw new cUnsupportedActionInvokedException("Unknown action: " + m_oClass.sFullName + ":" + i_iAction.ToString());
        //    }
        //    return true;
        //}
        //protected override bool onSecurityCheck(string i_sObjectID, int i_iAction)
        //{
        //    base.onSecurityCheck(i_sObjectID, i_iAction);
        //    // Perform any additional security checks that may be required before allowing the action to proceed on the specified object
        //    // If security checks are not successful then throw new cInsufficientRightsException(CLASS_ID + ":" + i_iAction + "<-" + i_oToken.ToString());
        //    switch ((enEmpLeave_Action)i_iAction)
        //    {
        //        case enEmpLeave_Action.Create:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Delete:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Find:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Update:
        //            // TODO: 
        //            break;
        //        case enEmpLeave_Action.Get:
        //            // TODO: 
        //            break;
        //        default:
        //            throw new cUnsupportedActionInvokedException("Unknown action: " + m_oClass.sFullName + ":" + i_iAction.ToString());
        //    }
        //    return true;
        //}
        //protected override string onGetSecurityScopeFilter(string i_sFilter, int i_iAction)
        //{
        //    //base.onGetSecurityScopeFilter(i_sFilter, i_iAction);
        //    // Return any additional scope filter (which will be used as a part of WHERE clause)
        //    // The returned filter condition will be added to any existing filter using AND
        //    // TODO:
        //    return string.Empty;
        //}
        public static DataTable GetEmpDetail(int LoginUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeCode");
            dt.Columns.Add("DepartmentType");
            dt.Columns.Add("Designation");
            dt.Columns.Add("Firstname");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginUserID;
            oDB.CallSPROC("uspGetLeaveData", a, dt);
            return dt;
        }
        public static DataTable GetPendinLeave(int LoginUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Days");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("LeaveType");
            dt.Columns.Add("Status");
            dt.Columns.Add("LeaveID");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginUserID;
            oDB.CallSPROC("uspPendingLeave", a, dt);
            return dt;
        }
        public static DataTable GetMyLeave(int LoginUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Days");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("LeaveType");
            dt.Columns.Add("Status");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginUserID;
            oDB.CallSPROC("uspMyLeave", a, dt);
            return dt;
        }
    }
}
