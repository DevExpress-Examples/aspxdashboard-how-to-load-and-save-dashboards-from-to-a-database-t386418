using System;
using System.Web.Hosting;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.Native.DashboardRestfulService;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.DashboardWeb;
using System.Configuration;
using T386418;


public partial class Global : System.Web.HttpApplication {
    protected void Application_Start(object sender, EventArgs e) {
        DataBaseEditaleDashboardStorage dataBaseDashboardStorage = new DataBaseEditaleDashboardStorage(ConfigurationManager.
    ConnectionStrings["DashboardStorageConnection"].ConnectionString);

        DashboardService.SetDashboardStorage(dataBaseDashboardStorage);

        DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source");
        TableQuery customerReportsQuery = new TableQuery("CustomerReports");
        customerReportsQuery.AddTable("CustomerReports").SelectColumns("CompanyName", "ProductName", "OrderDate", "ProductAmount");
        sqlDataSource.Queries.Add(customerReportsQuery);

        DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
        dataSourceStorage.RegisterDataSource("sqlDataSource1", sqlDataSource.SaveToXml());
        DashboardService.SetDataSourceStorage(dataSourceStorage);
        DashboardService.DataApi.ConfigureDataConnection += DataApi_ConfigureDataConnection;
    }

    void DataApi_ConfigureDataConnection(object sender, ServiceConfigureDataConnectionEventArgs e)
    {
        if (e.DataSourceName == "SQL Data Source")
        {
            Access97ConnectionParameters accessParams = new  Access97ConnectionParameters();
            accessParams.FileName = HostingEnvironment.MapPath("~/App_Data/nwind.mdb");
            e.ConnectionParameters = accessParams;
        }
    }

    protected void Session_Start(object sender, EventArgs e) {
    }
    protected void Application_BeginRequest(object sender, EventArgs e) {
    }
    protected void Application_AuthenticateRequest(object sender, EventArgs e) {
    }
    protected void Application_Error(object sender, EventArgs e) {
    }
    protected void Session_End(object sender, EventArgs e) {
    }
    protected void Application_End(object sender, EventArgs e) {
    }
}