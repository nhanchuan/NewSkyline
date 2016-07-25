﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class QuanLyHoSo_CapNhatThongTinKhachHang : BasePage
{
    CustomerBasicInfoBLL customerbasicinfo;
    CustomerProfilePrivateBLL customerProPri;
    CustomerProfileMoreInforBLL customerProfileMoreInfor;
    CustomerProfileTypeInforBLL customerProfileTypeInfor;
    REGISTRATION_FORM_ADVISORY_BLL registrationForm;
    CountryBLL country;
    ProvinceBLL province;
    DistrictBLL district;
    Registration_TypeBLL regisType;
    EducationLVBLL educationlvl;
    CountryAdvisoryBLL coutryadv;
    EmployeesBLL employees;
    UserProfileBLL userprofile;
    ImagesBLL images;
    ImagesTypeBLL imagestype;
    UserAccountsBLL useraccount;
    ListOfNationalitiesBLL lstOfNationalities;
    EthnicListBLL ethniclist;
    ListOfReligionBLL lstOfReligoin;
    BloodGroupBLL bloodgroup;
    BagProfileTypeBLL bagprofiletype;
    ThongTinPhuHuynhBLL thongtinphuhuynh;

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
                if (!check_permiss(user.UserID, FunctionName.UpdateThongTinHoSoKhachHang))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    string BaseCode = Request.QueryString["FileCode"];

                    if (string.IsNullOrEmpty(BaseCode) || !CheckQueryStr(BaseCode))
                    {
                        Response.Redirect("http://" + Request.Url.Authority + "/QuanLyHoSo/ThuLyHoSo.aspx");
                    }
                    else
                    {
                        //do something
                        this.getFormInfor(BaseCode);
                        this.load_dlUserUploadImg();
                        this.load_dlEdulvl();
                        this.load_dlEduCountry();
                        dlimgtype.Items.Insert(0, new ListItem("-- Hình ảnh tải lên bởi thành viên --", "0"));
                        dlimgtype.Items.FindByValue(user.UserID.ToString()).Selected = true;
                        this.load_rpLstImg();

                        this.load_FormAdvisory(BaseCode);

                        customerbasicinfo = new CustomerBasicInfoBLL();
                        customerProPri = new CustomerProfilePrivateBLL();
                        images = new ImagesBLL();
                        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
                        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
                        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(bsInfo.InfoID);
                        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();
                        List<Images> lstImg = images.getImagesWithId(CusPri.ProfileImg);
                        Images img = lstImg.FirstOrDefault();
                        imgCusprofile.Src = (img == null) ? "../images/default_images.jpg" : "../" + img.ImagesUrl;
                    }

                }

            }
        }
    }
    private string CountryStr(int countryId)
    {
        country = new CountryBLL();
        List<Country> lst = country.getCountryWithId(countryId);
        Country cty = lst.FirstOrDefault();
        return (cty == null) ? "" : cty.CountryName;
    }
    private string ProvinceStr(int provinceId)
    {
        province = new ProvinceBLL();
        List<Province> lst = province.getProvinceWithProId(provinceId);
        Province priv = lst.FirstOrDefault();
        return (priv == null) ? "" : priv.ProvinceName;
    }
    private string DistrictStr(int disID)
    {
        district = new DistrictBLL();
        List<District> lst = district.getDistrictwithDisId(disID);
        District dis = lst.FirstOrDefault();
        return (dis == null) ? "" : dis.DistrictName;
    }
    private void load_dlNationality()
    {
        lstOfNationalities = new ListOfNationalitiesBLL();
        dlNationality.DataSource = lstOfNationalities.getAllListOfNationalities();
        dlNationality.DataTextField = "NationalityName";
        dlNationality.DataValueField = "NationalityID";
        dlNationality.DataBind();
    }
    private void load_dlEthnic()
    {
        ethniclist = new EthnicListBLL();
        dlEthnic.DataSource = ethniclist.GetallEthnicList();
        dlEthnic.DataValueField = "EthnicID";
        dlEthnic.DataTextField = "EthnicName";
        dlEthnic.DataBind();
    }
    private void load_dlEthnic(int NaID)
    {
        ethniclist = new EthnicListBLL();
        dlEthnic.DataSource = ethniclist.GetallEthnicListWithNationID(NaID);
        dlEthnic.DataValueField = "EthnicID";
        dlEthnic.DataTextField = "EthnicName";
        dlEthnic.DataBind();
    }
    private void load_dlReligionID()
    {
        lstOfReligoin = new ListOfReligionBLL();
        dlReligion.DataSource = lstOfReligoin.getAllListOfReligion();
        dlReligion.DataTextField = "ReligionName";
        dlReligion.DataValueField = "ReligionID";
        dlReligion.DataBind();
    }
    private void load_dlCountrys()
    {
        country = new CountryBLL();
        dlCountry.DataSource = country.getAllCountry();
        dlCountry.DataTextField = "CountryName";
        dlCountry.DataValueField = "CountryID";
        dlCountry.DataBind();
    }
    private void load_dlBloodGroup()
    {
        bloodgroup = new BloodGroupBLL();
        dlBloodGroup.DataSource = bloodgroup.GetAllBloodGroup();
        dlBloodGroup.DataTextField = "BloodName";
        dlBloodGroup.DataValueField = "BloodID";
        dlBloodGroup.DataBind();
    }
    private void load_dlBagProfileType()
    {
        bagprofiletype = new BagProfileTypeBLL();
        dlBagProfileType.DataSource = bagprofiletype.getAllBagProfileType();
        dlBagProfileType.DataTextField = "TypeName";
        dlBagProfileType.DataValueField = "BagProfileTypeID";
        dlBagProfileType.DataBind();
    }
    protected void dlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        province = new ProvinceBLL();
        dlProvince.DataSource = province.getProvinceWithCid(int.Parse(dlCountry.SelectedValue));
        dlProvince.DataTextField = "ProvinceName";
        dlProvince.DataValueField = "ProvinceID";
        dlProvince.DataBind();
        dlProvince.Items.Insert(0, new ListItem("-- Select Province --", "0"));
        dlDistrict.DataSource = null;
        district = new DistrictBLL();
        dlDistrict.DataSource = district.getDistrictwithProid(0);
        dlDistrict.DataTextField = "DistrictName";
        dlDistrict.DataValueField = "DistrictID";
        dlDistrict.DataBind();
        dlDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));
    }
    protected void dlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        district = new DistrictBLL();
        dlDistrict.DataSource = district.getDistrictwithProid(int.Parse(dlProvince.SelectedValue));
        dlDistrict.DataTextField = "DistrictName";
        dlDistrict.DataValueField = "DistrictID";
        dlDistrict.DataBind();
        dlDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));
    }
    public Boolean CheckQueryStr(string query)
    {
        customerbasicinfo = new CustomerBasicInfoBLL();
        List<CustomerBasicInfo> lsCustBas = customerbasicinfo.GetCusBasicInfoWithCode(query);
        CustomerBasicInfo bs = lsCustBas.FirstOrDefault();
        if (bs == null)
        {
            return false;
        }
        return true;
    }
    private string RegisFormTypeStr(int typeId)
    {
        regisType = new Registration_TypeBLL();
        List<Registration_Type> lst = regisType.getAllRegistration_TypeWithId(typeId);
        Registration_Type rt = lst.FirstOrDefault();
        return (rt == null) ? "" : rt.TypeName;
    }
    private string EducationSrt(int EdiId)
    {
        educationlvl = new EducationLVBLL();
        List<EducationLV> lst = educationlvl.getallEducationLVWithId(EdiId);
        EducationLV edu = lst.FirstOrDefault();
        return (edu == null) ? "" : edu.NAME;
    }
    private string CountryAdvStr(int CountryId)
    {
        country = new CountryBLL();
        List<Country> lst = country.getCountryWithId(CountryId);
        Country ct = lst.FirstOrDefault();
        return (ct == null) ? "" : ct.CountryName;
    }
    //===Lay thong tin phieu tu van===
    private void load_FormAdvisory(string FileCode)
    {
        registrationForm = new REGISTRATION_FORM_ADVISORY_BLL();
        customerbasicinfo = new CustomerBasicInfoBLL();
        customerProPri = new CustomerProfilePrivateBLL();
        images = new ImagesBLL();
        province = new ProvinceBLL();
        district = new DistrictBLL();

        this.load_dlNationality(); //--GET ALL NATIONALITY
        dlNationality.Items.Insert(0, new ListItem("-- Quốc Tịch --", "0"));
        this.load_dlEthnic();
        dlEthnic.Items.Insert(0, new ListItem("-- Dân tộc --", "0"));
        this.load_dlReligionID();
        dlReligion.Items.Insert(0, new ListItem("-- Tôn Giáo --", "0"));
        this.load_dlCountrys();
        dlCountry.Items.Insert(0, new ListItem("-- Select Country --", "0"));
        this.load_dlBloodGroup();
        dlBloodGroup.Items.Insert(0, new ListItem("-- Nhóm máu --", "0"));
        this.load_dlBagProfileType();
        dlBagProfileType.Items.Insert(0, new ListItem("-- Loại hồ sơ --", "0"));

        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(FileCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(bsInfo.InfoID);
        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();
        List<REGISTRATION_FORM_ADVISORY> lstReF = registrationForm.getREGISTRATION_FORM_ADVISORY_WithId(CusPri.RegistrationID);
        REGISTRATION_FORM_ADVISORY regForm = lstReF.FirstOrDefault();
        List<Images> lstImg = images.getImagesWithId(CusPri.ProfileImg);
        Images img = lstImg.FirstOrDefault();

        dlProvince.DataSource = province.getAllProvince();
        dlProvince.DataTextField = "ProvinceName";
        dlProvince.DataValueField = "ProvinceID";
        dlProvince.DataBind();
        dlProvince.Items.Insert(0, new ListItem("-- Select Province --", "0"));
        dlDistrict.DataSource = null;

        dlDistrict.DataSource = district.getallDistrict();
        dlDistrict.DataTextField = "DistrictName";
        dlDistrict.DataValueField = "DistrictID";
        dlDistrict.DataBind();
        dlDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));

        if (regForm != null)
        {
            lblAdvFullName.Text = regForm.FullName;
            lbladvAddress.Text = regForm.Address_form + ", " + DistrictStr(regForm.DistrictID) + ", " + ProvinceStr(regForm.ProvinceID) + ", " + CountryStr(regForm.CountryID);
            lblAdvEmail.Text = regForm.Email;
            lblAdvPhone.Text = regForm.Phone;
            lblAdvSex.Text = (regForm.Sex == 1) ? "Nam" : (regForm.Sex == 2) ? "Nữ" : " - - -";

            switch (regForm.TypeID)
            {
                case 1:
                    lblAdvType.Text = RegisFormTypeStr(1);
                    spanTagsAdv.Attributes.Add("class", "label label-primary");
                    break;
                case 2:
                    lblAdvType.Text = RegisFormTypeStr(2);
                    spanTagsAdv.Attributes.Add("class", "label label-default");
                    break;
                case 3:
                    lblAdvType.Text = RegisFormTypeStr(3);
                    spanTagsAdv.Attributes.Add("class", "label label-success");
                    break;
                case 4:
                    lblAdvType.Text = RegisFormTypeStr(4);
                    spanTagsAdv.Attributes.Add("class", "label label-warning");
                    break;
            }
            lblAdvEdulvl.Text = EducationSrt(regForm.StudyLV);
            lblAdvcoutry.Text = CountryAdvStr(regForm.CountryAdvisoryID);
        }
        else
        {
            lblAdvFullName.Text = "";
            lbladvAddress.Text = "";
            lblAdvEmail.Text = "";
            lblAdvPhone.Text = "";
            lblAdvSex.Text = "";
            lblAdvEdulvl.Text = "";
            lblAdvcoutry.Text = "";
        }

        imgCusprofile.Src = (img == null) ? "../images/default_images.jpg" : "../" + img.ImagesUrl;

        txtformFirstName.Text = bsInfo.FirstName;
        //txtformFirstName.Text = "sdgfgjghzslghlsernjiljdnhlodrifnghlodrjiyn";
        txtformLastName.Text = bsInfo.LastName;
        txtformOtherName.Text = bsInfo.OtherName;
        txtformBirthday.Value = (bsInfo.Birthday.Year <= 1900) ? "" : bsInfo.Birthday.ToString("dd-MM-yyyy");
        txtformBirthPlace.Text = bsInfo.BirthPlace;
        rdformnam.Checked = (bsInfo.Sex == 1) ? true : false;
        rdformnu.Checked = (bsInfo.Sex == 2) ? true : false;
        txtformIdentityCard.Text = bsInfo.IdentityCard;
        txtformDateOfIdentityCard.Value = (bsInfo.DateOfIdentityCard.Year <= 1900) ? "" : bsInfo.DateOfIdentityCard.ToString("dd-MM-yyyy");
        txtformPlaceOfIdentityCard.Text = bsInfo.PlaceOfIdentityCard;

        dlNationality.Items.FindByValue(CusPri.NationalityID.ToString()).Selected = true;
        dlEthnic.Items.FindByValue(CusPri.EthnicID.ToString()).Selected = true;
        dlReligion.Items.FindByValue(CusPri.ReligionID.ToString()).Selected = true;
        dlCountry.Items.FindByValue(CusPri.CountryID.ToString()).Selected = true;
        dlProvince.Items.FindByValue(CusPri.ProvinceID.ToString()).Selected = true;
        dlDistrict.Items.FindByValue(CusPri.DistrictID.ToString()).Selected = true;
        dlBloodGroup.Items.FindByValue(CusPri.BloodID.ToString()).Selected = true;

        txtCommune_Ward.Text = CusPri.Commune_Ward;
        txtPermanentAddress.Text = CusPri.PermanentAddress;
        txtAddressPresent.Text = CusPri.AddressPresent;
        txtCompanyPhone.Text = CusPri.CompanyPhone;
        txtHomePhone.Text = CusPri.HomePhone;
        txtCellPhone.Text = CusPri.CellPhone;
        txtEmail.Text = CusPri.Email;
        txtMaritalStatus.Text = CusPri.MaritalStatus;
        txtTPXuatThan.Text = CusPri.TPXuatThan;
        txtUuTienGD.Text = CusPri.UuTienGD;
        txtUuTienBanThan.Text = CusPri.UuTienBanThan;
        txtNangKhieu.Text = CusPri.NangKhieu;
        txtDisability.Text = CusPri.Disability;
        txtHealthCondition.Text = CusPri.HealthCondition;
        txtHeight.Text = CusPri.Height.ToString();
        txtWeight.Text = CusPri.Weight.ToString();
        txtDateOfBHXH.Value = CusPri.DateOfBHXH.ToString("dd-MM-yyyy");
        txtNumOfBHXH.Text = CusPri.NumOfBHXH;
        txtBank.Text = CusPri.Bank;
        txtAccountNumber.Text = CusPri.AccountNumber;

        this.load_ThongTinPhuHuynh(bsInfo.InfoID);
        this.loadCustomerProfileMoreInfor(bsInfo.InfoID);
        this.load_CustomerProfileTypeInfor(bsInfo.InfoID);
    }
    private void getFormInfor(string queryStr)
    {
        employees = new EmployeesBLL();
        DataTable tb = employees.GetEmpNameCode(queryStr);
        foreach (DataRow r in tb.Rows)
        {
            txtEmpAdv.Text = (string)r[12] + " " + (string)r[11] + " - Mã NV: " + (string)r[7];
            txtprofilePriCode.Text = (string)r[2];
        }

    }
    //====LOAD MODAL IMAGES==========================================================================================================
    protected void load_rpLstImg()
    {
        images = new ImagesBLL();
        rpLstImg.DataSource = images.GetImagesTypeAndUserUpload(6, Session.GetCurrentUser().UserID);
        rpLstImg.DataBind();
    }
    protected void load_dlUserUploadImg()
    {
        useraccount = new UserAccountsBLL();
        dlimgtype.DataSource = useraccount.getAllUseraccount();
        dlimgtype.DataValueField = "UserID";
        dlimgtype.DataTextField = "UserName";
        dlimgtype.DataBind();
    }
    protected void dlimgtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        images = new ImagesBLL();
        rpLstImg.DataSource = new ObjectDataSource();
        rpLstImg.DataSource = (dlimgtype.SelectedValue == "0") ? images.GetImagesTypeUpload(6) : images.GetImagesTypeAndUserUpload(6, int.Parse(dlimgtype.SelectedValue));
        rpLstImg.DataBind();
    }
    private void UpdateImages(int proId, int ImgId)
    {
        customerProPri = new CustomerProfilePrivateBLL();
        if (this.customerProPri.UpdateImages(proId, ImgId))
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else
        {
            Response.Write("<script>alert('Change Image False !')</script>");
            return;
        }
    }
    protected int ImagesID(string filename)
    {
        int ImID = 0;
        images = new ImagesBLL();

        if (string.IsNullOrWhiteSpace(filename))
        {
            ImID = 0;
        }
        else
        {
            ImID = images.ImagesID(filename);
        }
        return ImID;
    }
    protected void btnchangeImgCusPro_Click(object sender, EventArgs e)
    {
        customerbasicinfo = new CustomerBasicInfoBLL();
        customerProPri = new CustomerProfilePrivateBLL();
        string http = "http://" + Request.Url.Authority + "/";
        txtPostImgTemp.Text = HiddenimgSelect.Value.Remove(0, http.Length);
        imgCusprofile.Src = "../" + HiddenimgSelect.Value.Remove(0, http.Length);
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(bsInfo.InfoID);
        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();
        UpdateImages(CusPri.ProfileID, ImagesID(txtPostImgTemp.Text));
    }
    protected void btnuploadImg_ServerClick(object sender, EventArgs e)
    {
        //ImgEditPC.Src = txtuploadImgTemp.Text;
        UserAccounts ac = Session.GetCurrentUser();
        customerbasicinfo = new CustomerBasicInfoBLL();
        customerProPri = new CustomerProfilePrivateBLL();
        images = new ImagesBLL();
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(bsInfo.InfoID);
        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();
        string dateString = DateTime.Now.ToString("MM-dd-yyyy");
        string fileName = Path.GetFileName(fileUploadImgPost.PostedFile.FileName);
        ImageCodecInfo jgpEncoder = null;
        string str_image = "";
        string fileExtension = "";
        if (!string.IsNullOrEmpty(fileName))
        {
            fileExtension = Path.GetExtension(fileName);
            str_image = "Hollywood-" + dateString + "-" + RandomName + fileExtension;
            string pathToSave = Server.MapPath("../images/Upload/ImagesForCustomerProfile/") + str_image;
            //file.SaveAs(pathToSave);
            System.Drawing.Image image = System.Drawing.Image.FromStream(fileUploadImgPost.FileContent);
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Gif);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Bmp.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Bmp);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Png);
            else
                throw new System.ArgumentException("Invalid File Type");
            Bitmap bmp1 = new Bitmap(fileUploadImgPost.FileContent);
            Encoder myEncoder = Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(pathToSave, jgpEncoder, myEncoderParameters);
            this.images.InsertImages(str_image, "images/Upload/ImagesForCustomerProfile/" + str_image, 6, ac.UserID);
            txtPostImgTemp.Text = "images/Upload/ImagesForCustomerProfile/" + str_image;
            imgCusprofile.Src = "../images/Upload/ImagesForCustomerProfile/" + str_image;
            this.load_rpLstImg();
            this.UpdateImages(CusPri.ProfileID, ImagesID(txtPostImgTemp.Text));
        }
        else
        {
            Response.Write("<script>alert('Upload Image False !')</script>");
            return;
        }
    }
    protected void dlNationality_SelectedIndexChanged(object sender, EventArgs e)
    {
        ethniclist = new EthnicListBLL();
        this.load_dlEthnic(Convert.ToInt32(dlNationality.SelectedValue));
        dlEthnic.Items.Add(new ListItem("_____________________Dân tộc khác________________________", "0"));
        List<EthnicList> lste = ethniclist.GetallEthnicListWithoutNationID(Convert.ToInt32(dlNationality.SelectedValue));
        foreach (EthnicList itm in lste)
        {
            dlEthnic.Items.Add(new ListItem(itm.EthnicName, itm.EthnicID.ToString()));
        }
        dlEthnic.Items.Insert(0, new ListItem("-- Dân tộc --", "0"));
    }
    private Boolean UpdateCustomerBasicInfo()
    {
        customerbasicinfo = new CustomerBasicInfoBLL();
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        int sex;
        if (!rdformnam.Checked && !rdformnu.Checked)
        {
            sex = 0;
        }
        else
        {
            sex = (rdformnam.Checked) ? 1 : (rdformnu.Checked) ? 2 : 0;
        }
        string Bday = txtformBirthday.Value;
        DateTime birthday;
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
        if (string.IsNullOrWhiteSpace(Bday) || DateTime.TryParseExact(Bday, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out birthday) || getday(Bday) == "" || getmonth(Bday) == "" || getyear(Bday) == "")
        {
            birthday = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            birthday = DateTime.ParseExact(getday(Bday) + "/" + getmonth(Bday) + "/" + getyear(Bday), "dd/MM/yyyy", null);
        }

        DateTime dateOfIdentityCard;
        string DOIC = txtformDateOfIdentityCard.Value;
        if (string.IsNullOrWhiteSpace(DOIC) || DateTime.TryParseExact(DOIC, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out dateOfIdentityCard) || getday(DOIC) == "" || getmonth(DOIC) == "" || getyear(DOIC) == "")
        {
            dateOfIdentityCard = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            dateOfIdentityCard = DateTime.ParseExact(getday(DOIC) + "/" + getmonth(DOIC) + "/" + getyear(DOIC), "dd/MM/yyyy", null);
        }
        bool hasUpadte = customerbasicinfo.UpdateCustomerBasicInfo(bsInfo.InfoID, txtformFirstName.Text, txtformLastName.Text, txtformOtherName.Text, birthday, txtformBirthPlace.Text, sex, txtformIdentityCard.Text, dateOfIdentityCard, txtformPlaceOfIdentityCard.Text);
        return (hasUpadte) ? true : false;
    }
    private Boolean UpdateCustomerProfilePrivate()
    {
        customerProPri = new CustomerProfilePrivateBLL();
        customerbasicinfo = new CustomerBasicInfoBLL();
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(bsInfo.InfoID);
        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();

        int Nationality = Convert.ToInt32(dlNationality.SelectedValue);
        int Ethnic = Convert.ToInt32(dlEthnic.SelectedValue);
        int Religion = Convert.ToInt32(dlReligion.SelectedValue);
        int Country = Convert.ToInt32(dlCountry.SelectedValue);
        int Province = Convert.ToInt32(dlProvince.SelectedValue);
        int District = Convert.ToInt32(dlDistrict.SelectedValue);
        int Height = (txtHeight.Text == "") ? 0 : Convert.ToInt32(txtHeight.Text);
        int Weight = (txtWeight.Text == "") ? 0 : Convert.ToInt32(txtWeight.Text);
        int BloodID = Convert.ToInt32(dlBloodGroup.SelectedValue);

        DateTime DateOfBHXH;
        string DaBHXH = txtDateOfBHXH.Value;
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
        if (string.IsNullOrWhiteSpace(DaBHXH) || DateTime.TryParseExact(DaBHXH, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out DateOfBHXH) || getday(DaBHXH) == "" || getmonth(DaBHXH) == "" || getyear(DaBHXH) == "")
        {
            DateOfBHXH = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            DateOfBHXH = DateTime.ParseExact(getday(DaBHXH) + "/" + getmonth(DaBHXH) + "/" + getyear(DaBHXH), "dd/MM/yyyy", null);
        }
        bool hasUpdate = customerProPri.UpdateCustomerProfilePrivate(CusPri.ProfileID, txtCompanyCopyright.Text, Nationality, Ethnic, Religion, Country, Province, District, txtCommune_Ward.Text, txtPermanentAddress.Text, txtAddressPresent.Text, txtCompanyPhone.Text, txtHomePhone.Text, txtCellPhone.Text, txtEmail.Text, txtMaritalStatus.Text, txtTPXuatThan.Text, txtUuTienGD.Text, txtUuTienBanThan.Text, txtNangKhieu.Text, txtDisability.Text, txtHealthCondition.Text, Height, Weight, BloodID, DateOfBHXH, txtNumOfBHXH.Text, txtBank.Text, txtAccountNumber.Text, 2);
        return (hasUpdate) ? true : false;
    }
    private void load_ThongTinPhuHuynh(int InfoID)
    {
        thongtinphuhuynh = new ThongTinPhuHuynhBLL();
        List<ThongTinPhuHuynh> lst = thongtinphuhuynh.getAllListWithInfoID(InfoID);
        ThongTinPhuHuynh infor = lst.FirstOrDefault();
        if (infor != null)
        {
            txtpFirstName_Dad.Text = infor.FirstName_Dad;
            txtpLastName_Dad.Text = infor.LastName_Dad;
            txtpNgaySinh_Dad.Text = (infor.NgaySinh_Dad.Year <= 1900) ? "" : infor.NgaySinh_Dad.ToString("dd-MM-yyyy");
            txtpNoiSinh_Dad.Text = infor.NoiSinh_Dad;
            txtpSoCmnd_Dad.Text = infor.SoCmnd_Dad;
            txtpNoiCap_Dad.Text = infor.NoiCap_Dad;
            txtpNgayCap_Dad.Text = (infor.NgayCap_Dad.Year <= 1900) ? "" : infor.NgayCap_Dad.ToString("dd-MM-yyyy");
            txtpSoDienthoai_Dad.Text = infor.SoDienThoai_Dad;

            txtpFirstName_Mom.Text = infor.FirstName_Mom;
            txtpLastName_Mom.Text = infor.LastName_Mom;
            txtpNgaySinh_Mom.Text = (infor.NgaySinh_Mom.Year <= 1900) ? "" : infor.NgaySinh_Mom.ToString("dd-MM-yyyy");
            txtpNoiSinh_Mom.Text = infor.NoiSinh_Mom;
            txtpSoCmnd_Mom.Text = infor.SoCmnd_Mom;
            txtpNoiCap_Mom.Text = infor.NoiCap_Mom;
            txtpNgayCap_Mom.Text = (infor.NgayCap_Mom.Year <= 1900) ? "" : infor.NgayCap_Mom.ToString("dd-MM-yyyy");
            txtpSoDienthoai_Mom.Text = infor.SoDienThoai_Mom;
        }
    }
    private Boolean UpdateThongTinPhuHuynh()
    {
        thongtinphuhuynh = new ThongTinPhuHuynhBLL();
        customerbasicinfo = new CustomerBasicInfoBLL();
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        string fname_Dad = txtpFirstName_Dad.Text;
        string lname_Dad = txtpLastName_Dad.Text;
        string ngaysinh_Dad = txtpNgaySinh_Dad.Text;
        DateTime NgSinh_Dad;
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
        if (string.IsNullOrWhiteSpace(ngaysinh_Dad) || DateTime.TryParseExact(ngaysinh_Dad, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgSinh_Dad) || getday(ngaysinh_Dad) == "" || getmonth(ngaysinh_Dad) == "" || getyear(ngaysinh_Dad) == "")
        {
            NgSinh_Dad = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            NgSinh_Dad = DateTime.ParseExact(getday(ngaysinh_Dad) + "/" + getmonth(ngaysinh_Dad) + "/" + getyear(ngaysinh_Dad), "dd/MM/yyyy", null);
        }
        string noisinh_Dad = txtpNoiSinh_Dad.Text;
        string socmnd_Dad = txtpSoCmnd_Dad.Text;
        string noicap_Dad = txtpNoiCap_Dad.Text;
        string ngaycap_Dad = txtpNgayCap_Dad.Text;
        DateTime NgayCap_Dad;
        if (string.IsNullOrWhiteSpace(ngaycap_Dad) || DateTime.TryParseExact(ngaycap_Dad, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayCap_Dad) || getday(ngaycap_Dad) == "" || getmonth(ngaycap_Dad) == "" || getyear(ngaycap_Dad) == "")
        {
            NgayCap_Dad = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            NgayCap_Dad = DateTime.ParseExact(getday(ngaycap_Dad) + "/" + getmonth(ngaycap_Dad) + "/" + getyear(ngaycap_Dad), "dd/MM/yyyy", null);
        }
        string sodienthoai_Dad = txtpSoDienthoai_Dad.Text;

        string fname_Mom = txtpFirstName_Mom.Text;
        string lname_Mom = txtpLastName_Mom.Text;
        string ngaysinh_Mom = txtpNgaySinh_Mom.Text;
        DateTime NgSinh_Mom;
        if (string.IsNullOrWhiteSpace(ngaysinh_Mom) || DateTime.TryParseExact(ngaysinh_Mom, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgSinh_Mom) || getday(ngaysinh_Mom) == "" || getmonth(ngaysinh_Mom) == "" || getyear(ngaysinh_Mom) == "")
        {
            NgSinh_Mom = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            NgSinh_Mom = DateTime.ParseExact(getday(ngaysinh_Mom) + "/" + getmonth(ngaysinh_Mom) + "/" + getyear(ngaysinh_Mom), "dd/MM/yyyy", null);
        }
        string noisinh_Mom = txtpNoiSinh_Mom.Text;
        string socmnd_Mom = txtpSoCmnd_Mom.Text;
        string noicap_Mom = txtpNoiCap_Mom.Text;
        string ngaycap_Mom = txtpNgayCap_Mom.Text;
        DateTime NgayCap_Mom;
        if (string.IsNullOrWhiteSpace(ngaycap_Mom) || DateTime.TryParseExact(ngaycap_Mom, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayCap_Mom) || getday(ngaycap_Mom) == "" || getmonth(ngaycap_Mom) == "" || getyear(ngaycap_Mom) == "")
        {
            NgayCap_Mom = Convert.ToDateTime("01/01/1900");
        }
        else
        {
            NgayCap_Mom = DateTime.ParseExact(getday(ngaycap_Mom) + "/" + getmonth(ngaycap_Mom) + "/" + getyear(ngaycap_Mom), "dd/MM/yyyy", null);
        }
        string sodienthoai_Mom = txtpSoDienthoai_Mom.Text;
        List<ThongTinPhuHuynh> lst = thongtinphuhuynh.getAllListWithInfoID(bsInfo.InfoID);
        ThongTinPhuHuynh infor = lst.FirstOrDefault();
        bool hasInfor = (infor == null) ? false : true;
        bool HasNewThongTin = true;
        switch (hasInfor)
        {
            case true:
                HasNewThongTin = thongtinphuhuynh.UpdateThongTinPhuHuynh(bsInfo.InfoID, fname_Dad, lname_Dad, NgSinh_Dad, noisinh_Dad, socmnd_Dad, noicap_Dad, NgayCap_Dad, sodienthoai_Dad, fname_Mom, lname_Mom, NgSinh_Mom, noisinh_Mom, socmnd_Mom, noicap_Mom, NgayCap_Mom, sodienthoai_Mom);
                break;
            case false:
                HasNewThongTin = thongtinphuhuynh.NewThongTinPhuHuynh(bsInfo.InfoID, fname_Dad, lname_Dad, NgSinh_Dad, noisinh_Dad, socmnd_Dad, noicap_Dad, NgayCap_Dad, sodienthoai_Dad, fname_Mom, lname_Mom, NgSinh_Mom, noisinh_Mom, socmnd_Mom, noicap_Mom, NgayCap_Mom, sodienthoai_Mom);
                break;
        }
        return (HasNewThongTin) ? true : false;
    }
    protected void btnSavePrivateProfile_Click(object sender, EventArgs e)
    {
        try
        {
            if (!UpdateCustomerBasicInfo() || !UpdateCustomerProfilePrivate() || !UpdateThongTinPhuHuynh() || !NewCustomerProfileMoreInfor() || !NewCustomerProfileTypeInfor())
            {
                Response.Write("<script>alert('Cập nhật thông tin [1] không thành công - lỗi kết nối CSDL !')</script>");
            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.ToString() + "!')</script>");
        }
    }
    //Load CustomerProfileMoreInfor
    private void loadCustomerProfileMoreInfor(int InfoID)
    {
        customerProfileMoreInfor = new CustomerProfileMoreInforBLL();
        List<CustomerProfileMoreInfor> lstmore = customerProfileMoreInfor.GetListWithInfoID(InfoID);
        CustomerProfileMoreInfor moreinfor = lstmore.FirstOrDefault();
        if (moreinfor != null)
        {
            txtHVGioiThieu.Text = moreinfor.HVGioiThieu;

            string trinhdoHV = moreinfor.TrinhDoHocVan;
            switch (trinhdoHV)
            {
                case "Đại Học - Cao Đẳng":
                    chkDH.Checked = true;
                    break;
                case "Trung học phổ thông":
                    chkTHPT.Checked = true;
                    break;
                case "Trung học cơ sở":
                    chkTHCS.Checked = true;
                    break;
                case "Tiểu học":
                    chkTieuHoc.Checked = true;
                    break;
                case "Mẫu giáo":
                    chkMauGiao.Checked = true;
                    break;
            }
            txtTenTruong.Text = moreinfor.TenTruong;
            string CCTiengAnh = moreinfor.CCTiengAnh;
            if (CCTiengAnh.Contains("Starters"))
                chkCC1.Checked = true;
            if (CCTiengAnh.Contains("Movers"))
                chkCC2.Checked = true;
            if (CCTiengAnh.Contains("Flyers"))
                chkCC3.Checked = true;
            if (CCTiengAnh.Contains("KET"))
                chkCC4.Checked = true;
            if (CCTiengAnh.Contains("PET"))
                chkCC5.Checked = true;
            if (CCTiengAnh.Contains("FCE"))
                chkCC6.Checked = true;


            string[] lstCCTiengAnh = CCTiengAnh.Split(';');
            string CCKHac = CCTiengAnh.Replace(";", "");
            for (int i = 0; i < lstCCTiengAnh.Length; i++)
            {

                if (lstCCTiengAnh[i] == "Starters")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "Movers")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "Flyers")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "KET")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "PET")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "FCE")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                txtCCKHac.Text = CCKHac;
            }

            string BietTT = moreinfor.BietThongTin;
            if (BietTT.Contains("Báo chí"))
                chkBTT1.Checked = true;
            if (BietTT.Contains("Internet"))
                chkBTT2.Checked = true;
            if (BietTT.Contains("Bạn bè"))
                chkBTT3.Checked = true;
            if (BietTT.Contains("Website"))
                chkBTT4.Checked = true;
            if (BietTT.Contains("Trực tiếp tại trung tâm"))
                chkBTT5.Checked = true;
            string[] lstBietTT = BietTT.Split(';');
            string strBietTT = BietTT.Replace(";", "");
            for (int j = 0; j < lstBietTT.Length; j++)
            {
                if (lstBietTT[j] == "Báo chí")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Internet")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Bạn bè")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Website")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Trực tiếp tại trung tâm")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");

                txtBTTKHac.Text = strBietTT;
            }
            CKDocNote.Text = moreinfor.GhiChu;
        }

    }
    //CustomerProfileMoreInfor
    private Boolean NewCustomerProfileMoreInfor()
    {
        customerProfileMoreInfor = new CustomerProfileMoreInforBLL();
        customerbasicinfo = new CustomerBasicInfoBLL();
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        List<CustomerProfileMoreInfor> lstmore = customerProfileMoreInfor.GetListWithInfoID(bsInfo.InfoID);
        CustomerProfileMoreInfor moreinfor = lstmore.FirstOrDefault();

        string HVGioiThieu = txtHVGioiThieu.Text;
        string TDHocVAn = "";
        if (chkMauGiao.Checked == true)
            TDHocVAn = "Mẫu giáo";
        else if (chkTieuHoc.Checked == true)
            TDHocVAn = "Tiểu học";
        else if (chkTHCS.Checked == true)
            TDHocVAn = "Trung học cơ sở";
        else if (chkTHPT.Checked == true)
            TDHocVAn = "Trung học phổ thông";
        else
            TDHocVAn = "Đại Học - Cao Đẳng";

        string tentruong = txtTenTruong.Text;
        string CCTA = txtCCKHac.Text;
        if (chkCC1.Checked == true)
            CCTA += ";" + "Starters";
        if (chkCC2.Checked == true)
            CCTA += ";" + "Movers";
        if (chkCC3.Checked == true)
            CCTA += ";" + "Flyers";
        if (chkCC4.Checked == true)
            CCTA += ";" + "KET";
        if (chkCC5.Checked == true)
            CCTA += ";" + "PET";
        if (chkCC6.Checked == true)
            CCTA += ";" + "FCE";

        string BTT = txtBTTKHac.Text;
        if (chkBTT1.Checked == true)
            BTT += ";" + "Báo chí";
        if (chkBTT2.Checked == true)
            BTT += ";" + "Internet";
        if (chkBTT3.Checked == true)
            BTT += ";" + "Bạn bè";
        if (chkBTT4.Checked == true)
            BTT += ";" + "Website";
        if (chkBTT5.Checked == true)
            BTT += ";" + "Trực tiếp tại trung tâm";

        string GhiChu = CKDocNote.Text;

        bool hasInfor = (moreinfor == null) ? false : true;
        bool HasNewThongTin = true;
        switch (hasInfor)
        {
            case true:
                HasNewThongTin = customerProfileMoreInfor.UpdateCustomerProfileMoreInfor(bsInfo.InfoID, HVGioiThieu, TDHocVAn, tentruong, CCTA, BTT, GhiChu);
                break;
            case false:
                HasNewThongTin = customerProfileMoreInfor.NewCustomerProfileMoreInfor(bsInfo.InfoID, HVGioiThieu, TDHocVAn, tentruong, CCTA, BTT, GhiChu);
                break;
        }
        return (HasNewThongTin) ? true : false;

        //return this.hocvienmoreinfo.kus_NewHocVienMoreInFo(HocVienID, HVGioiThieu, TDHocVAn, tentruong, CCTA, BTT);

    }

    //Thong tin ho so
    private void load_dlEdulvl()
    {
        educationlvl = new EducationLVBLL();
        this.load_DropdownList(dlEdulvl, educationlvl.getallEducationLV(), "NAME", "ID");
        dlEdulvl.Items.Insert(0, new ListItem("-- Chọn Trình độ - Cấp Bậc --", "0"));
    }
    private void load_dlEduCountry()
    {
        country = new CountryBLL();
        this.load_DropdownList(dlEduCountry, country.getAllCountry(), "CountryName", "CountryID");
        dlEduCountry.Items.Insert(0, new ListItem("-- Chọn Quốc Gia --", "0"));
    }

    private void load_CustomerProfileTypeInfor(int InfoID)
    {
        customerProPri = new CustomerProfilePrivateBLL();
        customerProfileTypeInfor = new CustomerProfileTypeInforBLL();
        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(InfoID);
        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();
        List<CustomerProfileTypeInfor> lstType = customerProfileTypeInfor.getListEithProfileID(CusPri.ProfileID);
        CustomerProfileTypeInfor cpinfor = lstType.FirstOrDefault();
        if(cpinfor!=null)
        {
            dlBagProfileType.Items.FindByValue(cpinfor.BagProfileTypeID.ToString()).Selected = true;
            dlEduCountry.Items.FindByValue(cpinfor.CountryID.ToString()).Selected = true;
            dlEdulvl.Items.FindByValue(cpinfor.Education.ToString()).Selected = true;
        }

    }
    private Boolean NewCustomerProfileTypeInfor()
    {
        customerbasicinfo = new CustomerBasicInfoBLL();
        customerProPri = new CustomerProfilePrivateBLL();
        customerProfileTypeInfor = new CustomerProfileTypeInforBLL();
        string BaseCode = Request.QueryString["FileCode"];
        List<CustomerBasicInfo> lstBsInfo = customerbasicinfo.GetCusBasicInfoWithCode(BaseCode);
        CustomerBasicInfo bsInfo = lstBsInfo.FirstOrDefault();
        List<CustomerProfilePrivate> lstPri = customerProPri.GetCustomerProfilePrivateWithInfoID(bsInfo.InfoID);
        CustomerProfilePrivate CusPri = lstPri.FirstOrDefault();

        List<CustomerProfileTypeInfor> lstType = customerProfileTypeInfor.getListEithProfileID(CusPri.ProfileID);
        CustomerProfileTypeInfor cpinfor = lstType.FirstOrDefault();

        int BagProfileTypeID = Convert.ToInt32(dlBagProfileType.SelectedValue);
        int CountryID = Convert.ToInt32(dlEduCountry.SelectedValue);
        int Education = Convert.ToInt32(dlEdulvl.SelectedValue);

        bool hasInfor = (cpinfor == null) ? false : true;
        bool HasNewThongTin = true;
        switch (hasInfor)
        {
            case true:
                HasNewThongTin = customerProfileTypeInfor.UpdateProfileTypeInfor(CusPri.ProfileID, BagProfileTypeID, CountryID, Education);
                break;
            case false:
                HasNewThongTin = customerProfileTypeInfor.NewProfileTypeInfor(CusPri.ProfileID, BagProfileTypeID, CountryID, Education);
                break;
        }
        return (HasNewThongTin) ? true : false;
    }
}