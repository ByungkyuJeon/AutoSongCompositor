using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACompositor.src
{
    /// <summary>
    /// test.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class test : Window
    {
        public int Case = 0;
        public int Success = 0;
        public int Fail = 0;
        public int Time = 0;

        Compositor compositor;

        List<int> _times = new List<int>();

        DateTime _time;

        Timer timer;

        bool checker = false;

        int count = 0;

        public test()
        {
            InitializeComponent();

            compositor = new Compositor();

            _time = new DateTime();

            SetList();

            timer = new Timer(50);
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
        }

        int calTime()
        {
            int buffer = 0;

            foreach(int iter in _times)
            {
                buffer += iter;
            }

            return buffer / _times.Count;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            checker = true;
            count++;

            Dispatcher.Invoke(new Action(delegate
            {
                testFull();
            }));
            
        }

        public void Reset()
        {
            Case = 0;
            Success = 0;
            Fail = 0;
            Time = 0;

            success_label.Content = Success.ToString();
            case_label.Content = Case.ToString();
            fail_label.Content = Fail.ToString();
            time_ms.Content = Time.ToString() + "  ms";
        }

        public void SetCase(int _case)
        {
            Case = _case;

            case_label.Content = Case.ToString();
        }

        public void SetFail(int _fail)
        {
            Fail = _fail;

            fail_label.Content = Fail.ToString();
        }

        public void SetTime(int _time)
        {
            Time = _time;

            time_ms.Content = Time.ToString() + "  ms";
        }

        public void SetSuccess(int _succ)
        {
            Success = _succ;

            success_label.Content = Success.ToString();
        }

        void SetAll()
        {
            success_label.Content = Success.ToString();
            time_ms.Content = Time.ToString() + "  ms";
            fail_label.Content = Fail.ToString();
            case_label.Content = Case.ToString();

            mother.UpdateLayout();
        }

        void SetList()
        {
            test_list.Items.Add("ui_4_layer_full");
            test_list.Items.Add("full_3chord");
            test_list.Items.Add("full_3_3chord");



        }

        SongSetting _setting;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch(test_list.SelectedIndex)
            {
                case (0):

                    _setting = new SongSetting();
                    _setting.BaseOctave = 5;
                    _setting.ChordCount = 4;
                    _setting.ChordHeight = 4;
                    _setting.Jengre = Jengre.NewAge;
                    _setting.Scale = Scale.NULL;
                    _setting.ScaleNote = Note.NULL;
                    _setting.Forms.Add(FormType.Bridge);
                    _setting.Variations.Add(Variation.Extend);
                    _setting.Forms.Add(FormType.Hook);
                    _setting.Variations.Add(Variation.Newition);
                    _setting.Forms.Add(FormType.Interlude);
                    _setting.Variations.Add(Variation.Octaviation);
                    _setting.Forms.Add(FormType.Intro);
                    _setting.Variations.Add(Variation.Origin);
                    _setting.Forms.Add(FormType.Outro);
                    _setting.Variations.Add(Variation.Shrink);
                    _setting.Forms.Add(FormType.Verse);
                    _setting.Variations.Add(Variation.Tailing);

                    composition = new Composition(_setting);

                    timer.Start();

                    count = 0;

                    

                    break;

                case (1):

                    _setting = new SongSetting();
                    _setting.BaseOctave = 5;
                    _setting.ChordCount = 4;
                    _setting.ChordHeight = 3;
                    _setting.Jengre = Jengre.NewAge;
                    _setting.Scale = Scale.NULL;
                    _setting.ScaleNote = Note.NULL;
                    _setting.Forms.Add(FormType.Bridge);
                    _setting.Variations.Add(Variation.Extend);
                    _setting.Forms.Add(FormType.Hook);
                    _setting.Variations.Add(Variation.Newition);
                    _setting.Forms.Add(FormType.Interlude);
                    _setting.Variations.Add(Variation.Octaviation);
                    _setting.Forms.Add(FormType.Intro);
                    _setting.Variations.Add(Variation.Origin);
                    _setting.Forms.Add(FormType.Outro);
                    _setting.Variations.Add(Variation.Shrink);
                    _setting.Forms.Add(FormType.Verse);
                    _setting.Variations.Add(Variation.Tailing);

                    composition = new Composition(_setting);

                    timer.Start();

                    count = 0;



                    break;

                case (2):

                    _setting = new SongSetting();
                    _setting.BaseOctave = 5;
                    _setting.ChordCount = 3;
                    _setting.ChordHeight = 3;
                    _setting.Jengre = Jengre.NewAge;
                    _setting.Scale = Scale.NULL;
                    _setting.ScaleNote = Note.NULL;
                    _setting.Forms.Add(FormType.Bridge);
                    _setting.Variations.Add(Variation.Extend);
                    _setting.Forms.Add(FormType.Hook);
                    _setting.Variations.Add(Variation.Newition);
                    _setting.Forms.Add(FormType.Interlude);
                    _setting.Variations.Add(Variation.Octaviation);
                    _setting.Forms.Add(FormType.Intro);
                    _setting.Variations.Add(Variation.Origin);
                    _setting.Forms.Add(FormType.Outro);
                    _setting.Variations.Add(Variation.Shrink);
                    _setting.Forms.Add(FormType.Verse);
                    _setting.Variations.Add(Variation.Tailing);

                    composition = new Composition(_setting);

                    timer.Start();

                    count = 0;



                    break;
            }
        }

        Composition composition;
        CompUI compUI;

        void testFull()
        {

            if (checker && count < 1001)
            {
                try
                {
                    _time = DateTime.Now;
             
                    composition = compositor.GenerateFull(composition);
                    compUI = new CompUI(composition, 1);

                    state_label.Content = composition.CoreForm.Mellody.FullMellody[0].ToString();

                    Case++;
                    Success++;
                    if(_times.Count > 200)
                    {
                        _times.RemoveAt(0);
                        _times.TrimExcess();
                    }
                    _times.Add((DateTime.Now - _time).Milliseconds);
                    Time = calTime();
                }
                catch
                {
                    Case++;
                    Fail++;
                }

                SetAll();

                GC.Collect();

                checker = false;

                timer.Start();


            }



        }
    }
}
