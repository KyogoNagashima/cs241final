namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap("C:\\Users\\jakem\\OneDrive\\Desktop\\241\\IMG-1834.jpg");
            pictureBox1.Image = bmp;
            var list = new List<Color>();
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    if (i >= 0 && i < bmp.Width && j >= 0 && j < bmp.Height)
                    {
                        Color pixel = bmp.GetPixel(i, j);
                        list.Add(pixel);
                    }
                       
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(list[i].ToString());
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