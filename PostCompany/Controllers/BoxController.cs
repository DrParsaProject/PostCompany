using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PostCompany.Models;
using PostCompany.Utils;
using PostCompany.OutputForms;
using PostCompany.InputForms;

namespace PostCompany.Controllers
{
    public class BoxController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

        // GET api/Box
        public List<BoxOForm> GetBoxes()
        {
            List<Box> list = null;

            if (Authorize.isA(UserType.Employee))
                list = db.Boxes.ToList();
            else if (Authorize.isA(UserType.Customer))
            {
                int id = Authentication.GetCurrnetUserId();
                list = db.Boxes.Where(box => box.SenderId == id)
                               .ToList();
            }

            List<BoxOForm> res = new List<BoxOForm>();
            foreach (Box b in list)
                res.Add(new BoxOForm(b));

            return res;
        }

        // PUT api/Box/5
        public HttpResponseMessage PutBox(int id, EditBoxIForm form)
        {
            if (!(Authorize.isA(UserType.Customer) ||
                  Authorize.hasRole(EmployeeRole.Counter) ||
                  Authorize.hasRole(EmployeeRole.Transport) ||
                  Authorize.hasRole(EmployeeRole.Weight)))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            Box box = db.Boxes.Find(id);
            if (box == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (Authorize.isA(UserType.Customer))
                CustomerEditBox(form, box);
            else if (Authorize.isA(UserType.Employee))
            {
                if (Authorize.hasRole(EmployeeRole.Counter))
                    EmpCounterEditBox(form, box);
                else if (Authorize.hasRole(EmployeeRole.Transport))
                    EmpTransportEditBox(form, box);
                else if (Authorize.hasRole(EmployeeRole.Weight))
                    EmpWeightEditBox(form, box);
            }

            db.Entry(box).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private void EmpWeightEditBox(EditBoxIForm form, Box box)
        {
            if (form.Weight > 0)
            {
                box.Weight = form.Weight;
                box.Cost = 1000.0 + form.Weight * 1500.0;
                if (box.Sender.City != box.ReceiverCity)
                    box.Cost += 2000;
            }
            if ((box.Status == PostStatus.Pending || box.Status == PostStatus.Sending) &&
                      (form.Status == PostStatus.Sending || form.Status == PostStatus.Pending))
                box.Status = form.Status;
        }

        private void EmpTransportEditBox(EditBoxIForm form, Box box)
        {
            if (box.Status == PostStatus.Pending && form.Status == PostStatus.Sending)
            {
                box.Status = form.Status;
                box.SentOn = DateTime.Now.ToLocalTime();
            }
            else if (box.Status == PostStatus.Sending && form.Status == PostStatus.Received)
            {
                box.Status = form.Status;
                box.ReceivedOn = DateTime.Now.ToLocalTime();
            }
            else if ((box.Status == PostStatus.Received || box.Status == PostStatus.Sending) &&
                      (form.Status == PostStatus.Failed || form.Status == PostStatus.Received || form.Status == PostStatus.Sending))
                box.Status = form.Status;
        }

        private void EmpCounterEditBox(EditBoxIForm form, Box box)
        {
            if (form.ReceiverName != null)
                box.ReceiverName = form.ReceiverName;
            if (form.ReceiverCity != null)
                box.ReceiverCity = form.ReceiverCity;
            if (form.ReceiverAddress != null)
                box.ReceiverAddress = form.ReceiverAddress;

            if (box.Status == PostStatus.NotPaid || box.Status == PostStatus.Pending)
                if (form.Status == PostStatus.NotPaid || form.Status == PostStatus.Pending)
                    box.Status = form.Status;
        }

        private void CustomerEditBox(EditBoxIForm form, Box box)
        {
            if (box.Status == PostStatus.Canceled || box.Status == PostStatus.Pending)
                if (form.Status == PostStatus.Canceled || form.Status == PostStatus.Pending)
                    box.Status = form.Status;
        }

        // POST api/Box
        public HttpResponseMessage PostBox(AddBoxIForm form)
        {
            if (!Authorize.hasRole(EmployeeRole.Counter))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            int senderId = (from c in db.Customers
                            where c.Username == form.SenderUsername
                            select c.Id).SingleOrDefault();

            if (senderId <= 0 || form.ReceiverName == null ||
                form.ReceiverCity == null || form.ReceiverAddress == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);



            Box box = new Box();
            box.SenderId = senderId;
            box.ReceiverName = form.ReceiverName;
            box.ReceiverCity = form.ReceiverCity;
            box.ReceiverAddress = form.ReceiverAddress;
            box.AddedOn = DateTime.Now.ToLocalTime();
            box.Status = PostStatus.NotPaid;

            if (ModelState.IsValid)
            {
                db.Boxes.Add(box);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}