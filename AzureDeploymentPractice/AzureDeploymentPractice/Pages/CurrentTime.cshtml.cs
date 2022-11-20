using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureDeploymentPractice.Pages
{
    public class CurrentTimeModel : PageModel
    {



        public void OnGet()
        {
           
            string dateTime = "Today's date is " + DateTime.Now.ToShortDateString() + " and the current time is " + DateTime.Now.ToShortTimeString() + ".";
            ViewData["TimeStamp"] = dateTime;

        }



     
    }
}
