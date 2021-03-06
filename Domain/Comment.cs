﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Comment : Entity
	{
		public virtual string Text { get; set; }
		public virtual DateTime TimeAdded { get; set; }
		public virtual News News { get; set; }
		public virtual long NewsId { get; set; }
		public virtual Author Author { get; set; }
		public virtual long AuthorId { get; set; }
	}
}
