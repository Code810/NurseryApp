﻿namespace NurseryApp.Core.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string RoomNumber { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<HomeWork> HomeWorks { get; set; }
    }
}
