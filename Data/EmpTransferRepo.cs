using hrd_backend.Interface;
using hrd_backend.Model.Employees_Transfer;
using hrd_backend.Model.Orientation_Checklist;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace hrd_backend.Data
{
    public class EmpTransferRepo : IEmpTransferRepo
    {
        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        public IDbRepo _dbRepo;

        CultureInfo culture = new CultureInfo("en-US");

        public EmpTransferRepo(IConfiguration config, IWebHostEnvironment env, IDbRepo dbRepo)
        {
            _config = config;

            this._env = env;
            _dbRepo = dbRepo;
        }

        public EmployeeTransferMD GetEmpTr(string refNo)
        {
            EmployeeTransferMD tr = new EmployeeTransferMD();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetEmpTrDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                //tr.hod_date = reader["Interviewer_Name"].ToString();
                tr.dateRequested = reader["Date_Requested"].ToString();
                tr.name = reader["Name"].ToString();
                tr.refNo = reader["Reference_Number"].ToString();
                
                tr.status = reader["STATUS"].ToString();

                tr.company = reader["Company"].ToString();
                tr.department = reader["Department"].ToString();
                tr.designation = reader["Designation"].ToString();
                tr.commencementDate = reader["Commencement_Date"].ToString();
                tr.highestQualification = reader["Highest_Qualification"].ToString();
                tr.positionInterested = reader["Position_Interested"].ToString();
                tr.transferDept = reader["Transfer_Department"].ToString();
                tr.workExp = reader["Work_Experience"].ToString();
                tr.transferReason = reader["Transfer_Reason"].ToString();

                tr.hod.refNo = reader["Reference_Number"].ToString();
                tr.hod.reasonRejection = reader["Reason_Rejected"].ToString();
                tr.hod.replacementRequired= reader["Employee_Replacement"].ToString();
                tr.hod.statusRequest = reader["HOD_StatusRequest"].ToString();
                tr.hod_date = reader["HOD_Date"].ToString();
                tr.hod_designation = reader["HOD_Designation"].ToString();
                tr.hod_name = reader["HOD_Name"].ToString();

                tr.hr.refNo = reader["Reference_Number"].ToString();
                tr.hr.hr_name = reader["HRD_Name"].ToString();
                tr.hr.hr_designation = reader["HRD_Designation"].ToString();
                tr.hr.receivedBy = reader["HRD_Name"].ToString();
                tr.hr.receivedDate = reader["receivedDate"].ToString();
                tr.hr.interviewDateTime = reader["Interview_Datetime"].ToString();
                tr.hr.interviewer = reader["Interviewer_Name"].ToString();
                tr.hr.approvedBy = reader["approvedBy"].ToString();
                tr.hr.approvedDate = reader["approvedDate"].ToString();
                tr.hr_date = reader["HRD_Date"].ToString();

                tr.ad.refNo = reader["Reference_Number"].ToString();
                tr.ad.statusRequest = reader["AD_Approval"].ToString();
                tr.ad.department = reader["AD_Department"].ToString();
                tr.ad.company = reader["AD_Company"].ToString();
                tr.ad.comment = reader["AD_Comments"].ToString();
                tr.ad_date = reader["AD_Date"].ToString();




                conn.Close();
                return tr;

            }
            else
            {

                conn.Close();
                return null;
            }
        }


        public string AddEmpTransfer(AddEmpTransfer tr)
        {
            string date = DateTime.Now.ToString("MMMyy");
            var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberEmpTr");



            string refNo = "EMPTR-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTEMPTR");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", refNo);
            cmd.Parameters.AddWithValue("@name", tr.name);
            cmd.Parameters.AddWithValue("@company", tr.company);
            cmd.Parameters.AddWithValue("@department", tr.department);
            cmd.Parameters.AddWithValue("@designation", tr.designation);
            cmd.Parameters.AddWithValue("@commenceDate", tr.commencementDate);
            cmd.Parameters.AddWithValue("@qualification", tr.highestQualification);
            cmd.Parameters.AddWithValue("@position", tr.positionInterested);
            cmd.Parameters.AddWithValue("@transferDept", tr.transferDept);
            cmd.Parameters.AddWithValue("@workExp", tr.workExp);
            cmd.Parameters.AddWithValue("@transferReason", tr.transferReason);
            cmd.Parameters.AddWithValue("@id", tr.requesterId);
            //cmd.Parameters.AddWithValue("@requesterDesignation", tr.requesterDesignation);

            cmd.Parameters.AddWithValue("@dateRequested", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();
            return refNo;
        }
    }
}
