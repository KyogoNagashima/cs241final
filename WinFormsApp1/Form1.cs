//variables to manually change for debugging : k and 

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //IMPORTANT VARAIBLES
            int k=5;                    //# of clusters
            double thresh = 0.05;        //threshhold for change before stopping


            InitializeComponent();
            Bitmap bmp = new Bitmap("C:\\Users\\etwan\\OneDrive\\Desktop\\Important images\\453423383_9ac92b8977.jpg");
            pictureBox1.Image = bmp;
            var list = new List<(int,int,Color)>();
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    if (i >= 0 && i < bmp.Height && j >= 0 && j < bmp.Width)
                    {
                        Color pixel = bmp.GetPixel(j, i);
                        list.Add((i,j,pixel));
                    }
                       
                }
            }
            /*for (int i = 0; i < list.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(list[i].ToString());
            } */

            //SETUP FOR K-MEANS ALG
            double[] avecolor = new double[k];
            List<(int,int,Color)>[] clusters = new List<(int,int,Color)>[k];

            //initialize clusters
            for(int i = 0; i<k;i++){
                clusters[i] = new List<(int,int,Color)>();
            }

            //randomly assign pixels to a cluster
            Random rnd = new Random();
            for(int i = 0; i<list.Count;i++){
                int rand = rnd.Next(0,k);
                clusters[rand].Add(list[i]);
            }

            //get initial averages
            setAves();
            //print out averages
            for(int i = 0; i<k;i++){
                System.Diagnostics.Debug.WriteLine(i);
                System.Diagnostics.Debug.WriteLine(avecolor[i]);
            }

            double[] oldavecolor;

            //while loop
            do {
                oldavecolor = new double[k];
                for(int averageindex=0; averageindex < k; averageindex++){
                    oldavecolor[averageindex]=avecolor[averageindex];
                }
                clusters = Regroup();
                setAves();
            } while(!threshold(oldavecolor,avecolor));

            //print aves
            for(int z = 0; z<k;z++){
            System.Diagnostics.Debug.WriteLine(z);
            System.Diagnostics.Debug.WriteLine(avecolor[z]);
            
            //recolor
            Color[] colors = new Color[k];
            Random rndcolor = new Random();
            for(int i=0; i<k;i++){
                colors[i]=Color.FromArgb(rndcolor.Next(256), rndcolor.Next(256), rndcolor.Next(256));
            }

            //System.Diagnostics.Debug.WriteLine(Regroup()[0][0]);
            for (int i = 0; i < k; i++) {
                    for (int j=0; j< clusters[i].Count; j++) {
                        bmp.SetPixel(clusters[i][j].Item2, clusters[i][j].Item1, colors[i]);
                    } 
                    //System.Diagnostics.Debug.WriteLine(Regroup()[0][0].Item1);
                //for (int j = 0; j < Regroup()[i][0].Length; )
            }

            }



            //set the averages of all clusters
            void setAves(){
                for(int clust = 0; clust<k; clust++){
                    double tot = 0;
                    for(int i = 0; i < clusters[clust].Count();i++){
                        double R = Math.Pow((clusters[clust][i].Item3.R),2);
                        double G = Math.Pow((clusters[clust][i].Item3.G),2);
                        double B = Math.Pow((clusters[clust][i].Item3.B),2);


                        tot += Math.Sqrt((R+G+B)/3);
                    }
                    //System.Diagnostics.Debug.WriteLine(tot);
                    tot = tot/clusters[clust].Count();
                    avecolor[clust] = tot;
                }
        
            }
            List<(int,int,Color)>[] Regroup(){
                List<(int,int,Color)>[] newlist = new List<(int,int,Color)>[k];
                //initialize clusters
                for(int i = 0; i<k;i++){
                    newlist[i] = new List<(int,int,Color)>();
                }
                for(int cluster = 0; cluster<k; cluster++){
                    for(int i = 0; i < clusters[cluster].Count();i++){
                        //assign pixel to cluster with closest ave

                        double R = Math.Pow((clusters[cluster][i].Item3.R),2);
                        double G = Math.Pow((clusters[cluster][i].Item3.G),2);
                        double B = Math.Pow((clusters[cluster][i].Item3.B),2);
                        double colorval = Math.Sqrt((R+G+B)/3);

                        double diff = double.PositiveInfinity;
                        int index =0;
                        //find closest ave
                        for(int aveindex=0; aveindex < k; aveindex++ ){
                            double currentdif = Math.Abs(colorval - avecolor[aveindex]);
                            if(currentdif < diff){
                                diff = currentdif;
                                index = aveindex;
                            }
                        }
                        newlist[index].Add(clusters[cluster][i]);
                    }
                }
                return newlist;
            }
            bool threshold(double[] old, double[] newbie){
                int count = 0;
                for(int indx = 0;indx<k;indx++){
                    if(Math.Abs(old[indx]-newbie[indx])<thresh){
                        count++;
                    }
                }
             return count==k;
            }
            
            
             
            
        } 

       


        private void pictureBox1_Click(object sender, EventArgs e)
        {
          




           
           
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}