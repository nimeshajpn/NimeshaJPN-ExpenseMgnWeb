using ExpenseWeb.DbConnection;
using ExpenseWeb.Models;
using ExpenseWeb.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseWeb.Controllers
{

    

    public class ExpenseController : Controller 
    {
      public  double I;
        double E;
        double A;
        double B;
        IEnumerable<MExpense> Get;


        private readonly EDbContext _db;
        

        public ExpenseController(EDbContext db)
        {
            _db = db;
      
        }

        public IActionResult Index(Mexpense2 e,double tb)
        {

           // IEnumerable<MExpense> obj = _db.TbExpensies;
            IEnumerable<MExpense> obj = null;





            //obj2 = _db.TbExpensies;


            IEnumerable<MExpense> obj2= Api(obj);
            Total(obj2);
            
            e.expanse = E;
            e.income = I;
            e.Total = I - E;
            tb = e.Total;
            ViewBag.Total = e.Total;
            ViewBag.income = e.income;
            ViewBag.expence = e.expanse;
            return View(obj2);

        }

        public IActionResult All(Mexpense2 e, double tb)
        {

           // IEnumerable<MExpense> obj = _db.TbExpensies; 
            IEnumerable<MExpense> obj = null;



            IEnumerable<MExpense> obj2 = Api(obj);
            Total1(obj2);
            e.expanse = E;
            e.income = I;
            e.Total = I - E;
            tb = e.Total;
            ViewBag.Total = e.Total;
            ViewBag.income = e.income;
            ViewBag.expence = e.expanse;
            return View(obj2);

        }





        //Get
        public IActionResult Create() { 
        
        return View();
        
        }


        [HttpPost]
        public IActionResult Create(MExpense e) {

            int id = 0;
            int i = 2;
            Api2(e, i,id);
            
            if (e.Amount != 0 )
            {


             //   _db.TbExpensies.Add(e);
             //   _db.SaveChanges();

            }
          
            return RedirectToAction("Index");
        
        }






        public IActionResult Edit(int? id)
        {
            MExpense e = null;

            //  var e = _db.TbExpensies.Find(id);
            int i = 3;
           
            e = Api2(e, i, id);
            return View(e);

        }


        [HttpPost]
        public IActionResult Edit(MExpense e)
        {
            int id = 0;
            int i = 4;
            Api2(e, i, id);
            if (e.Amount != 0)
            {
               
               


     //           _db.TbExpensies.Update(e);
         //       _db.SaveChanges();

            }

            return RedirectToAction("All");

        }
        public IActionResult Delete(int? id)
        {

            MExpense e = null;
            int i = 5;
            Api2(e, i, id);






            //    var e = _db.TbExpensies.Find(id);

            //    _db.TbExpensies.Remove(e);
            //     _db.SaveChanges();



            return RedirectToAction("All");
        }



        public void Total(IEnumerable<MExpense> obj) {
              
           var e = obj;
            foreach (var item in e) {
                if (item.Date.Date== DateTime.Today) {
                    if (item.Type == "Income")
                    {

                        A = (item.Amount);
                        I = I + (double)A;
                    }
                    else {


                        B = (item.Amount);
                        E = E + (double)B;


                    }




                }
            
            
            }

        }

        public void Total1(IEnumerable<MExpense> obj)
        {

            var e = obj;
            foreach (var item in e)
            {
                
                
                    if (item.Type == "Income")
                    {

                        A = (item.Amount);
                        I = I + (double)A;
                    }
                    else
                    {


                        B = (item.Amount);
                        E = E + (double)B;


                    }




                


            }

        }

        public IEnumerable<MExpense> Api(IEnumerable<MExpense> obj) {





            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7178/");
                


                
                    var r = client.GetAsync("Expense");
                    r.Wait();

                    var result = r.Result;

                    var readjob = result.Content.ReadAsAsync<IList<MExpense>>();
                    readjob.Wait();
                    obj = readjob.Result;



               

            }
        
        return obj;
        }
        
        public MExpense Api2(MExpense e, int i,int? id) {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7178/");



                if (i == 2)
                {
                    var r = client.PostAsJsonAsync<MExpense>("Expense", e);
                    r.Wait();

                    var result = r.Result;


                }
                else if (i == 3)
                {



                    var r = client.GetAsync("Expense/" + id.ToString());
                    r.Wait();

                    var result = r.Result;

                    var readjob = result.Content.ReadAsAsync<MExpense>();
                    readjob.Wait();
                    e = readjob.Result;

                }
                else if (i == 4)
                {

                    var r = client.PutAsJsonAsync<MExpense>("Expense/" + e.Id, e);
                    r.Wait();

                    var result = r.Result;



                }
                else if (i == 5) {



                    var r = client.DeleteAsync("Expense/" + id.ToString());
                    r.Wait();

                    var result = r.Result;

                    var readjob = result.Content.ReadAsAsync<MExpense>();
                    readjob.Wait();

                }

            }



            return e;
        }






    }
}
