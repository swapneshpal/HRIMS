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
    /// Quadrant Measure
    /// 
    /// 6/1/2015 12:03:32 PM
    /// </summary>
    public class cQuadrantMeasure : cQuadrantMeasure_Base
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
        //    switch ((enQuadrantMeasure_Action)i_iAction)
        //    {
        //        case enQuadrantMeasure_Action.Create:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Delete:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Find:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Update:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Get:
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
        //    switch ((enQuadrantMeasure_Action)i_iAction)
        //    {
        //        case enQuadrantMeasure_Action.Create:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Delete:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Find:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Update:
        //            // TODO: 
        //            break;
        //        case enQuadrantMeasure_Action.Get:
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

        public static DataTable getEmpQuadratants(int LoginUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GoalName");
            dt.Columns.Add("Measures");
            dt.Columns.Add("GoalID");
            dt.Columns.Add("ManagerComment");
            dt.Columns.Add("ManagerFlag");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginUserID;
            oDB.CallSPROC("uspEmpQuadrantMeasures", a, dt);
            return dt;
        }
        public static DataTable getEmpQuadratantList(int UserID, int userType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("Department");
            dt.Columns.Add("Designation");
            dt.Columns.Add("empId");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = UserID;
            a.Add(new SqlParameter("@userType", SqlDbType.Int));
            a[a.Count - 1].Value = userType;
            oDB.CallSPROC("uspEmployeeQuadrantList", a, dt);
            return dt;
        }
        public static DataTable reviewEmpQuadratants(int UserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GoalName");
            dt.Columns.Add("Measures");
            dt.Columns.Add("GoalID");
            dt.Columns.Add("statusID");
            dt.Columns.Add("StatusName");
            dt.Columns.Add("empcomment");
            dt.Columns.Add("EmpGoalID");
            dt.Columns.Add("ManagerComment");
            dt.Columns.Add("ManagerFlag");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@UserID", SqlDbType.Int));
            a[a.Count - 1].Value = UserID;
            oDB.CallSPROC("uspReviewEmployeeQuadrantList", a, dt);
            return dt;
        }
        public static DataTable getEmployeeManagerName(int LoginID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("ManagerName");           
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginID;
            oDB.CallSPROC("uspGetEmployeeManagerName", a, dt);
            return dt;
        }
        public static string updateEmployeeAcceptance(int LoginID, string empAcceptance, string empSignature)
        {

            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginID", SqlDbType.Int));
            a.Add(new SqlParameter("@empAcceptance", SqlDbType.Text));
            a.Add(new SqlParameter("@empSignature", SqlDbType.Text));
            a[a.Count - 3].Value = LoginID;
            a[a.Count - 2].Value = empAcceptance;
            a[a.Count - 1].Value = empSignature;
            oDB.CallSPROC("updateEmployeeAcceptance", a);
            return "";
        }
        public static DataTable getReviewRatingData(int LoginID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ManagerComment");
            dt.Columns.Add("ManagerSignature");
            dt.Columns.Add("EmpAcceptance");
            dt.Columns.Add("EmpFlag");
            dt.Columns.Add("RatingID");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginID;
            oDB.CallSPROC("uspGetReviewRatingData", a, dt);
            return dt;
        }
        public static DataTable getEmpDevelopmentGoals(int LoginUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GoalName");
            dt.Columns.Add("ActionRequired");
            dt.Columns.Add("Tracking");
            dt.Columns.Add("StatusID");
            dt.Columns.Add("DevID");
            dt.Columns.Add("ManagerComment");
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginUserID;
            oDB.CallSPROC("uspEmpDevelopmentGoals", a, dt);
            return dt;
        }
        public static DataTable getEmpSummaryComments(int LoginUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SummaryComments");           
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginUserID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginUserID;
            oDB.CallSPROC("uspEmpSummaryComments", a, dt);
            return dt;
        }
            
    }
}
