using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day04TodosEF
{
    public class Todo
    {

        public Todo() { }

        public Todo(string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            //Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), status);
            Status = status;
        }

        public int Id { get; set; }

        private string _task;

        [Required]
        [StringLength(100)]
        public string Task
        {
            get
            {
                return _task;
            }
            set
            {
                //Regex.IsMatch(Task, @"^[a-zA-Z0-9./,;\-+)(*!'""\s]{1,100}$");
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("Maximum task size exceeded: must be up to 100 characters long");
                }
                _task = value;
            }
        } // 1-100 characters, only letters, digits, space ./,;-+)(*! allowed

        private int _difficulty;

        [Required]
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentException("Maximum difficulty is up to 5");
                }
                _difficulty = value;
            }
        } // 1-5 only front-end validation

        DateTime _dueDate;


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set 
            { 
                if (value.Year < 1900 || value.Year > 2099)
                {
                    throw new ArgumentException("Invalid year. Must be between 1900-2099");
                }
                _dueDate = value;
            }
        }// 1900-2099 year required, format in GUI is whatever the OS is configured to use

        public enum StatusEnum { Pending, Done, Delegated }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
    }
}
