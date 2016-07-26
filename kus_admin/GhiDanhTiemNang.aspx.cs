using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;

public partial class kus_admin_GhiDanhTiemNang : BasePage
{
    REGISTRATION_FORM_ADVISORY_BLL registration_form;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setcurenturl();
        if (!IsPostBack)
        {
            UserAccounts ac = Session.GetCurrentUser();
            if (ac == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {

                if (!check_permiss(ac.UserID, FunctionName.GhiDanhHocVien))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {

                }
            }
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchHocVienCode(string prefixText, int count)
    {
        kus_HocVienBLL kus_hocvien = new kus_HocVienBLL();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["connectionStrCon"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from REGISTRATION_FORM_ADVISORY where FullName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> lstHVCode = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lstHVCode.Add(sdr["FullName"].ToString());
                    }
                }
                conn.Close();
                return lstHVCode;
            }
        }
    }

    protected void txtPhieuTvInfor_TextChanged(object sender, EventArgs e)
    {
        try
        {
            registration_form = new REGISTRATION_FORM_ADVISORY_BLL();
            REGISTRATION_FORM_ADVISORY phieutv = registration_form.GET_REGISTRATION_FORM_ADVISORY_ByFullName(txtPhieuTvInfor.Text).FirstOrDefault();
            txtLastNameHV.Text = phieutv.FullName.Substring(0, phieutv.FullName.LastIndexOf(" "));
            txtFirstNameHV.Text = phieutv.FullName.Substring(phieutv.FullName.LastIndexOf(" ") + 1);
            txtNgaySinh.Text = phieutv.Birthday.ToString("dd-MM-yyyy");
            if (phieutv.Sex == 1)
            {
                rdformnam.Checked = true;
            }
            else
            {
                if (phieutv.Sex == 2)
                {
                    rdformnu.Checked = true;
                }
            }
            txtEmail.Text = phieutv.Email;
            txtPhoneHV.Text = phieutv.Phone;

            txtDCTamTru.Text = phieutv.Address_form;
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}