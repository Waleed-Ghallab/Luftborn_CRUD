using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Luftborn_CRUD.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public string dateofbirth { get; set; }

        public int depto { get; set; }//non foreign reference to dept 


        [ForeignKey("dept")]
        public int deptID { get; set; }


        //navigation properties
        public virtual Department dept { get; set; }   //virtual=> EF will dynamically
                                                       //create class in runtime instead of 
                                                       //the original one to avoid loading
                                                       //an entire tree of dependent objects
                                                       //(Lazy Loading)

    }
}
