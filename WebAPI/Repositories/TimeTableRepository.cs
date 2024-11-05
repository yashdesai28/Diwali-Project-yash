using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class TimeTableRepository : ITimeTableRepository
    {
        private readonly NpgsqlConnection connection;

        public TimeTableRepository(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("POSTGRESQL_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString)) throw new Exception("PostgreSQL connection string is not defined.");
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
            this.connection = dataSource.OpenConnection();
        }

        public List<Teacherinfo> GetTeacherinfos()
        {
            List<Teacherinfo> teacherinfos = new List<Teacherinfo>();
            NpgsqlCommand getTeacherinfosCommand = new("SELECT c_user_id,c_name,c_image from t_users where c_role='Teacher'", connection);
            using NpgsqlDataReader reader = getTeacherinfosCommand.ExecuteReader();
            while (reader.Read())
            {
                var teacherinfo = new Teacherinfo{userid = reader.GetInt32(0),teachername = reader.GetString(1),img = reader.GetString(2)};
                teacherinfos.Add(teacherinfo);
            }
            return teacherinfos;
        }
    }
}