using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _462graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[] xmlYear = System.Xml.Linq.XDocument.Load(@"C:\Users\Nate\Desktop\462FINALPROJECT\milestraveled.xml").Descendants("year")
                  .Select(x => (int)x).ToArray();
            int[] xmlMiles = System.Xml.Linq.XDocument.Load(@"C:\Users\Nate\Desktop\462FINALPROJECT\milestraveled.xml").Descendants("vehicle_miles_travelled")
                    .Select(x => (int)x).ToArray();
            string[] xmlLand = System.Xml.Linq.XDocument.Load(@"C:\Users\Nate\Desktop\462FINALPROJECT\milestraveled.xml").Descendants("urban_area")
                   .Select(element => element.Value).ToArray();
            //Combines all the years into a list containing no duplicates
            ArrayList yearlist = new ArrayList();
            foreach (int s in xmlYear)
            {
                if (!yearlist.Contains(s))
                {
                    yearlist.Add(s);
                }
            }
            //takes out duplicate land types
            ArrayList landTypes = new ArrayList();
            foreach (string y in xmlLand)
            {
                if (!landTypes.Contains(y))
                {
                    landTypes.Add(y);
                }
            }
            //
            String[] landTyp = (String[])landTypes.ToArray(typeof(string));
            //counts how many duplicate land types
            int p = 0;
            int j = 0;
            int[] landval = new int[landTypes.Count];
            while (p < landTypes.Count)
            {
                while (j < xmlLand.Length)
                {
                    if (xmlLand[j] == landTyp[p])
                    {
                        landval[p] += 1;
                    }
                    j++;
                }
                j = 0;
                p++;
            }
            int b = 0;
            while (b < landTypes.Count)
            {
                Console.WriteLine("The type of land {0} has been traveled {1} times", landTypes[b], landval[b]);
                b++;
            }

            //Numerically sorts the years
            yearlist.Sort();
            //converts the array list into int array
            int[] ayear = yearlist.OfType<int>().ToArray();
            //sets a new array holding all the miles per year to the length of how many years there are
            int[] amiles = new int[ayear.Length];
            int i = 0;
            int t = 0;
            //overly complicated loop that cycles through xmlmiles based on each year using the numerically sorted ayear and adds up the miles for that specific year from the index of sorted ayear and then increases int t by one to get to next year index. it works...
            while (t < ayear.Length)
            {
                while (i < xmlMiles.Length)
                {
                    if (xmlYear[i] == ayear[t])
                    {
                        amiles[t] = amiles[t] + xmlMiles[i];
                    }
                    i++;
                }
                i = 0;
                t++;
            }

            fillChart(amiles, ayear, landTyp, landval);
        }
        private void fillChart(int[] amiles, int[] ayear, string[] landTyp, int[] landval)
        {
            int i = 0;
            while(i < ayear.Length)
            {
                chart1.Series["Miles"].Points.AddXY(ayear[i], amiles[i]);
                i++;
            }
            i = 0;
            while(i < landTyp.Length)
            {
                chart2.Series["Land"].Points.AddXY(landTyp[i], landval[i]);
                i++;
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
