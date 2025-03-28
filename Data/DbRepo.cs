using hrd_backend.Interface;
using hrd_backend.Model;
using hrd_backend.Model.Job_Description;
using hrd_backend.Model.Orientation_Checklist;
using hrd_backend.Model.Training_Evaluation;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;

namespace hrd_backend.Data
{
    public class DbRepo : IDbRepo
    {
        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        CultureInfo culture = new CultureInfo("en-US");

        public DbRepo(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;

            this._env = env;
        }

        //public List<PendingTasks> GetWaitingToVerify()
        //{
        //    List<PendingTasks> ent = new List<PendingTasks>();
        //    string connection = _config["ConnectionStrings:ServicePortalConnection"];
        //    SqlConnection conn = new SqlConnection(connection);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("sp_EC_GETWAITINGVERIFY");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection = conn;


        //    SqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        ent.Add(new PendingTasks
        //        {
        //            grandTotal = Convert.ToDecimal(reader["GRAND_TOTAL"]),
        //            reportName = reader["REPORT_NAME"].ToString(),
        //            referenceNumber = reader["REFERENCE_NUMBER"].ToString(),
        //            requestedDate = reader["DATE_REQUESTED"].ToString(),
        //            requesterName = reader["REQUESTER_NAME"].ToString(),
        //            email = reader["VERIFIER_EMAIL"].ToString(),


        //        });

        //    }

        //    if (ent != null)
        //    {
        //        conn.Close();
        //        return ent;
        //    }
        //    else
        //    {
        //        conn.Close();
        //        return null;
        //    }
        //}

        public void AddJobDesc(AddJD jd)
        {
            var runningNo = GetRunningNumber(DateTime.Now.Month.ToString("MMMYY"), "sp_HRD_GetNumberJD");

            string refNo = "JD/" + DateTime.Now.Month.ToString("MMMYY") + "/" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTJD");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@referenceNo", refNo);
            cmd.Parameters.AddWithValue("@companyName", jd.company);
            cmd.Parameters.AddWithValue("@department", jd.department);
            cmd.Parameters.AddWithValue("@designation", jd.designation);
            cmd.Parameters.AddWithValue("@companyName", jd.company);
            cmd.Parameters.AddWithValue("@companyName", jd.company);
            cmd.Parameters.AddWithValue("@companyName", jd.company);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
        public int GetRunningNumber(string date, string storePro)
        {
            int sn;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(storePro);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@condition", date);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                sn = Convert.ToInt32(reader["ID"]) + 1;
                conn.Close();
                return sn;
            }
            else
            {
                sn = 1;
                conn.Close();
                return sn;
            }

        }

        public string AddTrainingDetails(AddTraining tr)
        {
            string date = DateTime.Now.ToString("MMMyy");
            var runningNo = GetRunningNumber(date, "sp_HRD_GetNumberTR");

           

            string refNo = "TE-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_INSERTTR");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", refNo);
            cmd.Parameters.AddWithValue("@name", tr.name );
            cmd.Parameters.AddWithValue("@designation", tr.designation);
            cmd.Parameters.AddWithValue("@courseTitle",tr.courseTitle );
            cmd.Parameters.AddWithValue("@institution", tr.trainingInstitution);
            cmd.Parameters.AddWithValue("@trainerName", tr.trainerName );
            cmd.Parameters.AddWithValue("@trainingDate", tr.trainingDate );
            cmd.Parameters.AddWithValue("@trainingVenue", tr.trainingVenue);
            cmd.Parameters.AddWithValue("@nature", tr.trainingNature);
            cmd.Parameters.AddWithValue("@objective", tr.programObjective);
            cmd.Parameters.AddWithValue("@support", tr.programSupport);
            cmd.Parameters.AddWithValue("@presentation", tr.trainerPresentation);
            cmd.Parameters.AddWithValue("@testResult", tr.testResult);
            cmd.Parameters.AddWithValue("@benefit", tr.trainingBenefit);
            cmd.Parameters.AddWithValue("@comment", tr.generalComment);
            cmd.Parameters.AddWithValue("@id", tr.requesterId);
           // cmd.Parameters.AddWithValue("@verifierId", tr.verifierId);
            cmd.Parameters.AddWithValue("@dateRequested", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();

            return refNo;

        }

        public Training GetTrainingDetails(string refNo)
        {
            Training tr = new Training();
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetTrainingDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@refNo", refNo);


            SqlDataReader reader = cmd.ExecuteReader();

            var data = reader.Read();

            if (data)
            {
                tr.refNo = reader["REFERENCE_NUMBER"].ToString();
                tr.status = reader["STATUS"].ToString();
                tr.courseTitle = reader["COURSE_TITLE"].ToString();
                tr.trainingInstitution = reader["TRAINING_INSTITUTION"].ToString();
                tr.trainerName = reader["TRAINER_NAME"].ToString();
                tr.trainingDate = reader["TRAINING_DATE"].ToString();
                tr.trainingVenue = reader["TRAINING_VENUE"].ToString();
                tr.trainingNature = reader["TRAINING_NATURE"].ToString();
                tr.programObjective = reader["TICK_PROGRAM_OBJECTIVES"].ToString();
                tr.programSupport = reader["TICK_PROGRAM_SUPPORT"].ToString();
                tr.trainerPresentation = reader["TICK_TRAINER_PRESENTATION"].ToString();
                tr.testResult = reader["TICK_TEST_RESULT"].ToString();
                tr.trainingBenefit = reader["TRAINING_BENEFITS"].ToString();
                tr.generalComment = reader["GENERAL_COMMENTS"].ToString();
                tr.name = reader["REQUESTER_NAME"].ToString();
                tr.designation = reader["REQUESTER_DESIGNATION"].ToString();
                tr.verifierFeedback = reader["VERIFIER_FEEDBACK"].ToString();
                tr.verifierName = reader["VERIFIER_NAME"].ToString();
                tr.verifiedDate = reader["VERIFIER_DATE"].ToString();
                tr.dateRequested = reader["DATE_REQUESTED"].ToString();

                conn.Close();
                return tr;
            
            }
            else
            {
               
                conn.Close();
                return null;
            }

        }

        public void UpdateHodTR(UpdateData tr, string status)
        {
            //string date = DateTime.Now.ToString("MMMyy");
            //var runningNo = GetRunningNumber(date, "sp_HRD_GetNumberTR");



            //string refNo = "TE-" + date + "-" + runningNo;
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_HRD_UPDATETR_HOD");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@refNo", tr.refNo);
            cmd.Parameters.AddWithValue("@data", tr.data);
            cmd.Parameters.AddWithValue("@status", status);

            // cmd.Parameters.AddWithValue("@verifierId", tr.verifierId);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("f", culture));

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public SuperiorDetails get_verifier(string employee_id)
        {
            SuperiorDetails vf = new SuperiorDetails();

            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EC_GetVerifierEmail");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@REQUESTER_ID", employee_id);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                vf = new SuperiorDetails
                {
                    //verify_id = rd["EMP_ID"].ToString(),
                    id = (Guid)rd["USERNAME_ID"],
                    email = rd["EMAIL_ADDRESS"].ToString(),
                    name = rd["NAME"].ToString(),
                    department = rd["DEPARTMENT"].ToString(),
                    designation = rd["POSITION_TITLE"].ToString(),
                };
            }
            conn.Close();
            return vf;
        }

        public SuperiorDetails get_checker(Guid usernameId)
        {
            SuperiorDetails ck = new SuperiorDetails();

            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EC_GetChecker");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@USERNAME_ID", usernameId);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ck = new SuperiorDetails
                {
                    id = (Guid)rd["USERNAME_ID"],
                    email = rd["EMAIL_ADDRESS"].ToString(),
                    name = rd["NAME"].ToString(),
                    department = rd["DEPARTMENT"].ToString(),
                    designation = rd["POSITION_TITLE"].ToString(),
                };
            }
            conn.Close();
            return ck;
        }

        public SuperiorDetails get_approver1(string emp_id)
        {
            SuperiorDetails ap = new SuperiorDetails();

            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_EC_GetApprover1");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@EMP_ID", emp_id);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ap = new SuperiorDetails
                {
                    id = (Guid)rd["USERNAME_ID"],
                    email = rd["EMAIL_ADDRESS"].ToString(),
                    name = rd["NAME"].ToString(),
                    department = rd["DEPARTMENT"].ToString(),
                    designation = rd["POSITION_TITLE"].ToString(),
                };
            }
            conn.Close();
            return ap;
        }




    }

}

