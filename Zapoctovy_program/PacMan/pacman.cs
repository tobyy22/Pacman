using System;
using System.Collections.Generic;
using System.Drawing;


namespace PacMan

{
    //spolecna trida - predchudce - vsech pohybujicich se objektu ve hre
    class PohyblivyPrvek
    {
        //seznam promennych, ktere o sobe kazdy objekt vi
        static Random rgen = new Random(); //generator vyuzivavz k vzgenerovani nahodneho smeru - nevyuziva pacman(jednoznace rizen uzivatelem)
        public Mapa mapa;
        public int pocatecniX; //pocatecni souradnice, kde se prvek nachazel na zacatku hry
        public int pocatecniY;
        public int x; //aktualni souradnice prvku
        public int y;
        public int nove_x; //testovaci souradnice - pro tyto souradnice testujeme, zda by na ne eventualne prvek mohl prejit
        public int nove_y;
        public int Smer; //smer, kam se objekt pohybuje, 0 doleva, 1 nahoru, 2 doprava, 3 dolu
        public int zkusSmer; 
        public int zmenaX = 0; //kam se prvek posunul oproti minule pozici
        public int zmenaY = 0;
        public int kolikKroku = 0;
        public char id;
        public int rychlost = 2;
        public PohyblivyPrvek(Mapa mapa, int kdex, int kdey, char id)
        {
            this.kolikKroku = 0;
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;
            this.id = 'h';
        }
        //spolecna cast funkce UdelejKrok, ktere vyuzivaji diamanty a duchove
        //funkce se pro testovaci souradnice podiva, zda na nich je misto na mape(neni tam cihla) a zda by nova souradnice nekolidovala s jinym
        //objektem(krome hrdiny)
        //jelikoz pacman muze kolidovat s objekty (sbirat je, pripadne byt chycen duchy), tak tuto cast funkce nevyuziva a celou si ji prepisuje
        public virtual void UdelejKrok()
        {
            bool zmena = false;
            switch (this.Smer)
            {
                //testovani jednotlivych smeru, pokud dany smer projde, dojde jiste ke zmene
                case 0:
                    nove_x = x - rychlost;
                    if ((mapa.JeVolno(nove_x, nove_y)) && (mapa.JeVolno(nove_x, nove_y + 19)) && (!mapa.otestujKolizisostatnimi(this)))
                    {
                        zmena = true;
                    }
                    break;
                case 1:
                    nove_y = y - rychlost;
                    if ((mapa.JeVolno(nove_x, nove_y)) && (mapa.JeVolno(nove_x + 19, nove_y)) && (!mapa.otestujKolizisostatnimi(this)))
                    {
                        zmena = true;
                    }
                    break;
                case 2:
                    nove_x = x + rychlost;
                    if ((mapa.JeVolno(nove_x + 19, nove_y)) && (mapa.JeVolno(nove_x + 19, nove_y + 19)) && (!mapa.otestujKolizisostatnimi(this)))
                    {
                        zmena = true;
                    }
                    break;
                case 3:
                    nove_y = y + rychlost;
                    if ((mapa.JeVolno(nove_x, nove_y + 19)) && (mapa.JeVolno(nove_x + 19, nove_y + 19)) && (!mapa.otestujKolizisostatnimi(this)))
                    {
                        zmena = true;
                    }
                    break;
                default:
                    break;
            }
            if (zmena)
            {
                //vypocitame zmenu, o kolik se souradnice zmenily a objekt umistime na nove souradnice
                zmenaX = nove_x - this.x;
                zmenaY = nove_y - this.y;
                this.y = nove_y;
                this.x = nove_x;
                this.kolikKroku += 1;
            }
            else
            {
                //jinak vygenerujeme jiny nahodny smer
                this.kolikKroku = 0;
                this.Smer = rgen.Next(4);
            }
        }
    }


    class Pacman : PohyblivyPrvek
    {
        public Pacman(Mapa mapa, int x, int y) : base(mapa, x, y, 'H')
        {
            this.pocatecniX = x;
            this.pocatecniY = y;
            this.x = x;
            this.y = y;
            this.Smer = -1;
            this.zkusSmer = -1;
            this.mapa = mapa;
        }

        //tuto funkci si pacman celou prepisuje z duvodu, ze muze kolidovat s ostatnimi prvky
        public override void UdelejKrok()
        {
            nove_x = x;
            nove_y = y;
            //kdyz uzivatel zmeni smer pacmana, tato informace se dosadi do promenne zkusSmer
            //zde se overi, zda pacman timto smerem muze jit, pokud muze, zmeni se relevantne i Smer, v opacnem pripade Smer zustane stejny, a bude
            //se zkouset opet v dalsim kroku
            if(this.zkusSmer != this.Smer)
            {
                switch(this.zkusSmer)
                {
                    case 0:
                        nove_x -= rychlost;
                        if ((mapa.JeVolno(nove_x, nove_y)) && (mapa.JeVolno(nove_x, nove_y + 19)))
                        {
                            this.Smer = this.zkusSmer;
                        }
                        break;
                    case 1:
                        nove_y -= rychlost;
                        if ((mapa.JeVolno(nove_x, nove_y)) && (mapa.JeVolno(nove_x + 19, nove_y)))
                        {
                            this.Smer = this.zkusSmer;
                        }
                        break;
                    case 2:
                        nove_x += rychlost;
                        if ((mapa.JeVolno(nove_x + 19, nove_y)) && (mapa.JeVolno(nove_x + 19, nove_y + 19)))
                        {
                            this.Smer = this.zkusSmer;
                        }
                        break;
                    case 3:
                        nove_y += rychlost;
                        if ((mapa.JeVolno(nove_x, nove_y + 19)) && (mapa.JeVolno(nove_x + 19, nove_y + 19)))
                        {
                            this.Smer = this.zkusSmer;
                        }
                        break;
                    default:
                        break;

                }
            }
            //zde ma pacman jiz dany smer a bude overovat, zda se tim smerem muze vydat
            //muze se stat, ze stejna podminka se bude tesotvat vicekrat
            bool zmena = false;
            nove_x = x;
            nove_y = y;
            switch (this.Smer)
            {
                case 0:
                    nove_x -= rychlost;
                    if ((mapa.JeVolno(nove_x, nove_y)) && (mapa.JeVolno(nove_x,nove_y + 19)))
                    {
                        zmena = true;
                    }
                    break;
                case 1:
                    nove_y -= rychlost;
                    if ((mapa.JeVolno(nove_x, nove_y)) && (mapa.JeVolno(nove_x + 19, nove_y)))
                    {
                        zmena = true;
                    }
                    break;
                case 2:
                    nove_x += rychlost;
                    if ((mapa.JeVolno(nove_x + 19, nove_y)) && (mapa.JeVolno(nove_x + 19, nove_y + 19)))
                    {
                        zmena = true;
                    }
                    break;
                case 3:
                    nove_y += rychlost;
                    if ((mapa.JeVolno(nove_x, nove_y + 19)) && (mapa.JeVolno(nove_x +19, nove_y + 19)))
                    {
                        zmena = true;
                    }
                    break;
                default:
                    break;
            }
            if(zmena)
            {
                zmenaX = nove_x - this.x;
                zmenaY = nove_y - this.y;
                this.y = nove_y;
                this.x = nove_x;
            }
        }
    }

    class Diamant: PohyblivyPrvek
    {
        static Random rgen = new Random();

        public Diamant(Mapa mapa, int kdex, int kdey, char id):base(mapa, kdex, kdey, id)
        {
            this.pocatecniX = kdex;
            this.pocatecniY = kdey;
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;
            this.Smer = 1;
            this.id = id;
            //prvek se sam prida do listu pohyblivych prvku mapy - bez pacmana
            mapa.DiamantyaDuchove.Add(this);
        }
        public override void UdelejKrok()
        {
            this.zmenaX = 0;
            this.zmenaY = 0;
            nove_x = x;
            nove_y = y;
            //u duchu a diamantu testujeme pri pohybu specialni podminku - zda jejich x-ova nebo y-ova souradnice je rovna pacmanove souradnici
            //pokud je, posleme diamant opacnym smerem, aby to mel pacman narocnejsi
            //nechci ale, aby se prvek choval nezrizene - aby udelal krok jednim smerem a hned zas treba opacnym, pripadne aby se nekde zasekl (to se
            //muze jednoduse stat)
            //pokud ma tedy prvek zmenit smer smerem od pacmana, musi mit za sebou alespon 20 kroku od teto posledni zmeny (nezapocitavam zmenu, kdy
            //na dane pozici nebylo volno)
            if (this.kolikKroku >= 20)
            {
                if (mapa.pacman.x == this.x)
                {
                    if (this.y > mapa.pacman.y && (mapa.JeVolno(x, y + 2)) && (mapa.JeVolno(x + 19, y + 2)))
                    {
                        this.Smer = 3;
                    }
                    else if (this.y < mapa.pacman.y && (mapa.JeVolno(nove_x, y - 2 + 19)) && (mapa.JeVolno(nove_x + 19, y - 2 + 19)))
                    {
                        this.Smer = 1;
                    }
                }

                else if (mapa.pacman.y == this.y)
                {
                    if (this.x > mapa.pacman.x && (mapa.JeVolno(x + 2, nove_y)) && (mapa.JeVolno(x + 2, nove_y + 19)))
                    {
                        this.Smer = 2;
                    }
                    else if (this.x < mapa.pacman.y && (mapa.JeVolno(x - 2 + 19, nove_y)) && (mapa.JeVolno(x - 2 + 19, nove_y + 19)))
                    {
                        this.Smer = 0;
                    }
                }
            }
            //vyuzivame jiz popsane funkce
            base.UdelejKrok();
        }
    }

    class DiaDuch: PohyblivyPrvek
    {
        static Random rgen = new Random();

        public DiaDuch(Mapa mapa, int kdex, int kdey, char id) : base(mapa, kdex,kdey, id)
        {
            this.pocatecniX = kdex;
            this.pocatecniY = kdey;
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;
            this.Smer = 1;
            this.id = id;
            mapa.DiamantyaDuchove.Add(this);
        }
        public override void UdelejKrok()
        {
            this.zmenaX = 0;
            this.zmenaY = 0;
            nove_x = x;
            nove_y = y;
            //ducha posilam smerem k pacmanovi
            if(this.kolikKroku >= 20)
            {
                if (mapa.pacman.x == this.x)
                {
                    if (this.y > mapa.pacman.y && (mapa.JeVolno(nove_x, y - rychlost)) && (mapa.JeVolno(nove_x + 19, y - rychlost)))
                    {
                        this.Smer = 1;
                    }
                    else if (this.y < mapa.pacman.y && (mapa.JeVolno(nove_x, y + rychlost + 19)) && (mapa.JeVolno(nove_x + 19, y + rychlost + 19)))
                    {
                        this.Smer = 3;
                    }
                }

                else if (mapa.pacman.y == this.y)
                {
                    if (this.x > mapa.pacman.x && (mapa.JeVolno(x - rychlost, nove_y)) && (mapa.JeVolno(x - rychlost, nove_y + 19)))
                    {
                        this.Smer = 0;
                    }
                    else if (this.x < mapa.pacman.y && (mapa.JeVolno(x - rychlost + 19, nove_y)) && (mapa.JeVolno(x - rychlost + 19, nove_y + 19)))
                    {
                        this.Smer = 2;
                    }
                }
            }
            base.UdelejKrok();
        }
    }

    public enum Stav { nezacala, bezi, vyhra, prohra };
    class Mapa
    {

        public int posunutiHernihoPole = 40; //pri hre se v horni casti zobrazuje lista, ktera ukazuje zivoty a zbyvajici diamanty, o tuto listu
        //horni mapu posunout dolu
        public int pocetZivotu = 3;
        private char[,] plan; // nactena mapa reprezentovana pomoci pismen, jedna se pouze o samotnou mapu, neobsahuje zadne pohyblive prvky
        public int sirka;
        public int vyska;
        public int ZbyvaDiamantu;

        public Stav stav = Stav.nezacala;

        Bitmap[] ikonky; //ikonky nacitam z jedne velke bitmapy, kazda ikonka ma v mem pripade velikost 20x20px
        public int sx; //velikost jedne ikonky

        public Pacman pacman;

        public List<PohyblivyPrvek> DiamantyaDuchove = new List<PohyblivyPrvek>();

        public Mapa(string cestaMapa, string cestaIkonky)
        {
            NactiIkonky(cestaIkonky);
            NactiMapu(cestaMapa);
            stav = Stav.bezi;
        }

        public bool JeVolno(int x, int y)
        {
            //zajima nas, zda je da dane pozici x,y volno - reprezentovane v pixelech
            return (plan[x / sx, y / sx] == 'h');
        }

        public void NactiMapu(string cesta)
        {
            //ze vstupniho souboru nacitam mapu do dvourozmerneho pole plan
            System.IO.StreamReader sr = new System.IO.StreamReader(cesta);
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());
            plan = new char[sirka, vyska];
            ZbyvaDiamantu = 0;

            for (int y = 0; y < vyska; y++)
            {
                string radek = sr.ReadLine();
                for (int x = 0; x < sirka; x++)
                {
                    char znak = radek[x];
                    plan[x, y] = znak;
                    switch(znak)
                    {
                        //hrdinu i duchy nacitam pomoci vstupniho souboru, nicmene dale si je reprezentuji jako samostatne objekty a o jejich vykresleni
                        //a pripadne kolize se staram separatne, jejich pozici tedy vzdy zmenim na volno
                        case 'H':
                            this.pacman = new Pacman(this, x * sx, y * sx);
                            plan[x, y] = 'h';
                            pacman.rychlost = 2;
                            break;
                        case 'D':
                            DiaDuch duch = new DiaDuch(this, x * sx, y * sx, 'D');
                            duch.rychlost = 2;
                            plan[x, y] = 'h';
                            break;
                        case 'd':
                            Diamant diamant = new Diamant(this, x * sx, y * sx, 'd');
                            plan[x, y] = 'h';
                            ZbyvaDiamantu++;
                            break;
                    }
                }
            }
            sr.Close();
        }
        public void NactiIkonky(string cesta)
        {
            //nacteni jednotlivych ikonek do seznamu
            Bitmap bmp = new Bitmap(cesta);
            this.sx = bmp.Height;
            int pocet = bmp.Width / sx + 2; 
            ikonky = new Bitmap[pocet];
            for (int i = 0; i < pocet-2; i++)
            {
                Rectangle rect = new Rectangle(i * sx, 0, sx, sx);
                ikonky[i] = bmp.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
            }
            //pri vykreslovani, kdyz prvek posouvam, musim spravit tu cast, ze ktere se posunul, aby tam nebyla stale vykreslena cast tohoto prvku
            //na to si vytvarim 2 uzke pruhy odpovidajici barvy, 1. ma rozmery 2x20, druhy 20x2 - odpovida tomu, ze se prvek posunul po ose x, pripadne y
            Rectangle rect1 = new Rectangle(41, 0, 2, 20);
            ikonky[pocet-2] = bmp.Clone(rect1, System.Drawing.Imaging.PixelFormat.DontCare);
            Rectangle rect2 = new Rectangle(41, 0, 20, 2);
            ikonky[pocet-1] = bmp.Clone(rect2, System.Drawing.Imaging.PixelFormat.DontCare);
        }
        private bool prekryvajiSe(PohyblivyPrvek pacman, PohyblivyPrvek duch)
        {
            //testuji, zda se 2 pohyblive prvky prekryvaji - zda pacman lezi alespon jednim rohem v pacmanovi
            //diky tomu, ze se jedna o ctverce, pokryju takto vsechny moznosti 
            //jelikoz se 2 prvky nikdy nemohou prekryvat, tato situace by nedavala smysl, proto u pacmana(nemusi se jednat nutne o objekt pacmana)
            //testuji, zda by jeho testovaci souradnice kolidovala s jinym objektem
            if(duch.y + 19 >= pacman.nove_y && duch.x + 19 >= pacman.nove_x && duch.x <= pacman.nove_x && duch.y <= pacman.nove_y)
            {
                return true;
            }
            if(pacman.nove_y + 19 >= duch.y && pacman.nove_y + 19 <= duch.y + 19 && pacman.nove_x >= duch.x && pacman.nove_x <= duch.x + 19)
            {
                return true;
            }
            if(pacman.nove_y + 19 >= duch.y && pacman.nove_y <= duch.y + 19 && pacman.nove_x + 19 >= duch.x &&pacman.nove_x <= duch.x + 19)
            {
                return true;
            }
            if(pacman.nove_x + 19 >= duch.x && pacman.nove_x + 19 <= duch.x +19 && pacman.y >= duch.y && pacman.y <= duch.y + 19)
            {
                return true;
            }
            return false;
        }
        public bool otestujKolizisostatnimi(PohyblivyPrvek p)
        {
            //pro dany prvek me zajima, zda by pri danem posunuti kolidoval s jinym objektem a podle toho vracim hodnotu
            foreach(PohyblivyPrvek p1 in DiamantyaDuchove)
            {
                if(prekryvajiSe(p,p1) && p != p1)
                {
                    return true;
                }
                else if(prekryvajiSe(p1,p) && p != p1)
                {
                    return true;
                }
            }
            return false; ;
        }
        private void aktualizujHorniListu(Graphics g)
        {
            //lista je siroka vysoka 40px
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < sirka; j++)
                {
                    g.DrawImage(ikonky[2], j * sx, i * sx);
                }
            }
            //nalevo na listu vykreslujeme obrazky pacmana vyjadrujici zbyvajici zivoty
            for (int i = sirka - 1; i > sirka - 1 - pocetZivotu; i--)
            {
                g.DrawImage(ikonky[0], i * sx, 10);
            }

        }
        public void VykresliMapu(Graphics g)
        {
            //pri vykreslovani mapy zaroven vykresluji listu - souvisi primo s hrou
            aktualizujHorniListu(g);
            //jedna se pouze o vykresleni mapy
            //jelikoz se mapa v prubehu hry nemeni, vykresluji ji pouze na zacatku, nasledne pracuji pouze s objekty
            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska; y++)
                {
                    char c = plan[x, y];
                    int indexObrazku = "HXhDd".IndexOf(c);
                    g.DrawImage(ikonky[indexObrazku], x * sx, y * sx + posunutiHernihoPole);
                    
                }
            }
        }
        public void SmazMapu(Graphics g)
        {
            //smazani mapy - vykresleni cerne barvy v celem okne - vyuzivam pri konci hry
            for (int x = 0; x < sirka; x++)
            {
                for (int y = 0; y < vyska + (posunutiHernihoPole/sx); y++)
                {
                    g.DrawImage(ikonky[2], x * sx, y * sx);

                }
            }
        }

        private void SpravVykresleni(Graphics g, PohyblivyPrvek p)
        {
            //zde se jedna o pripad, kdy jsme pohnuli objektem, ktery jsme vykreslili jinde, ale jeho tento objekt se na puvodni pozici nesmazal
            //funkce tedy bere parametr objekt, diva se, jak se tento objekt naposledy posunul a relevantne spravi jeho vykresleni
            //vyuziva uzkych pruhu na konci seznamu ikonky
            //vzhledem k tomu, ze ikonky jsou 2x20 nebo 20x2, je mozne, aby rychlost byla vzdy nasobkem 2 - vysvetleni indexu for cyklu
            if (p.zmenaX > 0)
            {
                for (int i = 2; i <= p.rychlost; i+=2)
                {
                    g.DrawImage(ikonky[8], p.x - i, p.y + posunutiHernihoPole);
                }
            }
            else if (p.zmenaX < 0)
            {
                for (int i = 2; i <= p.rychlost; i += 2)
                {
                    g.DrawImage(ikonky[8], p.x + 18 +i, p.y + posunutiHernihoPole);
                }
            }
            else if (p.zmenaY > 0)
            {
                for (int i = 2; i <= p.rychlost; i += 2)
                {
                    g.DrawImage(ikonky[9], p.x, p.y - i + posunutiHernihoPole);
                }

            }
            else if (p.zmenaY < 0)
            {
                for (int i = 2; i <= p.rychlost; i += 2)
                {
                    g.DrawImage(ikonky[9], p.x, p.y +18 +i + posunutiHernihoPole);
                }
            }
        }
        private void vykresliHrdinu(Graphics g)
        {
            //pouze vykresli hrdinu na jeho pozici, bere v potaz smer - jednotlive smery odpovidaji ruznym obrazkum
            if(pacman.Smer == 1)
            {
                g.DrawImage(ikonky[7], pacman.x, pacman.y + posunutiHernihoPole);
            }
            else if(pacman.Smer == 2)
            {
                g.DrawImage(ikonky[0], pacman.x, pacman.y + posunutiHernihoPole);
            }
            else if(pacman.Smer ==3)
            {
                g.DrawImage(ikonky[6], pacman.x, pacman.y + posunutiHernihoPole);
            }
            else
            {
                g.DrawImage(ikonky[5], pacman.x, pacman.y + posunutiHernihoPole);
            }
            //na konci spravujeme vykresleni
            SpravVykresleni(g, pacman);
        }

        private void vykresliPohyblivePrvky(Graphics g)
        {
            //funkce projde vsechny pohyblive prvky a vykresli je na jejich aktualni pozici
            //pochopitelne rozlisuje, o ktery prvek se jedna pomoci jejich id
            //nasledne volame funkci sprav vykresleni, aby prvky nezanechavaly stopy
            foreach (PohyblivyPrvek p in DiamantyaDuchove)
            {
                switch (p.id)
                {
                    case 'D':
                        g.DrawImage(ikonky[3], p.x, p.y + posunutiHernihoPole);
                        break;
                    case 'd':
                        g.DrawImage(ikonky[4], p.x, p.y + posunutiHernihoPole);
                        break;
                }
                SpravVykresleni(g, p);
            }
        }
        private void VratZbylePrvkynaPocatek()
        {
            //kdyz hrac prijde o zivot, vratime vsechny zbyle prvky vcetne hrdiny na pocatecni pozice - vychozi stav pro dalsi kolo
            foreach (PohyblivyPrvek p in DiamantyaDuchove)
            {
                p.x = p.pocatecniX;
                p.y = p.pocatecniY;
            }
            //vcetne hrdiny
            pacman.x = pacman.pocatecniX;
            pacman.y = pacman.pocatecniY;

        }
        private void smazPrvek(Graphics g,PohyblivyPrvek p)
        {
            //parametrem dostane prvek, na misto ktereho vykresli cernou barvu - smaze ho
            g.DrawImage(ikonky[2], p.x, p.y + posunutiHernihoPole);
            //nasledne upravime vykresleni
            SpravVykresleni(g, p);
        }
        public void pohybPrvku(Graphics g)
        {
            //funkce, ktera pohne vsemi prvky
            //nejprve pacman udela krok
            pacman.UdelejKrok();
            PohyblivyPrvek odstranit = new PohyblivyPrvek(this, 6, 6,'g'); //diamant, ktery budeme pripadne odstranovat pri kolizi s pacmanem
            foreach (PohyblivyPrvek p in DiamantyaDuchove)
            {
                bool prekryvaj = false;
                p.UdelejKrok(); //prvek udela krok
                //nasledne kontrolujeme kolizi s hrdinou
                if(prekryvajiSe(p, pacman))
                {
                    prekryvaj = true;
                }
                else if(prekryvajiSe(pacman,p))
                {
                    prekryvaj = true;
                }
                if(prekryvaj)//pokud kolidovaly tak...
                {
                    switch(p.id) { 
                        //pokud kolidoval s duchem, ubereme zivot a zkontrolujeme, kolik zivotu zbyva
                        case 'D':
                            pocetZivotu--;
                            if(pocetZivotu == 0)
                            {
                                stav = Stav.prohra;
                                return;
                            }
                            //prvky vratime na pocatek a vykreslime mapu (i graficky se mi to tak libi);
                            VratZbylePrvkynaPocatek();
                            VykresliMapu(g);

                            break;
                        case 'd':
                            //pokud kolidoval s diamantem, odecteme jeden diamant, zkontrolujeme vyhru
                            ZbyvaDiamantu--;
                            if(ZbyvaDiamantu == 0)
                            {
                                stav = Stav.vyhra;
                                return;
                            }
                            //dosadime, ktery diamant se ma odstranit
                            odstranit = p;
                            //diamant prestaneme vykreslovat
                            smazPrvek(g, p);
                            break;
                    }
                }
            }
            //diamant odstranime ze seznamu prvku
            DiamantyaDuchove.Remove(odstranit);
        }

        public void vykresliPrvky(Graphics g)
        {
            vykresliHrdinu(g);
            vykresliPohyblivePrvky(g);
        }
    }
}
