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

public partial class Demo_In_Project_AddDistrict : BasePage
{
    CountryBLL country;
    ProvinceBLL province;
    DistrictBLL district;
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
                this.load_dlcheckCountry();
            }
        }
    }
    private void load_dlcountry()
    {
        country = new CountryBLL();
        this.load_DropdownList(dlcountry, country.getAllCountry(), "CountryName", "CountryID");
        dlcountry.Items.Insert(0, new ListItem("-- select country --", "0"));
    }
    protected void dlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        province = new ProvinceBLL();
        this.load_DropdownList(dlProvince, province.getProvinceWithCid(Convert.ToInt32(dlcountry.SelectedValue)), "ProvinceName", "ProvinceID");
        dlProvince.Items.Insert(0, new ListItem("-- select Province --", "0"));
    }
    protected void btntem_Click(object sender, EventArgs e)
    {
        district = new DistrictBLL();
        //province = new ProvinceBLL();
        string[] arrItem = txtDistrict.Text.Split(';');
        if (kt(txtDistrict.Text, Convert.ToInt32(dlProvince.SelectedValue)) > 0)
        {
            lblcheckk.Text = "Có trong bảng rồi pa. đừng cố gắng thêm. không ít gì đâu !";
        }
        else
        {
            for (int i = 0; i < arrItem.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(arrItem[i]))
                {
                    this.district.NewDistrict(arrItem[i], Convert.ToInt32(dlProvince.SelectedValue));
                }
            }
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        dlcountry.ClearSelection();
        dlProvince.ClearSelection();
        txtDistrict.Text = "";
        lblcheckk.Text = "";
    }
    //==============================================================================================
    private void load_dlcheckCountry()
    {
        country = new CountryBLL();
        this.load_DropdownList(dlcheckCountry, country.getAllCountry(), "CountryName", "CountryID");
        dlcheckCountry.Items.Insert(0, new ListItem("-- select country --", "0"));
    }
    protected void dlcheckCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        province = new ProvinceBLL();
        this.load_DropdownList(dlcheckProvinces, province.getProvinceWithCid(Convert.ToInt32(dlcheckCountry.SelectedValue)), "ProvinceName", "ProvinceID");
        dlcheckProvinces.Items.Insert(0, new ListItem("-- select Province --", "0"));
    }

    protected void dlcheckProvinces_SelectedIndexChanged(object sender, EventArgs e)
    {
        district = new DistrictBLL();
        gwdata.DataSource = district.getDistrictwithProid(Convert.ToInt32(dlcheckProvinces.SelectedValue));
        gwdata.DataBind();
    }
    //Kiem tra
    private int kt(string lst, int pro)
    {
        int k = 0;
        string[] arrItem = lst.Split(';');
        for (int i = 0; i < arrItem.Length; i++)
        {
            if (!kttrung(arrItem[i], pro))
            {
                k = k + 1;
            }
        }
        return k;
    }
    private Boolean kttrung(string name, int pro)
    {
        district = new DistrictBLL();
        List<District> lst = district.getLstwithNameProID(name, pro);
        District dis = lst.FirstOrDefault();
        if (dis != null)
        {
            return false;
        }
        return true;
    }
}