﻿using AutoMapper;
using ERP.TEST.Cources;
using ERP.TEST.Courses;
using ERP.TEST.Students;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.TEST;

public class TESTApplicationAutoMapperProfile : Profile
{
    public TESTApplicationAutoMapperProfile()
    {
      
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<CreateUpdateCourseDto, Course>().ReverseMap();
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.CourseNames, opt => opt.MapFrom(src => src.RelationCourses
            .Select(x=> x.Name)
        )).ReverseMap();

        CreateMap<CreateUpdateStudentDto, Student>()
         .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.CoursesId.
         Select(x => new StudentCourse(Guid.NewGuid(),(Guid)src.Id, x)).ToList())).ReverseMap();

    }
}
