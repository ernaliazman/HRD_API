using hrd_backend.Interface;
using hrd_backend.Model.Orientation_Checklist;
using hrd_backend.Model.Personnel_R;
using hrd_backend.Model.Training_Evaluation;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;

namespace hrd_backend.Data
{
    public class PRRepo : IPRRepo
    {
        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        public IDbRepo _dbRepo;

        CultureInfo culture = new CultureInfo("en-US");

        public PRRepo(IConfiguration config, IWebHostEnvironment env, IDbRepo dbRepo)
        {
            _config = config;

            this._env = env;
            _dbRepo = dbRepo;
        }

        public PRDetails GetPR(PRDetails tr, string refNo)
        {
            //PRDetails tr = new PRDetails();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetPR");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                tr.uniqueKey = reader["UNIQUE_KEY"].ToString();
                tr.refNo = reader["REFERENCE_NUMBER"].ToString();
                tr.status = reader["STATUS"].ToString();
                tr.position = reader["POSITION"].ToString();
                tr.company = reader["COMPANY"].ToString();
                tr.department = reader["DEPARTMENT"].ToString();
                tr.location = reader["LOCATION"].ToString();
                tr.dateRequired = reader["DATE_REQUIRED"].ToString();
                tr.numberPersonnel = Convert.ToInt32(reader["NO_PERSONNEL_REQUIRED"].ToString());
                tr.basicSalary = Convert.ToInt32(reader["BASIC_SALARY_PROPOSE"]);
                tr.requisitionPurpose = reader["REQUISITION_PURPOSE"].ToString();
                tr.name = reader["NAME_PERSON_REPLACED"].ToString();
                tr.manpowerBudget = reader["MANPOWER_BUDGET"].ToString();
                tr.reasonUnbudget = reader["REASON_UNBUDGET"].ToString();
                tr.requestReason = reader["REASON_FOR_REQUEST"].ToString();
                tr.ageLimit = reader["AGE_LIMIT"].ToString().ToString();
                tr.expRequired = reader["EXPERIENCE_REQUIRED"].ToString();
                tr.yearsRequired = Convert.ToInt32(reader["EXPERIENCE_YEARS"]);
                tr.qualificationRequired = reader["QUALIFICATION_REQUIRED"].ToString();
                tr.disciplineSpecification = reader["DISCIPLINE_SPECIFICATION"].ToString();
                tr.computerLiteracyRequired = reader["COMPUTER_LITERACY_REQUIRED"].ToString();
                tr.computerSpecification = reader["COMPUTER_SPECIFICATION"].ToString();
                tr.ownTransportRequired = reader["OWN_TRANSPORT_REQUIRED"].ToString();

                tr.others = reader["OTHERS"].ToString();

                tr.dateRequested = reader["DATE_REQUESTED"].ToString();
                tr.requesterName = reader["REQUESTER_NAME"].ToString();
                tr.requesterDesignation = reader["REQUESTER_DESIGNATION"].ToString();
                tr.requesterDept = reader["DEPARTMENT"].ToString();

                tr.verifier1.name = reader["VERIFIER1_NAME"].ToString();
                tr.verifier1.designation = reader["VERIFIER1_DESIGNATION"].ToString();
                tr.verifier1.date = reader["VERIFIER1_DATE"].ToString();
                tr.verifier1.dept = reader["DEPARTMENT"].ToString();

                tr.verifier2.name = reader["VERIFIER2_NAME"].ToString();
                tr.verifier2.designation = reader["VERIFIER2_DESIGNATION"].ToString();
                tr.verifier2.date = reader["VERIFIER2_DATE"].ToString();
                tr.verifier2.dept = reader["VERIFIER2_DEPT"].ToString();

                tr.approver.name = reader["APPROVER_NAME"].ToString();
                tr.approver.designation = reader["APPROVER_DESIGNATION"].ToString();
                tr.approver.date = reader["APPROVER_DATE"].ToString();
                tr.approver.dept = reader["APPROVER_DEPT"].ToString();

                tr.hrd.name = reader["HRD_NAME"].ToString();
                tr.hrd.date = reader["HRD_DATE"].ToString();
                tr.hrd.actionBy = reader["HRD_ACTION_BY"].ToString();
                tr.hrd.dateBy = reader["HRD_DATEBY"].ToString();

                tr.hrd.positionBy = reader["POSITION_FILLED_BY"].ToString();
                tr.hrd.dateJoined = reader["DATE_JOINED"].ToString();
                conn.Close();
                return tr;

            }
            else
            {

                conn.Close();
                return null;
            }

        }

        public List<string> GetPRDetail_Job(string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetPR_Job");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            //var data = ;

            while (reader.Read())
            {
                detail.Add((reader["FUNCTION_TECHNICALJOB"]).ToString());

             
            }
            conn.Close();
            return detail;

        }
        public List<string> GetPRDetail_Personal( string refNo)
        {
            //PRDetails tr = new PRDetails();
            List<string> detail = new List<string>();
            // List<AddPR_Details> detail = new List<AddPR_Details>();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetPR_Personal");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

           // var data = reader.Read();

            while (reader.Read())
            {
                detail.Add((reader["Competency"]).ToString());


            }
            conn.Close();
            return detail;


        }

        public string AddPRData(AddPR tr)
        {
            string date = DateTime.Now.ToString("MMMyy");
            var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberPR");

            string refNo = "PR-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTPR");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", refNo);
            cmd.Parameters.AddWithValue("@position", tr.name);
            cmd.Parameters.AddWithValue("@company", tr.company);
            cmd.Parameters.AddWithValue("@department", tr.department);
            cmd.Parameters.AddWithValue("@location", tr.location);
            cmd.Parameters.AddWithValue("@dateRequired", tr.dateRequired);
            cmd.Parameters.AddWithValue("@noPersonnel", tr.numberPersonnel);
            cmd.Parameters.AddWithValue("@basicSalary", tr.basicSalary);
            cmd.Parameters.AddWithValue("@reqPurpose", tr.requisitionPurpose);
            cmd.Parameters.AddWithValue("@nameReplaced", tr.name);
            cmd.Parameters.AddWithValue("@manpowerBudget", tr.manpowerBudget);
            cmd.Parameters.AddWithValue("@reasonUnbudget", tr.reasonUnbudget);
            cmd.Parameters.AddWithValue("@reasonRequest", tr.requestReason);
            cmd.Parameters.AddWithValue("@ageLimit", tr.ageLimit);
            cmd.Parameters.AddWithValue("@expRequired", tr.expRequired);
            cmd.Parameters.AddWithValue("@expYears", tr.yearsRequired);
            cmd.Parameters.AddWithValue("@qualificationReq", tr.qualificationRequired);
            cmd.Parameters.AddWithValue("@disciplineSpec", tr.disciplineSpecification);
            cmd.Parameters.AddWithValue("@computerLiteracy", tr.computerLiteracyRequired);
            cmd.Parameters.AddWithValue("@computerSpec", tr.computerSpecification);
            cmd.Parameters.AddWithValue("@ownTransport", tr.ownTransportRequired);
            cmd.Parameters.AddWithValue("@others", tr.others);
            cmd.Parameters.AddWithValue("@uniquekey", tr.uniqueKey);

            cmd.Parameters.AddWithValue("@id", tr.requesterId);
            cmd.Parameters.AddWithValue("@requesterName", tr.requesterName);
            //cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDept);
            cmd.Parameters.AddWithValue("@designation", tr.requesterDesignation);
            // cmd.Parameters.AddWithValue("@requesterdept", tr.requesterDesignation);

            cmd.Parameters.AddWithValue("@dateRequested", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();
            return refNo;

        }

        public void AddPR_Job(AddPR tr, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberPR");

            //string refNo = "PR-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

          //  SqlCommand cmd = new SqlCommand("sp_HRD_INSERTPR_JOB");

            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = conn;

            for (int i = 0; i < tr.jobCompetency.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTPR_JOB");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@job", tr.jobCompetency[i]);
         
                cmd.ExecuteNonQuery();
            }



            conn.Close();

        }

        public void AddPR_Personal(AddPR tr, string refNo)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = _dbRepo.GetRunningNumber(date, "sp_HRD_GetNumberPR");

            //string refNo = "PR-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            //SqlCommand cmd = new SqlCommand("sp_HRD_INSERTPR_PERSONAL");

            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = conn;

            for (int i = 0; i < tr.personalCompetency.Length; i++)
            {
                // Clear previous parameters
                //Console.WriteLine(addPTW.hazard.Length);
                //Console.WriteLine(addPTW.hazard[i]);
                SqlCommand cmd = new SqlCommand("sp_HRD_INSERTPR_PERSONAL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@refNo", refNo);
                cmd.Parameters.AddWithValue("@personal", tr.personalCompetency[i]);

                cmd.ExecuteNonQuery();
            }

            conn.Close();

        }
    }
}
