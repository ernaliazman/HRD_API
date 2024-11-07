using hrd_backend.Interface;
using hrd_backend.Model;
using hrd_backend.Model.Job_Description;
using hrd_backend.Model.Orientation_Checklist;
using hrd_backend.Model.Personnel_R;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace hrd_backend.Data
{
    public class JDRepo :IJDRepo
    {
        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        public IDbRepo _dbRepo;

        CultureInfo culture = new CultureInfo("en-US");

        public JDRepo(IConfiguration config, IWebHostEnvironment env, IDbRepo dbRepo)
        {
            _config = config;

            this._env = env;
            _dbRepo = dbRepo;
        }

        public JD GetJD(JD tr, string uniqueKey)
        {
            //Orientation tr = new Orientation();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetJD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@uniqueKey", uniqueKey);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                tr.refNo = reader["REFERENCE_NUMBER"].ToString();
                //tr.status = reader["STATUS"].ToString();
                tr.company = reader["COMPANY_NAME"].ToString();
                tr.department = reader["DEPARTMENT"].ToString();

                tr.designation = reader["DESIGNATION"].ToString();
                tr.reportTo = reader["REPORT_TO"].ToString();
                tr.requesterId = (Guid)reader["REQUESTER_ID"];
                tr.requesterName = reader["REQUESTER_NAME"].ToString();
                tr.requesterDesignation = reader["REQUESTER_DESIGNATION"].ToString();
                tr.approverId = reader["APPROVER_ID"] != DBNull.Value
                ? (Guid)(reader["APPROVER_ID"]): Guid.Empty;
                tr.approverName = reader["APPROVER_NAME"].ToString();
                tr.approverDesignation = reader["APPROVER_DESIGNATION"].ToString();
                tr.pr_uniqueKey = reader["PR_UNIQUE_KEY"].ToString();
                tr.dateRequested = reader["REQUESTER_DATE"].ToString();
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

        public List<string> GetJD_Duty(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetJD_Duty");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["DUTY"]).ToString());


            }
            conn.Close();
            return detail;

        }



        public List<string> GetJD_Edu(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetJD_Edu");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["EDU"]).ToString());


            }
            conn.Close();
            return detail;

        }

        public List<string> GetJD_Exp(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetJD_Exp");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["EXP"]).ToString());


            }
            conn.Close();
            return detail;

        }

        public List<string> GetJD_Resp(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetJD_Resp");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["RESPONSIBILITIES"]).ToString());


            }
            conn.Close();
            return detail;

        }

        public List<string> GetJD_Skill(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetJD_Skill");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["SKILLS"]).ToString());


            }
            conn.Close();
            return detail;

        }

        public string AddJD(AddJD tr)
        {
            string date = DateTime.Now.ToString("MMMyy");
            var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);
            cmd.Parameters.AddWithValue("@uniqueKey", tr.pr_uniqueKey);
            //cmd.Parameters.AddWithValue("@position", tr.name);
            cmd.Parameters.AddWithValue("@company", tr.company);
            cmd.Parameters.AddWithValue("@department", tr.department);
            cmd.Parameters.AddWithValue("@designation", tr.designation);
            cmd.Parameters.AddWithValue("@reportTo", tr.reportTo);

            cmd.Parameters.AddWithValue("@id", tr.requesterId);
            cmd.Parameters.AddWithValue("@requesterName", tr.requesterName);
            //cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDept);
            cmd.Parameters.AddWithValue("@requesterDesignation", tr.requesterDesignation);
            // cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDesignation);

            cmd.Parameters.AddWithValue("@dateRequested", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();
            return refNo;

        }

        public void AddJD_Duty(AddJD jd, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            for (int i = 0; i < jd.duty.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD_DUTY");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@data", jd.duty[i]);

                cmd.ExecuteNonQuery();
            }



            conn.Close();

        }

        public void AddJD_Edu(AddJD jd, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            for (int i = 0; i < jd.education.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD_EDU");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@data", jd.education[i]);

                cmd.ExecuteNonQuery();
            }



            conn.Close();

        }

        public void AddJD_Exp(AddJD jd, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            for (int i = 0; i < jd.experience.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD_EXP");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@data", jd.experience[i]);

                cmd.ExecuteNonQuery();
            }

            conn.Close();

        }

        public void AddJD_Resp(AddJD jd, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            for (int i = 0; i < jd.responsibility.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD_RESP");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@data", jd.responsibility[i]);

                cmd.ExecuteNonQuery();
            }



            conn.Close();

        }

        public void AddJD_Skill(AddJD jd, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            for (int i = 0; i < jd.skills.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD_SKILLS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@data", jd.skills[i]);

                cmd.ExecuteNonQuery();
            }

            conn.Close();

        }

        public void UpdateHodJD(UpdateData tr, string status)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberJD");

            //string refNo = "JD-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_UPDATEJD_HOD");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", tr.refNo);
            cmd.Parameters.AddWithValue("@status", status);

            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();
          

        }
    }
}
