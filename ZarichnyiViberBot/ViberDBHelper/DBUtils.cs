using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ZarichnyiViberBot.ViberDBHelper
{
    public class DBUtils
    {
        public static List<WalkAnalytics> GetWalkAnalyticsAllTime(string IMEI) {
            List<WalkAnalytics> walkAnalyticss = new List<WalkAnalytics>();
            string sqlText = @$"SELECT 
                                COUNT(1) AS CountOfWalk, 
                                SUM(Distance) AS TotalDistance, 
                                CAST(DATEADD(SECOND, SUM(DATEDIFF(SECOND, 0, CONVERT(TIME, TimeWalk))), 0) AS TIME(0)) AS TotalTime 
                            FROM [dbo].[WalkInfo] 
                            $WHERE IMEI = '{IMEI}'";
            try {
                using (SqlConnection conn = new SqlConnection(Startup.CONNECTION_STRING)) {
                    using (SqlCommand cmd = new SqlCommand(sqlText, conn)) {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    WalkAnalytics WalkAnalytics = new WalkAnalytics();
                                    int a = 0;
                                    decimal b = 0;
                                    TimeSpan c = TimeSpan.Parse("00:00:00");
                                    if (int.TryParse(reader["CountOfWalk"].ToString(), out a) &&
                                            decimal.TryParse(reader["TotalDistance"].ToString(), out b) &&
                                            TimeSpan.TryParse(reader["TotalTime"].ToString(), out c)) {
                                        WalkAnalytics.CountOfWalk = a;
                                        WalkAnalytics.TotalDistance = b;
                                        WalkAnalytics.TotalTime = c;
                                    } else {
                                        WalkAnalytics.CountOfWalk = a;
                                        WalkAnalytics.TotalDistance = b;
                                        WalkAnalytics.TotalTime = c;
                                    }
                                    walkAnalyticss.Add(WalkAnalytics);
                                }
                            }
                        }
                    }
                }

                if (walkAnalyticss.Count > 1) {
                    throw new Exception("Invalid walk analytics data");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Unidentified DB exception: {ex}");
            }
            return walkAnalyticss;
        }

        public static List<WalkAnalytics> GetWalkAnalyticsOneDay(string IMEI) {
            List<WalkAnalytics> walkAnalyticss = new List<WalkAnalytics>();
            ///test date, in db old date
            ///DateTime test
            string sqlText = "SELECT " +
                                "COUNT(1) AS CountOfWalk, " +
                                "SUM(Distance) AS TotalDistance, " +
                                "CAST(DATEADD(SECOND, SUM(DATEDIFF(SECOND, 0, CONVERT(TIME, TimeWalk))), 0) AS TIME(0)) AS TotalTime " +
                            "FROM[dbo].[WalkInfo] " +
                            $"WHERE IMEI = '{IMEI}' " +
                            $"AND DATEWALK = '{DateTime.UtcNow.ToString("yyyy-MM-dd")}'";
            try {
                using (SqlConnection conn = new SqlConnection(Startup.CONNECTION_STRING)) {
                    using (SqlCommand cmd = new SqlCommand(sqlText, conn)) {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    WalkAnalytics WalkAnalytics = new WalkAnalytics();
                                    int a = 0;
                                    decimal b = 0;
                                    TimeSpan c = TimeSpan.Parse("00:00:00");
                                    if (int.TryParse(reader["CountOfWalk"].ToString(), out a) &&
                                            decimal.TryParse(reader["TotalDistance"].ToString(), out b) &&
                                            TimeSpan.TryParse(reader["TotalTime"].ToString(), out c)) {
                                        WalkAnalytics.CountOfWalk = a;
                                        WalkAnalytics.TotalDistance = b;
                                        WalkAnalytics.TotalTime = c;
                                    } else {
                                        WalkAnalytics.CountOfWalk = a;
                                        WalkAnalytics.TotalDistance = b;
                                        WalkAnalytics.TotalTime = c;
                                    }
                                    walkAnalyticss.Add(WalkAnalytics);
                                }
                            }
                        }
                    }
                }

                if (walkAnalyticss.Count > 1) {
                    throw new Exception("Invalid walk analytics data");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Unidentified DB exception: {ex}");
            }
            return walkAnalyticss;
        }

        public static List<TopWalk> GetTopWalkAllTime(string IMEI) {
            List<TopWalk> topWalk = new List<TopWalk>();
            string sqlText = "SELECT TOP 10 " +
                               "WalkNumber, " +
                               "Distance, " +
                               "TimeWalk " +
                            "FROM[dbo].[WalkInfo] " +
                            $"WHERE IMEI = '{IMEI}' " +
                            "ORDER BY Distance";
            try {
                using (SqlConnection conn = new SqlConnection(Startup.CONNECTION_STRING)) {
                    using (SqlCommand cmd = new SqlCommand(sqlText, conn)) {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    TopWalk TopWalk = new TopWalk();
                                    int a;
                                    decimal b;
                                    TimeSpan c;
                                    if (int.TryParse(reader["WalkNumber"].ToString(), out a) &&
                                            decimal.TryParse(reader["Distance"].ToString(), out b) &&
                                            TimeSpan.TryParse(reader["TimeWalk"].ToString(), out c)) {
                                        TopWalk.WalkNumber = a;
                                        TopWalk.Distance = b;
                                        TopWalk.TimeWalk = c;
                                    } else { break; }
                                    topWalk.Add(TopWalk);
                                }
                            }
                        }
                    }
                }

                if (topWalk.Count > 1) {
                    throw new Exception("Invalid walk analytics data");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Unidentified DB exception: {ex}");
            }
            return topWalk;
        }

        public static List<TopWalk> GetTopWalkOneDay(string IMEI) {
            List<TopWalk> topWalk = new List<TopWalk>();
            string sqlText = "SELECT TOP 10 " +
                               "WalkNumber, " +
	                           "Distance, " +
	                           "TimeWalk " +
                            "FROM[dbo].[WalkInfo] " +
                            $"WHERE IMEI = 'IMEI' " +
                            $"AND DATEWALK = '{DateTime.UtcNow.ToString("yyyy-MM-dd")}' " +
                            "ORDER BY Distance";
            try {
                using (SqlConnection conn = new SqlConnection(Startup.CONNECTION_STRING)) {
                    using (SqlCommand cmd = new SqlCommand(sqlText, conn)) {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    TopWalk TopWalk = new TopWalk();
                                    int a;
                                    decimal b;
                                    TimeSpan c;
                                    if (int.TryParse(reader["WalkNumber"].ToString(), out a) &&
                                            decimal.TryParse(reader["Distance"].ToString(), out b) &&
                                            TimeSpan.TryParse(reader["TimeWalk"].ToString(), out c)) {
                                        TopWalk.WalkNumber = a;
                                        TopWalk.Distance = b;
                                        TopWalk.TimeWalk = c;
                                    } else { break; }
                                    topWalk.Add(TopWalk);
                                }
                            }
                        }
                    }
                }

                if (topWalk.Count > 1) {
                    throw new Exception("Invalid walk analytics data");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Unidentified DB exception: {ex}");
            }
            return topWalk;
        }
    }
}
