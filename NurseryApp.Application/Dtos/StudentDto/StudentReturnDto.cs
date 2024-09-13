﻿namespace NurseryApp.Application.Dtos.StudentDto
{
    public class StudentReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string FileName { get; set; }
        public string GroupName { get; set; }
    }
}
