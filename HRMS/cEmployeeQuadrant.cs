using System;
using System.Data.SqlClient;
using System.Data;
        public static string updateManagerFlag(int LoginID)
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
        }