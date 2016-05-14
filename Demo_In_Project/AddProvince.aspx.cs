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

public partial class Demo_In_Project_AddProvince : BasePage
{
    CountryBLL country;
    ProvinceBLL province;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setcurenturl();
        if (!IsPostBack)
        {
            UserAccounts user = Session.GetCurrentUser();
            if (user == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {
                this.load_dlcountry();
                this.load_dlcheckcountry();
            }
        }
    }
    private void load_dlcountry()
    {
        country = new CountryBLL();
        this.load_DropdownList(dlcountry, country.getAllCountry(), "CountryName", "CountryID");
        dlcountry.Items.Insert(0, new ListItem("-- select country --", "0"));
    }

    protected void btntem_Click(object sender, EventArgs e)
    {
        province = new ProvinceBLL();
        string[] arrItem = txtprovince.Text.Split(';');
        if (kt(txtprovince.Text, Convert.ToInt32(dlcountry.SelectedValue)) > 0)
        {
            lblkoq.Text = "có trong bảng rồi pa. làm cái khác đi !";
        }
        else
        {
            for (int i = 0; i < arrItem.Length; i++)
            {
                //lblkoq.Text += arrItem[i]+"<br />";
                if (!string.IsNullOrWhiteSpace(arrItem[i]))
                {
                    this.province.NewProvince(arrItem[i], Convert.ToInt32(dlcountry.SelectedValue));
                }
            }
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        dlcountry.ClearSelection();
        txtprovince.Text = "";
        lblkoq.Text = "";
    }
    //=============
    private void load_dlcheckcountry()
    {
        country = new CountryBLL();
        this.load_DropdownList(dlcheckcountry, country.getAllCountry(), "CountryName", "CountryID");
        dlcheckcountry.Items.Insert(0, new ListItem("-- select country --", "0"));
    }
    protected void dlcheckcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        province = new ProvinceBLL();
        gwProvice.DataSource = province.getProvinceWithCid(Convert.ToInt32(dlcheckcountry.SelectedValue));
        gwProvice.DataBind();
    }
    //Kiem tra
    private int kt(string lst, int cid)
    {
        int k = 0;
        string[] arrItem = lst.Split(';');
        for (int i = 0; i < arrItem.Length; i++)
        {
            if(!kttrung(arrItem[i], cid))
            {
                k = k + 1;
            }
        }
        return k;
    }
    private Boolean kttrung(string name,int cid)
    {
        province = new ProvinceBLL();
        List<Province> lst = province.getProvinceWithProvinceName(name, cid);
        Province pro = lst.FirstOrDefault();
        if (pro != null)
        {
            return false;
        }
        return true;
    }
}