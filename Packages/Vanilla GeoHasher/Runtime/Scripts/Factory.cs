//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//
//using UnityEngine;
//
//public class Factory<V>
//	where V : IVehicle, IShape
//{
//
//	public V something;
//
//}
//
//
//public interface IShape
//{
//
//	
//
//}
//
//public class Circle : IShape
//{
//
//	
//
//}
//
//public interface IVehicle
//{
//
//	
//
//}
//
//public class Boat : IVehicle
//{
//
//	public float motorSpeed = 10.0f;
//
//}
//
//public class School
//{
//
//	public Student[] students = new Student[30];
//
//	public List<Teacher> teachers = new List<Teacher>();
//
//	public Queue<Student> lunchLine = new Queue<Student>();
//
//	public Stack<Student> stacksOn = new Stack<Student>();
//
//	public Dictionary<int, Student> lockerSystem = new Dictionary<int, Student>();
//
//	public HashSet<Student> studentSet = new HashSet<Student>();
//
//	public SortedSet<Student> studentSet2 = new SortedSet<Student>();
//	
//	void Start()
//	{
//		foreach (var student in students)
//		{
//			Debug.Log(student.Name);
//		}
//		
//		teachers.Add(new Teacher());
//		teachers.Add(new Teacher());
//		
//		lunchLine.Enqueue(students[0]);
//		lunchLine.Enqueue(students[1]);
//
//		while (lunchLine.Count > 0)
//		{
//			var someone = lunchLine.Dequeue();
//		}
//		
//		stacksOn.Push(students[2]);
//		stacksOn.Push(students[3]);
//
//		stacksOn.Pop();
//
//		Debug.Log(stacksOn.Peek());
//		
//		lockerSystem.Add(key: 1, value: students[0]);
//		lockerSystem.Add(key: 2, value: students[1]);
//
//		lockerSystem.Remove(2);
//
//		studentSet.Add(students[0]);
//		
////		studentSet.Contains()
//
//		studentSet2.Add(students[2]);
//		
//	}
//
//}
//
//public class Student : IComparable<Student>
//{
//
//	public string Name;
//	public int    age;
//
//
//	public int CompareTo(Student other)
//	{
//		if (ReferenceEquals(this,
//		                    other)) return 0;
//
//		if (ReferenceEquals(null,
//		                    other)) return 1;
//
//		return age.CompareTo(other.age);
//	}
//
//}
//
//public partial class Teacher
//{
//
//	public int age;
//
//}
//
//public partial class Teacher
//{
//
//	public string name;
//
//}