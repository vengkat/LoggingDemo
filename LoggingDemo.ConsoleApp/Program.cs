using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
namespace LoggingDemo.ConsoleApp
{
    /*
     * Author   : Venkatesh Segar
     * Purpose  : To demonstrate logging using log4net c# library
     * Created date     : 14 Sep 2019
     * Revison History  : 
     */

    /// <summary>
    /// Employee
    /// </summary>
    public class Employee
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public string employee_salary { get; set; }
        public int employee_age { get; set; }
    }
    class Program
    {
        //private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
        private static readonly ILog logger = LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string host = "http://dummy.restapiexample.com";
        static void Main(string[] args)
        {
            try
            {
                logger.Info("THE MAIN METHOD IS HIT");
                var emp = new Employee { id = 1, employee_name = "Iam Groot" };
                logger.Info($"Calling the method GetEmployeeList");
                var empList = GetEmployeeList(emp).Result;
                logger.Info($"Records received - {empList.Count()}");
                throw new Exception();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        #region GetEmployeeList
        /// <summary>
        /// GetEmployeeList
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Employee>> GetEmployeeList(Employee emp)
        {
            try
            {
                logger.Debug(JsonConvert.SerializeObject(emp));               
                var path = "api/v1/employees";
                HttpClient client = new HttpClient {BaseAddress=new Uri(host) };
                logger.Info($"Making api call | {host}/{path}");
                HttpResponseMessage response = client.GetAsync(path).Result;
                if (response.IsSuccessStatusCode)
                {
                    logger.Info("response code is success");
                    return JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    logger.Info("api call has failed");
                }                
                return new List<Employee>();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        } 
        #endregion
    }
}
