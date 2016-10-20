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
using System.Globalization;

public partial class kus_admin_Promotions : BasePage
{
    PromotionsBLL promotions;
    public int PageSize = 20;
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
                this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                txtDiscount.ReadOnly = true;
                this.GetPromotionsPageWise(1);
                btneditpromotion.Attributes.Add("class", "btn btn-warning disabled");
            }
        }
    }

    protected void btnSavePromotions_Click(object sender, EventArgs e)
    {
        try
        {
            promotions = new PromotionsBLL();

            string promcode = txtPromCode.Text;
            string promname = txtPromName.Text;
            int rate = (txtReducedRate.Text.Length == 0) ? 0 : Convert.ToInt32(txtReducedRate.Text);
            int discount = (txtDiscount.Text.Length == 0) ? 0 : Convert.ToInt32(txtDiscount.Text);

            string stdate = txtStartDate.Text;
            string edate = txtEndDate.Text;

            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            DateTime startdate;
            if (DateTime.TryParseExact(stdate, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out startdate) || getday(stdate) == "" || getmonth(stdate) == "" || getyear(stdate) == "")
            {
                startdate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
            }
            else
            {
                //lblstartdateFalse.Text = "";
                startdate = DateTime.ParseExact(getday(stdate) + "/" + getmonth(stdate) + "/" + getyear(stdate), "dd/MM/yyyy", null);
            }
            DateTime enddate;
            if (DateTime.TryParseExact(edate, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out enddate) || getday(edate) == "" || getmonth(edate) == "" || getyear(edate) == "")
            {
                enddate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
            }
            else
            {
                //lblEnddatefalse.Text = "";
                enddate = DateTime.ParseExact(getday(edate) + "/" + getmonth(edate) + "/" + getyear(edate), "dd/MM/yyyy", null);
            }


            if (promotions.NewPromotions(promcode, promname, rate, discount, startdate, enddate, chkIsActive.Checked))
            {
                //this.AlertPageValid(true, "New Success !", alertPageValid, lblPageValid);
                this.GetPromotionsPageWise(1);
            }
            else
            {
                this.AlertPageValid(true, "New Promotions False !", alertPageValid, lblPageValid);
            }


        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void btniscustom_ServerClick(object sender, EventArgs e)
    {
        try
        {
            //txtDiscount.ReadOnly = false;
            //txtReducedRate.Text = "";
            //txtReducedRate.ReadOnly = true;

            if (txtDiscount.ReadOnly == true)
            {
                txtReducedRate.ReadOnly = true;
                txtDiscount.ReadOnly = false;
                txtReducedRate.Text = "";
            }
            else
            {
                if (txtDiscount.ReadOnly == false)
                {
                    txtReducedRate.ReadOnly = false;
                    txtDiscount.ReadOnly = true;
                    txtDiscount.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    private void GetPromotionsPageWise(int pageIndex)
    {
        promotions = new PromotionsBLL();
        int recordCount = 0;
        gwPromotions.DataSource = promotions.GetPromotionsPageWise(pageIndex, PageSize);
        recordCount = promotions.CountPromotions();
        gwPromotions.DataBind();
        this.PopulatePager(rptPager, recordCount, pageIndex, PageSize);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetPromotionsPageWise(pageIndex);
    }


    protected void gwPromotions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btneditpromotion.Attributes.Add("class", "btn btn-warning");
            int promoID = Convert.ToInt32((gwPromotions.SelectedRow.FindControl("lblPromotionID") as Label).Text);
            promotions = new PromotionsBLL();

            Promotions promo = promotions.ListByPromotionID(promoID).FirstOrDefault();
            if(promo!=null)
            {
                txtEPromCode.Text = promo.PromCode;
                txtEPromName.Text = promo.PromName;

                if(promo.ReducedRate==0)
                {
                    txtEReducedRate.ReadOnly = true;
                    txtEReducedRate.Text = "";
                }
                else
                {
                    txtEReducedRate.ReadOnly = false;
                    txtEReducedRate.Text = promo.ReducedRate.ToString();
                }

                if (promo.Discount == 0)
                {
                    txtEDiscount.ReadOnly = true;
                    txtEDiscount.Text = "";
                }
                else
                {
                    txtEDiscount.ReadOnly = false;
                    txtEDiscount.Text = promo.Discount.ToString();
                }


                txtEStartDate.Text = promo.StartDate.ToString("dd-MM-yyyy");
                txtEEndDate.Text = promo.EndDate.ToString("dd-MM-yyyy");
                chkEIsActive.Checked = promo.IsActive;

            }




        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void btnSaveEditPromotion_Click(object sender, EventArgs e)
    {
        try
        {
            promotions = new PromotionsBLL();
            int promoID = Convert.ToInt32((gwPromotions.SelectedRow.FindControl("lblPromotionID") as Label).Text);
            string promcode = txtEPromCode.Text;
            string promname = txtEPromName.Text;
            int rate = (txtEReducedRate.Text.Length == 0) ? 0 : Convert.ToInt32(txtEReducedRate.Text);
            int discount = (txtEDiscount.Text.Length == 0) ? 0 : Convert.ToInt32(txtEDiscount.Text);

            string stdate = txtEStartDate.Text;
            string edate = txtEEndDate.Text;

            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            DateTime startdate;
            if (DateTime.TryParseExact(stdate, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out startdate) || getday(stdate) == "" || getmonth(stdate) == "" || getyear(stdate) == "")
            {
                startdate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
            }
            else
            {
                //lblstartdateFalse.Text = "";
                startdate = DateTime.ParseExact(getday(stdate) + "/" + getmonth(stdate) + "/" + getyear(stdate), "dd/MM/yyyy", null);
            }
            DateTime enddate;
            if (DateTime.TryParseExact(edate, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out enddate) || getday(edate) == "" || getmonth(edate) == "" || getyear(edate) == "")
            {
                enddate = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
            }
            else
            {
                //lblEnddatefalse.Text = "";
                enddate = DateTime.ParseExact(getday(edate) + "/" + getmonth(edate) + "/" + getyear(edate), "dd/MM/yyyy", null);
            }

            if(promotions.UpdatePromotions(promoID,promcode,promname,rate,discount,startdate,enddate,chkEIsActive.Checked))
            {
                this.GetPromotionsPageWise(1);
            }
            else
            {
                this.AlertPageValid(true, "Update false . Erorr to connect server !", alertPageValid, lblPageValid);
            }

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwPromotions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("linkBtnDel") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Bạn chắc chắn muốn xóa ?')");
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwPromotions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            promotions = new PromotionsBLL();
            int promoID = Convert.ToInt32((gwPromotions.Rows[e.RowIndex].FindControl("lblPromotionID") as Label).Text);
            if (promotions.DeleteByPromotionID(promoID))
            {
                this.GetPromotionsPageWise(1);
            }
            else
            {
                this.AlertPageValid(true, "Delete false . Erorr to connect server !", alertPageValid, lblPageValid);
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void btnswitchedit_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (txtEDiscount.ReadOnly == true)
            {
                txtEReducedRate.ReadOnly = true;
                txtEDiscount.ReadOnly = false;
                txtEReducedRate.Text = "";
            }
            else
            {
                if (txtEDiscount.ReadOnly == false)
                {
                    txtEReducedRate.ReadOnly = false;
                    txtEDiscount.ReadOnly = true;
                    txtEDiscount.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}

