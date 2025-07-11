using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeManagementSystem
{
	[TestClass]
	public sealed class CourseSchedulerTests
	{
		[TestMethod]
		public void Test_ValidCourseOrder()
		{
			// Arrange
			List<Course> lstCourses = new List<Course>
			{
				new Course("Math101"),
				new Course("Physics101", new List<string> { "Math101" }),
				new Course("Chemistry101", new List<string> { "Math101" })
			};

			List<string> expected = new List<string> { "Math101", "Physics101", "Chemistry101" };

			// Act
			List<string> actual = Course.GetTopologicalOrder(lstCourses);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Test_CycleDetected()
		{
			// Arrange
			List<Course> lstCourses = new List<Course>
			{
				new Course("CourseA", new List<string> { "CourseB" }),
				new Course("CourseB", new List<string> { "CourseA" })
			};

			List<string> expected = new List<string> { "Error: Cycle detected in course prerequisites involving: CourseA" };

			// Act
			List<string> actual = Course.GetTopologicalOrder(lstCourses);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Test_MissingPrerequisite()
		{
			// Arrange
			List<Course> lstCourses = new List<Course>
			{
				new Course("Biology101", new List<string> { "ScienceBasics" }) 
            };

			List<string> expected = new List<string> { "Error: Prerequisite course not found: ScienceBasics" };

			// Act
			List<string> actual = Course.GetTopologicalOrder(lstCourses);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Test_SingleCourseOnly()
		{
			// Arrange
			List<Course> lstCourses = new List<Course>
			{
				new Course("History101")
			};

			List<string> expected = new List<string> { "History101" };

			// Act
			List<string> actual = Course.GetTopologicalOrder(lstCourses);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
