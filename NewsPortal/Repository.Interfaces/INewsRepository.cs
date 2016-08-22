﻿
using NewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Repository.Interfaces
{
	public interface INewsRepository : IRepository
	{
		IEnumerable<News> GetByCategory(Category category);
	}
}