using WebAPI.Models;

namespace WebAPI.Repositories;

interface ISchoolInfoRepository
{
    public SchoolInfo GetSchoolInfo();
    public void UpdateSchoolInfo(SchoolInfo schoolInfo);
}