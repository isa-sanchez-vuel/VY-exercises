﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice3_Part2_WorkersManagement;
{
	internal class Task
	{
		internal static int IdCount = 0;
		int Id;
		int IdWorker = -1;
		string Description;
		string Technology;

		internal enum Status
		{
			ToDo,
			Doing,
			Done
		}

		Status TaskStatus;

		public Task(string description, string technology)
		{
			IdCount++;
			Id = IdCount;
			IdWorker = -1;
			Description = description;
			Technology = technology;
			TaskStatus = Status.ToDo;
		}

		public int GetId()
		{
			return Id;
		} 

		public int GetWorkerId()
		{
			return IdWorker;
		} 

		public string GetDescription()
		{
			return Description;
		} 

		public string GetTechnology()
		{
			return Technology;
		} 

		public Status GetStatus()
		{
			return TaskStatus;
		}

		public void SetStatus(Status newStatus)
		{
			TaskStatus = newStatus;
		}

		public void AssignWorker(int workerId)
		{
			IdWorker = workerId;
		}

		public void UnassignWorker()
		{
			IdWorker = -1;
		}

	}
}
