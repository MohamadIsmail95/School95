using AutoMapper;
using ERP.TEST.Cources;
using ERP.TEST.Courses;
using ERP.TEST.Students;

namespace ERP.TEST;

public class TESTApplicationAutoMapperProfile : Profile
{
    public TESTApplicationAutoMapperProfile()
    {
      
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<CreateUpdateCourseDto, Course>().ReverseMap();
        CreateMap<StudentWithDetails, StudentDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();


    }
}
