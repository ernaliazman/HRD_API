using hrd_backend.Interface;
using hrd_backend.Model.General;
using hrd_backend.Model.Training_Evaluation;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace hrd_backend.Data
{
    public class GeneralRepo : IGeneralRepo
    {
        public readonly IConfiguration _config;

        public readonly IWebHostEnvironment _env;

        public IDbRepo _dbRepo;

        CultureInfo culture = new CultureInfo("en-US");

        public GeneralRepo(IConfiguration config, IWebHostEnvironment env, IDbRepo dbRepo)
        {
            _config = config;

            this._env = env;
            _dbRepo = dbRepo;
        }

        public List<Request> GetAllRequests(Guid id)
        {
            List<Request> request = new List<Request>(); 
            string connection = _config["ConnectionStrings:ServicePortalConnection"];
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_HRD_GetAllRequests");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@userId", id);


            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
               
                request.Add(new Request
                {
                    
                    refNo = reader["Reference_Number"].ToString(),
                    dateRequested = reader["Date_requested"].ToString(),
                    requesterName = reader["Name"].ToString(),
                    requesterDept = reader["Department"].ToString(),
                    status = reader["Status"].ToString(),
                  
                 
                });

            }
            if (request != null)
            {
                conn.Close();
                return request;
            }
            else
            {
                conn.Close();
                return null;
            }


        }
    }
}
