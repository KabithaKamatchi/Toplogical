using System;
using System.Collections.Generic;

namespace CollegeManagementSystem
{
	public class Course
	{
		public string strCourseName;
		public List<string> lstPrerequisites;

		public Course(string strCourseName, List<string> lstPrerequisites = null)
		{
			this.strCourseName = strCourseName;
			this.lstPrerequisites = lstPrerequisites ?? new List<string>();
		}

		public static List<string> GetTopologicalOrder(List<Course> lstCourses)
		{
			var lstSorted = new List<string>();
			var lstVisited = new List<string>();
			var lstVisiting = new List<string>();

			try
			{
				foreach(var objCourse in lstCourses)
				{
					if(!lstVisited.Contains(objCourse.strCourseName))
					{
						bool blnResult = DFS(objCourse, lstCourses, lstVisited, lstVisiting, lstSorted);
						if(!blnResult)
						{
							return new List<string> { $"Error: Cycle detected in course prerequisites involving: {objCourse.strCourseName}" };
						}
					}
				}
				return lstSorted;
			}
			catch(Exception ex)
			{
				
				return new List<string> { "Error: " + ex.Message };
			}
		}

		private static bool DFS(Course objCourse, List<Course> lstCourses,
			List<string> lstVisited, List<string> lstVisiting, List<string> lstResult)
		{
			string strName = objCourse.strCourseName;

			if(lstVisiting.Contains(strName))
				return false; 

			if(lstVisited.Contains(strName))
				return true; 

			lstVisiting.Add(strName);

			foreach(var strPrereq in objCourse.lstPrerequisites)
			{
				var prereqCourse = FindCourse(strPrereq, lstCourses);
				if(prereqCourse == null)
					throw new Exception($"Prerequisite course not found: {strPrereq}");

				if(!DFS(prereqCourse, lstCourses, lstVisited, lstVisiting, lstResult))
					return false;
			}

			lstVisiting.Remove(strName);
			lstVisited.Add(strName);
			lstResult.Add(strName);

			return true;
		}

		private static Course FindCourse(string strName, List<Course> lstCourses)
		{
			foreach(var course in lstCourses)
			{
				if(course.strCourseName == strName)
					return course;
			}
			return null;
		}
	}
}
