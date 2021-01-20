using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.IO;

namespace Windows_Voice_Asistan
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            
            InitializeComponent();

        }
        
        bool izin;
        SpeechRecognitionEngine recoEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speechSyn = new SpeechSynthesizer();
        string isim;
        int sayac = 0;
        bool mouseDown;
        int counter = 0;
        Point lastLocation; string line;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                sesTanima();
                izin = true;
                recoEngine.RecognizeAsync();
                label5.Text = "I am listening ....";
            }
            catch
            { }
        }
        void sesTanima()
        {
            try
            {
                string[] ornek = { "open file", "read", "open editor", "open youtube", "Hello", "Hi", "How are you", "Who am i", "open google", "Who is your creator", "Show me your creator", "Who are you", "Show me my mails", "How is the weather today", "close yourself" };
                if (!File.Exists("Command.txt"))
                {
                    using (StreamWriter sw = File.CreateText("Command.txt"))
                    {
                        foreach (var eleman in ornek)
                        {
                            sw.WriteLine(eleman);
                        }
                    }

                }
                    string[] sorular = File.ReadAllLines("Command.txt");

                   
                    Choices olasiliklar = new Choices(sorular);
                    Grammar gram = new Grammar(new GrammarBuilder(olasiliklar));
                    recoEngine.LoadGrammar(gram);
                    recoEngine.SetInputToDefaultAudioDevice();
                    recoEngine.SpeechRecognized += sesTani;
               
 
            
           

            }
            catch
            { }
        
        }

        void sesTani(object sender, SpeechRecognizedEventArgs e)
        {
            int sayac = 0;
                speechSyn.SelectVoiceByHints(VoiceGender.Female);
                speechSyn.Rate = -1;
                string name = "Clara";
                string mesaj = "";
                string a = e.Result.Text;
                string[] answer = File.ReadAllLines("Answer.txt");
                string[] doit = File.ReadAllLines("Do.txt");
                string[] message = File.ReadAllLines("Message.txt");
                string[] web = File.ReadAllLines("Web.txt");
                string[] com = File.ReadAllLines("Command.txt");


            try
            {

                if (izin == true)
                {
                    
                       
                    
                    listBox1.Items.Add("Me:" + e.Result.Text);
                    for (int i = 0; i < answer.Length; i++)
                    {
                        if (a == answer[i])
                        {
                            mesaj = message[i].ToString();

                        
                        if (doit[i]!="0")
                        {
                            if (web[i] == "1")
                            {
                                Process.Start("chrome.exe", doit[i]);
                            }
                            else
                            { Process.Start( doit[i]); }
                        
                        }
                      }
                    }
                      
                    
                   
                    if (a == "Who am i")
                    { mesaj = isim; }
                   
                    if(a=="open file")
                    {
                        speechSyn.SpeakAsync("Please select your file ");
                        Editor();
                         mesaj ="You can edit your file this page and I can read the file just say it ";
                        OpenFile();
                    }
                    for (int i = 0; i < answer.Length; i++)
                    {
                        if (a == "read")
                        {
                            
                                if (richTextBox1.Text != "")
                                {
                                    speechSyn.SpeakAsync(richTextBox1.Text);
                                    break;
                                }
                                else
                                {
                                    speechSyn.SpeakAsync("I cant find any text file");
                                    break;
                                }
                            
                        }
                    
                    }
                      
                    if (a == "How is the weather today")
                    {
                        Process.Start("chrome.exe", "https://www.google.com/search?ei=QeN2XK_dJI78kwW9ibyADQ&q=bug%C3%BCn+hava+nas%C4%B1l+&oq=bug%C3%BCn+hava+nas%C4%B1l+&gs_l=psy-ab.3..0i67j0l9.66375.66375..66537...0.0..0.110.110.0j1......0....1..gws-wiz.......0i71.VJuwNitgFbc");
                    }
                    if (a == "Open the Search Page")
                    {

                        mesaj = "Okey I am opening Google";
                        Process.Start("chrome.exe", "http://www.google.com");
                    }
                    if (a == "Who is your creator")
                    {
                        mesaj = "My creator is Ugur ilgin ";


                    }
                    if (a == "close yourself")
                    {

                        Application.Exit();
                    }
                    if (a == "open editor")
                    {
                        mesaj = "I am opening Editor";
                        Editor();
                        
                    }
                    if (a == "open menu")
                    {
                        mesaj = "I am opening Menu";
                        Menu();

                    }
                    if (a == "open settings")
                    {
                        mesaj = "I am opening Settings";
                        Settings();

                    }
                    if (a == "Show me your creator")
                    {
                        mesaj = "Okey";
                        Process.Start("chrome.exe", "https://www.linkedin.com/in/u%C4%9Fur-i-9a0939155/");

                    }
                    if (a == "Who are you")
                    {
                        mesaj = "I am " + name + " I created by ugur ilgin in 2019 ";

                    }
                    
                    

                    speechSyn.SpeakAsync(mesaj);
                    listBox1.Items.Add(name + ":" + mesaj);

                    label5.Text = "";

                    izin = false;
                }
            }
            catch
            {
                
            }
        }

        void CommanLines() {
            
            
        
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            pictureBox13.Visible = false;
            isim = System.Environment.UserName.ToString();
            label4.Text += isim+",";

            string[] ornek = {"open file","read","open editor","open youtube", "Hello", "Hi", "How are you", "Who am i", "open google", "Who is your creator", "Show me your creator", "Who are you", "Show me my mails", "How is the weather today", "close yourself" };
            if (!File.Exists("Command.txt"))
            {
                using (StreamWriter sw = File.CreateText("Command.txt"))
                { foreach( var eleman in ornek)
                {
                    sw.WriteLine(eleman);
                }
                }

            }
            string[] sorular = File.ReadAllLines("Command.txt");
            foreach (string eleman in sorular)
            {
                listBox2.Items.Add(eleman);
            }
            speechSyn.Rate = 0;
            speechSyn.SelectVoiceByHints(VoiceGender.Female,VoiceAge.Adult);
            speechSyn.SpeakAsync("Hello  "+isim+"  I am your personel assistant Clara How can I help you ");

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            if (sayac == 0)
            {
                this.Width = pictureBox13.Width;
                this.Height = pictureBox1.Height + 20;
                pictureBox13.Visible = true;
                sayac = 1;
            }
            else
            {
                this.Width = 1266;
                this.Height = 668;
                pictureBox13.Visible = false;
                sayac = 0;
            }
            //this.WindowState = FormWindowState.Minimized;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;


        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
           
                try
                {
                    sesTanima();
                    izin = true;
                    recoEngine.RecognizeAsync();
                    //label6.Text = "I am listening ....";
                }
                catch
                { }
            
            
        }
        void closeSetting()
        {

            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            textBox3.Visible = false;
            textBox2.Visible = false;
            textBox1.Visible = false;
            comboBox1.Visible = false;
            pictureBox15.Visible = false;
            checkBox1.Visible = false;
        
        }
        void Editor()
        {
            pictureBox12.Visible = false;
            pictureBox6.Visible = false;
            pictureBox11.Visible = true;
            pictureBox10.Visible = true;
            listBox1.Visible = false;
            closeSetting();
            richTextBox1.Visible = true;
        }
        void Menu()
        {
            listBox1.Visible = true;
            pictureBox12.Visible = true;
            pictureBox6.Visible = true;
            pictureBox11.Visible = true;
            pictureBox10.Visible = true;
            listBox1.Visible = true;
            richTextBox1.Text = "";
            closeSetting();
        }
        void Settings()
        {
            pictureBox12.Visible = true;
            pictureBox6.Visible = false;
            pictureBox11.Visible = false;
            pictureBox10.Visible = true;
            richTextBox1.Visible=false;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            textBox3.Visible = true;
            textBox2.Visible = true;
            textBox1.Visible = true;
            comboBox1.Visible = true;
            pictureBox15.Visible = true;
            listBox1.Visible = false;
            checkBox1.Visible = true;
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            speechSyn.SpeakAsync(" this page , You can open a text file and you can edit . İf you want to read the file just say it ");

            Editor();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Menu();
            speechSyn.SpeakAsync(" this page , You can look our talking history and you can say what you want to do ");
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Settings();
            speechSyn.SpeakAsync(" this page , You can add a new command to me");
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void OpenFile()
        {
             OpenFileDialog fileDialog = new OpenFileDialog();
             fileDialog.Title = "Open Text File";
             fileDialog.Filter = "TXT files|*.txt";
             fileDialog.InitialDirectory = @"C:\";
             if (fileDialog.ShowDialog() == DialogResult.OK)
             {
                 try
                 {

                     using (StreamReader sr = new StreamReader(fileDialog.FileName, Encoding.Default))
                     {

                         String line = sr.ReadToEnd();
                         richTextBox1.Text = line;

                     }
                 }
                 catch (IOException e)
                 {
                     Console.WriteLine("The file could not be read:");
                     MessageBox.Show(e.Message);
                 }
             }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            string com, mess, process, link;
            com = textBox1.Text;
            mess = textBox2.Text;
            link = textBox3.Text;
           
            process = comboBox1.Text;
            string[] adres={"Do.txt","Web.txt","Command.txt","Answer.txt","Message.txt"};
            for (int i = 0; i < adres.Length; i++)
            {
                if (!File.Exists(adres[i]))
                {
                    using (StreamWriter sw = File.CreateText(adres[i]))
                    { }

                }
            
            }
                


            if (process == "True" || process == "False")
            {
                
                if (com != "" && mess != "" && link != "" && process != "")
                {
                    StreamWriter sw = File.AppendText("Do.txt");
                    {
                    sw.WriteLine(link);
                        sw.Close();
                    }
                    StreamWriter wsw = File.AppendText("Web.txt");
                    {
                        if (checkBox1.Checked == true)
                        {
                            wsw.WriteLine("1");
                        }
                        else
                        {
                            wsw.WriteLine("0");
                        }
                        wsw.Close();
                    }
                    StreamWriter ssw = File.AppendText("Command.txt");
                    {
                        ssw.WriteLine(com);
                        ssw.Close();
                    }
                    StreamWriter assw = File.AppendText("Answer.txt");
                    {
                        assw.WriteLine(com);
                        assw.Close();
                    }
                    StreamWriter msw = File.AppendText("Message.txt");
                    {
                        msw.WriteLine(mess);
                        msw.Close();
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("It is not be null");
                }
                string[] sorular = File.ReadAllLines("Command.txt");
                listBox2.Items.Clear();
                foreach (string eleman in sorular)
                {
                    listBox2.Items.Add(eleman);
                }
            }
            else { MessageBox.Show("Please Choice True or False"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           string process = comboBox1.Text;
           if (process == "False")
           {
               textBox3.Text = "0";
               textBox3.Enabled = false;
           }
           else
           { textBox3.Enabled = true; }
        }
        
       

        
       
    }
}
