using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Reportdata
/// </summary>
public class Reportdata
{
    


         //public ReportDocument Report(ReportDocument Rptdoc, string str, SqlConnection con, string st, string FileName, CrystalReportViewer Id)
         //{          
         //    dt = getdata(st, con);
         //    try
         //    {
         //        ParameterField paramField = new ParameterField();
         //        ParameterFields paramFields = new ParameterFields();                
         //        ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
         //        Rptdoc.Load(HttpContext.Current.Server.MapPath(FileName));
         //        Rptdoc.SetDataSource(dt);
         //        paramField.Name = "tittle";
         //        paramDValue.Value = str;
         //        paramField.CurrentValues.Add(paramDValue);
         //        paramField.HasCurrentValue = true;
         //        paramFields.Add(paramField);
         //        Id.ReportSource = Rptdoc;
         //        Id.ParameterFieldInfo = paramFields;
         //        Id.EnableDatabaseLogonPrompt = false;
         //        Id.EnableParameterPrompt = false;
                
         //    }
         //    catch (NullReferenceException ex)
         //    {
         //        Id.Visible = false;

         //    }
         //    catch (IndexOutOfRangeException ex)
         //    {
         //        Id.Visible = false;

         //    }
         //    catch (ObjectDisposedException ex)
         //    {
         //        Id.Visible = false;
         //    }

         //    catch (ArgumentNullException ex)
         //    {
         //        Id.Visible = false;
         //    }
         //    catch (COMException ex)
         //    {
         //       HttpContext.Current.Response.Redirect("../../Login.aspx");
         //    }
         //    return Rptdoc;

         //}




    // public ReportDocument Report(ReportDocument Rptdoc, SqlConnection con, string st, string FileName, CrystalReportViewer Id, ParameterFields paramFields)
    //{
    //    dt = getdata(st, con);
    //    try
    //    {
          
    //        ParameterDiscreteValue paramDValue = new ParameterDiscreteValue();
    //        Rptdoc.Load(HttpContext.Current.Server.MapPath(FileName));
    //        Rptdoc.SetDataSource(dt);          
    //        Id.ReportSource = Rptdoc;
    //        Id.ParameterFieldInfo = paramFields;
    //        Id.EnableDatabaseLogonPrompt = false;
    //        Id.EnableParameterPrompt = false;

    //    }
    //    catch (NullReferenceException ex)
    //    {
    //        Id.Visible = false;

    //    }
    //    catch (IndexOutOfRangeException ex)
    //    {
    //        Id.Visible = false;

    //    }
    //    catch (ObjectDisposedException ex)
    //    {
    //        Id.Visible = false;
    //    }

    //    catch (ArgumentNullException ex)
    //    {
    //        Id.Visible = false;
    //    }
    //    catch (COMException ex)
    //    {
    //        HttpContext.Current.Response.Redirect("../../Login.aspx");
    //    }
    //    return Rptdoc;

    //}

}