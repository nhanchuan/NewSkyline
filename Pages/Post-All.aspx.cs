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

public partial class Pages_Post_All : BasePage
{
    PostBLL post;
    Post_Category_relationshipsBLL post_category_relationships;
    CategoryBLL category;
    Tags_relationshipsBLL tagsrelationships;
    private int PageSize = 20;
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
                if (!check_permiss(ac.UserID, FunctionName.PostManager))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    if (HasPermission(ac.UserID, FunctionName.PostManager, TypeAudit.View))
                    {
                        PostManager.Attributes.Add("class", "row");
                        //do somewthing
                        this.load_dlCategory();
                        this.GetPostPageWise(1, 0);
                        lblstartindex.Text = ((1 - 1) * PageSize + 1).ToString();
                        lblendindex.Text = ((((1 - 1) * PageSize + 1) + PageSize) - 1).ToString();
                    }
                    else
                    {
                        PostManager.Attributes.Add("class", "row display-none");
                        this.AlertPageValid(true, "Bạn không có quyền xem nội dung này !", alertPageValid, lblPageValid);
                    }

                }
            }
        }
    }
    private void load_dlCategory()
    {
        category = new CategoryBLL();
        dlCategory.DataSource = category.getTBCategoryForDL();
        dlCategory.DataTextField = "CTName";
        dlCategory.DataValueField = "CategoryID";
        dlCategory.DataBind();
        dlCategory.Items.Insert(0, new ListItem("-- Chọn danh mục --", "0"));
    }
    private void GetPostPageWise(int pageIndex, int categoryID)
    {
        post = new PostBLL();
        gwPostmanager.DataSource = new ObjectDataSource();
        int recordCount = 0;
        if (categoryID <= 0)
        {
            gwPostmanager.DataSource = post.GetAllPostPageWise(pageIndex, PageSize);
            recordCount = post.CountPost();
        }
        else
        {
            gwPostmanager.DataSource = post.GetPostWithCategoryPageWise(pageIndex, PageSize, categoryID);
            recordCount = post.CountPostCategory(categoryID);
        }

        gwPostmanager.DataBind();
        this.PopulatePager(rptPager, recordCount, pageIndex, PageSize);
        lbltotalPost.Text = recordCount.ToString();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetPostPageWise(pageIndex, 0);
        lblstartindex.Text = ((pageIndex - 1) * PageSize + 1).ToString();
        lblendindex.Text = ((((pageIndex - 1) * PageSize + 1) + PageSize) - 1).ToString();
    }
    protected void gwPostmanager_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("linkBtnDelPostItem") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Xóa có thể dẫn tới lỗi hệ thống. Bạn có chắc muốn xóa nữa không ? OK -> Bạn chịu trách nhiệm || Cancel -> coi như không có gì xảy ra !')");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.ToString() + "')</script>");
        }
    }
    protected void gwPostmanager_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session.GetCurrentUser() == null)
        {
            Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
        }
        else
        {
            if (HasPermission(Session.GetCurrentUser().UserID, FunctionName.PostManager, TypeAudit.Delete))
            {
                post = new PostBLL();
                tagsrelationships = new Tags_relationshipsBLL();
                post_category_relationships = new Post_Category_relationshipsBLL();
                int postID = Convert.ToInt32((gwPostmanager.Rows[e.RowIndex].FindControl("lblPostID") as Label).Text);
                bool delTags = this.tagsrelationships.DeleteWithPostId(postID);
                bool deletepostCT = this.post_category_relationships.DeleteWithPostId(Convert.ToInt32(postID));
                bool delPost = this.post.DeleteWithPostID(Convert.ToInt32(postID));
                if (!delTags || !deletepostCT || !delPost)
                {
                    Response.Write("<script>alert('Xóa Bài Viết thất bại. Lỗi kết nối csdl !')</script>");
                }
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                Response.Write("<script>alert('Bạn không có quyền thực hiện chức năng này !')</script>");
            }
        }

    }
    protected void dlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int categoryID = Convert.ToInt32(dlCategory.SelectedValue);
        this.GetPostPageWise(1, categoryID);
    }
}