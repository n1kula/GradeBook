﻿using System.Net;

namespace Gradebook.Domain.Exceptions
{
    public class StudentAlreadyExistException : GradebookException
    {
        public string Email {  get; set; }

        public StudentAlreadyExistException(string email) : base($"Student with email {email} already exists.") => Email = email;
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    }
}
