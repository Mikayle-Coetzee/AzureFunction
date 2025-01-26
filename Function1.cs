using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunction1
{
    public static class Function1
    {
        //・♫-------------------------------------------------------------------------------------------------♫・//

        [FunctionName("Function1")] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
                // Authorization level is set to 'Function', and route is set to null for default path
                // 'req' represents the incoming HTTP request, while 'log' is used for logging purposes
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Initialize validation class
            ValidateClass validate = new ValidateClass();

            // Initialize connectToDatabase class
            ConnectToDatabaseClass connectToDatabase = new ConnectToDatabaseClass();

            // Extract query parameters
            string name = req.Query["name"];
            string studentNumber = req.Query["studentNumber"];
            string age = req.Query["age"];

            // Read the request body and deserialize JSON data
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Assign values from query parameters or request body
            name = name ?? data?.name;
            studentNumber = studentNumber ?? data?.studentNumber;
            age = age ?? data?.age;

            // Construct response message
            string responseMessage = string.IsNullOrEmpty(name) && string.IsNullOrEmpty(studentNumber)
                ? "This HTTP triggered function executed successfully. Pass a name and a surname in the query string or in the request body for a personalized response."
                : $"Hello, {name}: {studentNumber}. Your age is {age}. This HTTP triggered function executed successfully.";

            // Validate age
            if (!string.IsNullOrEmpty(age) && !validate.ValidateAge(age))
            {
                return new BadRequestObjectResult("Invalid age.");
            }

            // Validate student number
            if (!string.IsNullOrEmpty(studentNumber) && !validate.ValidateStudentNo(studentNumber))
            {
                return new BadRequestObjectResult("Invalid student number.");
            }

            // Save the data to the database 
            if (name != null && studentNumber != null && age != null)
            {
                int userAge = Convert.ToInt32(age);
                if (connectToDatabase.SaveCustomer(studentNumber, name, userAge) == true)
                {
                    return new BadRequestObjectResult("Data saved to the database 'StudentDB'.");
                }
            }

            // Return the result message 
            return new OkObjectResult(responseMessage);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
