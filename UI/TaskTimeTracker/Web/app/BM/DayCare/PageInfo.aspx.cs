using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.app.BM.DayCare
{
    public partial class PageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lstItems = new List<KeyValuePair<string, string>>();

            lstItems.Add(new KeyValuePair<string, string>("CourseInstance", "The course-instance (and sections) connects groups of users, content, and term to define a particular class (subject, course, etc.)"));
            lstItems.Add(new KeyValuePair<string, string>("SubjectMatter", "Matter presented for consideration in discussion, thought, or study"));
            lstItems.Add(new KeyValuePair<string, string>("Registration", "A document certifying an act of registering"));
            lstItems.Add(new KeyValuePair<string, string>("Student", "A person who is studying at a university or other place of higher education."));
            lstItems.Add(new KeyValuePair<string, string>("Teacher", "A person who teaches, especially in a school."));
            lstItems.Add(new KeyValuePair<string, string>("FoodType", "Variety of different type of food"));
            lstItems.Add(new KeyValuePair<string, string>("ActivitySubType", "Secondary activity in main activity will be sub type"));
            lstItems.Add(new KeyValuePair<string, string>("ActivityType", "The activity type classifies the activities that are to be performed within a controlling area by one or several cost centers."));
            lstItems.Add(new KeyValuePair<string, string>("Course", "A set of classes or a plan of study on a particular subject, usually leading to an exam or qualification"));
            lstItems.Add(new KeyValuePair<string, string>("Department", "A division of a large organization such as a government, university, or business, dealing with a specific area of activity"));
            lstItems.Add(new KeyValuePair<string, string>("DiaperStatus", "Record of diaper used by child through out the day"));
            lstItems.Add(new KeyValuePair<string, string>("Discount", "A deduction from the usual cost of something"));
            lstItems.Add(new KeyValuePair<string, string>("EventType", "Record of types of event held"));            
            lstItems.Add(new KeyValuePair<string, string>("MealType", "Meal type can be divided into breakfast, lunch and dinner"));
            lstItems.Add(new KeyValuePair<string, string>("NeedItem", "List of item needed"));
            lstItems.Add(new KeyValuePair<string, string>("PaymentMethod", "The way that a buyer chooses to compensate the seller of a good -  like cash, checks, credit or debit cards, money orders, bank transfers and online payment services"));
            lstItems.Add(new KeyValuePair<string, string>("AccidentPlace", "Place where road traffic accidents have historically been concentrated"));
            lstItems.Add(new KeyValuePair<string, string>("AccidentReport", "A form that is filled out in order to record details of an unusual event that occurs at the facility, such as an injury to a patient"));
            lstItems.Add(new KeyValuePair<string, string>("BathRoom", "A room containing a bath or shower and typically also a washbasin and a toilet."));
            lstItems.Add(new KeyValuePair<string, string>("Comment", "A verbal or written remark expressing an opinion or reaction"));
            lstItems.Add(new KeyValuePair<string, string>("Meal", "Eating occasion that takes place at a certain time and includes specific, prepared food, or the food eaten on that occasion"));
            lstItems.Add(new KeyValuePair<string, string>("MealDetials", "Details of meal served like salad, vegetables etc"));
            lstItems.Add(new KeyValuePair<string, string>("SickReport", "Students when get sick a report will be created to keep record"));
            lstItems.Add(new KeyValuePair<string, string>("Sleep", "A report will be created to keep track of student sleep time"));
            lstItems.Add(new KeyValuePair<string, string>("Tuition", "Record of teaching or instruction, especially of individual pupils or small groups."));
            lstItems.Add(new KeyValuePair<string, string>("VacationPlan", "Record of vacation taken by students"));
            
            gridContent.DataSource = lstItems;
            gridContent.DataBind();
 
        }
    }
}