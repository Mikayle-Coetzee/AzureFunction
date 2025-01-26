using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction1
{
    public class ValidateClass
    {
        public ValidateClass() { }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will validate the age 
        /// </summary>
        /// <param name="inputAge"></param>
        /// <returns></returns>
        public bool ValidateAge(string inputAge)
        {
            return Int32.TryParse(inputAge, out var age) && (age > 0) && (age < 200);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will validate the student number
        /// </summary>
        /// <param name="inputStudentNo"></param>
        /// <returns></returns>
        public bool ValidateStudentNo(string inputStudentNo)
        {
            var valid = false;

            switch (inputStudentNo.Length)
            {
                case > 5:
                    {
                        if (char.IsLetter(inputStudentNo[0]) &&
                            char.IsLetter(inputStudentNo[1]))
                        {
                            if (inputStudentNo[2..].All(char.IsDigit))
                            {
                                valid = true;
                            }
                        }

                        break;
                    }
            }

            return valid;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
