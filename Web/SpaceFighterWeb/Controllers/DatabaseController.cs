using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using SpaceFighterWeb.Models;

namespace SpaceFighterWeb.Controllers
{
    public class DatabaseController
    {
        public string[] GetTopTenScores()
        {
            var retVal = new List<string>();
            var models = new List<ScoreModel>();

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                var proc =  @"SP_TOP_TEN_SCORES_GET";
                con = new SqlConnection();
                con.Open();
                cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Date", System.DateTime.Today.ToString());
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    models.Add(new ScoreModel
                    {
                        user = rdr["username"].ToString(),
                        score = rdr["score"].ToString()
                    });
                }
            }
            catch (System.Exception)
            {
                models = null;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            foreach(var m in models)
            {
                retVal.Add(m.user);
                retVal.Add(m.score);
            }

            return retVal.ToArray();
        }


        public bool PutScore(string misc)
        {
            bool retVal = false;
            var data = misc.Split('|');
            var username = (data.Length == 2) ? data[0] : "...";
            var score = (data.Length == 2) ? data[1] : "0";

            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.Open();
                cmd = new SqlCommand("SP_SCORE_PUT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Username", username);
                cmd.Parameters.AddWithValue("@p_Score", score);
                cmd.Parameters.AddWithValue("@p_Misc", misc);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }


        public bool ClearScores()
        {
            bool retVal = false;

            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection();
                con.Open();
                cmd = new SqlCommand("SP_SCORES_DEL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }
    }
}
