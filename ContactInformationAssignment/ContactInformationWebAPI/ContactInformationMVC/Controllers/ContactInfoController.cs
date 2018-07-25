using System.Collections.Generic;
using System.Web.Mvc;
using ContactInformationMVC.Models;
using System.Net.Http;

namespace ContactInformationMVC.Controllers
{
    public class ContactInfoController : Controller
    {
        // GET: ContactInfo
        public ActionResult Index()
        {
            IEnumerable<ContactInformationModels> contactList;
            HttpResponseMessage respose = GlobalVariables.webApiClient.GetAsync("ContactInfo").Result;
            contactList = respose.Content.ReadAsAsync<IEnumerable<ContactInformationModels>>().Result; 
            return View(contactList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
                   return View(new ContactInformationModels()); 
            else
            {
                HttpResponseMessage respose = GlobalVariables.webApiClient.GetAsync("ContactInfo/" + id.ToString()).Result;
                return View(respose.Content.ReadAsAsync<ContactInformationModels>().Result);
            }     

        }

        [HttpPost]
        public ActionResult AddOrEdit(ContactInformationModels ContactInformation)
        {
            if (ContactInformation.ContactID == 0)
            {
                HttpResponseMessage respose = GlobalVariables.webApiClient.PostAsJsonAsync("ContactInfo", ContactInformation).Result;
                TempData["SuccessMessage"] = "Saved Successfully.";
            }
            else
            {
                HttpResponseMessage respose = GlobalVariables.webApiClient.PutAsJsonAsync("ContactInfo/" + ContactInformation.ContactID, ContactInformation).Result;
                TempData["SuccessMessage"] = "Updated Successfully.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {           
            HttpResponseMessage respose = GlobalVariables.webApiClient.DeleteAsync("ContactInfo/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully.";
            return RedirectToAction("Index");
        }

    }
}