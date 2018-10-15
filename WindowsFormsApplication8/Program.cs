using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication8
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }

    public static class Globals
    {
        internal static double iSPrice;
        internal static int stQuantity;
        internal static double totalAmnt;
        internal static double iPrice;
        internal static int stItemc;
        internal static int transQuantity;
        internal static int stocksQuantity;
        internal static int salesId;
        internal static int purchID;
        internal static string transType;
        internal static int salesRID;
        internal static int purchRID;

        public static string result { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string status { get; set; }
        public static int id { get; set; }
        public static int itemcode { get; set; }
        public static double itemprice { get; set; }
        public static double quantity { get; set; }
        public static int transid { get; set; }
        public static string itemname { get; set; }
        public static int salesid { get; set; }
        public static int purchid { get; set; }
        public static int customerid { get; set; }
        public static string customername { get; set; }
        public static int supplierid { get; set; }
        public static string suppliername { get; set; }
        public static int employeeid { get; set; }
        public static int salesinvoicenum { get; set; }
    }
}
