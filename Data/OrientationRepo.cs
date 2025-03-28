using hrd_backend.Interface;
using hrd_backend.Model.Orientation_Checklist;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace hrd_backend.Data
{
    public class OrientationRepo : IOrientationRepo
    {
        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        public IDbRepo _dbRepo;

        CultureInfo culture = new CultureInfo("en-US");

        public OrientationRepo(IConfiguration config, IWebHostEnvironment env, IDbRepo dbRepo)
        {
            _config = config;

            this._env = env;
            _dbRepo = dbRepo;
        }

        //public int GetRunningNumber(string date, string storePro)
        //{
        //    int sn;
        //    string connection = _config["ConnectionStrings:ServicePortalConnection"];
        //    SqlConnection conn = new SqlConnection(connection);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(storePro);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = conn;

        //    cmd.Parameters.AddWithValue("@condition", date);


        //    SqlDataReader reader = cmd.ExecuteReader();

        //    var data = reader.Read();

        //    if (data)
        //    {
        //        sn = Convert.ToInt32(reader["ID"]) + 1;
        //        conn.Close();
        //        return sn;
        //    }
        //    else
        //    {
        //        sn = 1;
        //        conn.Close();
        //        return sn;
        //    }

        //}


        public string AddOrientationDetails(AddOrientation tr)
        {
            string date = DateTime.Now.ToString("MMMyy");
            var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberORT");



            string refNo = "ORT-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTORT");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", refNo);
            cmd.Parameters.AddWithValue("@name", tr.name);
            cmd.Parameters.AddWithValue("@company", tr.company);
            cmd.Parameters.AddWithValue("@department", tr.department);
            cmd.Parameters.AddWithValue("@dateJoined", tr.dateJoined);
            cmd.Parameters.AddWithValue("@branch", tr.branch);
            cmd.Parameters.AddWithValue("@office", tr.office);
            cmd.Parameters.AddWithValue("@floor", tr.floor);
            cmd.Parameters.AddWithValue("@purpose", tr.purpose);
            cmd.Parameters.AddWithValue("@email", tr.HOD_Emp_Email);
            cmd.Parameters.AddWithValue("@namecard", tr.HOD_Emp_NameCard);
            cmd.Parameters.AddWithValue("@tagline", tr.HOD_Emp_Tagline);
            cmd.Parameters.AddWithValue("@fb", tr.HOD_Emp_FB);
            cmd.Parameters.AddWithValue("@chinese", tr.HOD_Emp_ChineseName);
            cmd.Parameters.AddWithValue("@phoneNumber", tr.HOD_Emp_PhoneNo);
            cmd.Parameters.AddWithValue("@desktop", tr.HOD_Emp_Desktop);
            cmd.Parameters.AddWithValue("@laptop", tr.HOD_Emp_Laptop);
            cmd.Parameters.AddWithValue("@otherItem", tr.HOD_Emp_Other);
            cmd.Parameters.AddWithValue("@id", tr.requesterId);
            cmd.Parameters.AddWithValue("@requesterName", tr.requesterName);
            cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDept);
            cmd.Parameters.AddWithValue("@designation", tr.requesterDesignation);
           // cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDesignation);

            cmd.Parameters.AddWithValue("@dateRequested", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();

            return refNo;

        }

        public Orientation GetOrientationList(string refNo)
        {
            Orientation tr = new Orientation();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetORTDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                tr.dateRequested = reader["REQUESTER_DATE"].ToString();
                tr.hod.name = reader["EMPLOYEE_NAME"].ToString();
                tr.hr.refNo = reader["REFERENCE_NUMBER"].ToString();
                tr.status = reader["STATUS"].ToString();
                
                tr.hod.company = reader["EMPLOYEE_COMPANY"].ToString();
                tr.hod.department = reader["EMPLOYEE_DEPARTMENT"].ToString();
                tr.hod.dateJoined = reader["EMPLOYEE_DATE_JOINED"].ToString();
                tr.hod.branch = reader["EMPLOYEE_BRANCH"].ToString();
                tr.hod.office = reader["EMPLOYEE_OFFICE"].ToString();
                tr.hod.floor = reader["EMPLOYEE_FLOOR"].ToString();
                tr.hod.purpose = reader["EMPLOYEE_PURPOSE"].ToString();
                tr.hod.HOD_Emp_Email = reader["HOD_EMPLOYEE_EMAIL"].ToString();
                tr.hod.HOD_Emp_NameCard = reader["HOD_EMPLOYEE_NAME_CARD"].ToString();
                tr.hod.HOD_Emp_Tagline = reader["HOD_EMPLOYEE_TAGLINE"].ToString();
                tr.hod.HOD_Emp_FB = reader["HOD_EMPLOYEE_FB"].ToString();
                tr.hod.HOD_Emp_ChineseName = reader["HOD_EMPLOYEE_CHINESE_NAME"].ToString();
                tr.hod.HOD_Emp_PhoneNo = reader["HOD_EMPLOYEE_PHONE_NUMBER"].ToString();
                tr.hod.HOD_Emp_Desktop = reader["HOD_EMPLOYEE_DESKTOP"].ToString();
                tr.hod.HOD_Emp_Laptop = reader["HOD_EMPLOYEE_LAPTOP"].ToString();
                tr.hod.HOD_Emp_Other = reader["HOD_OTHER_ITEMS"].ToString();
                tr.hod.requesterName = reader["REQUESTER_NAME"].ToString();
                tr.hod.requesterDept = reader["REQUESTER_DEPT"].ToString();
                tr.hod.requesterDesignation = reader["REQUESTER_DESIGNATION"].ToString();

                tr.hr.HR_Email = reader["HR_EMPLOYEE_EMAIL"].ToString();
                tr.hr.HR_SitArg = reader["HR_EMPLOYEE_SIT_ARG"].ToString();
                tr.hr.HR_PhoneExt = reader["HR_EMPLOYEE_PHONE_EXT"].ToString();
                tr.hr.HR_PhonePin = reader["HR_EMPLOYEE_PHONE_PIN"].ToString();
                tr.hr.HR_FB = reader["HR_EMPLOYEE_FB"].ToString();
                tr.hr.HR_Other = reader["HR_OTHER_ITEMS"].ToString();
                tr.hr.HR_Tagline = reader["HR_EMPLOYEE_TAGLINE"].ToString();
                tr.hr.HR_Laptop = reader["HR_EMPLOYEE_LAPTOP"].ToString();
                tr.hr.HR_Desktop = reader["HR_EMPLOYEE_DESKTOP"].ToString();
                //tr.hr.HR_ChineseName = reader["HOD_EMPLOYEE_CHINESE_NAME"].ToString();
                //tr.hr.HR_PhoneNo = reader["HOD_EMPLOYEE_PHONE_NUMBER"].ToString();
                tr.hr.HR_NameCard = reader["HR_EMPLOYEE_NAMECARD"].ToString();
                tr.hr.welcomingPhoto = reader["WELCOMING_PHOTO"].ToString();
                tr.hr.orientBrief = reader["ORIENTATION_BRIEF"].ToString();
                tr.hr.compBrief = reader["COMPANY_BRIEF"].ToString();
                tr.hr.handbookAdvice = reader["HANDBOOK_ADVICE"].ToString();
                tr.hr.panelClinicInfo = reader["PANELCLINIC_INFO"].ToString();
                tr.hr.mcNote = reader["MC_NOTE"].ToString();
                tr.hr.tardiness = reader["TARDINESS"].ToString();
                tr.hr.hraForm = reader["HRA_FORMS"].ToString();
                tr.hr.phoneUsage = reader["PHONE_USAGE"].ToString();
                tr.hr.qessitBrief = reader["QESSIT_BRIEF"].ToString();
                tr.hr.workplaceTour = reader["WORKPLACE_TOUR"].ToString();
                tr.hr.facilityComp = reader["FACILITIES_COMPANY"].ToString();
                tr.hr.honestyCorner = reader["HONESTY_CORNER"].ToString();
                tr.hr.empItems = reader["EMPLOYEE_ITEMS"].ToString();
                tr.hr.fbGroup = reader["FB_GROUP"].ToString();
                tr.hr.fbPost = reader["FB_POST"].ToString();
                tr.hr.approverDesignation = reader["APPROVER_DESIGNATION"].ToString();
                tr.hr.approverName = reader["APPROVER_NAME"].ToString();
                tr.approvedDate = reader["APPROVER_DATE"].ToString();


                conn.Close();
                return tr;

            }
            else
            {

                conn.Close();
                return null;
            }
        }

        public void UpdateOrtHR(AddOrientationHR tr)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberORT");



            //string refNo = "ORT-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_UPDATEORTHR");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", tr.refNo);
            cmd.Parameters.AddWithValue("@email", tr.HR_Email);
            cmd.Parameters.AddWithValue("@phone", tr.HR_PhoneNo);
            cmd.Parameters.AddWithValue("@namecard", tr.HR_NameCard);
            cmd.Parameters.AddWithValue("@desktop", tr.HR_Desktop);
            cmd.Parameters.AddWithValue("@laptop", tr.HR_Laptop);
            cmd.Parameters.AddWithValue("@arrange", tr.HR_SitArg);
            cmd.Parameters.AddWithValue("@ext", tr.HR_PhoneExt);
            cmd.Parameters.AddWithValue("@pin", tr.HR_PhonePin);
            cmd.Parameters.AddWithValue("@fb", tr.HR_FB);
            cmd.Parameters.AddWithValue("@tagline", tr.HR_Tagline);
            cmd.Parameters.AddWithValue("@welcome", tr.welcomingPhoto);
            cmd.Parameters.AddWithValue("@ort", tr.orientBrief);
            cmd.Parameters.AddWithValue("@company", tr.compBrief);
            cmd.Parameters.AddWithValue("@panel", tr.panelClinicInfo);
            cmd.Parameters.AddWithValue("@medical", tr.mcNote);
            cmd.Parameters.AddWithValue("@tardiness", tr.tardiness);
            cmd.Parameters.AddWithValue("@forms", tr.hraForm);
            cmd.Parameters.AddWithValue("@phoneusage", tr.phoneUsage);
            cmd.Parameters.AddWithValue("@qessit", tr.qessitBrief);
            cmd.Parameters.AddWithValue("@workplace", tr.workplaceTour);
            cmd.Parameters.AddWithValue("@facility", tr.facilityComp);
            cmd.Parameters.AddWithValue("@honesty", tr.honestyCorner);
            cmd.Parameters.AddWithValue("@pinno", tr.empItems);
            cmd.Parameters.AddWithValue("@fbgroup", tr.fbGroup);
            cmd.Parameters.AddWithValue("@fbpost", tr.fbPost);
          
            cmd.Parameters.AddWithValue("@id", tr.approverId);
            cmd.Parameters.AddWithValue("@requesterName", tr.approverName);
           // cmd.Parameters.AddWithValue("@requesterdept", tr.app);
            cmd.Parameters.AddWithValue("@designation", tr.approverDesignation);
            // cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDesignation);

            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();

           

        }
    }
}

