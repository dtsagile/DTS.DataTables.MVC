using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTS.DataTables.MVC.Tests.Models
{
    public static class Mocks
    {
        /// <summary>
        /// Gets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        public static IEnumerable<Person> People
        {
            get
            {
                string json = @"[{'id':1,'age':27,'firstName':'Kirkland','lastName':'Ford','email':'kirklandford@pulze.com','phone':'(927)514-3532'},{'id':2,'age':23,'firstName':'Gilda','lastName':'Ford','email':'gildacastillo@pulze.com','phone':'(811)573-2721'},{'id':3,'age':29,'firstName':'Henderson','lastName':'Ramos','email':'hendersonramos@pulze.com','phone':'(861)570-3601'},{'id':4,'age':29,'firstName':'Dodson','lastName':'Young','email':'dodsonyoung@pulze.com','phone':'(998)544-3213'},{'id':5,'age':30,'firstName':'Burks','lastName':'Mccullough','email':'burksmccullough@pulze.com','phone':'(901)550-3341'},{'id':6,'age':20,'firstName':'Kay','lastName':'Bates','email':'kaybates@pulze.com','phone':'(920)585-2885'},{'id':7,'age':27,'firstName':'Swanson','lastName':'Santiago','email':'swansonsantiago@pulze.com','phone':'(821)471-2785'},{'id':8,'age':38,'firstName':'Horn','lastName':'Cantu','email':'horncantu@pulze.com','phone':'(890)419-2442'},{'id':9,'age':20,'firstName':'Johns','lastName':'Barton','email':'johnsbarton@pulze.com','phone':'(895)503-2554'},{'id':10,'age':26,'firstName':'Socorro','lastName':'Langley','email':'socorrolangley@pulze.com','phone':'(909)466-3923'},{'id':11,'age':23,'firstName':'Stark','lastName':'Parker','email':'starkparker@pulze.com','phone':'(872)508-3702'},{'id':12,'age':40,'firstName':'Janice','lastName':'Baxter','email':'janicebaxter@pulze.com','phone':'(843)400-2899'},{'id':13,'age':35,'firstName':'Dominguez','lastName':'Deleon','email':'dominguezdeleon@pulze.com','phone':'(970)522-2766'},{'id':14,'age':30,'firstName':'Hendricks','lastName':'Maxwell','email':'hendricksmaxwell@pulze.com','phone':'(964)444-3697'},{'id':15,'age':28,'firstName':'Joyner','lastName':'Trujillo','email':'joynertrujillo@pulze.com','phone':'(881)541-2029'},{'id':16,'age':34,'firstName':'Sullivan','lastName':'Rutledge','email':'sullivanrutledge@pulze.com','phone':'(928)480-3706'},{'id':17,'age':36,'firstName':'Vargas','lastName':'Harrell','email':'vargasharrell@pulze.com','phone':'(941)421-2137'},{'id':18,'age':38,'firstName':'Norman','lastName':'Shaw','email':'normanshaw@pulze.com','phone':'(866)455-2753'},{'id':19,'age':37,'firstName':'Gayle','lastName':'Duke','email':'gayleduke@pulze.com','phone':'(881)406-3731'},{'id':20,'age':30,'firstName':'Katelyn','lastName':'Mcmahon','email':'katelynmcmahon@pulze.com','phone':'(999)552-3277'}]";
                return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            }
        }
    }
}
