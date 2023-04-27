namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap("C:\\Users\\etwan\\OneDrive\\Desktop\\Important images\\QoeGLyD.jpg");
            pictureBox1.Image = bmp;
            var list = new List<(int,int,Color)>();
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    if (i >= 0 && i < bmp.Width && j >= 0 && j < bmp.Height)
                    {
                        Color pixel = bmp.GetPixel(i, j);
                        list.Add((j,i,pixel));
                    }
                       
                }
            }
            /*for (int i = 0; i < list.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(list[i].ToString());
            } */

            //SETUP FOR K-MEANS ALG
            int k=5;
            double[] avecolor = new double[k];
            List<(int,int,Color)>[] clusters = new List<(int,int,Color)>[k];

            //initialize clusters
            for(int i = 0; i<k;i++){
                clusters[i] = new List<(int,int,Color)>();
            }

            //randomly assign pixels to a cluster
            Random rnd = new Random();
            for(int i = 0; i<list.Count;i++){
                int rand = rnd.Next(0,5);
                clusters[rand].Add(list[i]);
            }

            //get initial averages
            setAves();

            for(int i = 0; i<k;i++){
                System.Diagnostics.Debug.WriteLine(i);
                System.Diagnostics.Debug.WriteLine(avecolor[i]);
            }




            /*
            int k =5;
            //This array stores the average color for each group 
            int[] avecolor = new int[k];
            //This array holds the different groups
            int[,,] groups = new int[k,(bmp.Width*bmp.Width),2];  //note: disregarding a couple pixels in this build
            //fill groups, initially it doesn't matter which pixel goes to which group, just add sequentially
            int x=0;
            int y=0;
            for(int g = 0; g<k;g++){ //for each group
                for(int element = 0;element<(bmp.Width*bmp.Width)/k;element++){ //for each element in a group
                    groups[g,element,0]=x;
                    groups[g,element,1]=y;
                    if(x==bmp.Width-1){
                        x=0;
                        y++;
                    } else{x++;}
                }
            } 
            //compute averages

            for(int g =0; g<k;g++){
                int tot = 0;
                for(int element = 0;element<(bmp.Width*bmp.Width);element++){
                    Color pixel = bmp.GetPixel(groups[g,element,0],groups[g,element,1]);
                    tot += pixel.ToArgb();
                }
                System.Diagnostics.Debug.WriteLine(tot);
          
            } */

            //set the averages of all clusters
            void setAves(){
                for(int clust = 0; clust<k; clust++){
                    double tot = 0;
                    for(int i = 0; i < clusters[clust].Count();i++){
                        double R = (clusters[clust][i].Item3.R);
                        double G = (clusters[clust][i].Item3.G);
                        double B = (clusters[clust][i].Item3.B);
                        tot += (R+G+B)/3;
                    }
                    System.Diagnostics.Debug.WriteLine(tot);
                    tot = tot/clusters[clust].Count();
                    avecolor[clust] = tot;
                }
        
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