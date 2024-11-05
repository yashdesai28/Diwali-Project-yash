using Npgsql;
using WebAPI.Models;

namespace WebAPI.Repositories;

public class ClasswiseSubjectRepository : IClasswiseSubjectRepository
{
    private readonly NpgsqlConnection connection;
    public ClasswiseSubjectRepository(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("POSTGRESQL_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString)) throw new Exception("PostgreSQL connection string is not defined.");
        NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
        this.connection = dataSource.OpenConnection();
    }

    public void AddClasswiseSubject(ClasswiseSubjects.Post addDetails)
    {
        NpgsqlCommand addClasswiseSubjectCommand = new("INSERT INTO t_classwise_subjects(c_standard, c_teacher_id, c_subject_id) VALUES(@standard, @teacher, @subject)", connection);
        addClasswiseSubjectCommand.Parameters.AddWithValue("standard", addDetails.Standard);
        addClasswiseSubjectCommand.Parameters.AddWithValue("teacher", addDetails.TeacherId);
        addClasswiseSubjectCommand.Parameters.AddWithValue("subject", addDetails.SubjectId);
        addClasswiseSubjectCommand.ExecuteNonQuery();
    }

    public List<ClasswiseSubjects.Default> GetClasswiseSubjects()
    {
        List<ClasswiseSubjects.Default> classwiseSubjectDetails = [];
        NpgsqlCommand getClasswiseSubjectCommand = new("SELECT * FROM t_classwise_subjects", connection);
        using NpgsqlDataReader reader = getClasswiseSubjectCommand.ExecuteReader();
        while (reader.Read())
        {
            classwiseSubjectDetails.Add(new() { Id = reader.GetInt16(0), Standard = reader.GetString(1), TeacherId = reader.GetInt16(3), SubjectId = reader.GetInt16(2) });
        }
        return classwiseSubjectDetails;
    }

    public void RemoveClasswiseSubject(int id)
    {
        NpgsqlCommand removeClasswiseSubjectCommand = new("DELETE FROM t_classwise_subjects WHERE c_id = @id", connection);
        removeClasswiseSubjectCommand.Parameters.AddWithValue("id", id);
        removeClasswiseSubjectCommand.ExecuteNonQuery();
    }

    public void UpdateClasswiseSubject(ClasswiseSubjects.Default details)
    {
        NpgsqlCommand updateClasswiseSubjectCommand = new("UPDATE t_classwise_subjects SET c_standard = @standard, c_teacher_id = @teacher, c_subject_id  = @subject WHERE c_id = @id", connection);
        updateClasswiseSubjectCommand.Parameters.AddWithValue("standard", details.Standard);
        updateClasswiseSubjectCommand.Parameters.AddWithValue("teacher", details.TeacherId);
        updateClasswiseSubjectCommand.Parameters.AddWithValue("subject", details.SubjectId);
        updateClasswiseSubjectCommand.Parameters.AddWithValue("id", details.Id);
        updateClasswiseSubjectCommand.ExecuteNonQuery();
    }
}