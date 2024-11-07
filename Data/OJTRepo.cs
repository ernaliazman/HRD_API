using hrd_backend.Interface;
using hrd_backend.Model;
using hrd_backend.Model.Employees_Transfer;
using hrd_backend.Model.Job_Description;
using hrd_backend.Model.OJT;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace hrd_backend.Data
{
    public class OJTRepo : IOJTRepo
    {

        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        public IDbRepo _dbRepo;

        CultureInfo culture = new CultureInfo("en-US");

        public OJTRepo(IConfiguration config, IWebHostEnvironment env, IDbRepo dbRepo)
        {
            _config = config;

            this._env = env;
            _dbRepo = dbRepo;
        }

        public string AddOJT(AddOJT tr)
        {
            string date = DateTime.Now.ToString("MMMyy");
            var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberOJT");

            string refNo = "OJT-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTOJT");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", refNo);
            cmd.Parameters.AddWithValue("@name", tr.staffName);
            cmd.Parameters.AddWithValue("@company", tr.company);
            cmd.Parameters.AddWithValue("@department", tr.department);
            cmd.Parameters.AddWithValue("@dateJoined", tr.dateJoined);
            cmd.Parameters.AddWithValue("@formType", tr.formType);
           // cmd.Parameters.AddWithValue("@desc", tr.description);
            cmd.Parameters.AddWithValue("@verification", tr.requesterVerification);
            cmd.Parameters.AddWithValue("@id", tr.requesterId);
            cmd.Parameters.AddWithValue("@designation", tr.requesterDesignation);
            cmd.Parameters.AddWithValue("@hrdatefinish", tr.hrDateFinish);
            cmd.Parameters.AddWithValue("@staffdatefinish", tr.staffDateFinish);
            cmd.Parameters.AddWithValue("@dateRequested", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();
            return refNo;
        }

        public void AddOJT_Desc(AddOJT ojt, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            for (int i = 0; i < ojt.descriptions.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTOJT_Desc");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@data", ojt.descriptions[i]);

                cmd.ExecuteNonQuery();
            }



            conn.Close();

        }

        public OJTData GetOJT(OJTData tr, string refNo)
        {
            //Orientation tr = new Orientation();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetOJT");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                tr.hrDateFinish = reader["HR_DATEFINISH"].ToString();
                tr.staffDateFinish = reader["STAFF_DATEFINISH"].ToString();
                tr.refNo = reader["REFERENCE_NUMBER"].ToString();
                //tr.status = reader["STATUS"].ToString();
                tr.company = reader["COMPANY"].ToString();
                tr.department = reader["DEPARTMENT"].ToString();
                tr.dateJoined = reader["DATE_JOINED"].ToString();
                tr.staffName = reader["REQUESTER_NAME"].ToString();
                // tr.designation = reader["DESIGNATION"].ToString();
                tr.status = reader["STATUS"].ToString();
                tr.formType = reader["FORM_TYPE"].ToString();
                tr.achieveTarget = reader["ACHIEVE_TARGET"].ToString();
                tr.reasonifNo = reader["REASON_IFNO"].ToString();
                tr.requesterId = (Guid)reader["REQUESTER_ID"];
                tr.requesterName = reader["REQUESTER_NAME"].ToString();
                tr.requesterDesignation = reader["REQUESTER_DESIGNATION"].ToString();
                tr.requesterDept = reader["DEPARTMENT"].ToString();
                tr.requesterVerification = reader["REQUESTER_VERIFY"].ToString();


                tr.hodName = reader["VERIFIER1_NAME"].ToString();
                tr.hodDesignation = reader["VERIFIER1_DESIGNATION"].ToString();
                tr.hodDate = reader["VERIFIER1_DATE"].ToString();
                tr.trainerVerification = reader["VERIFIER1_VERIFY"].ToString();

                tr.hrName = reader["VERIFIER2_NAME"].ToString();
                tr.hrDesignation = reader["VERIFIER2_DESIGNATION"].ToString();
                tr.hrDate = reader["VERIFIER2_DATE"].ToString();
                tr.hrVerification = reader["VERIFIER2_VERIFY"].ToString();

              
                tr.dateRequested = reader["REQUESTER_DATE"].ToString();
               


                conn.Close();
                return tr;

            }
            else
            {

                conn.Close();
                return null;
            }
        }

        public List<string> GetOJT_Desc(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetOJT_Desc");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["DESCRIPTION"]).ToString());


            }
            conn.Close();
            return detail;

        }

        public void UpdateHod_OJT(UpdateHodOJT tr, string status)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_UPDATEOJT_HOD");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", tr.refNo);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@verifier_verify", tr.verify);
            cmd.Parameters.AddWithValue("@achieveTarget", tr.achieveTarget);
            cmd.Parameters.AddWithValue("@reason", tr.reason);

            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();


        }
    }
}
