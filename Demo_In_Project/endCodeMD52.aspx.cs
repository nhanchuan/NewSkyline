using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo_In_Project_endCodeMD52 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            
        }
    }

    protected void btnEncoer_Click(object sender, EventArgs e)
    {
        txtEncoder.Text = endCodeMD52(txtInput.Text);
    }
    public string endCodeMD52(string pas_)
    {

        if (string.IsNullOrEmpty(pas_))
        {
            return string.Empty;
        }

        using (MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider())
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(pas_);
            Byte[] hashedBytes = hasher.ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }
        
    }
}