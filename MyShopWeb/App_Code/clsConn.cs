using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

/// <summary>
/// clsConn 的摘要描述
/// </summary>
public class clsConn
{
    public clsConn()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public SqlConnection GetConnection()
    {
        /*
        SqlConnectionStringBuilder ConnectionStringBuild = new SqlConnectionStringBuilder();
        ConnectionStringBuild.DataSource = "BENSONWORK";
        ConnectionStringBuild.InitialCatalog = "ShopWeb";
        ConnectionStringBuild.IntegratedSecurity = true;
        ConnectionStringBuild.PersistSecurityInfo = true;
        ConnectionStringBuild.UserID = "sa";
        ConnectionStringBuild.Password = "sa";
        string ConnStr = ConnectionStringBuild.ConnectionString;*/
        string ConnStr = "Data Source=RPaGearNB;Initial Catalog=ShopWeb;User ID=sa;Password=sa";
        SqlConnection Conn = new SqlConnection(ConnStr);
        Conn.Open();

        return Conn;
    }

    public bool CloseConnectionObject(SqlConnection objConnction)
    {
        if(objConnction.State == ConnectionState.Open ){
                objConnction.Close();
                objConnction.Dispose();
                objConnction = null;
        }


        return true;

    }
}