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
using System.Data.SqlClient;namespace HRIMS{        /// <summary>    /// Emp Goal    ///     /// 6/4/2015 11:49:22 AM    /// </summary>    public class cEmpGoal : cEmpGoal_Base    {        //public override void _onValidateBeforeSaving()        //{        //    // This is invoked on calling Save() i.e. while creation and updation        //    base._onValidateBeforeSaving();        //    // If validations are not successful throw new cValidationException(sReason);        //    if (m_bCreating)        //    {        //        // Validations while creating a new object        //        // TODO:         //    }        //    else        //    {        //        // Validations while updating a new object        //        // TODO:         //    }        //}        //protected override bool onSecurityCheck(int i_iAction)        //{        //    base.onSecurityCheck(i_iAction);        //    // Perform any additional security checks that may be required before allowing the action to proceed        //    // If security checks are not successful then throw new cInsufficientRightsException(CLASS_ID + ":" + i_iAction + "<-" + i_oToken.ToString());        //    switch ((enEmpGoal_Action)i_iAction)        //    {        //        case enEmpGoal_Action.Create:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Delete:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Find:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Update:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Get:        //            // TODO:         //            break;        //        default:        //            throw new cUnsupportedActionInvokedException("Unknown action: " + m_oClass.sFullName + ":" + i_iAction.ToString());        //    }        //    return true;        //}        //protected override bool onSecurityCheck(string i_sObjectID, int i_iAction)        //{        //    base.onSecurityCheck(i_sObjectID, i_iAction);        //    // Perform any additional security checks that may be required before allowing the action to proceed on the specified object        //    // If security checks are not successful then throw new cInsufficientRightsException(CLASS_ID + ":" + i_iAction + "<-" + i_oToken.ToString());        //    switch ((enEmpGoal_Action)i_iAction)        //    {        //        case enEmpGoal_Action.Create:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Delete:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Find:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Update:        //            // TODO:         //            break;        //        case enEmpGoal_Action.Get:        //            // TODO:         //            break;        //        default:        //            throw new cUnsupportedActionInvokedException("Unknown action: " + m_oClass.sFullName + ":" + i_iAction.ToString());        //    }        //    return true;        //}        //protected override string onGetSecurityScopeFilter(string i_sFilter, int i_iAction)        //{        //    //base.onGetSecurityScopeFilter(i_sFilter, i_iAction);        //    // Return any additional scope filter (which will be used as a part of WHERE clause)        //    // The returned filter condition will be added to any existing filter using AND        //    // TODO:        //    return string.Empty;        //}                public static string updateManagerFlag(int LoginID)
        {           
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginID;
            oDB.CallSPROC("uspUpdateManagerFlag", a);
            return "";
        }
        public static string updateManagerReviewRating(int LoginID)
        {
            List<SqlParameter> a = new List<SqlParameter>();
            a.Add(new SqlParameter("@LoginID", SqlDbType.Int));
            a[a.Count - 1].Value = LoginID;
            oDB.CallSPROC("uspUpdateManagerFlag", a);
            return "";
        }    }}