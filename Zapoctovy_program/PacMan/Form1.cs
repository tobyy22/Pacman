using System;
using System.Drawing;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "PacMan"; 
            nastavButton(zacni_hru);
            nastavButton(button_ukonci);
            mapa = new Mapa("HerniPlan.txt", "HerniGrafika.png");
           // this.ClientSize = new System.Drawing.Size(mapa.sirka * mapa.sx, mapa.vyska * mapa.sx + mapa.posunutiHernihoPole);

        }
        //nastaveni vsech buttonu, predevsim proto, aby se kolem nich nezobrazovalo ohraniceni
        private void nastavButton(Button b)
        {
            b.TabStop = false;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
        }
        Mapa mapa;
        Graphics g;
        //co se ma stat pri zacatku hry
        private void zacniHru(object sender, EventArgs e)
        {
            mapa = new Mapa("HerniPlan.txt", "HerniGrafika.png"); //pri zacatku hry vytvorime novou mapu

            this.ClientSize = new System.Drawing.Size(mapa.sirka * mapa.sx, mapa.vyska * mapa.sx + mapa.posunutiHernihoPole);
            vitezna_zprava.Visible = false; //nechceme ukazovat vysledek
            zacni_hru.Visible = false; //nechceme ukazovat button zacni hru
            GameOverPic.Visible = false; //nechceme ukazovat obrazek GameOver
            button_ukonci.Visible = false; //neukazuji button na ukonceni hry
            PacmanGif.Visible = false; //neukazuji uvodni GIF
            g = CreateGraphics(); //vytvarime objekt Graphics, ktery bude vyuzivat mapa k vykreslovani 
            mapa.pocetZivotu = 3; // zde nastavujeme pocet zivotu hrace

            mapa.VykresliMapu(g); //vykreslime mapu - pouze mapu, nikoliv objekty
            skore_label.Visible = true; //ukazujeme skore
            skore_label.Text = "Zbývá " + mapa.ZbyvaDiamantu; 
            skore_label.BackColor = Color.Black;
            casovac.Enabled = true; //poustime casovac
            mapa.stav = Stav.bezi; //mapu nastavujeme do stavu Bezi
        }
        private void KonecHry()
        {
            this.ClientSize = new System.Drawing.Size(665, 441);

            mapa.SmazMapu(g); //mazeme mapu, neboli vykreslujeme cerno
            casovac.Enabled = false; //dale nastavuji vsechny labely a buttony tak, aby se ukazovala vitezna plocha

            GameOverPic.Visible = true;
            button_ukonci.Visible = true;
            zacni_hru.Visible = true;
            skore_label.Visible = false;
            vitezna_zprava.Visible = true;
        }
        //casovac, tika kazdych 25 ms
        private void casovac_tik(object sender, EventArgs e)
        {
            switch (mapa.stav)
            {
                //ve stavu bezi pohneme kazdych 25 ms vsemi prvky
                //pak je vykreslime
                //pak aktualizujeme skore
                case Stav.bezi:
                    mapa.pohybPrvku(g);
                    mapa.vykresliPrvky(g);
                    skore_label.Text = "Zbývá " + mapa.ZbyvaDiamantu;
                    break;
                case Stav.vyhra:
                    //pokud hra skoncila, vypiseme vysledek hrace a vypneme casovac - viz funkce KonecHry
                    vitezna_zprava.Text = "Vyhrál jsi! Sebral jsi všechny diamanty!";
                    KonecHry();
                    break;
                case Stav.prohra:
                    //to same, akorat vysledek je prohra
                    vitezna_zprava.Text = "Prohrál jsi! K výhře ti zbývalo sebrat " + mapa.ZbyvaDiamantu + " diamantů!";
                    KonecHry();
                    break;
                default:
                    break;
            }
        }
        //funkce, ktera dostava data z klavesnice podle toho, zda nektera sipka byla stisknuta, vidi na mapu a podle toho
        //zkousi menit hrdinuv smer
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                mapa.pacman.zkusSmer = 1;
                return true;
            }
            if (keyData == Keys.Down)
            {
                mapa.pacman.zkusSmer = 3;
                return true;
            }
            if (keyData == Keys.Left)
            {
                mapa.pacman.zkusSmer = 0;
                return true;
            }
            if (keyData == Keys.Right)
            {
                mapa.pacman.zkusSmer = 2;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void ZavriOkno(object sender, EventArgs e)
        {
            //ukonceni hry
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
