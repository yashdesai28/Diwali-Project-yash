using WebAPI.Models;

namespace WebAPI.Repositories;

interface ISchoolInfoRepository
{
    public (string?, SchoolInfo?) GetSchoolInfo();
    public void UpdateSchoolInfo(SchoolInfo schoolInfo);
}