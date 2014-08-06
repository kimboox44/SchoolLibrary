using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary.BusinessLogic.Managers
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text.RegularExpressions;

    using LinqToExcel;

    using SchoolLibrary.BusinessLogic.Managers.Interfaces;
    using SchoolLibrary.BusinessModels.MVCModels;
    using SchoolLibrary.BusinessModels.Models;


    public class ExcelManager : IExcelManager
    {
        public ReadingFromExcelModel GetReadersFromFile(string fileName)
        {
            var result = new ReadingFromExcelModel();

            //reading from excel
            var excel = new ExcelQueryFactory();
            List<Row> readers;
            try
            {
                excel.FileName = fileName;
                readers = excel.Worksheet().ToList();
                if (readers.Count > 0)
                {
                    if (!readers[0].ColumnNames.Contains("FirstName") || !readers[0].ColumnNames.Contains("LastName")
                        || !readers[0].ColumnNames.Contains("Address") || !readers[0].ColumnNames.Contains("Birthday")
                        || !readers[0].ColumnNames.Contains("Email") || !readers[0].ColumnNames.Contains("Telephone"))
                    {
                        throw new Exception();
                    }
                }
            }
            catch
            {
                throw new ArgumentException("Invalid input file structure!");
            }

            for (int i = 0; i < readers.Count(); i++)
            {
                var validationResult = this.ValidateExcelRow(readers[i]);
                if (validationResult.MemberNames.Count() == 0)
                {
                    var readerBm = new ReaderBusinessModel();
                    readerBm.FirstName = readers[i]["FirstName"];
                    readerBm.LastName = readers[i]["LastName"];
                    readerBm.Address = readers[i]["Address"];
                    readerBm.Birthday = readers[i]["Birthday"].Cast<DateTime>();
                    readerBm.EMail = readers[i]["Email"];
                    readerBm.Phone = readers[i]["Telephone"];
                    result.Readers.Add(readerBm);
                }
                else
                {
                    validationResult.ErrorMessage = string.Format("Line: {0}", i + 1);
                    result.Errors.Add(validationResult);
                }
            }

            return result;
        }

        public string GenerateErrorString(ReadingFromExcelModel model)
        {
            var errors = string.Empty;
            if (model.Errors != null)
            {
                errors = "Errors: " + Environment.NewLine;
                foreach (var error in model.Errors)
                {
                    errors += error.ErrorMessage + " incorrect: ";
                    foreach (var name in error.MemberNames)
                    {
                        errors += name + " ";
                    }

                    errors += Environment.NewLine;
                }
            }

            return errors;
        }

        private ValidationResult ValidateExcelRow(Row row)
        {
            var errors = new List<string>();

            Regex regex = new Regex("[A-Za-zА-Яа-я]{2,}");
            if (row["FirstName"] == string.Empty || !regex.IsMatch(row["FirstName"]))
            {
                errors.Add("First Name");
            }

            if (row["LastName"] == string.Empty || !regex.IsMatch(row["LastName"]))
            {
                errors.Add("Last Name");
            }

            if (row["Address"] == string.Empty)
            {
                errors.Add("Address");
            }

            regex = new Regex("^[+38-]?[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}");
            if (row["Telephone"] == string.Empty || !regex.IsMatch(row["Telephone"]))
            {
                errors.Add("Telephone");
            }

            regex = new Regex("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!regex.IsMatch(row["Email"]))
            {
                errors.Add("Email");
            }

            try
            {
                var date = row["Birthday"].Cast<DateTime>();
            }
            catch
            {
                errors.Add("Birthday");
            }

            return new ValidationResult(string.Empty, errors);
        }
    }
}
