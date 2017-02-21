using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class ResultEntryController : Controller
    {
        private AustDBContext db = new AustDBContext();
        //
        // GET: /ResultEntry/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectEnrolledCourseForStudent(int? studentId)
        {
            var courses = GetEnrolledCourses(studentId);
            ViewBag.CourseId = new SelectList(courses.ToArray(), "CourseId", "Code");
            return PartialView("_Course", ViewData["CourseId"]);
        }

        private List<Course> GetEnrolledCourses(int? studentId)
        {
            List<Enrollment> enrollments = db.Enrollments.Where(e => e.StudentId == studentId).ToList();
            List<Course> courseList = db.Courses.ToList();
            List<Course> courses = new List<Course>();
            foreach (Course course in courseList)
            {
                foreach (Enrollment enrollment in enrollments)
                {
                    if (enrollment.CourseId == course.CourseId)
                    {
                        courses.Add(enrollment.Course);
                    }
                }
            }
            return courses;
        }

        public ActionResult SelectStudent(int? studentId)
        {
            Student student = (db.Students.Where(s => s.StudentId == studentId)).Single();
            Enrollment enrollment = new Enrollment();
            enrollment.Student = student;
            return PartialView("_StudentDetails", enrollment);
        }

        public ActionResult StudentResultEntry()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
            ViewBag.CourseId = new SelectList("", "CourseId", "Code");
            ViewBag.GradeLetterId = new SelectList(db.GradeLetters, "GradeLetterId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult StudentResultEntry(Enrollment enrollment)
        {
            LoadDropdownList(enrollment);

            if (enrollment.StudentId == 0 || enrollment.CourseId == 0 || enrollment.GradeLetterId == null)
            {
                ViewBag.Message = "All fields are required.";
                return View();
            }
            GradeLetter gradeLetter = (db.GradeLetters.Where(g => g.GradeLetterId == enrollment.GradeLetterId)).Single();
            Course course = (db.Courses.Where(c => c.CourseId == enrollment.CourseId)).Single();
            Student student = (db.Students.Where(s => s.StudentId == enrollment.StudentId)).Single();
            Enrollment enrolled = (db.Enrollments.Where(e => (e.StudentId == student.StudentId && e.CourseId == course.CourseId))).Single();

            bool check = db.Enrollments.Count(e => (e.StudentId == student.StudentId && e.CourseId == course.CourseId && e.GradeLetter != null)) == 1;
            enrollment = enrolled;
            enrollment.GradeLetter = gradeLetter;
            enrollment.GradeLetterId = gradeLetter.GradeLetterId;
            enrollment.Student = enrolled.Student;
            enrollment.StudentId = enrolled.StudentId;
            enrollment.Course = course;
            if (!check)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.Saved = student.Name + " got " + gradeLetter.Name + " in " + course.Name;
                return View(enrollment);
            }

            ViewBag.Message = course.Name + " course grade already assigned for " + student.Name;
            return View(enrollment);
        }

        private void LoadDropdownList(Enrollment enrollment)
        {
            List<Course> enrolledCourses = GetEnrolledCourses(enrollment.StudentId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
            ViewBag.CourseId = new SelectList(enrolledCourses, "CourseId", "Code");
            ViewBag.GradeLetterId = new SelectList(db.GradeLetters, "GradeLetterId", "Name");
        }

        //public ActionResult ViewResult(int? studentId)
        //{
        //    ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
        //    var enrollments = db.Enrollments.Where(e => e.StudentId == studentId);
        //    return View(enrollments.ToList());
        //}

        //[HttpPost]
        //public ActionResult ViewResult(int studentId)
        //{
        //    var enrollments = db.Enrollments.Where(e => e.StudentId == studentId);
        //    var pdf = new PdfResult(enrollments.ToArray(), "ViewPDFResult");
        //    return pdf;
        //}

        public PartialViewResult SelectEnrolledStudentInformation(int? studentId)
        {
            var enrollments = db.Enrollments.Where(e => e.StudentId == studentId);
            return PartialView("_CourseInformation", enrollments.ToList());
        }

        //public ActionResult SelectStudentDetails(int? studentId)
        //{
        //    var enrollments = db.Enrollments.Include(e => e.Student).Where(e => e.StudentId == studentId).Include(e => e.Course).Include(e => e.GradeLetter);
        //    return PartialView("_StudentDetails", enrollments.ToList());
        //}
	}
}