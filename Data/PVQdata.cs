using MVCTEST.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVCTEST.Data
{
    public class PVQdata
    {
        string connetionString = "Data Source=LOCALHOST;Initial Catalog=DummyDatabase;Integrated Security=True;";
        List<Pvqquestionsmodel> pvqAnswers = new List<Pvqquestionsmodel>();
        public PVQdata()
        {

        }
        public List<Pvqquestionsmodel> GetQuestionsAndAnswersbyid(int? userid)
        {
            try
            {
                if (userid == null || userid == 0)
                {
                    return null;
                }
                else
                {
                    SqlConnection sqlCnn = new SqlConnection(connetionString);

                    sqlCnn.Open();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.CommandText = "GetUserAnswersByUserId";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.Parameters.Add("@user_id", SqlDbType.VarChar).Value = userid;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    for (int i = 0; i <= dt.Rows.Count-1; i++)
                    {
                        pvqAnswers.Add(new Pvqquestionsmodel { Q_ID = (int)dt.Rows[i][0], Question = (string)dt.Rows[i][1], Answer = (string)dt.Rows[i][2] });
                    }

                    sqlCmd.Dispose();
                    sqlCnn.Close();
                    return pvqAnswers;
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}