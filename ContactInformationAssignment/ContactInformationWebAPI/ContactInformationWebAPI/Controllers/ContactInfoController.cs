using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ContactInformationWebAPI.Models;

namespace ContactInformationWebAPI.Controllers
{
    public class ContactInfoController : ApiController
    {
        private ContactInfoDBModel ContactInfoDB = new ContactInfoDBModel();

        /// <summary>
        /// Get Contact Information
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContactInfo> GetContactInfoes()
        {
            return ContactInfoDB.ContactInfoes;
        }
                
        /// <summary>
        /// Get Contact Information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(ContactInfo))]
        public IHttpActionResult GetContactInfo(int id)
        {
            ContactInfo contactInfo = ContactInfoDB.ContactInfoes.Find(id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return Ok(contactInfo);
        }
                
        /// <summary>
        /// Update Contact Information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactInfo(int id, ContactInfo contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactInfo.ContactID)
            {
                return BadRequest();
            }

            ContactInfoDB.Entry(contactInfo).State = EntityState.Modified;

            try
            {
                ContactInfoDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        
        /// <summary>
        /// Add Contact Information
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        [ResponseType(typeof(ContactInfo))]
        public IHttpActionResult PostContactInfo(ContactInfo contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContactInfoDB.ContactInfoes.Add(contactInfo);
            ContactInfoDB.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactInfo.ContactID }, contactInfo);
        }
                
        /// <summary>
        /// Delete Contact Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(ContactInfo))]
        public IHttpActionResult DeleteContactInfo(int id)
        {
            ContactInfo contactInfo = ContactInfoDB.ContactInfoes.Find(id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            ContactInfoDB.ContactInfoes.Remove(contactInfo);
            ContactInfoDB.SaveChanges();
            return Ok(contactInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ContactInfoDB.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Check contact Exist or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ContactInfoExists(int id)
        {
            return ContactInfoDB.ContactInfoes.Count(e => e.ContactID == id) > 0;
        }
    }
}